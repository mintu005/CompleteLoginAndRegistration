using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompleteLoginAndRegistration.Models.Extended
{
    [MetadataType(typeof(RegistrationMetadata))]
    public partial class Registration
    {
        public string Re_Password { get; set; }
    }
    public class RegistrationMetadata
        {[Display(Name = "First Name:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

        [Display(Name = "E-Mail:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        [DataType(DataType.EmailAddress)]
        public string Email_ID { get; set; }

        [Display(Name = "Date of Birth:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Enter Password:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Minimum 6 charecter is required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Re_Password { get; set; }

        [Display(Name ="Profile Image:")]
        [Required(ErrorMessage ="Upload Profile Image")]
        public string ImageFile { get; set; }

        [Display(Name ="Gender:")]
        [Required(ErrorMessage ="Select the Gender")]
         public string Gender { get; set; }


    }
}