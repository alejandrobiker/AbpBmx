import type {
  AthleteCreateDto,
  AthleteDto,
  AthleteExcelDownloadDto,
  AthleteUpdateDto,
  AthleteWithNavigationPropertiesDto,
  GetAthletesInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class AthleteService {
  apiName = 'CompetencyEvaluator';

  create = (input: AthleteCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AthleteDto>(
      {
        method: 'POST',
        url: '/api/competency-evaluator/athletes',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/competency-evaluator/athletes/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AthleteDto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/athletes/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getCategoryLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/athletes/category-lookup',
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
        url: '/api/competency-evaluator/athletes/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getGenderLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/athletes/gender-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetAthletesInput, config?: Partial<Rest.Config>) => {
    console.log('getList');
    return this.restService.request<any, PagedResultDto<AthleteWithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/competency-evaluator/athletes',
        params: {
          filterText: input.filterText,
          name: input.name,
          dateOfBirthMin: input.dateOfBirthMin,
          dateOfBirthMax: input.dateOfBirthMax,
          genderId: input.genderId,
          categoryId: input.categoryId,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );
  }
    

  getListAsExcelFile = (input: AthleteExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/competency-evaluator/athletes/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AthleteWithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/competency-evaluator/athletes/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: AthleteUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AthleteDto>(
      {
        method: 'PUT',
        url: `/api/competency-evaluator/athletes/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
