using CompetencyEvaluator.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Evaluation1s
{
    public partial interface IEvaluation1sAppService : IApplicationService
    {
        Task<PagedResultDto<Evaluation1WithNavigationPropertiesDto>> GetListAsync(GetEvaluation1sInput input);

        Task<Evaluation1WithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<Evaluation1Dto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetAthleteLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<Evaluation1Dto> CreateAsync(Evaluation1CreateDto input);

        Task<Evaluation1Dto> UpdateAsync(Guid id, Evaluation1UpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(Evaluation1ExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}