using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.TypeRules;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.TypeRules
{
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(ITypeRulesAppService typeRulesAppService)
            : base(typeRulesAppService)
        {
        }
    }
}