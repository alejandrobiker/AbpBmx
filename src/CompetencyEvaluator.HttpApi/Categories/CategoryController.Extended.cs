using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Categories;

namespace CompetencyEvaluator.Categories
{
    [RemoteService(Name = "CompetencyEvaluator")]
    [Area("competencyEvaluator")]
    [ControllerName("Category")]
    [Route("api/competency-evaluator/categories")]
    public class CategoryController : CategoryControllerBase, ICategoriesAppService
    {
        public CategoryController(ICategoriesAppService categoriesAppService) : base(categoriesAppService)
        {
        }
    }
}