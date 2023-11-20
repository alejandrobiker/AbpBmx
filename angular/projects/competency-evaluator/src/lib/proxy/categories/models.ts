import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CategoryCreateDto {
  name: string;
  maxAge?: number;
}

export interface CategoryDto extends FullAuditedEntityDto<string> {
  name: string;
  maxAge?: number;
  concurrencyStamp?: string;
}

export interface CategoryExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CategoryUpdateDto {
  name: string;
  maxAge?: number;
  concurrencyStamp?: string;
}

export interface GetCategoriesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  maxAgeMin?: number;
  maxAgeMax?: number;
}
