using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Extensions;

public static class UrlHelperExtensions
{
    public static string MakeActiveClass(
        this IUrlHelper urlHelper,
        string controller,
        string action)
    {
        try
        {
            var result = "active";
            var controllerName = urlHelper.ActionContext.RouteData.Values["controller"]!.ToString();
            var methodName = urlHelper.ActionContext.RouteData.Values["action"]!.ToString();

            if (string.IsNullOrEmpty(controllerName))
                return null;

            var actions = action.Split(',')?
                .ToList()
                .Select(x => x.ToUpper());

            if (!action.Any())
                return null;

            if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                if (actions.Contains(methodName!.ToUpper()))
                    return result;

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}