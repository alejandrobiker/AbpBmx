using CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Categories;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Genders;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Web.Pages.CompetencyEvaluator.TypeRules;
using Volo.Abp.AutoMapper;
using CompetencyEvaluator.TypeRules;
using AutoMapper;

namespace CompetencyEvaluator.Web;

public class CompetencyEvaluatorWebAutoMapperProfile : Profile
{
    public CompetencyEvaluatorWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<TypeRuleDto, TypeRuleUpdateViewModel>();
        CreateMap<TypeRuleUpdateViewModel, TypeRuleUpdateDto>();
        CreateMap<TypeRuleCreateViewModel, TypeRuleCreateDto>();

        CreateMap<GenderDto, GenderUpdateViewModel>();
        CreateMap<GenderUpdateViewModel, GenderUpdateDto>();
        CreateMap<GenderCreateViewModel, GenderCreateDto>();

        CreateMap<CategoryDto, CategoryUpdateViewModel>();
        CreateMap<CategoryUpdateViewModel, CategoryUpdateDto>();
        CreateMap<CategoryCreateViewModel, CategoryCreateDto>();

        CreateMap<AthleteDto, AthleteUpdateViewModel>();
        CreateMap<AthleteUpdateViewModel, AthleteUpdateDto>();
        CreateMap<AthleteCreateViewModel, AthleteCreateDto>();

        CreateMap<Evaluation1Dto, Evaluation1UpdateViewModel>();
        CreateMap<Evaluation1UpdateViewModel, Evaluation1UpdateDto>();
        CreateMap<Evaluation1CreateViewModel, Evaluation1CreateDto>();
    }
}