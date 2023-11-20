import { NgModule } from '@angular/core';
import { TypeRuleComponent } from './components/type-rule.component';
import { TypeRuleRoutingModule } from './type-rule-routing.module';

@NgModule({
  declarations: [],
  imports: [TypeRuleComponent, TypeRuleRoutingModule],
})
export class TypeRuleModule {}

export function loadTypeRuleModuleAsChild() {
  return Promise.resolve(TypeRuleModule);
}
