import { NgModule } from '@angular/core';
import { CategoryComponent } from './components/category.component';
import { CategoryRoutingModule } from './category-routing.module';

@NgModule({
  declarations: [],
  imports: [CategoryComponent, CategoryRoutingModule],
})
export class CategoryModule {}

export function loadCategoryModuleAsChild() {
  return Promise.resolve(CategoryModule);
}
