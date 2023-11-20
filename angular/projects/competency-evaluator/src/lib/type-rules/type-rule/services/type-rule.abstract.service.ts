import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type { GetTypeRulesInput, TypeRuleDto } from '../../../proxy/type-rules/models';
import { TypeRuleService } from '../../../proxy/type-rules/type-rule.service';

export abstract class AbstractTypeRuleViewService {
  protected readonly proxyService = inject(TypeRuleService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  isExportToExcelBusy = false;

  data: PagedResultDto<TypeRuleDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetTypeRulesInput;

  delete(record: TypeRuleDto) {
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

    const setData = (list: PagedResultDto<TypeRuleDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetTypeRulesInput;
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
        downloadBlob(result, 'TypeRule.xlsx');
      });
  }
}
