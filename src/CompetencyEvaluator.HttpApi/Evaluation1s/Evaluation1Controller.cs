using CompetencyEvaluator.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Evaluation1s;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Evaluation1s
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Evaluation1")]
    [Route("api/competency-evaluator/evaluation1s")]
    public abstract class Evaluation1ControllerBase : AbpController
    {
        protected IEvaluation1sAppService _evaluation1sAppService;

        public Evaluation1ControllerBase(IEvaluation1sAppService evaluation1sAppService)
        {
            _evaluation1sAppService = evaluation1sAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<Evaluation1WithNavigationPropertiesDto>> GetListAsync(GetEvaluation1sInput input)
        {
            return _evaluation1sAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<Evaluation1WithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _evaluation1sAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<Evaluation1Dto> GetAsync(Guid id)
        {
            return _evaluation1sAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("athlete-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetAthleteLookupAsync(LookupRequestDto input)
        {
            return _evaluation1sAppService.GetAthleteLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<Evaluation1Dto> CreateAsync(Evaluation1CreateDto input)
        {
            return _evaluation1sAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<Evaluation1Dto> UpdateAsync(Guid id, Evaluation1UpdateDto input)
        {
            return _evaluation1sAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _evaluation1sAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(Evaluation1ExcelDownloadDto input)
        {
            return _evaluation1sAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _evaluation1sAppService.GetDownloadTokenAsync();
        }
    }
}