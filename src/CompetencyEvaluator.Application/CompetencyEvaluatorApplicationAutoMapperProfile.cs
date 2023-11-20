using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using System;
using CompetencyEvaluator.Shared;
using Volo.Abp.AutoMapper;
using CompetencyEvaluator.TypeRules;
using AutoMapper;

namespace CompetencyEvaluator;

public class CompetencyEvaluatorApplicationAutoMapperProfile : Profile
{
    public CompetencyEvaluatorApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<TypeRule, TypeRuleDto>();
        CreateMap<TypeRule, TypeRuleExcelDto>();

        CreateMap<Gender, GenderDto>();
        CreateMap<Gender, GenderExcelDto>();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryExcelDto>();

        CreateMap<Athlete, AthleteDto>();
        CreateMap<Athlete, AthleteExcelDto>();

        CreateMap<AthleteWithNavigationProperties, AthleteWithNavigationPropertiesDto>();
        CreateMap<Gender, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.name));
        CreateMap<Category, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Evaluation1, Evaluation1Dto>();
        CreateMap<Evaluation1, Evaluation1ExcelDto>();

        CreateMap<Evaluation1WithNavigationProperties, Evaluation1WithNavigationPropertiesDto>();
        CreateMap<Athlete, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}