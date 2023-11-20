import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GenderCreateDto {
  name: string;
  shortName?: string;
}

export interface GenderDto extends FullAuditedEntityDto<string> {
  name: string;
  shortName?: string;
  concurrencyStamp?: string;
}

export interface GenderExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface GenderUpdateDto {
  name: string;
  shortName?: string;
  concurrencyStamp?: string;
}

export interface GetGendersInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  shortName?: string;
}
