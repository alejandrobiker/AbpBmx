using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.Genders
{
    public class Gender : GenderBase
    {
        //<suite-custom-code-autogenerated>
        protected Gender()
        {

        }

        public Gender(Guid id, string name, string? shortName = null)
            : base(id, name, shortName)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}