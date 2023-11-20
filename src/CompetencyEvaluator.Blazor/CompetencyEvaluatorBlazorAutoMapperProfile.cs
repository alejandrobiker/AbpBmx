using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using Volo.Abp.AutoMapper;
using CompetencyEvaluator.TypeRules;
using AutoMapper;

namespace CompetencyEvaluator.Blazor;

public class CompetencyEvaluatorBlazorAutoMapperProfile : Profile
{
    public CompetencyEvaluatorBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<TypeRuleDto, TypeRuleUpdateDto>();

        CreateMap<GenderDto, GenderUpdateDto>();

        CreateMap<CategoryDto, CategoryUpdateDto>();

        CreateMap<AthleteDto, AthleteUpdateDto>();

        CreateMap<Evaluation1Dto, Evaluation1UpdateDto>();
    }
}