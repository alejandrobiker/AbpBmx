using CompetencyEvaluator.Athletes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using CompetencyEvaluator.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class MongoEvaluation1RepositoryBase : MongoDbRepository<CompetencyEvaluatorMongoDbContext, Evaluation1, Guid>
    {
        public MongoEvaluation1RepositoryBase(IMongoDbContextProvider<CompetencyEvaluatorMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<Evaluation1WithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var evaluation1 = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var athlete = await (await GetMongoQueryableAsync<Athlete>(cancellationToken)).FirstOrDefaultAsync(e => e.Id == evaluation1.AthleteId, cancellationToken: cancellationToken);

            return new Evaluation1WithNavigationProperties
            {
                Evaluation1 = evaluation1,
                Athlete = athlete,

            };
        }

        public virtual async Task<List<Evaluation1WithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            double? criterio_1_R1Min = null,
            double? criterio_1_R1Max = null,
            double? criterio_1_R2Min = null,
            double? criterio_1_R2Max = null,
            double? criterio_2_R1Min = null,
            double? criterio_2_R1Max = null,
            double? criterio_2_R2Min = null,
            double? criterio_2_R2Max = null,
            double? criterio_3_R1Min = null,
            double? criterio_3_R1Max = null,
            double? criterio_3_R2Min = null,
            double? criterio_3_R2Max = null,
            double? criterio_4_R1Min = null,
            double? criterio_4_R1Max = null,
            double? criterio_4_R2Min = null,
            double? criterio_4_R2Max = null,
            double? resultado_R1Min = null,
            double? resultado_R1Max = null,
            double? resultado_R2Min = null,
            double? resultado_R2Max = null,
            Guid? athleteId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, criterio_1_R1Min, criterio_1_R1Max, criterio_1_R2Min, criterio_1_R2Max, criterio_2_R1Min, criterio_2_R1Max, criterio_2_R2Min, criterio_2_R2Max, criterio_3_R1Min, criterio_3_R1Max, criterio_3_R2Min, criterio_3_R2Max, criterio_4_R1Min, criterio_4_R1Max, criterio_4_R2Min, criterio_4_R2Max, resultado_R1Min, resultado_R1Max, resultado_R2Min, resultado_R2Max, athleteId);
            var evaluation1s = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? Evaluation1Consts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<Evaluation1>>()
                .PageBy<Evaluation1, IMongoQueryable<Evaluation1>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return evaluation1s.Select(s => new Evaluation1WithNavigationProperties
            {
                Evaluation1 = s,
                Athlete = ApplyDataFilters<IMongoQueryable<Athlete>, Athlete>(dbContext.Collection<Athlete>().AsQueryable()).FirstOrDefault(e => e.Id == s.AthleteId),

            }).ToList();
        }

        public virtual async Task<List<Evaluation1>> GetListAsync(
            string? filterText = null,
            double? criterio_1_R1Min = null,
            double? criterio_1_R1Max = null,
            double? criterio_1_R2Min = null,
            double? criterio_1_R2Max = null,
            double? criterio_2_R1Min = null,
            double? criterio_2_R1Max = null,
            double? criterio_2_R2Min = null,
            double? criterio_2_R2Max = null,
            double? criterio_3_R1Min = null,
            double? criterio_3_R1Max = null,
            double? criterio_3_R2Min = null,
            double? criterio_3_R2Max = null,
            double? criterio_4_R1Min = null,
            double? criterio_4_R1Max = null,
            double? criterio_4_R2Min = null,
            double? criterio_4_R2Max = null,
            double? resultado_R1Min = null,
            double? resultado_R1Max = null,
            double? resultado_R2Min = null,
            double? resultado_R2Max = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, criterio_1_R1Min, criterio_1_R1Max, criterio_1_R2Min, criterio_1_R2Max, criterio_2_R1Min, criterio_2_R1Max, criterio_2_R2Min, criterio_2_R2Max, criterio_3_R1Min, criterio_3_R1Max, criterio_3_R2Min, criterio_3_R2Max, criterio_4_R1Min, criterio_4_R1Max, criterio_4_R2Min, criterio_4_R2Max, resultado_R1Min, resultado_R1Max, resultado_R2Min, resultado_R2Max);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? Evaluation1Consts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Evaluation1>>()
                .PageBy<Evaluation1, IMongoQueryable<Evaluation1>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            double? criterio_1_R1Min = null,
            double? criterio_1_R1Max = null,
            double? criterio_1_R2Min = null,
            double? criterio_1_R2Max = null,
            double? criterio_2_R1Min = null,
            double? criterio_2_R1Max = null,
            double? criterio_2_R2Min = null,
            double? criterio_2_R2Max = null,
            double? criterio_3_R1Min = null,
            double? criterio_3_R1Max = null,
            double? criterio_3_R2Min = null,
            double? criterio_3_R2Max = null,
            double? criterio_4_R1Min = null,
            double? criterio_4_R1Max = null,
            double? criterio_4_R2Min = null,
            double? criterio_4_R2Max = null,
            double? resultado_R1Min = null,
            double? resultado_R1Max = null,
            double? resultado_R2Min = null,
            double? resultado_R2Max = null,
            Guid? athleteId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, criterio_1_R1Min, criterio_1_R1Max, criterio_1_R2Min, criterio_1_R2Max, criterio_2_R1Min, criterio_2_R1Max, criterio_2_R2Min, criterio_2_R2Max, criterio_3_R1Min, criterio_3_R1Max, criterio_3_R2Min, criterio_3_R2Max, criterio_4_R1Min, criterio_4_R1Max, criterio_4_R2Min, criterio_4_R2Max, resultado_R1Min, resultado_R1Max, resultado_R2Min, resultado_R2Max, athleteId);
            return await query.As<IMongoQueryable<Evaluation1>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Evaluation1> ApplyFilter(
            IQueryable<Evaluation1> query,
            string? filterText = null,
            double? criterio_1_R1Min = null,
            double? criterio_1_R1Max = null,
            double? criterio_1_R2Min = null,
            double? criterio_1_R2Max = null,
            double? criterio_2_R1Min = null,
            double? criterio_2_R1Max = null,
            double? criterio_2_R2Min = null,
            double? criterio_2_R2Max = null,
            double? criterio_3_R1Min = null,
            double? criterio_3_R1Max = null,
            double? criterio_3_R2Min = null,
            double? criterio_3_R2Max = null,
            double? criterio_4_R1Min = null,
            double? criterio_4_R1Max = null,
            double? criterio_4_R2Min = null,
            double? criterio_4_R2Max = null,
            double? resultado_R1Min = null,
            double? resultado_R1Max = null,
            double? resultado_R2Min = null,
            double? resultado_R2Max = null,
            Guid? athleteId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(criterio_1_R1Min.HasValue, e => e.Criterio_1_R1 >= criterio_1_R1Min!.Value)
                    .WhereIf(criterio_1_R1Max.HasValue, e => e.Criterio_1_R1 <= criterio_1_R1Max!.Value)
                    .WhereIf(criterio_1_R2Min.HasValue, e => e.Criterio_1_R2 >= criterio_1_R2Min!.Value)
                    .WhereIf(criterio_1_R2Max.HasValue, e => e.Criterio_1_R2 <= criterio_1_R2Max!.Value)
                    .WhereIf(criterio_2_R1Min.HasValue, e => e.Criterio_2_R1 >= criterio_2_R1Min!.Value)
                    .WhereIf(criterio_2_R1Max.HasValue, e => e.Criterio_2_R1 <= criterio_2_R1Max!.Value)
                    .WhereIf(criterio_2_R2Min.HasValue, e => e.Criterio_2_R2 >= criterio_2_R2Min!.Value)
                    .WhereIf(criterio_2_R2Max.HasValue, e => e.Criterio_2_R2 <= criterio_2_R2Max!.Value)
                    .WhereIf(criterio_3_R1Min.HasValue, e => e.Criterio_3_R1 >= criterio_3_R1Min!.Value)
                    .WhereIf(criterio_3_R1Max.HasValue, e => e.Criterio_3_R1 <= criterio_3_R1Max!.Value)
                    .WhereIf(criterio_3_R2Min.HasValue, e => e.Criterio_3_R2 >= criterio_3_R2Min!.Value)
                    .WhereIf(criterio_3_R2Max.HasValue, e => e.Criterio_3_R2 <= criterio_3_R2Max!.Value)
                    .WhereIf(criterio_4_R1Min.HasValue, e => e.Criterio_4_R1 >= criterio_4_R1Min!.Value)
                    .WhereIf(criterio_4_R1Max.HasValue, e => e.Criterio_4_R1 <= criterio_4_R1Max!.Value)
                    .WhereIf(criterio_4_R2Min.HasValue, e => e.Criterio_4_R2 >= criterio_4_R2Min!.Value)
                    .WhereIf(criterio_4_R2Max.HasValue, e => e.Criterio_4_R2 <= criterio_4_R2Max!.Value)
                    .WhereIf(resultado_R1Min.HasValue, e => e.Resultado_R1 >= resultado_R1Min!.Value)
                    .WhereIf(resultado_R1Max.HasValue, e => e.Resultado_R1 <= resultado_R1Max!.Value)
                    .WhereIf(resultado_R2Min.HasValue, e => e.Resultado_R2 >= resultado_R2Min!.Value)
                    .WhereIf(resultado_R2Max.HasValue, e => e.Resultado_R2 <= resultado_R2Max!.Value)
                    .WhereIf(athleteId != null && athleteId != Guid.Empty, e => e.AthleteId == athleteId);
        }
    }
}