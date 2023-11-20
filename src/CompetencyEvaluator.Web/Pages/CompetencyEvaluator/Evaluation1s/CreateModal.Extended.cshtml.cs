using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.Evaluation1s;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s
{
    public class CreateModalModel : CreateModalModelBase
    {
        public CreateModalModel(IEvaluation1sAppService evaluation1sAppService)
            : base(evaluation1sAppService)
        {
        }
    }
}