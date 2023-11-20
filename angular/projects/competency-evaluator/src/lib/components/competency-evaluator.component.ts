import { Component, OnInit } from '@angular/core';
import { CompetencyEvaluatorService } from '../services/competency-evaluator.service';

@Component({
  selector: 'lib-competency-evaluator',
  template: ` <p>competency-evaluator works!</p> `,
  styles: [],
})
export class CompetencyEvaluatorComponent implements OnInit {
  constructor(private service: CompetencyEvaluatorService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
