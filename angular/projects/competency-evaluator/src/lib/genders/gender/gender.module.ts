import { NgModule } from '@angular/core';
import { GenderComponent } from './components/gender.component';
import { GenderRoutingModule } from './gender-routing.module';

@NgModule({
  declarations: [],
  imports: [GenderComponent, GenderRoutingModule],
})
export class GenderModule {}

export function loadGenderModuleAsChild() {
  return Promise.resolve(GenderModule);
}
