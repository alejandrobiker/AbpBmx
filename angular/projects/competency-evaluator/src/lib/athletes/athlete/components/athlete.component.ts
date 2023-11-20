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

import { AthleteViewService } from '../services/athlete.service';
import { AthleteDetailViewService } from '../services/athlete-detail.service';
import { AthleteDetailComponent } from './athlete-detail.component';
import { AbstractAthleteComponent } from './athlete.abstract.component';

@Component({
  selector: 'lib-athlete',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    CoreModule,
    ThemeSharedModule,
    AthleteDetailComponent,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

    PageModule,
  ],
  providers: [
    ListService,
    AthleteViewService,
    AthleteDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './athlete.component.html',
  styles: [],
})
export class AthleteComponent extends AbstractAthleteComponent {}
