import { ModuleWithProviders, NgModule } from '@angular/core';
import { COMPETENCY_EVALUATOR_ROUTE_PROVIDERS } from './providers/route.provider';
import { TYPE_RULES_TYPE_RULE_ROUTE_PROVIDER } from './providers/type-rule-route.provider';
import { GENDERS_GENDER_ROUTE_PROVIDER } from './providers/gender-route.provider';
import { CATEGORIES_CATEGORY_ROUTE_PROVIDER } from './providers/category-route.provider';
import { ATHLETES_ATHLETE_ROUTE_PROVIDER } from './providers/athlete-route.provider';
import { EVALUATION1S_EVALUATION1_ROUTE_PROVIDER } from './providers/evaluation1-route.provider';
import { SELECT_CATEGORY_EVALUATION_ROUTE_PROVIDER } from './providers/select-category-evaluation-route.provider';

@NgModule()
export class CompetencyEvaluatorConfigModule {
  static forRoot(): ModuleWithProviders<CompetencyEvaluatorConfigModule> {
    return {
      ngModule: CompetencyEvaluatorConfigModule,
      providers: [
        COMPETENCY_EVALUATOR_ROUTE_PROVIDERS,
        TYPE_RULES_TYPE_RULE_ROUTE_PROVIDER,
        GENDERS_GENDER_ROUTE_PROVIDER,
        CATEGORIES_CATEGORY_ROUTE_PROVIDER,
        ATHLETES_ATHLETE_ROUTE_PROVIDER,
        EVALUATION1S_EVALUATION1_ROUTE_PROVIDER
      ],
    };
  }
}
