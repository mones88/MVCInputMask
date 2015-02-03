using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInputMask.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NgFieldAttribute : Attribute
    {
        public Object DefaultValue { get; set; }
    }
}