import type {
  Evaluation1CreateDto,
  Evaluation1Dto,
  Evaluation1ExcelDownloadDto,
  Evaluation1UpdateDto,
  Evaluation1WithNavigationPropertiesDto,
  GetEvaluation1sInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class Evaluation1Service {
  apiName = 'CompetencyEvaluator';

  create = (input: Evaluation1CreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Evaluation1Dto>(
      {
        method: 'POST',
        url: '/api/competency-evaluator/evaluation1s',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/competency-evaluator/evaluation1s/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Evaluation1Dto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/evaluation1s/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getAthleteLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/evaluation1s/athlete-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/evaluation1s/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetEvaluation1sInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<Evaluation1WithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/evaluation1s',
        params: {
          filterText: input.filterText,
          criterio_1_R1Min: input.criterio_1_R1Min,
          criterio_1_R1Max: input.criterio_1_R1Max,
          criterio_1_R2Min: input.criterio_1_R2Min,
          criterio_1_R2Max: input.criterio_1_R2Max,
          criterio_2_R1Min: input.criterio_2_R1Min,
          criterio_2_R1Max: input.criterio_2_R1Max,
          criterio_2_R2Min: input.criterio_2_R2Min,
          criterio_2_R2Max: input.criterio_2_R2Max,
          criterio_3_R1Min: input.criterio_3_R1Min,
          criterio_3_R1Max: input.criterio_3_R1Max,
          criterio_3_R2Min: input.criterio_3_R2Min,
          criterio_3_R2Max: input.criterio_3_R2Max,
          criterio_4_R1Min: input.criterio_4_R1Min,
          criterio_4_R1Max: input.criterio_4_R1Max,
          criterio_4_R2Min: input.criterio_4_R2Min,
          criterio_4_R2Max: input.criterio_4_R2Max,
          resultado_R1Min: input.resultado_R1Min,
          resultado_R1Max: input.resultado_R1Max,
          resultado_R2Min: input.resultado_R2Min,
          resultado_R2Max: input.resultado_R2Max,
          athleteId: input.athleteId,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: Evaluation1ExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/competency-evaluator/evaluation1s/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Evaluation1WithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/evaluation1s/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: Evaluation1UpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Evaluation1Dto>(
      {
        method: 'PUT',
        url: `/api/competency-evaluator/evaluation1s/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
