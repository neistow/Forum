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
            var posts = _unitOfWork.Posts.GetPostsWithAuthors().OrderByDescending(p => p.DateCreated);
            return View("PostList", posts.ToPagedList(page, 5));
        }

        [Route("Forum/Posts/{id=1}")]
        public ActionResult Post(int id)
        {
            var post = _unitOfWork.Posts.GetPostWithAuthor(id);
            var replies = _unitOfWork.Replies.GetAllRepliesToPost(id);
            var viewModel = new PostViewModel
            {
                Post = post,
                Replies = replies,
            };
            return View(viewModel);
        }

        [Route("Forum/Posts/New")]
        public ActionResult NewPost()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }

            var post = new Post();
            return View("PostForm", post);
        }

        [Route("Forum/Posts/edit/{id}")]
        public ActionResult EditPost(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);

            if (post == null || post.AuthorId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            return View("PostForm", post);
        }

        public ActionResult SavePost(Post post)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }

            if (post.Id == 0)
            {
                post.DateCreated = DateTime.Now;
                post.AuthorId = User.Identity.GetUserId();
                _unitOfWork.Posts.Add(post);
            }
            else
            {
                var postInDb = _unitOfWork.Posts.SingleOrDefault(p => p.Id == post.Id);

                postInDb.Title = post.Title;
                postInDb.Text = post.Text;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Posts", "Forum");
        }

        [Route("Forum/Posts/Delete/{id}")]
        public ActionResult DeletePost(int id)
        {
            var post = _unitOfWork.Posts.GetPostWithAuthor(id);

            if (User.Identity.GetUserId() != post.Author.Id)
            {
                return HttpNotFound();
            }

            _unitOfWork.Posts.Remove(post);
            _unitOfWork.Complete();

            return RedirectToAction("Posts", "Forum");
        }

        [Route("Forum/Posts/{postId}/NewReply")]
        public ActionResult NewReply(int postId)
        {
            var post = _unitOfWork.Posts.GetPostWithAuthor(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            var reply = new Reply {Post = post}; // Fix this
            return View("ReplyForm", reply);
        }

        [Route("Forum/Posts/{postId}/Reply/{replyId}/Edit")]
        public ActionResult EditReply(int postId, int replyId)
        {
            var reply = _unitOfWork.Replies.GetById(replyId);

            if (reply == null || reply.AuthorId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            reply.Post = new Post { Id = postId};
            
            return View("ReplyForm", reply);
        }

        [Route("Forum/Posts/{postId}/Reply/Save")]
        public ActionResult SaveReply(Reply reply, int postId)
        {
            if (reply.Id == 0)
            {
                reply.DateCreated = DateTime.Now;
                reply.AuthorId = User.Identity.GetUserId();
                reply.Post = _unitOfWork.Posts.GetById(postId);
                _unitOfWork.Replies.Add(reply);
            }
            else
            {
                var replyInDb = _unitOfWork.Replies.SingleOrDefault(r => r.Id == reply.Id);

                if (replyInDb.AuthorId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                replyInDb.Text = reply.Text;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Post", "Forum", new {id = postId});
        }

        [Route("Forum/Posts/{postId}/DeleteReply/{replyId}")]
        public ActionResult DeleteReply(int postId, int replyId)
        {
            var reply = _unitOfWork.Replies.GetReplyWithAuthor(replyId);

            if (User.Identity.GetUserId() != reply.Author.Id)
            {
                return HttpNotFound();
            }

            _unitOfWork.Replies.Remove(reply);
            _unitOfWork.Complete();

            return RedirectToAction("Post", "Forum", new {id = postId});
        }
    }
}