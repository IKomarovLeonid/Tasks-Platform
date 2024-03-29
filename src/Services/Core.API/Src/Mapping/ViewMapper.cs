﻿using System.Collections.Generic;
using AutoMapper;
using Core.API.View;
using Environment;
using Environment.Queries;
using Environment.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Objects.Common;

namespace Core.API.Mapping
{
    public class ViewMapper : IViewMapper
    {
        //services
        private readonly IMapper _mapper;

        public ViewMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ActionResult<AffectionViewModel> ToView(StateResult result)
        {
            if (result.Code == ErrorCode.None)
            {
                return new OkObjectResult(AffectionViewModel.New(result.Id));
            }

            return ToErrorResult(result);
        }

        public ActionResult<PageViewModel<TView>> ToView<TModel, TView>(SelectResult<TModel> result)
        {
            if (result.Code != ErrorCode.None) return ToErrorResult(result);

            var items = _mapper.Map<ICollection<TView>>(result.Data);

            var view = PageViewModel<TView>.New(items);

            return new OkObjectResult(view);

        }

        public ActionResult<TView> ToView<TModel, TView>(FindResult<TModel> result)
        {
            if (result.Code != ErrorCode.None) return ToErrorResult(result);

            var view = _mapper.Map<TView>(result.Data);
            return new OkObjectResult(view);

        }

        private ActionResult ToErrorResult(IAbstractResult result)
        {
            var isNotFound = result.Code.ToString().Contains("NotFound");

            if (isNotFound)
            {
                return new NotFoundObjectResult(ErrorViewResponse.Build(result.Code, result.Message)) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new BadRequestObjectResult(ErrorViewResponse.Build(result.Code, result.Message));
        }
    }
}
