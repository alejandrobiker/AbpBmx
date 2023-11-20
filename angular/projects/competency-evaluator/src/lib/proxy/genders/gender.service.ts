import type {
  GenderCreateDto,
  GenderDto,
  GenderExcelDownloadDto,
  GenderUpdateDto,
  GetGendersInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class GenderService {
  apiName = 'CompetencyEvaluator';

  create = (input: GenderCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, GenderDto>(
      {
        method: 'POST',
        url: '/api/competency-evaluator/genders',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/competency-evaluator/genders/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, GenderDto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/genders/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/genders/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetGendersInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<GenderDto>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/genders',
        params: {
          filterText: input.filterText,
          name: input.name,
          shortName: input.shortName,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: GenderExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/competency-evaluator/genders/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: GenderUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, GenderDto>(
      {
        method: 'PUT',
        url: `/api/competency-evaluator/genders/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
