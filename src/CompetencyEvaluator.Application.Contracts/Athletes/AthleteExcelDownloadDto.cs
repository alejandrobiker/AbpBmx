using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public DateTime? DateOfBirthMin { get; set; }
        public DateTime? DateOfBirthMax { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? CategoryId { get; set; }

        public AthleteExcelDownloadDtoBase()
        {

        }
    }
}