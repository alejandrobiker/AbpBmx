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
using CompetencyEvaluator.TypeRules;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.TypeRules
{
    public class TypeRulesAppService : TypeRulesAppServiceBase, ITypeRulesAppService
    {
        //<suite-custom-code-autogenerated>
        public TypeRulesAppService(ITypeRuleRepository typeRuleRepository, TypeRuleManager typeRuleManager, IDistributedCache<TypeRuleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(typeRuleRepository, typeRuleManager, excelDownloadTokenCache)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}