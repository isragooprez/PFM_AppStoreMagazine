using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Magazine.Models;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Magazine.Content.Utils;

namespace Magazine.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterViewModel _registerViewModel;


                //var myContent = JsonConvert.SerializeObject(model);
                //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                //var byteContent = new ByteArrayContent(buffer);
                //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //segunda forma 
                var json = JsonConvert.SerializeObject(model);

                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage response = await GlobalVarApi.WebApiClient.PostAsync("/api/account/register", stringContent);
                _registerViewModel = response.Content.ReadAsAsync<RegisterViewModel>().Result;
                var result = await response.Content.ReadAsStringAsync();
                if (result != string.Empty)
                {
                    var resulDecode = JsonConvert.DeserializeObject<dynamic>(result);
                 
                    string ggggme = resulDecode.ModelState[""][0];
                    String[] strArrayError = new String[] { resulDecode.Message, resulDecode.ModelState[""][0] };
                    IdentityResult sdf = new IdentityResult(strArrayError);
                    if (sdf.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                //AddErrors(sdf);
                 
                    
                    //IdentityResult.Failed(new IdentityError
                    //{
                    //    Code = "UsernameAsPassword",
                    //    Description = "You cannot use your username as your password"
                    //});



                }
                return RedirectToAction("Index", "Home");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
    }
}