using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_blogs_architecture.blogs.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
