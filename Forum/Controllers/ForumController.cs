using System.Web.Mvc;
using Forum.Core;
using PagedList;    

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("Forum/Posts/page{page=1}")]
        public ActionResult Posts(int page)
        {
            var posts = _unitOfWork.Posts.GetAll();
            return View(posts.ToPagedList(page,5));
        }
        
        [Route("Forum/Post/id{id=1}")]
        public ActionResult Post(int id)
        {
            var post = _unitOfWork.Posts.GetPostWithReplies(id);
            return View(post);
        }
    }
}