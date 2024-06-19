namespace Yomikaze.Domain.Models;

public class TranslationReportModel : ReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public TranslationModel Translation { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)] public string TranslationId { get; set; } = default!;

    #endregion
}