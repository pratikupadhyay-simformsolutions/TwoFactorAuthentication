using Google.Authenticator;
using TwoFactor_Auth.Models;
using System;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace TwoFactor_Auth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Username"] == null || Session["IsValidTwoFactorAuthentication"] == null || !(bool)Session["IsValidTwoFactorAuthentication"])
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult About()
        {
            if (Session["Username"] == null || Session["IsValidTwoFactorAuthentication"] == null || !(bool)Session["IsValidTwoFactorAuthentication"])
            {
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Username"] == null || Session["IsValidTwoFactorAuthentication"] == null || !(bool)Session["IsValidTwoFactorAuthentication"])
            {
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Login()
        {
            Session["UserName"] = "";
            Session["IsValidTwoFactorAuthentication"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            bool status = false;
            if (Session["Username"] == null || Session["IsValidTwoFactorAuthentication"] == null || !(bool)Session["IsValidTwoFactorAuthentication"])
            {
                string googleAuthKey = WebConfigurationManager.AppSettings["GoogleAuthKey"];
                string UserUniqueKey = (login.UserName + googleAuthKey);

                Session["UserName"] = login.UserName;

                //Two Factor Authentication Setup
                TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                var setupInfo = TwoFacAuth.GenerateSetupCode("www.twofactorauthentication.com", login.UserName, ConvertSecretToBytes(UserUniqueKey, false), 300);
                Session["UserUniqueKey"] = UserUniqueKey;
                ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
                status = true;
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.Status = status;
            return View();
        }


        private static byte[] ConvertSecretToBytes(string secret, bool secretIsBase32) =>
           secretIsBase32 ? Base32Encoding.ToBytes(secret) : Encoding.UTF8.GetBytes(secret);


        public ActionResult TwoFactorAuthenticate()
        {
            var token = Request["CodeDigit"];
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            string UserUniqueKey = Session["UserUniqueKey"].ToString();
            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, token, false);
            if (isValid)
            {
                HttpCookie TwoFCookie = new HttpCookie("TwoFCookie");
                Session["IsValidTwoFactorAuthentication"] = true;
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Google Two Factor PIN is expired or wrong";
            return RedirectToAction("Login");
        }

        public ActionResult Logoff()
        {
            Session["UserName"] = null;
            Session["IsValidTwoFactorAuthentication"] = null;
            return RedirectToAction("Login");
        }
    }
}