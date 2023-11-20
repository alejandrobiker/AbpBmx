import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CompetencyEvaluatorComponent } from './components/competency-evaluator.component';
import { CompetencyEvaluatorRoutingModule } from './competency-evaluator-routing.module';

@NgModule({
  declarations: [CompetencyEvaluatorComponent],
  imports: [CoreModule, ThemeSharedModule, CompetencyEvaluatorRoutingModule],
  exports: [CompetencyEvaluatorComponent],
})
export class CompetencyEvaluatorModule {
  static forChild(): ModuleWithProviders<CompetencyEvaluatorModule> {
    return {
      ngModule: CompetencyEvaluatorModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<CompetencyEvaluatorModule> {
    return new LazyModuleFactory(CompetencyEvaluatorModule.forChild());
  }
}
