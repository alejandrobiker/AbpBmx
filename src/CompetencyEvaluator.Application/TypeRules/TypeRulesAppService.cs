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

    [Authorize(CompetencyEvaluatorPermissions.TypeRules.Default)]
    public abstract class TypeRulesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<TypeRuleExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected ITypeRuleRepository _typeRuleRepository;
        protected TypeRuleManager _typeRuleManager;

        public TypeRulesAppServiceBase(ITypeRuleRepository typeRuleRepository, TypeRuleManager typeRuleManager, IDistributedCache<TypeRuleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _typeRuleRepository = typeRuleRepository;
            _typeRuleManager = typeRuleManager;
        }

        public virtual async Task<PagedResultDto<TypeRuleDto>> GetListAsync(GetTypeRulesInput input)
        {
            var totalCount = await _typeRuleRepository.GetCountAsync(input.FilterText, input.name);
            var items = await _typeRuleRepository.GetListAsync(input.FilterText, input.name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TypeRuleDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TypeRule>, List<TypeRuleDto>>(items)
            };
        }

        public virtual async Task<TypeRuleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TypeRule, TypeRuleDto>(await _typeRuleRepository.GetAsync(id));
        }

        [Authorize(CompetencyEvaluatorPermissions.TypeRules.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _typeRuleRepository.DeleteAsync(id);
        }

        [Authorize(CompetencyEvaluatorPermissions.TypeRules.Create)]
        public virtual async Task<TypeRuleDto> CreateAsync(TypeRuleCreateDto input)
        {

            var typeRule = await _typeRuleManager.CreateAsync(
            input.name
            );

            return ObjectMapper.Map<TypeRule, TypeRuleDto>(typeRule);
        }

        [Authorize(CompetencyEvaluatorPermissions.TypeRules.Edit)]
        public virtual async Task<TypeRuleDto> UpdateAsync(Guid id, TypeRuleUpdateDto input)
        {

            var typeRule = await _typeRuleManager.UpdateAsync(
            id,
            input.name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<TypeRule, TypeRuleDto>(typeRule);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TypeRuleExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _typeRuleRepository.GetListAsync(input.FilterText, input.name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TypeRule>, List<TypeRuleExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "TypeRules.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TypeRuleExcelDownloadTokenCacheItem { Token = token },
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