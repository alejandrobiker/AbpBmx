import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PageModule } from '@abp/ng.components/page';

import { TypeRuleViewService } from '../services/type-rule.service';
import { TypeRuleDetailViewService } from '../services/type-rule-detail.service';
import { TypeRuleDetailComponent } from './type-rule-detail.component';
import { AbstractTypeRuleComponent } from './type-rule.abstract.component';

@Component({
  selector: 'lib-type-rule',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    CoreModule,
    ThemeSharedModule,
    TypeRuleDetailComponent,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

    PageModule,
  ],
  providers: [
    ListService,
    TypeRuleViewService,
    TypeRuleDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './type-rule.component.html',
  styles: [],
})
export class TypeRuleComponent extends AbstractTypeRuleComponent {}
