using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Genders
{
    public partial interface IGendersAppService : IApplicationService
    {
        Task<PagedResultDto<GenderDto>> GetListAsync(GetGendersInput input);

        Task<GenderDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<GenderDto> CreateAsync(GenderCreateDto input);

        Task<GenderDto> UpdateAsync(Guid id, GenderUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(GenderExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}