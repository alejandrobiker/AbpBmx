using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Athletes;

namespace CompetencyEvaluator.Athletes
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Athlete")]
    [Route("api/competency-evaluator/athletes")]
    public class AthleteController : AthleteControllerBase, IAthletesAppService
    {
        public AthleteController(IAthletesAppService athletesAppService) : base(athletesAppService)
        {
        }
    }
}