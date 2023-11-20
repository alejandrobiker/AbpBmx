import { NgModule } from '@angular/core';
import { Evaluation1Component } from './components/evaluation1.component';
import { Evaluation1RoutingModule } from './evaluation1-routing.module';

@NgModule({
  declarations: [],
  imports: [Evaluation1Component, Evaluation1RoutingModule],
})
export class Evaluation1Module {}

export function loadEvaluation1ModuleAsChild() {
  return Promise.resolve(Evaluation1Module);
}
