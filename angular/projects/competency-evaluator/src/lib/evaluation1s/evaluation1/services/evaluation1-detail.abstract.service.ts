import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { Evaluation1WithNavigationPropertiesDto } from '../../../proxy/evaluation1s/models';
import { Evaluation1Service } from '../../../proxy/evaluation1s/evaluation1.service';
import { Evaluation1Dto } from '../../../proxy/evaluation1s/models';

export abstract class AbstractEvaluation1DetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(Evaluation1Service);
  public readonly list = inject(ListService);

  public readonly getAthleteLookup = this.proxyService.getAthleteLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const {
      criterio_1_R1,
      criterio_1_R2,
      criterio_2_R1,
      criterio_2_R2,
      criterio_3_R1,
      criterio_3_R2,
      criterio_4_R1,
      criterio_4_R2,
      resultado_R1,
      resultado_R2,
      athleteId,
    } = this.selected?.evaluation1 || {};

    this.form = this.fb.group({
      criterio_1_R1: [criterio_1_R1 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_1_R2: [criterio_1_R2 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_2_R1: [criterio_2_R1 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_2_R2: [criterio_2_R2 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_3_R1: [criterio_3_R1 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_3_R2: [criterio_3_R2 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_4_R1: [criterio_4_R1 ?? '0', [Validators.min(0), Validators.max(100)]],
      criterio_4_R2: [criterio_4_R2 ?? '0', [Validators.min(0), Validators.max(100)]],
      resultado_R1: [resultado_R1 ?? '0', []],
      resultado_R2: [resultado_R2 ?? '0', []],
      athleteId: [athleteId ?? null, [Validators.required]],
    });

    // Deshabilitar campos
    this.form.controls['resultado_R1'].disable();
    this.form.controls['resultado_R2'].disable();
    if (this.selected) {
      this.form.controls['athleteId'].disable();
    }

  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: Evaluation1WithNavigationPropertiesDto) {
    this.selected = record;
    this.showForm();
  }

  hideForm() {
    this.isVisible = false;
    this.form.reset();
  }

  submitForm() {
    if (this.form.invalid) return;
    
    this.isBusy = true;

    const request = this.createRequest().pipe(
      finalize(() => (this.isBusy = false)),
      tap(() => this.hideForm())
    );

    request.subscribe(this.list.get);
  }

  private createRequest() {
    if (this.selected) {
      return this.proxyService.update(this.selected.evaluation1.id, {
        ...this.form.getRawValue(),
        concurrencyStamp: this.selected.evaluation1.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.getRawValue());
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }

  inputFields(value: number, formControlName: string) {
    // Resetear valor en 0 o colocar el valor ingresado por el usuario
    value = value <= 0 ? 0 : value;
    this.form.controls[formControlName].reset(value);
    // Cálculo del resultado R1 y R2
    const form: Evaluation1Dto = this.form.getRawValue();
    const { criterio_1_R1, criterio_2_R1, criterio_3_R1, criterio_4_R1, criterio_1_R2, criterio_2_R2, criterio_3_R2, criterio_4_R2 } = form;
    const result_1 = (Number(criterio_1_R1) + Number(criterio_2_R1) + Number(criterio_3_R1) + Number(criterio_4_R1)) / 4;
    const result_2 = (Number(criterio_1_R2) + Number(criterio_2_R2) + Number(criterio_3_R2) + Number(criterio_4_R2)) / 4;
    // Establecer resultados R1 y R2
    this.form.controls['resultado_R1'].reset(result_1);
    this.form.controls['resultado_R2'].reset(result_2);
  }
}
