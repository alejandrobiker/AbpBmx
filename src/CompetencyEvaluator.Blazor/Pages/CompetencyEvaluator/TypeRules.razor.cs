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
using CompetencyEvaluator.TypeRules;
using CompetencyEvaluator.Permissions;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Blazor.Pages.CompetencyEvaluator
{
    public partial class TypeRules
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<TypeRuleDto> TypeRuleList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateTypeRule { get; set; }
        private bool CanEditTypeRule { get; set; }
        private bool CanDeleteTypeRule { get; set; }
        private TypeRuleCreateDto NewTypeRule { get; set; }
        private Validations NewTypeRuleValidations { get; set; } = new();
        private TypeRuleUpdateDto EditingTypeRule { get; set; }
        private Validations EditingTypeRuleValidations { get; set; } = new();
        private Guid EditingTypeRuleId { get; set; }
        private Modal CreateTypeRuleModal { get; set; } = new();
        private Modal EditTypeRuleModal { get; set; } = new();
        private GetTypeRulesInput Filter { get; set; }
        private DataGridEntityActionsColumn<TypeRuleDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "typeRule-create-tab";
        protected string SelectedEditTab = "typeRule-edit-tab";
        
        public TypeRules()
        {
            NewTypeRule = new TypeRuleCreateDto();
            EditingTypeRule = new TypeRuleUpdateDto();
            Filter = new GetTypeRulesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            TypeRuleList = new List<TypeRuleDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:TypeRules"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewTypeRule"], async () =>
            {
                await OpenCreateTypeRuleModalAsync();
            }, IconName.Add, requiredPolicyName: CompetencyEvaluatorPermissions.TypeRules.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateTypeRule = await AuthorizationService
                .IsGrantedAsync(CompetencyEvaluatorPermissions.TypeRules.Create);
            CanEditTypeRule = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.TypeRules.Edit);
            CanDeleteTypeRule = await AuthorizationService
                            .IsGrantedAsync(CompetencyEvaluatorPermissions.TypeRules.Delete);
        }

        private async Task GetTypeRulesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await TypeRulesAppService.GetListAsync(Filter);
            TypeRuleList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetTypeRulesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await TypeRulesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("CompetencyEvaluator") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/competency-evaluator/type-rules/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TypeRuleDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetTypeRulesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateTypeRuleModalAsync()
        {
            NewTypeRule = new TypeRuleCreateDto{
                
                
            };
            await NewTypeRuleValidations.ClearAll();
            await CreateTypeRuleModal.Show();
        }

        private async Task CloseCreateTypeRuleModalAsync()
        {
            NewTypeRule = new TypeRuleCreateDto{
                
                
            };
            await CreateTypeRuleModal.Hide();
        }

        private async Task OpenEditTypeRuleModalAsync(TypeRuleDto input)
        {
            var typeRule = await TypeRulesAppService.GetAsync(input.Id);
            
            EditingTypeRuleId = typeRule.Id;
            EditingTypeRule = ObjectMapper.Map<TypeRuleDto, TypeRuleUpdateDto>(typeRule);
            await EditingTypeRuleValidations.ClearAll();
            await EditTypeRuleModal.Show();
        }

        private async Task DeleteTypeRuleAsync(TypeRuleDto input)
        {
            await TypeRulesAppService.DeleteAsync(input.Id);
            await GetTypeRulesAsync();
        }

        private async Task CreateTypeRuleAsync()
        {
            try
            {
                if (await NewTypeRuleValidations.ValidateAll() == false)
                {
                    return;
                }

                await TypeRulesAppService.CreateAsync(NewTypeRule);
                await GetTypeRulesAsync();
                await CloseCreateTypeRuleModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditTypeRuleModalAsync()
        {
            await EditTypeRuleModal.Hide();
        }

        private async Task UpdateTypeRuleAsync()
        {
            try
            {
                if (await EditingTypeRuleValidations.ValidateAll() == false)
                {
                    return;
                }

                await TypeRulesAppService.UpdateAsync(EditingTypeRuleId, EditingTypeRule);
                await GetTypeRulesAsync();
                await EditTypeRuleModal.Hide();                
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
        

    }
}
