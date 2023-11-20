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
using CompetencyEvaluator.Categories;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Categories
{

    [Authorize(CompetencyEvaluatorPermissions.Categories.Default)]
    public abstract class CategoriesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<CategoryExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected ICategoryRepository _categoryRepository;
        protected CategoryManager _categoryManager;

        public CategoriesAppServiceBase(ICategoryRepository categoryRepository, CategoryManager categoryManager, IDistributedCache<CategoryExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
        }

        public virtual async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoriesInput input)
        {
            var totalCount = await _categoryRepository.GetCountAsync(input.FilterText, input.Name, input.MaxAgeMin, input.MaxAgeMax);
            var items = await _categoryRepository.GetListAsync(input.FilterText, input.Name, input.MaxAgeMin, input.MaxAgeMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CategoryDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Category>, List<CategoryDto>>(items)
            };
        }

        public virtual async Task<CategoryDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Category, CategoryDto>(await _categoryRepository.GetAsync(id));
        }

        [Authorize(CompetencyEvaluatorPermissions.Categories.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        [Authorize(CompetencyEvaluatorPermissions.Categories.Create)]
        public virtual async Task<CategoryDto> CreateAsync(CategoryCreateDto input)
        {

            var category = await _categoryManager.CreateAsync(
            input.Name, input.MaxAge
            );

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        [Authorize(CompetencyEvaluatorPermissions.Categories.Edit)]
        public virtual async Task<CategoryDto> UpdateAsync(Guid id, CategoryUpdateDto input)
        {

            var category = await _categoryManager.UpdateAsync(
            id,
            input.Name, input.MaxAge, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CategoryExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _categoryRepository.GetListAsync(input.FilterText, input.Name, input.MaxAgeMin, input.MaxAgeMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Category>, List<CategoryExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Categories.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CategoryExcelDownloadTokenCacheItem { Token = token },
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