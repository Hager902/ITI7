using Microsoft.AspNetCore.Routing;
 
namespace StudentPortalWeb.Constraints
{
    public class IntakeCodeConstraint : IRouteConstraint{
        private const string ValidIntakeCode = "itiA";   // Lab ID 15: IntakeCode = itiA

        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
            {
            if(!values.TryGetValue(routeKey, out object? value)){
                return false;
            }

            string? code = value?.ToString();
            if(string.IsNullOrEmpty(code)){
                return false;
            }

            return code.Equals(ValidIntakeCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}

// The constraint should be lightweight so Database queries would be slow and unnecessary and The valid intake code is configuration not database state