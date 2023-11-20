import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompetencyEvaluatorComponent } from './components/competency-evaluator.component';
import { loadTypeRuleModuleAsChild } from './type-rules/type-rule/type-rule.module';
import { loadGenderModuleAsChild } from './genders/gender/gender.module';
import { loadCategoryModuleAsChild } from './categories/category/category.module';
import { loadAthleteModuleAsChild } from './athletes/athlete/athlete.module';
import { loadEvaluation1ModuleAsChild } from './evaluation1s/evaluation1/evaluation1.module';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: CompetencyEvaluatorComponent,
  },
  { path: 'type-rules', loadChildren: loadTypeRuleModuleAsChild },
  { path: 'genders', loadChildren: loadGenderModuleAsChild },
  { path: 'categories', loadChildren: loadCategoryModuleAsChild },
  { path: 'athletes', loadChildren: loadAthleteModuleAsChild },
  { path: 'evaluation1s', loadChildren: loadEvaluation1ModuleAsChild },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CompetencyEvaluatorRoutingModule {}
