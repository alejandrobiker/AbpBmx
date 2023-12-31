using CompetencyEvaluator.Shared;
using CompetencyEvaluator.Athletes;
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
    public class Evaluation1sAppService : Evaluation1sAppServiceBase, IEvaluation1sAppService
    {
        //<suite-custom-code-autogenerated>
        public Evaluation1sAppService(IEvaluation1Repository evaluation1Repository, Evaluation1Manager evaluation1Manager, IDistributedCache<Evaluation1ExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Athlete, Guid> athleteRepository)
            : base(evaluation1Repository, evaluation1Manager, excelDownloadTokenCache, athleteRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}