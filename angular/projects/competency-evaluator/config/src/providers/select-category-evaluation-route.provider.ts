import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCompetencyEvaluatorRouteNames } from '../enums/route-names';

export const SELECT_CATEGORY_EVALUATION_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/competency-evaluator/select-category-evaluation',
        parentName: eCompetencyEvaluatorRouteNames.CompetencyEvaluator,
        name: 'CompetencyEvaluator::Select Category',
        layout: eLayoutType.application,
        requiredPolicy: 'CompetencyEvaluator.Evaluation1s',
      },
    ]);
  };
}
