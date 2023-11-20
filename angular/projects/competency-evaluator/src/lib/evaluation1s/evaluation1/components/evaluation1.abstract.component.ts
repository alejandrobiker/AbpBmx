import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { Evaluation1WithNavigationPropertiesDto } from '../../../proxy/evaluation1s/models';
import { Evaluation1ViewService } from '../services/evaluation1.service';
import { Evaluation1DetailViewService } from '../services/evaluation1-detail.service';

@Component({
  template: '',
})
export abstract class AbstractEvaluation1Component implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(Evaluation1ViewService);
  public readonly serviceDetail = inject(Evaluation1DetailViewService);
  protected title = 'CompetencyEvaluator::Evaluation1s';

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

  update(record: Evaluation1WithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: Evaluation1WithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
