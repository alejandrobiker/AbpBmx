import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCompetencyEvaluatorRouteNames } from '../enums/route-names';

export const GENDERS_GENDER_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/competency-evaluator/genders',
        parentName: eCompetencyEvaluatorRouteNames.CompetencyEvaluator,
        name: 'CompetencyEvaluator::Menu:Genders',
        layout: eLayoutType.application,
        requiredPolicy: 'CompetencyEvaluator.Genders',
      },
    ]);
  };
}
