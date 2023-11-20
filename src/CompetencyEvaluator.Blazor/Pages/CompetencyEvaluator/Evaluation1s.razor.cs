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
using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Permissions;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Blazor.Pages.CompetencyEvaluator
{
    public partial class Evaluation1s
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<Evaluation1WithNavigationPropertiesDto> Evaluation1List { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateEvaluation1 { get; set; }
        private bool CanEditEvaluation1 { get; set; }
        private bool CanDeleteEvaluation1 { get; set; }
        private Evaluation1CreateDto NewEvaluation1 { get; set; }
        private Validations NewEvaluation1Validations { get; set; } = new();
        private Evaluation1UpdateDto EditingEvaluation1 { get; set; }
        private Validations EditingEvaluation1Validations { get; set; } = new();
        private Guid EditingEvaluation1Id { get; set; }
        private Modal CreateEvaluation1Modal { get; set; } = new();
        private Modal EditEvaluation1Modal { get; set; } = new();
        private GetEvaluation1sInput Filter { get; set; }
        private DataGridEntityActionsColumn<Evaluation1WithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "evaluation1-create-tab";
        protected string SelectedEditTab = "evaluation1-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> AthletesCollection { get; set; } = new List<LookupDto<Guid>>();

        public Evaluation1s()
        {
            NewEvaluation1 = new Evaluation1CreateDto();
            EditingEvaluation1 = new Evaluation1UpdateDto();
            Filter = new GetEvaluation1sInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            Evaluation1List = new List<Evaluation1WithNavigationPropertiesDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetAthleteCollectionLookupAsync();


        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Evaluation1s"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewEvaluation1"], async () =>
            {
                await OpenCreateEvaluation1ModalAsync();
            }, IconName.Add, requiredPolicyName: CompetencyEvaluatorPermissions.Evaluation1s.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateEvaluation1 = await AuthorizationService
                .IsGrantedAsync(CompetencyEvaluatorPermissions.Evaluation1s.Create);
            CanEditEvaluation1 = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.Evaluation1s.Edit);
            CanDeleteEvaluation1 = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.Evaluation1s.Delete);
        }

        private async Task GetEvaluation1sAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await Evaluation1sAppService.GetListAsync(Filter);
            Evaluation1List = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetEvaluation1sAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await Evaluation1sAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("CompetencyEvaluator") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/competency-evaluator/evaluation1s/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<Evaluation1WithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetEvaluation1sAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateEvaluation1ModalAsync()
        {
            NewEvaluation1 = new Evaluation1CreateDto{
                
                AthleteId = AthletesCollection.Select(i=>i.Id).FirstOrDefault(),

            };
            await NewEvaluation1Validations.ClearAll();
            await CreateEvaluation1Modal.Show();
        }

        private async Task CloseCreateEvaluation1ModalAsync()
        {
            NewEvaluation1 = new Evaluation1CreateDto{
                
                AthleteId = AthletesCollection.Select(i=>i.Id).FirstOrDefault(),

            };
            await CreateEvaluation1Modal.Hide();
        }

        private async Task OpenEditEvaluation1ModalAsync(Evaluation1WithNavigationPropertiesDto input)
        {
            var evaluation1 = await Evaluation1sAppService.GetWithNavigationPropertiesAsync(input.Evaluation1.Id);
            
            EditingEvaluation1Id = evaluation1.Evaluation1.Id;
            EditingEvaluation1 = ObjectMapper.Map<Evaluation1Dto, Evaluation1UpdateDto>(evaluation1.Evaluation1);
            await EditingEvaluation1Validations.ClearAll();
            await EditEvaluation1Modal.Show();
        }

        private async Task DeleteEvaluation1Async(Evaluation1WithNavigationPropertiesDto input)
        {
            await Evaluation1sAppService.DeleteAsync(input.Evaluation1.Id);
            await GetEvaluation1sAsync();
        }

        private async Task CreateEvaluation1Async()
        {
            try
            {
                if (await NewEvaluation1Validations.ValidateAll() == false)
                {
                    return;
                }

                await Evaluation1sAppService.CreateAsync(NewEvaluation1);
                await GetEvaluation1sAsync();
                await CloseCreateEvaluation1ModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditEvaluation1ModalAsync()
        {
            await EditEvaluation1Modal.Hide();
        }

        private async Task UpdateEvaluation1Async()
        {
            try
            {
                if (await EditingEvaluation1Validations.ValidateAll() == false)
                {
                    return;
                }

                await Evaluation1sAppService.UpdateAsync(EditingEvaluation1Id, EditingEvaluation1);
                await GetEvaluation1sAsync();
                await EditEvaluation1Modal.Hide();                
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

        protected virtual async Task OnCriterio_1_R1MinChangedAsync(double? criterio_1_R1Min)
        {
            Filter.Criterio_1_R1Min = criterio_1_R1Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_1_R1MaxChangedAsync(double? criterio_1_R1Max)
        {
            Filter.Criterio_1_R1Max = criterio_1_R1Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_1_R2MinChangedAsync(double? criterio_1_R2Min)
        {
            Filter.Criterio_1_R2Min = criterio_1_R2Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_1_R2MaxChangedAsync(double? criterio_1_R2Max)
        {
            Filter.Criterio_1_R2Max = criterio_1_R2Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_2_R1MinChangedAsync(double? criterio_2_R1Min)
        {
            Filter.Criterio_2_R1Min = criterio_2_R1Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_2_R1MaxChangedAsync(double? criterio_2_R1Max)
        {
            Filter.Criterio_2_R1Max = criterio_2_R1Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_2_R2MinChangedAsync(double? criterio_2_R2Min)
        {
            Filter.Criterio_2_R2Min = criterio_2_R2Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_2_R2MaxChangedAsync(double? criterio_2_R2Max)
        {
            Filter.Criterio_2_R2Max = criterio_2_R2Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_3_R1MinChangedAsync(double? criterio_3_R1Min)
        {
            Filter.Criterio_3_R1Min = criterio_3_R1Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_3_R1MaxChangedAsync(double? criterio_3_R1Max)
        {
            Filter.Criterio_3_R1Max = criterio_3_R1Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_3_R2MinChangedAsync(double? criterio_3_R2Min)
        {
            Filter.Criterio_3_R2Min = criterio_3_R2Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_3_R2MaxChangedAsync(double? criterio_3_R2Max)
        {
            Filter.Criterio_3_R2Max = criterio_3_R2Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_4_R1MinChangedAsync(double? criterio_4_R1Min)
        {
            Filter.Criterio_4_R1Min = criterio_4_R1Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_4_R1MaxChangedAsync(double? criterio_4_R1Max)
        {
            Filter.Criterio_4_R1Max = criterio_4_R1Max;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_4_R2MinChangedAsync(double? criterio_4_R2Min)
        {
            Filter.Criterio_4_R2Min = criterio_4_R2Min;
            await SearchAsync();
        }
        protected virtual async Task OnCriterio_4_R2MaxChangedAsync(double? criterio_4_R2Max)
        {
            Filter.Criterio_4_R2Max = criterio_4_R2Max;
            await SearchAsync();
        }
        protected virtual async Task OnResultado_R1MinChangedAsync(double? resultado_R1Min)
        {
            Filter.Resultado_R1Min = resultado_R1Min;
            await SearchAsync();
        }
        protected virtual async Task OnResultado_R1MaxChangedAsync(double? resultado_R1Max)
        {
            Filter.Resultado_R1Max = resultado_R1Max;
            await SearchAsync();
        }
        protected virtual async Task OnResultado_R2MinChangedAsync(double? resultado_R2Min)
        {
            Filter.Resultado_R2Min = resultado_R2Min;
            await SearchAsync();
        }
        protected virtual async Task OnResultado_R2MaxChangedAsync(double? resultado_R2Max)
        {
            Filter.Resultado_R2Max = resultado_R2Max;
            await SearchAsync();
        }
        protected virtual async Task OnAthleteIdChangedAsync(Guid? athleteId)
        {
            Filter.AthleteId = athleteId;
            await SearchAsync();
        }
        

        private async Task GetAthleteCollectionLookupAsync(string? newValue = null)
        {
            AthletesCollection = (await Evaluation1sAppService.GetAthleteLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
