using CompetencyEvaluator.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Athletes;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Athletes
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Athlete")]
    [Route("api/competency-evaluator/athletes")]
    public abstract class AthleteControllerBase : AbpController
    {
        protected IAthletesAppService _athletesAppService;

        public AthleteControllerBase(IAthletesAppService athletesAppService)
        {
            _athletesAppService = athletesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AthleteWithNavigationPropertiesDto>> GetListAsync(GetAthletesInput input)
        {
            return _athletesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<AthleteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _athletesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AthleteDto> GetAsync(Guid id)
        {
            return _athletesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("gender-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetGenderLookupAsync(LookupRequestDto input)
        {
            return _athletesAppService.GetGenderLookupAsync(input);
        }

        [HttpGet]
        [Route("category-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetCategoryLookupAsync(LookupRequestDto input)
        {
            return _athletesAppService.GetCategoryLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<AthleteDto> CreateAsync(AthleteCreateDto input)
        {
            return _athletesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AthleteDto> UpdateAsync(Guid id, AthleteUpdateDto input)
        {
            return _athletesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _athletesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(AthleteExcelDownloadDto input)
        {
            return _athletesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _athletesAppService.GetDownloadTokenAsync();
        }
    }
}