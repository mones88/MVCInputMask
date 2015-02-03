using MVCInputMask.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCInputMask.NgHelper
{
    public static class NgHelperExtension
    {
        private static NgHelper _helper = new NgHelper();

        public static NgHelper Ng(this WebViewPage wvp)
        {
            return _helper;
        }
    }

    public class NgHelper
    {
        public MvcHtmlString BuildNgModels()
        {
            const String ModelTemplate = @"app.service('{0}', function () {{
                {1}
            }});";
            const String FieldTemplate = "this.{0} = {1};";

            var modelsToBuild = GetTypesWith<NgModelAttribute>(true);
            var sb = new StringBuilder(modelsToBuild.Count());
            foreach (var model in modelsToBuild)
            {
                var fields = String.Join("\n", model.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .Select(pi => String.Format(FieldTemplate, pi.Name,
                    pi.IsDefined(typeof(NgFieldAttribute), false) ? "'" + ((NgFieldAttribute)pi.GetCustomAttributes(typeof(NgFieldAttribute), false)[0]).DefaultValue.ToString() + "'" : "null")));

                var ngModel = String.Format(ModelTemplate, model.Name, fields);
                sb.AppendLine(ngModel);
            }
            return new MvcHtmlString(sb.ToString());
        }

        public MvcHtmlString BuildNgControllers()
        {
            const String ControllerTemplate =
            @"app.controller('{0}', ['$scope', {1}, function ($scope, {2}) {{
                {3}    
            }}]);";
            const String FieldTemplate = "$scope.{0} = {0};";
            const String InheritanceTemplate =
            @"app.controller('{0}', ['$scope', '$controller', function($scope, $controller) {{
                $controller('{1}', {{$scope: $scope}});

                // Your code here!
            }}]);";

            var controllersToBuild = GetTypesWith<NgControllerAttribute>(true);
            var sb = new StringBuilder(controllersToBuild.Count());
            foreach (var controller in controllersToBuild)
            {
                var ngControllerAttr = (NgControllerAttribute)controller.GetCustomAttributes(typeof(NgControllerAttribute), true)[0];
                var controllerName = ngControllerAttr.Name ?? controller.Name;
                String baseControllerName;
                if (ngControllerAttr.InheritanceFriendly)
                {
                    baseControllerName = "_" + controllerName;
                } else
                {
                    baseControllerName = controllerName;
                }
                var modelsNames = ngControllerAttr.ModelsTypes.Select(t => t.Name);
                var injectionParams = String.Join(", ", modelsNames.Select(mn => "'" + mn + "'"));
                var functionParams = String.Join(", ", modelsNames);
                var fields = String.Join("\n", modelsNames.Select(mn => String.Format(FieldTemplate, mn)));

                sb.AppendLine(String.Format(ControllerTemplate,
                    baseControllerName,
                    injectionParams,
                    functionParams,
                    fields));

                if (ngControllerAttr.InheritanceFriendly)
                {
                    sb.AppendLine(String.Format(InheritanceTemplate,
                        controllerName,
                        baseControllerName));
                }
            }
            return new MvcHtmlString(sb.ToString());
        }

        IEnumerable<Type> GetTypesWith<TAttribute>(bool inherit) where TAttribute : Attribute
        {
            return from a in AppDomain.CurrentDomain.GetAssemblies()
                   from t in a.GetTypes()
                   where t.IsDefined(typeof(TAttribute), inherit)
                   select t;
        }
    }

    public class NgHelper<TModel>
    {

    }
}