using System.Web.Mvc;

namespace project03.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["name_admin"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { Controller = "LoginAdmin", Action = "Index" })
                    );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}