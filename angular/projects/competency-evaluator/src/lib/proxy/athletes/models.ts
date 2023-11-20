import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { GenderDto } from '../genders/models';
import type { CategoryDto } from '../categories/models';

export interface AthleteCreateDto {
  name: string;
  dateOfBirth?: string;
  genderId: string;
  categoryId: string;
}

export interface AthleteDto extends FullAuditedEntityDto<string> {
  name: string;
  dateOfBirth?: string;
  genderId: string;
  categoryId: string;
  concurrencyStamp?: string;
}

export interface AthleteExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface AthleteUpdateDto {
  name: string;
  dateOfBirth?: string;
  genderId: string;
  categoryId: string;
  concurrencyStamp?: string;
}

export interface AthleteWithNavigationPropertiesDto {
  athlete: AthleteDto;
  gender: GenderDto;
  category: CategoryDto;
}

export interface GetAthletesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  dateOfBirthMin?: string;
  dateOfBirthMax?: string;
  genderId?: string;
  categoryId?: string;
}
