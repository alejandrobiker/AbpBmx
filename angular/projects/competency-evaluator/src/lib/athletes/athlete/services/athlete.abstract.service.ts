import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type {
  GetAthletesInput,
  AthleteWithNavigationPropertiesDto,
} from '../../../proxy/athletes/models';
import { AthleteService } from '../../../proxy/athletes/athlete.service';

export abstract class AbstractAthleteViewService {
  protected readonly proxyService = inject(AthleteService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  isExportToExcelBusy = false;

  data: PagedResultDto<AthleteWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetAthletesInput;

  delete(record: AthleteWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('CompetencyEvaluator::DeleteConfirmationMessage', 'CompetencyEvaluator::AreYouSure', {
        messageLocalizationParams: [],
      })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.athlete.id))
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

    const setData = (list: PagedResultDto<AthleteWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetAthletesInput;
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
        downloadBlob(result, 'Athlete.xlsx');
      });
  }
}
