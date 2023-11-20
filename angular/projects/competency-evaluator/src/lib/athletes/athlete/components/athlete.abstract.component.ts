import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { AthleteWithNavigationPropertiesDto } from '../../../proxy/athletes/models';
import { AthleteViewService } from '../services/athlete.service';
import { AthleteDetailViewService } from '../services/athlete-detail.service';

@Component({
  template: '',
})
export abstract class AbstractAthleteComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(AthleteViewService);
  public readonly serviceDetail = inject(AthleteDetailViewService);
  protected title = 'CompetencyEvaluator::Athletes';

  ngOnInit() {
    this.service.hookToQuery();
  }

  clearFilters() {
    this.service.clearFilters();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: AthleteWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: AthleteWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
