import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { AthleteWithNavigationPropertiesDto } from '../../../proxy/athletes/models';
import { AthleteService } from '../../../proxy/athletes/athlete.service';

export abstract class AbstractAthleteDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(AthleteService);
  public readonly list = inject(ListService);

  public readonly getGenderLookup = this.proxyService.getGenderLookup;

  public readonly getCategoryLookup = this.proxyService.getCategoryLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { name, dateOfBirth, genderId, categoryId } = this.selected?.athlete || {};

    this.form = this.fb.group({
      name: [
        name ?? null,
        [Validators.required, Validators.minLength(2), Validators.maxLength(64)],
      ],
      dateOfBirth: [dateOfBirth ? new Date(dateOfBirth) : null, []],
      genderId: [genderId ?? null, [Validators.required]],
      categoryId: [categoryId ?? null, [Validators.required]],
    });
  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: AthleteWithNavigationPropertiesDto) {
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
      return this.proxyService.update(this.selected.athlete.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.athlete.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
