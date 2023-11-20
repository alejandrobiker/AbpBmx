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
using CompetencyEvaluator.Genders;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Genders
{

    [Authorize(CompetencyEvaluatorPermissions.Genders.Default)]
    public abstract class GendersAppServiceBase : ApplicationService
    {
        protected IDistributedCache<GenderExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IGenderRepository _genderRepository;
        protected GenderManager _genderManager;

        public GendersAppServiceBase(IGenderRepository genderRepository, GenderManager genderManager, IDistributedCache<GenderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _genderRepository = genderRepository;
            _genderManager = genderManager;
        }

        public virtual async Task<PagedResultDto<GenderDto>> GetListAsync(GetGendersInput input)
        {
            var totalCount = await _genderRepository.GetCountAsync(input.FilterText, input.name, input.ShortName);
            var items = await _genderRepository.GetListAsync(input.FilterText, input.name, input.ShortName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<GenderDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Gender>, List<GenderDto>>(items)
            };
        }

        public virtual async Task<GenderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Gender, GenderDto>(await _genderRepository.GetAsync(id));
        }

        [Authorize(CompetencyEvaluatorPermissions.Genders.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _genderRepository.DeleteAsync(id);
        }

        [Authorize(CompetencyEvaluatorPermissions.Genders.Create)]
        public virtual async Task<GenderDto> CreateAsync(GenderCreateDto input)
        {

            var gender = await _genderManager.CreateAsync(
            input.name, input.ShortName
            );

            return ObjectMapper.Map<Gender, GenderDto>(gender);
        }

        [Authorize(CompetencyEvaluatorPermissions.Genders.Edit)]
        public virtual async Task<GenderDto> UpdateAsync(Guid id, GenderUpdateDto input)
        {

            var gender = await _genderManager.UpdateAsync(
            id,
            input.name, input.ShortName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Gender, GenderDto>(gender);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(GenderExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _genderRepository.GetListAsync(input.FilterText, input.name, input.ShortName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Gender>, List<GenderExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Genders.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new GenderExcelDownloadTokenCacheItem { Token = token },
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