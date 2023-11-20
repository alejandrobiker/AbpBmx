import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCompetencyEvaluatorRouteNames } from '../enums/route-names';

export const EVALUATION1S_EVALUATION1_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/competency-evaluator/evaluation1s',
        parentName: eCompetencyEvaluatorRouteNames.CompetencyEvaluator,
        name: 'CompetencyEvaluator::Menu:Evaluation1s',
        layout: eLayoutType.application,
        requiredPolicy: 'CompetencyEvaluator.Evaluation1s',
      },
    ]);
  };
}
