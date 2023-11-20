import { NgModule } from '@angular/core';
import { AthleteComponent } from './components/athlete.component';
import { AthleteRoutingModule } from './athlete-routing.module';

@NgModule({
  declarations: [],
  imports: [AthleteComponent, AthleteRoutingModule],
})
export class AthleteModule {}

export function loadAthleteModuleAsChild() {
  return Promise.resolve(AthleteModule);
}
