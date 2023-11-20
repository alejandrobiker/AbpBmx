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

import { Evaluation1ViewService } from '../services/evaluation1.service';
import { Evaluation1DetailViewService } from '../services/evaluation1-detail.service';
import { Evaluation1DetailComponent } from './evaluation1-detail.component';
import { AbstractEvaluation1Component } from './evaluation1.abstract.component';

@Component({
  selector: 'lib-evaluation1',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    CoreModule,
    ThemeSharedModule,
    Evaluation1DetailComponent,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

    PageModule,
  ],
  providers: [
    ListService,
    Evaluation1ViewService,
    Evaluation1DetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './evaluation1.component.html',
  styles: [],
})
export class Evaluation1Component extends AbstractEvaluation1Component {}
