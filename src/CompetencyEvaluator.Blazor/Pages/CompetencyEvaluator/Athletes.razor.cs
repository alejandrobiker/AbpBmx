using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Permissions;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Blazor.Pages.CompetencyEvaluator
{
    public partial class Athletes
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<AthleteWithNavigationPropertiesDto> AthleteList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateAthlete { get; set; }
        private bool CanEditAthlete { get; set; }
        private bool CanDeleteAthlete { get; set; }
        private AthleteCreateDto NewAthlete { get; set; }
        private Validations NewAthleteValidations { get; set; } = new();
        private AthleteUpdateDto EditingAthlete { get; set; }
        private Validations EditingAthleteValidations { get; set; } = new();
        private Guid EditingAthleteId { get; set; }
        private Modal CreateAthleteModal { get; set; } = new();
        private Modal EditAthleteModal { get; set; } = new();
        private GetAthletesInput Filter { get; set; }
        private DataGridEntityActionsColumn<AthleteWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "athlete-create-tab";
        protected string SelectedEditTab = "athlete-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> GendersCollection { get; set; } = new List<LookupDto<Guid>>();
private IReadOnlyList<LookupDto<Guid>> CategoriesCollection { get; set; } = new List<LookupDto<Guid>>();

        public Athletes()
        {
            NewAthlete = new AthleteCreateDto();
            EditingAthlete = new AthleteUpdateDto();
            Filter = new GetAthletesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            AthleteList = new List<AthleteWithNavigationPropertiesDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetGenderCollectionLookupAsync();


            await GetCategoryCollectionLookupAsync();


        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Athletes"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewAthlete"], async () =>
            {
                await OpenCreateAthleteModalAsync();
            }, IconName.Add, requiredPolicyName: CompetencyEvaluatorPermissions.Athletes.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateAthlete = await AuthorizationService
                .IsGrantedAsync(CompetencyEvaluatorPermissions.Athletes.Create);
            CanEditAthlete = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.Athletes.Edit);
            CanDeleteAthlete = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.Athletes.Delete);
        }

        private async Task GetAthletesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await AthletesAppService.GetListAsync(Filter);
            AthleteList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetAthletesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await AthletesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("CompetencyEvaluator") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/competency-evaluator/athletes/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AthleteWithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetAthletesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateAthleteModalAsync()
        {
            NewAthlete = new AthleteCreateDto{
                DateOfBirth = DateTime.Now,

                GenderId = GendersCollection.Select(i=>i.Id).FirstOrDefault(),
CategoryId = CategoriesCollection.Select(i=>i.Id).FirstOrDefault(),

            };
            await NewAthleteValidations.ClearAll();
            await CreateAthleteModal.Show();
        }

        private async Task CloseCreateAthleteModalAsync()
        {
            NewAthlete = new AthleteCreateDto{
                DateOfBirth = DateTime.Now,

                GenderId = GendersCollection.Select(i=>i.Id).FirstOrDefault(),
CategoryId = CategoriesCollection.Select(i=>i.Id).FirstOrDefault(),

            };
            await CreateAthleteModal.Hide();
        }

        private async Task OpenEditAthleteModalAsync(AthleteWithNavigationPropertiesDto input)
        {
            var athlete = await AthletesAppService.GetWithNavigationPropertiesAsync(input.Athlete.Id);
            
            EditingAthleteId = athlete.Athlete.Id;
            EditingAthlete = ObjectMapper.Map<AthleteDto, AthleteUpdateDto>(athlete.Athlete);
            await EditingAthleteValidations.ClearAll();
            await EditAthleteModal.Show();
        }

        private async Task DeleteAthleteAsync(AthleteWithNavigationPropertiesDto input)
        {
            await AthletesAppService.DeleteAsync(input.Athlete.Id);
            await GetAthletesAsync();
        }

        private async Task CreateAthleteAsync()
        {
            try
            {
                if (await NewAthleteValidations.ValidateAll() == false)
                {
                    return;
                }

                await AthletesAppService.CreateAsync(NewAthlete);
                await GetAthletesAsync();
                await CloseCreateAthleteModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditAthleteModalAsync()
        {
            await EditAthleteModal.Hide();
        }

        private async Task UpdateAthleteAsync()
        {
            try
            {
                if (await EditingAthleteValidations.ValidateAll() == false)
                {
                    return;
                }

                await AthletesAppService.UpdateAsync(EditingAthleteId, EditingAthlete);
                await GetAthletesAsync();
                await EditAthleteModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }

        protected virtual async Task OnNameChangedAsync(string? name)
        {
            Filter.Name = name;
            await SearchAsync();
        }
        protected virtual async Task OnDateOfBirthMinChangedAsync(DateTime? dateOfBirthMin)
        {
            Filter.DateOfBirthMin = dateOfBirthMin.HasValue ? dateOfBirthMin.Value.Date : dateOfBirthMin;
            await SearchAsync();
        }
        protected virtual async Task OnDateOfBirthMaxChangedAsync(DateTime? dateOfBirthMax)
        {
            Filter.DateOfBirthMax = dateOfBirthMax.HasValue ? dateOfBirthMax.Value.Date.AddDays(1).AddSeconds(-1) : dateOfBirthMax;
            await SearchAsync();
        }
        protected virtual async Task OnGenderIdChangedAsync(Guid? genderId)
        {
            Filter.GenderId = genderId;
            await SearchAsync();
        }
        protected virtual async Task OnCategoryIdChangedAsync(Guid? categoryId)
        {
            Filter.CategoryId = categoryId;
            await SearchAsync();
        }
        

        private async Task GetGenderCollectionLookupAsync(string? newValue = null)
        {
            GendersCollection = (await AthletesAppService.GetGenderLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        private async Task GetCategoryCollectionLookupAsync(string? newValue = null)
        {
            CategoriesCollection = (await AthletesAppService.GetCategoryLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
