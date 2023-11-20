import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44309/',
  redirectUri: baseUrl,
  clientId: 'CompetencyEvaluator_App',
  responseType: 'code',
  scope: 'offline_access CompetencyEvaluator',
  requireHttps: true
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'CompetencyEvaluator',
    logoUrl: '',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44309',
      rootNamespace: 'CompetencyEvaluator',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
    CompetencyEvaluator: {
      url: 'https://localhost:44312',
      rootNamespace: 'CompetencyEvaluator',
    },
  },
} as Environment;
