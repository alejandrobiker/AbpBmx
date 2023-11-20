using CompetencyEvaluator.Shared;
using CompetencyEvaluator.Athletes;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using CompetencyEvaluator.Permissions;
using CompetencyEvaluator.Evaluation1s;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Evaluation1s
{

    [Authorize(CompetencyEvaluatorPermissions.Evaluation1s.Default)]
    public abstract class Evaluation1sAppServiceBase : ApplicationService
    {
        protected IDistributedCache<Evaluation1ExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IEvaluation1Repository _evaluation1Repository;
        protected Evaluation1Manager _evaluation1Manager;
        protected IRepository<Athlete, Guid> _athleteRepository;

        public Evaluation1sAppServiceBase(IEvaluation1Repository evaluation1Repository, Evaluation1Manager evaluation1Manager, IDistributedCache<Evaluation1ExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Athlete, Guid> athleteRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _evaluation1Repository = evaluation1Repository;
            _evaluation1Manager = evaluation1Manager; _athleteRepository = athleteRepository;
        }

        public virtual async Task<PagedResultDto<Evaluation1WithNavigationPropertiesDto>> GetListAsync(GetEvaluation1sInput input)
        {
            var totalCount = await _evaluation1Repository.GetCountAsync(input.FilterText, input.Criterio_1_R1Min, input.Criterio_1_R1Max, input.Criterio_1_R2Min, input.Criterio_1_R2Max, input.Criterio_2_R1Min, input.Criterio_2_R1Max, input.Criterio_2_R2Min, input.Criterio_2_R2Max, input.Criterio_3_R1Min, input.Criterio_3_R1Max, input.Criterio_3_R2Min, input.Criterio_3_R2Max, input.Criterio_4_R1Min, input.Criterio_4_R1Max, input.Criterio_4_R2Min, input.Criterio_4_R2Max, input.Resultado_R1Min, input.Resultado_R1Max, input.Resultado_R2Min, input.Resultado_R2Max, input.AthleteId);
            var items = await _evaluation1Repository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Criterio_1_R1Min, input.Criterio_1_R1Max, input.Criterio_1_R2Min, input.Criterio_1_R2Max, input.Criterio_2_R1Min, input.Criterio_2_R1Max, input.Criterio_2_R2Min, input.Criterio_2_R2Max, input.Criterio_3_R1Min, input.Criterio_3_R1Max, input.Criterio_3_R2Min, input.Criterio_3_R2Max, input.Criterio_4_R1Min, input.Criterio_4_R1Max, input.Criterio_4_R2Min, input.Criterio_4_R2Max, input.Resultado_R1Min, input.Resultado_R1Max, input.Resultado_R2Min, input.Resultado_R2Max, input.AthleteId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<Evaluation1WithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Evaluation1WithNavigationProperties>, List<Evaluation1WithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<Evaluation1WithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<Evaluation1WithNavigationProperties, Evaluation1WithNavigationPropertiesDto>
                (await _evaluation1Repository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<Evaluation1Dto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Evaluation1, Evaluation1Dto>(await _evaluation1Repository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetAthleteLookupAsync(LookupRequestDto input)
        {
            var query = (await _athleteRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Athlete>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Athlete>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(CompetencyEvaluatorPermissions.Evaluation1s.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _evaluation1Repository.DeleteAsync(id);
        }

        [Authorize(CompetencyEvaluatorPermissions.Evaluation1s.Create)]
        public virtual async Task<Evaluation1Dto> CreateAsync(Evaluation1CreateDto input)
        {
            if (input.AthleteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Athlete"]]);
            }

            var evaluation1 = await _evaluation1Manager.CreateAsync(
            input.AthleteId, input.Criterio_1_R1, input.Criterio_1_R2, input.Criterio_2_R1, input.Criterio_2_R2, input.Criterio_3_R1, input.Criterio_3_R2, input.Criterio_4_R1, input.Criterio_4_R2, input.Resultado_R1, input.Resultado_R2
            );

            return ObjectMapper.Map<Evaluation1, Evaluation1Dto>(evaluation1);
        }

        [Authorize(CompetencyEvaluatorPermissions.Evaluation1s.Edit)]
        public virtual async Task<Evaluation1Dto> UpdateAsync(Guid id, Evaluation1UpdateDto input)
        {
            if (input.AthleteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Athlete"]]);
            }

            var evaluation1 = await _evaluation1Manager.UpdateAsync(
            id,
            input.AthleteId, input.Criterio_1_R1, input.Criterio_1_R2, input.Criterio_2_R1, input.Criterio_2_R2, input.Criterio_3_R1, input.Criterio_3_R2, input.Criterio_4_R1, input.Criterio_4_R2, input.Resultado_R1, input.Resultado_R2, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Evaluation1, Evaluation1Dto>(evaluation1);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(Evaluation1ExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var evaluation1s = await _evaluation1Repository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Criterio_1_R1Min, input.Criterio_1_R1Max, input.Criterio_1_R2Min, input.Criterio_1_R2Max, input.Criterio_2_R1Min, input.Criterio_2_R1Max, input.Criterio_2_R2Min, input.Criterio_2_R2Max, input.Criterio_3_R1Min, input.Criterio_3_R1Max, input.Criterio_3_R2Min, input.Criterio_3_R2Max, input.Criterio_4_R1Min, input.Criterio_4_R1Max, input.Criterio_4_R2Min, input.Criterio_4_R2Max, input.Resultado_R1Min, input.Resultado_R1Max, input.Resultado_R2Min, input.Resultado_R2Max);
            var items = evaluation1s.Select(item => new
            {
                Criterio_1_R1 = item.Evaluation1.Criterio_1_R1,
                Criterio_1_R2 = item.Evaluation1.Criterio_1_R2,
                Criterio_2_R1 = item.Evaluation1.Criterio_2_R1,
                Criterio_2_R2 = item.Evaluation1.Criterio_2_R2,
                Criterio_3_R1 = item.Evaluation1.Criterio_3_R1,
                Criterio_3_R2 = item.Evaluation1.Criterio_3_R2,
                Criterio_4_R1 = item.Evaluation1.Criterio_4_R1,
                Criterio_4_R2 = item.Evaluation1.Criterio_4_R2,
                Resultado_R1 = item.Evaluation1.Resultado_R1,
                Resultado_R2 = item.Evaluation1.Resultado_R2,

                Athlete = item.Athlete?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Evaluation1s.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new Evaluation1ExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}