using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Evaluation1s;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s
{
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(IEvaluation1sAppService evaluation1sAppService)
            : base(evaluation1sAppService)
        {
        }
    }
}