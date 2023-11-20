import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CompetencyEvaluatorComponent } from './competency-evaluator.component';

describe('CompetencyEvaluatorComponent', () => {
  let component: CompetencyEvaluatorComponent;
  let fixture: ComponentFixture<CompetencyEvaluatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetencyEvaluatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetencyEvaluatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
