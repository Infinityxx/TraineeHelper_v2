﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;

using System.Web.Mvc;

namespace TraineeHelper.Web.CustomModelBinders
{
    public class BsonObjectIdBinder : IModelBinder
    {
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext modelBindingContext)
        {
            //// Retrieve a value object using modelBindingContext.ModelName as a key
            var valueProviderResult = modelBindingContext.ValueProvider.GetValue(modelBindingContext.ModelName);
            //// Now, create and return a new instance of MongoDB.Bson.ObjectId with the raw string retrieved from the model's property
            return new ObjectId(valueProviderResult.AttemptedValue);
        }
    }
}