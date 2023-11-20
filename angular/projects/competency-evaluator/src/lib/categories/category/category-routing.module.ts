import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { CategoryComponent } from './components/category.component';

export const routes: Routes = [
  {
    path: '',
    component: CategoryComponent,
    canActivate: mapToCanActivate([AuthGuard, PermissionGuard]),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoryRoutingModule {}
