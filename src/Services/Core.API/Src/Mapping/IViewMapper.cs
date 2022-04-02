using Core.API.View;
using Environment.Queries;
using Environment.State;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Mapping
{
    public interface IViewMapper
    {
        ActionResult<AffectionViewModel> ToView(StateResult result);

        ActionResult<PageViewModel<TView>> ToView<TModel, TView>(SelectResult<TModel> result);

        ActionResult<TView> ToView<TModel, TView>(FindResult<TModel> result);
    }
}
