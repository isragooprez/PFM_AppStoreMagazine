using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Models.ModelBinder
{
    public class NsoupModelBinder : IModelBinder
    {
        //Variable de session
        private string token_session_nsoup = "token_session_nsoup";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NsoupVirtualModels _nsoupVirtualModels = (NsoupVirtualModels)controllerContext.HttpContext.Session[token_session_nsoup];
            if (_nsoupVirtualModels == null)
            {
                _nsoupVirtualModels = new NsoupVirtualModels();
                controllerContext.HttpContext.Session[token_session_nsoup] = _nsoupVirtualModels;
            }
            return _nsoupVirtualModels;
        }
    }
}