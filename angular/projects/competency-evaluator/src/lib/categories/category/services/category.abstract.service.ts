import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type { GetCategoriesInput, CategoryDto } from '../../../proxy/categories/models';
import { CategoryService } from '../../../proxy/categories/category.service';

export abstract class AbstractCategoryViewService {
  protected readonly proxyService = inject(CategoryService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  isExportToExcelBusy = false;

  data: PagedResultDto<CategoryDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetCategoriesInput;

  delete(record: CategoryDto) {
    this.confirmationService
      .warn('CompetencyEvaluator::DeleteConfirmationMessage', 'CompetencyEvaluator::AreYouSure', {
        messageLocalizationParams: [],
      })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.id))
      )
      .subscribe(this.list.get);
  }

  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this.proxyService.getList({
        ...query,
        ...this.filters,
        filterText: query.filter,
      });

    const setData = (list: PagedResultDto<CategoryDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetCategoriesInput;
    this.list.get();
  }

  exportToExcel() {
    this.isExportToExcelBusy = true;
    this.proxyService
      .getDownloadToken()
      .pipe(
        switchMap(({ token }) =>
          this.proxyService.getListAsExcelFile({
            downloadToken: token,
            filterText: this.list.filter,
          })
        ),
        finalize(() => (this.isExportToExcelBusy = false))
      )
      .subscribe(result => {
        downloadBlob(result, 'Category.xlsx');
      });
  }
}
