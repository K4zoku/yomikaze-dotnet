using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models.Search;

public class WithdrawalRequestSearchModel
{
    public WithdrawalRequestStatus? Status { get; set; }

    public WithdrawalRequestOrderBy OrderBy { get; set; } = WithdrawalRequestOrderBy.CreationTimeDesc;
}

public enum WithdrawalRequestOrderBy
{
    CreationTime,
    CreationTimeDesc
}