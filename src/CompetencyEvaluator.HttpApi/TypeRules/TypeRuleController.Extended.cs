using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.TypeRules;

namespace CompetencyEvaluator.TypeRules
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("TypeRule")]
    [Route("api/competency-evaluator/type-rules")]
    public class TypeRuleController : TypeRuleControllerBase, ITypeRulesAppService
    {
        public TypeRuleController(ITypeRulesAppService typeRulesAppService) : base(typeRulesAppService)
        {
        }
    }
}