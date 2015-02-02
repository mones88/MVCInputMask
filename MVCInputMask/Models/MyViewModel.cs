using MVCInputMask.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCInputMask.Models
{
    public class MyViewModel
    {
        [Mask("99/99/9999")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Mask(@"(+3\\9) 999-9999999")]
        public String PhoneNumber { get; set; }
    }
}