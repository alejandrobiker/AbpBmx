using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Evaluation1s;

namespace CompetencyEvaluator.Evaluation1s
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Evaluation1")]
    [Route("api/competency-evaluator/evaluation1s")]
    public class Evaluation1Controller : Evaluation1ControllerBase, IEvaluation1sAppService
    {
        public Evaluation1Controller(IEvaluation1sAppService evaluation1sAppService) : base(evaluation1sAppService)
        {
        }
    }
}