using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AniBaseFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private Uri RediredtUri
        {

            get
            {

                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;

            }

        }




        [AllowAnonymous]

        public ActionResult Facebook()
        {

            var fb = new FacebookClient();

            var loginUrl = fb.GetLoginUrl(new

            {




                client_id = "255057255208613",

                client_secret = "6e73ae96a58b7575c0021d8bc1084568",

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"



            });

            return Redirect(loginUrl.AbsoluteUri);

        }




        public ActionResult FacebookCallback(string code)
        {

            var fb = new FacebookClient();

            dynamic result = fb.Post("oauth/access_token", new

            {

                client_id = "255057255208613",

                client_secret = "6e73ae96a58b7575c0021d8bc1084568",

                redirect_uri = RediredtUri.AbsoluteUri,

                code = code




            });

            var accessToken = result.access_token;

            Session["AccessToken"] = accessToken;

            fb.AccessToken = accessToken;

            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");

            string email = me.email;

            TempData["email"] = me.email;

            TempData["first_name"] = me.first_name;

            TempData["lastname"] = me.last_name;

            TempData["picture"] = me.picture.data.url;

            FormsAuthentication.SetAuthCookie(email, false);

            return RedirectToAction("Index", "Home");

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}