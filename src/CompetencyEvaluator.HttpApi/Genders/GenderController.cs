using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Genders;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Genders
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Gender")]
    [Route("api/competency-evaluator/genders")]
    public abstract class GenderControllerBase : AbpController
    {
        protected IGendersAppService _gendersAppService;

        public GenderControllerBase(IGendersAppService gendersAppService)
        {
            _gendersAppService = gendersAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<GenderDto>> GetListAsync(GetGendersInput input)
        {
            return _gendersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<GenderDto> GetAsync(Guid id)
        {
            return _gendersAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<GenderDto> CreateAsync(GenderCreateDto input)
        {
            return _gendersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<GenderDto> UpdateAsync(Guid id, GenderUpdateDto input)
        {
            return _gendersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _gendersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(GenderExcelDownloadDto input)
        {
            return _gendersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _gendersAppService.GetDownloadTokenAsync();
        }
    }
}