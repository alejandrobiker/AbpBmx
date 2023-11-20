import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { GenderComponent } from './components/gender.component';

export const routes: Routes = [
  {
    path: '',
    component: GenderComponent,
    canActivate: mapToCanActivate([AuthGuard, PermissionGuard]),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GenderRoutingModule {}
