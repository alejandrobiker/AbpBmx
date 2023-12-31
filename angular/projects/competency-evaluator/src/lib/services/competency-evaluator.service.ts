import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class CompetencyEvaluatorService {
  apiName = 'CompetencyEvaluator';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/CompetencyEvaluator/sample' },
      { apiName: this.apiName }
    );
  }
}
