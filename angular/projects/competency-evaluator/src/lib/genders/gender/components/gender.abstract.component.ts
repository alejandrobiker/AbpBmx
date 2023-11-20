import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { GenderDto } from '../../../proxy/genders/models';
import { GenderViewService } from '../services/gender.service';
import { GenderDetailViewService } from '../services/gender-detail.service';

@Component({
  template: '',
})
export abstract class AbstractGenderComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(GenderViewService);
  public readonly serviceDetail = inject(GenderDetailViewService);
  protected title = 'CompetencyEvaluator::Genders';

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

  update(record: GenderDto) {
    this.serviceDetail.update(record);
  }

  delete(record: GenderDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
