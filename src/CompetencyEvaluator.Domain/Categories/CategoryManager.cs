using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CompetencyEvaluator.Categories
{
    public abstract class CategoryManagerBase : DomainService
    {
        protected ICategoryRepository _categoryRepository;

        public CategoryManagerBase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public virtual async Task<Category> CreateAsync(
        string name, int? maxAge = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CategoryConsts.NameMaxLength, CategoryConsts.NameMinLength);

            var category = new Category(
             GuidGenerator.Create(),
             name, maxAge
             );

            return await _categoryRepository.InsertAsync(category);
        }

        public virtual async Task<Category> UpdateAsync(
            Guid id,
            string name, int? maxAge = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CategoryConsts.NameMaxLength, CategoryConsts.NameMinLength);

            var category = await _categoryRepository.GetAsync(id);

            category.Name = name;
            category.MaxAge = maxAge;

            category.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _categoryRepository.UpdateAsync(category);
        }

    }
}