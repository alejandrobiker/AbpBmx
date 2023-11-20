using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Categories;
using Volo.Abp.Content;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Categories
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Category")]
    [Route("api/competency-evaluator/categories")]
    public abstract class CategoryControllerBase : AbpController
    {
        protected ICategoriesAppService _categoriesAppService;

        public CategoryControllerBase(ICategoriesAppService categoriesAppService)
        {
            _categoriesAppService = categoriesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoriesInput input)
        {
            return _categoriesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CategoryDto> GetAsync(Guid id)
        {
            return _categoriesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CategoryDto> CreateAsync(CategoryCreateDto input)
        {
            return _categoriesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CategoryDto> UpdateAsync(Guid id, CategoryUpdateDto input)
        {
            return _categoriesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _categoriesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CategoryExcelDownloadDto input)
        {
            return _categoriesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _categoriesAppService.GetDownloadTokenAsync();
        }
    }
}