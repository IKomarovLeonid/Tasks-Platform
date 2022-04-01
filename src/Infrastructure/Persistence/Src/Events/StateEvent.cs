﻿using System;
using Objects;

namespace Persistence.Src.Events
{
    public class StateEvent<TModel> where TModel: class
    {
        public TModel Data { get; private set; }

        public StateEventType Type { get; private set; }

        private StateEvent() { }

        public static StateEvent<TModel> Create(TModel aggregate) => new StateEvent<TModel>()
        {
            Data = aggregate,
            Type = StateEventType.Create
        };

        public static StateEvent<TModel> Update(TModel aggregate) => new StateEvent<TModel>()
        {
            Data = aggregate,
            Type = StateEventType.Update
        };

        public static StateEvent<TModel> Remove(TModel aggregate) => new StateEvent<TModel>()
        {
            Data = aggregate,
            Type = StateEventType.Delete
        };
    }

    public enum StateEventType
    {
        Create,
        Update,
        Delete
    }
}
