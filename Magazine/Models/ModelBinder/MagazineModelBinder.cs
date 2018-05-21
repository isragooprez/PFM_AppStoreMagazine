using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Models.ModelBinder
{
    public class MagazineModelBinder : IModelBinder
    {
        //Variable de session
        private string token_session_magazine_diary = "token_session_magazine_diary";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            MagazinesVirtualModels _magazineDiaryModels = (MagazinesVirtualModels)controllerContext.HttpContext.Session[token_session_magazine_diary];
            if (_magazineDiaryModels == null)
            {
                _magazineDiaryModels = new MagazinesVirtualModels();
                controllerContext.HttpContext.Session[token_session_magazine_diary] = _magazineDiaryModels;
            }
            return _magazineDiaryModels;
        }
    }
}