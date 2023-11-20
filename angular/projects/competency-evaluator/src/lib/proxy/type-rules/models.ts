import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetTypeRulesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}

export interface TypeRuleCreateDto {
  name: string;
}

export interface TypeRuleDto extends FullAuditedEntityDto<string> {
  name: string;
  concurrencyStamp?: string;
}

export interface TypeRuleExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface TypeRuleUpdateDto {
  name: string;
  concurrencyStamp?: string;
}
