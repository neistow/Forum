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
            return View("PostList",posts.ToPagedList(page, 5));
        }

        [Route("Forum/Posts/{id=1}")]
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
            var post = new Post();
            return View("PostForm", post);
        }
        
        [Route("Forum/Posts/edit/{id}")]
        public ActionResult Edit(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);
            
            if (post == null || post.AuthorId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            return View("PostForm", post);
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
                postInDb.Title = post.Title;
                postInDb.Text = post.Text;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Posts", "Forum");
        }
    }
}