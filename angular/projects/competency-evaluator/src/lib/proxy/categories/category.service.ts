import type {
  CategoryCreateDto,
  CategoryDto,
  CategoryExcelDownloadDto,
  CategoryUpdateDto,
  GetCategoriesInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  apiName = 'CompetencyEvaluator';

  create = (input: CategoryCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CategoryDto>(
      {
        method: 'POST',
        url: '/api/competency-evaluator/categories',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/competency-evaluator/categories/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CategoryDto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/categories/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/categories/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetCategoriesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CategoryDto>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/categories',
        params: {
          filterText: input.filterText,
          name: input.name,
          maxAgeMin: input.maxAgeMin,
          maxAgeMax: input.maxAgeMax,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: CategoryExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/competency-evaluator/categories/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: CategoryUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CategoryDto>(
      {
        method: 'PUT',
        url: `/api/competency-evaluator/categories/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
