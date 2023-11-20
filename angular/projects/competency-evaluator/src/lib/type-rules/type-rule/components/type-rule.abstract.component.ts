import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { TypeRuleDto } from '../../../proxy/type-rules/models';
import { TypeRuleViewService } from '../services/type-rule.service';
import { TypeRuleDetailViewService } from '../services/type-rule-detail.service';

@Component({
  template: '',
})
export abstract class AbstractTypeRuleComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(TypeRuleViewService);
  public readonly serviceDetail = inject(TypeRuleDetailViewService);
  protected title = 'CompetencyEvaluator::TypeRules';

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

  update(record: TypeRuleDto) {
    this.serviceDetail.update(record);
  }

  delete(record: TypeRuleDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
