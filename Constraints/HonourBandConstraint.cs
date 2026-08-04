using Microsoft.AspNetCore.Routing;
 
namespace StudentPortalWeb.Constraints
{
    public class HonorBandConstraint : IRouteConstraint{
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)

            {
                if(!values.TryGetValue(routeKey, out object? value))
                {
                    return false;
                }

                string?band = value?.ToString();

                if(string.IsNullOrEmpty(band))
                {
                    return false;
                }

                return band.Equals("honor", StringComparison.OrdinalIgnoreCase);

                return band switch
                {
                    "A" => true,
                    "B" => true,
                    "C" => true,
                    _ => false
                };


            }
    }
}
     