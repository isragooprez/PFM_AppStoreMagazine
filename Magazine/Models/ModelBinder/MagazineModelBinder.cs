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
            MagazinesDiaryModels magazineDiaryModels = (MagazinesDiaryModels)controllerContext.HttpContext.Session[token_session_magazine_diary];
            if (magazineDiaryModels == null)
            {
                magazineDiaryModels = new MagazinesDiaryModels();
                controllerContext.HttpContext.Session[token_session_magazine_diary] = magazineDiaryModels;
            }
            return magazineDiaryModels;
        }
    }
}