using System;
using System.Linq;
using System.Web.Mvc;
using Forum.Core;
using Forum.Core.Domain;
using Forum.Models;
using Microsoft.AspNet.Identity;
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
            var posts = _unitOfWork.Posts.GetAll().OrderByDescending(p => p.DateCreated);
            return View(posts.ToPagedList(page, 5));
        }

        [Route("Forum/Posts/id{id=1}")]
        public ActionResult Post(int id)
        {
            var viewModel = new PostViewModel
            {
                Post = _unitOfWork.Posts.GetPostWithAuthor(id),
                Replies = _unitOfWork.Replies.GetAllRepliesToPost(id)
            };
            return View(viewModel);
        }

        [Route("Forum/Posts/New")]
        public ActionResult New()
        {
            var viewModel = new PostFormViewModel
            {
                Post = new Post()
            };
            return View("PostForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Post post)
        {
            if (post.Id == 0)
            {
                post.DateCreated = DateTime.Now;
                post.AuthorId = User.Identity.GetUserId();
                _unitOfWork.Posts.Add(post);
            }
            else
            {
                var postInDb = _unitOfWork.Posts.SingleOrDefault(p => p.Id == post.Id);

                // TEMPORARILY
                postInDb.Author = post.Author;
                postInDb.Title = post.Title;
                postInDb.Text = post.Text;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Posts","Forum");
        }
    }
}