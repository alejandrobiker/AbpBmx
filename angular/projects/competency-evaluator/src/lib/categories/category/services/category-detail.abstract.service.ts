import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { CategoryDto } from '../../../proxy/categories/models';
import { CategoryService } from '../../../proxy/categories/category.service';

export abstract class AbstractCategoryDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(CategoryService);
  public readonly list = inject(ListService);

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { name, maxAge } = this.selected || {};

    this.form = this.fb.group({
      name: [
        name ?? null,
        [Validators.required, Validators.minLength(3), Validators.maxLength(64)],
      ],
      maxAge: [maxAge ?? null, [Validators.min(0), Validators.max(99)]],
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

  update(record: CategoryDto) {
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
      return this.proxyService.update(this.selected.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
