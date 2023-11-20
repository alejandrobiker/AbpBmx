import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { SelectCategoryEvaluationComponent } from './components/select-category-evaluation/select-category-evaluation.component';

export const routes: Routes = [
  {
    path: '',
    component: SelectCategoryEvaluationComponent,
    canActivate: mapToCanActivate([AuthGuard, PermissionGuard]),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SelectCategoryEvaluationRoutingModule { }
