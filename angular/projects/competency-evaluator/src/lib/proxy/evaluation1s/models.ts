import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { AthleteDto } from '../athletes/models';

export interface Evaluation1CreateDto {
  criterio_1_R1?: number;
  criterio_1_R2?: number;
  criterio_2_R1?: number;
  criterio_2_R2?: number;
  criterio_3_R1?: number;
  criterio_3_R2?: number;
  criterio_4_R1?: number;
  criterio_4_R2?: number;
  resultado_R1?: number;
  resultado_R2?: number;
  athleteId: string;
}

export interface Evaluation1Dto extends FullAuditedEntityDto<string> {
  criterio_1_R1?: number;
  criterio_1_R2?: number;
  criterio_2_R1?: number;
  criterio_2_R2?: number;
  criterio_3_R1?: number;
  criterio_3_R2?: number;
  criterio_4_R1?: number;
  criterio_4_R2?: number;
  resultado_R1?: number;
  resultado_R2?: number;
  athleteId: string;
  concurrencyStamp?: string;
}

export interface Evaluation1ExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface Evaluation1UpdateDto {
  criterio_1_R1?: number;
  criterio_1_R2?: number;
  criterio_2_R1?: number;
  criterio_2_R2?: number;
  criterio_3_R1?: number;
  criterio_3_R2?: number;
  criterio_4_R1?: number;
  criterio_4_R2?: number;
  resultado_R1?: number;
  resultado_R2?: number;
  athleteId: string;
  concurrencyStamp?: string;
}

export interface Evaluation1WithNavigationPropertiesDto {
  evaluation1: Evaluation1Dto;
  athlete: AthleteDto;
}

export interface GetEvaluation1sInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  criterio_1_R1Min?: number;
  criterio_1_R1Max?: number;
  criterio_1_R2Min?: number;
  criterio_1_R2Max?: number;
  criterio_2_R1Min?: number;
  criterio_2_R1Max?: number;
  criterio_2_R2Min?: number;
  criterio_2_R2Max?: number;
  criterio_3_R1Min?: number;
  criterio_3_R1Max?: number;
  criterio_3_R2Min?: number;
  criterio_3_R2Max?: number;
  criterio_4_R1Min?: number;
  criterio_4_R1Max?: number;
  criterio_4_R2Min?: number;
  criterio_4_R2Max?: number;
  resultado_R1Min?: number;
  resultado_R1Max?: number;
  resultado_R2Min?: number;
  resultado_R2Max?: number;
  athleteId?: string;
}
