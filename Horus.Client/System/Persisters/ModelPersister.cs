﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Interfaces;

namespace Horus.Client.System.Persisters
{
    internal interface IModelPersister
    {
        string ToHttpResponse(object instance);
        object FromHttpResponse(string response);
    }

    internal class ModelPersister
    {
        private Dictionary<string, IModelPersister> registeredPersisters = new Dictionary<string, IModelPersister>();
        public static ModelPersister Instance = new ModelPersister();

        private ModelPersister()
        {
            RegisterPersister<IVideoFrame, VideoFramePersister>();
        }

        private void RegisterPersister<TModel, TPersister>() 
            where TModel : class 
            where TPersister : class, IModelPersister, new()
        {
            registeredPersisters.Add(typeof (TModel).FullName, new TPersister());
        }

        public IModelPersister GetCustomPersister(Type modelType)
        {
            IModelPersister rv;
            if (registeredPersisters.TryGetValue(modelType.FullName, out rv))
                return rv;

            foreach(string typeName in registeredPersisters.Keys)
            {
                if (modelType.GetInterface(typeName) != null)
                    return registeredPersisters[typeName];
            }

            return null;
        }
    }
}
