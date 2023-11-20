using CompetencyEvaluator.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Athletes
{
    public partial interface IAthletesAppService : IApplicationService
    {
        Task<PagedResultDto<AthleteWithNavigationPropertiesDto>> GetListAsync(GetAthletesInput input);

        Task<AthleteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<AthleteDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetGenderLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCategoryLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<AthleteDto> CreateAsync(AthleteCreateDto input);

        Task<AthleteDto> UpdateAsync(Guid id, AthleteUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AthleteExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}