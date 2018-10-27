using SIS.Framework.ActionsResults.Base.Contracts;

namespace SIS.Framework.ActionsResults.Contracts
{
    public interface IRedirectable : IActionResult
    {
        string RedirectUrl { get; }
    }
}
