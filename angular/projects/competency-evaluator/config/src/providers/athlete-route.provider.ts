import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCompetencyEvaluatorRouteNames } from '../enums/route-names';

export const ATHLETES_ATHLETE_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/competency-evaluator/athletes',
        parentName: eCompetencyEvaluatorRouteNames.CompetencyEvaluator,
        name: 'CompetencyEvaluator::Menu:Athletes',
        layout: eLayoutType.application,
        requiredPolicy: 'CompetencyEvaluator.Athletes',
      },
    ]);
  };
}
