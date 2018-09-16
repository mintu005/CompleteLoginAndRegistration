using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompleteLoginAndRegistration.Models;
using System.Net.Mail;

namespace CompleteLoginAndRegistration.Controllers
{
    public class RegistrationsController : Controller
    {
        // Registrations GET Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        // Registrations POST Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrationForm([Bind(Exclude = "isEmailVarified,ActivationCode")]Registration reg)
        {
            bool Status = false;
            string message = "";
            //
            //Model Validation
            if(ModelState.IsValid)
            {
                #region //Email is already exist
                var isExist = IsEmailExist(reg.Email_ID);
                if(isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email is already exist");
                    return View(reg);
                }
                #endregion

                #region Generate Activation Code
                reg.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hasing
                reg.Password = Crypto.Hash(reg.Password);
                reg.ConfirmPassword = Crypto.Hash(reg.ConfirmPassword); //
                reg.isEmailVarified = false;
                #endregion

                #region Save to Database
                using (LoginRegEntities1 dc = new LoginRegEntities1())
                {
                    dc.Registrations.Add(reg);
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(reg.Email_ID, reg.ActivationCode.ToString());
                }

                #endregion
            }
            else
            {
                message = "Invalid Requiest";
            }
            
            
          
          
            return View(reg);
        }
        //Verify Email

        //Verify Email Link

        //Login

        //Login Post
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (LoginRegEntities1 dc= new LoginRegEntities1())
            {
                var v = dc.Registrations.Where(a => a.Email_ID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Registration/VerifyAccount" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
            var toMail = new MailAddress(emailID);
            var fromEmailPassword = "**********"; //Replace with Actual Password
            var subject = "Your Account is successfully created!";
        }

    }
}