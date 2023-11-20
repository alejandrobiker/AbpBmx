using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.TypeRules;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.TypeRules
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("TypeRule")]
    [Route("api/competency-evaluator/type-rules")]
    public abstract class TypeRuleControllerBase : AbpController
    {
        protected ITypeRulesAppService _typeRulesAppService;

        public TypeRuleControllerBase(ITypeRulesAppService typeRulesAppService)
        {
            _typeRulesAppService = typeRulesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TypeRuleDto>> GetListAsync(GetTypeRulesInput input)
        {
            return _typeRulesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TypeRuleDto> GetAsync(Guid id)
        {
            return _typeRulesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TypeRuleDto> CreateAsync(TypeRuleCreateDto input)
        {
            return _typeRulesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TypeRuleDto> UpdateAsync(Guid id, TypeRuleUpdateDto input)
        {
            return _typeRulesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _typeRulesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TypeRuleExcelDownloadDto input)
        {
            return _typeRulesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _typeRulesAppService.GetDownloadTokenAsync();
        }
    }
}