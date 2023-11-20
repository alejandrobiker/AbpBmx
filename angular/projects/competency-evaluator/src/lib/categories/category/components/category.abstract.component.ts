import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { CategoryDto } from '../../../proxy/categories/models';
import { CategoryViewService } from '../services/category.service';
import { CategoryDetailViewService } from '../services/category-detail.service';

@Component({
  template: '',
})
export abstract class AbstractCategoryComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(CategoryViewService);
  public readonly serviceDetail = inject(CategoryDetailViewService);
  protected title = 'CompetencyEvaluator::Categories';

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

  update(record: CategoryDto) {
    this.serviceDetail.update(record);
  }

  delete(record: CategoryDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
