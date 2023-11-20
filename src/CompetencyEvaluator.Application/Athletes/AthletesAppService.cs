using CompetencyEvaluator.Shared;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
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
using CompetencyEvaluator.Athletes;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Athletes
{

    [Authorize(CompetencyEvaluatorPermissions.Athletes.Default)]
    public abstract class AthletesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<AthleteExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IAthleteRepository _athleteRepository;
        protected AthleteManager _athleteManager;
        protected IRepository<Gender, Guid> _genderRepository;
        protected IRepository<Category, Guid> _categoryRepository;

        public AthletesAppServiceBase(IAthleteRepository athleteRepository, AthleteManager athleteManager, IDistributedCache<AthleteExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Gender, Guid> genderRepository, IRepository<Category, Guid> categoryRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _athleteRepository = athleteRepository;
            _athleteManager = athleteManager; _genderRepository = genderRepository;
            _categoryRepository = categoryRepository;
        }

        public virtual async Task<PagedResultDto<AthleteWithNavigationPropertiesDto>> GetListAsync(GetAthletesInput input)
        {
            var totalCount = await _athleteRepository.GetCountAsync(input.FilterText, input.Name, input.DateOfBirthMin, input.DateOfBirthMax, input.GenderId, input.CategoryId);
            var items = await _athleteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.DateOfBirthMin, input.DateOfBirthMax, input.GenderId, input.CategoryId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AthleteWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AthleteWithNavigationProperties>, List<AthleteWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<AthleteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<AthleteWithNavigationProperties, AthleteWithNavigationPropertiesDto>
                (await _athleteRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<AthleteDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Athlete, AthleteDto>(await _athleteRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGenderLookupAsync(LookupRequestDto input)
        {
            var query = (await _genderRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.name != null &&
                         x.name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Gender>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Gender>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCategoryLookupAsync(LookupRequestDto input)
        {
            var query = (await _categoryRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Category>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Category>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(CompetencyEvaluatorPermissions.Athletes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _athleteRepository.DeleteAsync(id);
        }

        [Authorize(CompetencyEvaluatorPermissions.Athletes.Create)]
        public virtual async Task<AthleteDto> CreateAsync(AthleteCreateDto input)
        {
            if (input.GenderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Gender"]]);
            }
            if (input.CategoryId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Category"]]);
            }

            var athlete = await _athleteManager.CreateAsync(
            input.GenderId, input.CategoryId, input.Name, input.DateOfBirth
            );

            return ObjectMapper.Map<Athlete, AthleteDto>(athlete);
        }

        [Authorize(CompetencyEvaluatorPermissions.Athletes.Edit)]
        public virtual async Task<AthleteDto> UpdateAsync(Guid id, AthleteUpdateDto input)
        {
            if (input.GenderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Gender"]]);
            }
            if (input.CategoryId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Category"]]);
            }

            var athlete = await _athleteManager.UpdateAsync(
            id,
            input.GenderId, input.CategoryId, input.Name, input.DateOfBirth, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Athlete, AthleteDto>(athlete);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AthleteExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var athletes = await _athleteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.DateOfBirthMin, input.DateOfBirthMax);
            var items = athletes.Select(item => new
            {
                Name = item.Athlete.Name,
                DateOfBirth = item.Athlete.DateOfBirth,

                Gender = item.Gender?.name,
                Category = item.Category?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Athletes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AthleteExcelDownloadTokenCacheItem { Token = token },
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