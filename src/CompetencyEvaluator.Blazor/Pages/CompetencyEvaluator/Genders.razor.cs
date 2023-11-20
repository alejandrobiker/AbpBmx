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
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Permissions;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Blazor.Pages.CompetencyEvaluator
{
    public partial class Genders
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<GenderDto> GenderList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateGender { get; set; }
        private bool CanEditGender { get; set; }
        private bool CanDeleteGender { get; set; }
        private GenderCreateDto NewGender { get; set; }
        private Validations NewGenderValidations { get; set; } = new();
        private GenderUpdateDto EditingGender { get; set; }
        private Validations EditingGenderValidations { get; set; } = new();
        private Guid EditingGenderId { get; set; }
        private Modal CreateGenderModal { get; set; } = new();
        private Modal EditGenderModal { get; set; } = new();
        private GetGendersInput Filter { get; set; }
        private DataGridEntityActionsColumn<GenderDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "gender-create-tab";
        protected string SelectedEditTab = "gender-edit-tab";
        
        public Genders()
        {
            NewGender = new GenderCreateDto();
            EditingGender = new GenderUpdateDto();
            Filter = new GetGendersInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            GenderList = new List<GenderDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Genders"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewGender"], async () =>
            {
                await OpenCreateGenderModalAsync();
            }, IconName.Add, requiredPolicyName: CompetencyEvaluatorPermissions.Genders.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateGender = await AuthorizationService
                .IsGrantedAsync(CompetencyEvaluatorPermissions.Genders.Create);
            CanEditGender = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.Genders.Edit);
            CanDeleteGender = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.Genders.Delete);
        }

        private async Task GetGendersAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await GendersAppService.GetListAsync(Filter);
            GenderList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetGendersAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await GendersAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("CompetencyEvaluator") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/competency-evaluator/genders/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<GenderDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetGendersAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateGenderModalAsync()
        {
            NewGender = new GenderCreateDto{
                
                
            };
            await NewGenderValidations.ClearAll();
            await CreateGenderModal.Show();
        }

        private async Task CloseCreateGenderModalAsync()
        {
            NewGender = new GenderCreateDto{
                
                
            };
            await CreateGenderModal.Hide();
        }

        private async Task OpenEditGenderModalAsync(GenderDto input)
        {
            var gender = await GendersAppService.GetAsync(input.Id);
            
            EditingGenderId = gender.Id;
            EditingGender = ObjectMapper.Map<GenderDto, GenderUpdateDto>(gender);
            await EditingGenderValidations.ClearAll();
            await EditGenderModal.Show();
        }

        private async Task DeleteGenderAsync(GenderDto input)
        {
            await GendersAppService.DeleteAsync(input.Id);
            await GetGendersAsync();
        }

        private async Task CreateGenderAsync()
        {
            try
            {
                if (await NewGenderValidations.ValidateAll() == false)
                {
                    return;
                }

                await GendersAppService.CreateAsync(NewGender);
                await GetGendersAsync();
                await CloseCreateGenderModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditGenderModalAsync()
        {
            await EditGenderModal.Hide();
        }

        private async Task UpdateGenderAsync()
        {
            try
            {
                if (await EditingGenderValidations.ValidateAll() == false)
                {
                    return;
                }

                await GendersAppService.UpdateAsync(EditingGenderId, EditingGender);
                await GetGendersAsync();
                await EditGenderModal.Hide();                
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

        protected virtual async Task OnnameChangedAsync(string? name)
        {
            Filter.name = name;
            await SearchAsync();
        }
        protected virtual async Task OnShortNameChangedAsync(string? shortName)
        {
            Filter.ShortName = shortName;
            await SearchAsync();
        }
        

    }
}
