using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Categories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.Athletes
{
    public class Athlete : AthleteBase
    {
        //<suite-custom-code-autogenerated>
        protected Athlete()
        {

        }

        public Athlete(Guid id, Guid genderId, Guid categoryId, string name, DateTime dateOfBirth)
            : base(id, genderId, categoryId, name, dateOfBirth)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}