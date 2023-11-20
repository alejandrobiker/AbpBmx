import { TestBed } from '@angular/core/testing';

import { CompetencyEvaluatorService } from './competency-evaluator.service';

describe('CompetencyEvaluatorService', () => {
  let service: CompetencyEvaluatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompetencyEvaluatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
