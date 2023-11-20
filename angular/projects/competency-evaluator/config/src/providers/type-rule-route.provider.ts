import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCompetencyEvaluatorRouteNames } from '../enums/route-names';

export const TYPE_RULES_TYPE_RULE_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/competency-evaluator/type-rules',
        parentName: eCompetencyEvaluatorRouteNames.CompetencyEvaluator,
        name: 'CompetencyEvaluator::Menu:TypeRules',
        layout: eLayoutType.application,
        requiredPolicy: 'CompetencyEvaluator.TypeRules',
      },
    ]);
  };
}
