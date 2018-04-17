using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BirthdayCard.Models
{
    public class CardContents
    {
        [Required(ErrorMessage="Please enter From")]
        public string Sender { get; set; }
        [Required(ErrorMessage ="Please enter To")]
        public string Recipient { get; set; }
        [Required(ErrorMessage ="Please enter Message")]
        public string Message { get; set; }
    }
}