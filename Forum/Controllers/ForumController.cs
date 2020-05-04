using System.Web.Mvc;
using Forum.Core;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}