using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInputMask.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NgControllerAttribute : Attribute
    {
        public String Name { get; set; }

        public bool InheritanceFriendly { get; set; }

        public IReadOnlyCollection<Type> ModelsTypes { get; private set; }

        public NgControllerAttribute(params Type[] modelsTypes)
        {
            this.ModelsTypes = new List<Type>(modelsTypes).AsReadOnly();
        }
    }
}