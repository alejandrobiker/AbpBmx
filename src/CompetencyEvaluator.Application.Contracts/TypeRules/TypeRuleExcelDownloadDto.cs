using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class TypeRuleExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? name { get; set; }

        public TypeRuleExcelDownloadDtoBase()
        {

        }
    }
}