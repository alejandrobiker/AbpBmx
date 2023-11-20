import type {
  GetTypeRulesInput,
  TypeRuleCreateDto,
  TypeRuleDto,
  TypeRuleExcelDownloadDto,
  TypeRuleUpdateDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class TypeRuleService {
  apiName = 'CompetencyEvaluator';

  create = (input: TypeRuleCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TypeRuleDto>(
      {
        method: 'POST',
        url: '/api/competency-evaluator/type-rules',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/competency-evaluator/type-rules/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TypeRuleDto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/type-rules/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/type-rules/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetTypeRulesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TypeRuleDto>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/type-rules',
        params: {
          filterText: input.filterText,
          name: input.name,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: TypeRuleExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/competency-evaluator/type-rules/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: TypeRuleUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TypeRuleDto>(
      {
        method: 'PUT',
        url: `/api/competency-evaluator/type-rules/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
