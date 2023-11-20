using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using CompetencyEvaluator.TypeRules;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace CompetencyEvaluator.EntityFrameworkCore;

public static class CompetencyEvaluatorDbContextModelCreatingExtensions
{
    public static void ConfigureCompetencyEvaluator(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(CompetencyEvaluatorDbProperties.DbTablePrefix + "Questions", CompetencyEvaluatorDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        builder.Entity<TypeRule>(b =>
    {
        b.ToTable(CompetencyEvaluatorDbProperties.DbTablePrefix + "TypeRules", CompetencyEvaluatorDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(TypeRule.TenantId));
        b.Property(x => x.name).HasColumnName(nameof(TypeRule.name)).IsRequired().HasMaxLength(TypeRuleConsts.nameMaxLength);
    });
        builder.Entity<Gender>(b =>
    {
        b.ToTable(CompetencyEvaluatorDbProperties.DbTablePrefix + "Genders", CompetencyEvaluatorDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Gender.TenantId));
        b.Property(x => x.name).HasColumnName(nameof(Gender.name)).IsRequired().HasMaxLength(GenderConsts.nameMaxLength);
        b.Property(x => x.ShortName).HasColumnName(nameof(Gender.ShortName)).HasMaxLength(GenderConsts.ShortNameMaxLength);
    });

        builder.Entity<Category>(b =>
    {
        b.ToTable(CompetencyEvaluatorDbProperties.DbTablePrefix + "Categories", CompetencyEvaluatorDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Category.TenantId));
        b.Property(x => x.Name).HasColumnName(nameof(Category.Name)).IsRequired().HasMaxLength(CategoryConsts.NameMaxLength);
        b.Property(x => x.MaxAge).HasColumnName(nameof(Category.MaxAge)).HasMaxLength(CategoryConsts.MaxAgeMaxLength);
    });

        builder.Entity<Athlete>(b =>
    {
        b.ToTable(CompetencyEvaluatorDbProperties.DbTablePrefix + "Athletes", CompetencyEvaluatorDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Athlete.TenantId));
        b.Property(x => x.Name).HasColumnName(nameof(Athlete.Name)).IsRequired().HasMaxLength(AthleteConsts.NameMaxLength);
        b.Property(x => x.DateOfBirth).HasColumnName(nameof(Athlete.DateOfBirth));
        b.HasOne<Gender>().WithMany().IsRequired().HasForeignKey(x => x.GenderId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Category>().WithMany().IsRequired().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<Evaluation1>(b =>
    {
        b.ToTable(CompetencyEvaluatorDbProperties.DbTablePrefix + "Evaluation1s", CompetencyEvaluatorDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Evaluation1.TenantId));
        b.Property(x => x.Criterio_1_R1).HasColumnName(nameof(Evaluation1.Criterio_1_R1)).HasMaxLength((int)Evaluation1Consts.Criterio_1_R1MaxLength);
        b.Property(x => x.Criterio_1_R2).HasColumnName(nameof(Evaluation1.Criterio_1_R2)).HasMaxLength((int)Evaluation1Consts.Criterio_1_R2MaxLength);
        b.Property(x => x.Criterio_2_R1).HasColumnName(nameof(Evaluation1.Criterio_2_R1)).HasMaxLength((int)Evaluation1Consts.Criterio_2_R1MaxLength);
        b.Property(x => x.Criterio_2_R2).HasColumnName(nameof(Evaluation1.Criterio_2_R2)).HasMaxLength((int)Evaluation1Consts.Criterio_2_R2MaxLength);
        b.Property(x => x.Criterio_3_R1).HasColumnName(nameof(Evaluation1.Criterio_3_R1)).HasMaxLength((int)Evaluation1Consts.Criterio_3_R1MaxLength);
        b.Property(x => x.Criterio_3_R2).HasColumnName(nameof(Evaluation1.Criterio_3_R2)).HasMaxLength((int)Evaluation1Consts.Criterio_3_R2MaxLength);
        b.Property(x => x.Criterio_4_R1).HasColumnName(nameof(Evaluation1.Criterio_4_R1)).HasMaxLength((int)Evaluation1Consts.Criterio_4_R1MaxLength);
        b.Property(x => x.Criterio_4_R2).HasColumnName(nameof(Evaluation1.Criterio_4_R2)).HasMaxLength((int)Evaluation1Consts.Criterio_4_R2MaxLength);
        b.Property(x => x.Resultado_R1).HasColumnName(nameof(Evaluation1.Resultado_R1));
        b.Property(x => x.Resultado_R2).HasColumnName(nameof(Evaluation1.Resultado_R2));
        b.HasOne<Athlete>().WithMany().IsRequired().HasForeignKey(x => x.AthleteId).OnDelete(DeleteBehavior.NoAction);
    });
    }
}