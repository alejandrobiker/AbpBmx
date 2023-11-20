using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Genders;

namespace CompetencyEvaluator.Genders
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Gender")]
    [Route("api/competency-evaluator/genders")]
    public class GenderController : GenderControllerBase, IGendersAppService
    {
        public GenderController(IGendersAppService gendersAppService) : base(gendersAppService)
        {
        }
    }
}