using SIS.Framework.ActionsResults.Base.Contracts;

namespace SIS.Framework.ActionsResults.Contracts
{
    public interface IViewable : IActionResult
    {
        IRenderable View { get; set; }
    }
}
