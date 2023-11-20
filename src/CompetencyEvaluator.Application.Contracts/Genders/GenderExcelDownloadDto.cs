using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? name { get; set; }
        public string? ShortName { get; set; }

        public GenderExcelDownloadDtoBase()
        {

        }
    }
}