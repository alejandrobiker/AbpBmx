using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using CompetencyEvaluator.EntityFrameworkCore;

namespace CompetencyEvaluator.TypeRules
{
    public class EfCoreTypeRuleRepository : EfCoreTypeRuleRepositoryBase, ITypeRuleRepository
    {
        public EfCoreTypeRuleRepository(IDbContextProvider<CompetencyEvaluatorDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}