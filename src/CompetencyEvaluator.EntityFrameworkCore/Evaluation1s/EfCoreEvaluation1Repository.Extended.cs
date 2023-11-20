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

namespace CompetencyEvaluator.Evaluation1s
{
    public class EfCoreEvaluation1Repository : EfCoreEvaluation1RepositoryBase, IEvaluation1Repository
    {
        public EfCoreEvaluation1Repository(IDbContextProvider<CompetencyEvaluatorDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}