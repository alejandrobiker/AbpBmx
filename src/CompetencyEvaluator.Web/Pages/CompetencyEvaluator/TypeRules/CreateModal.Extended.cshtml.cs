using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.TypeRules;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.TypeRules
{
    public class CreateModalModel : CreateModalModelBase
    {
        public CreateModalModel(ITypeRulesAppService typeRulesAppService)
            : base(typeRulesAppService)
        {
        }
    }
}