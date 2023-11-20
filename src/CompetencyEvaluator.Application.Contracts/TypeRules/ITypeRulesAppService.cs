using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.TypeRules
{
    public partial interface ITypeRulesAppService : IApplicationService
    {
        Task<PagedResultDto<TypeRuleDto>> GetListAsync(GetTypeRulesInput input);

        Task<TypeRuleDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TypeRuleDto> CreateAsync(TypeRuleCreateDto input);

        Task<TypeRuleDto> UpdateAsync(Guid id, TypeRuleUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TypeRuleExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}