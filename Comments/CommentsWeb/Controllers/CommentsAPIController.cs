using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comments.DB.Models;
using Comments.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CommentsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsAPIController : ControllerBase
    {
        protected ICommentsRepo commentsRepo;

        public CommentsAPIController(ICommentsRepo commentsRepo)
        {
            this.commentsRepo = commentsRepo;
        }

        [HttpGet]
        [Route("comments")]
        public async Task<IEnumerable<IComment>> Comments()
        {
            return await commentsRepo.CommentsAsync();
        }

        [HttpPost]
        [Route("addComments")]
        public async Task<bool> AddComment([FromBody] JObject model)
        {
            IComment comment = model.ToObject<Comment>(new Newtonsoft.Json.JsonSerializer { DateFormatString = "d.M.yyyy" });
            return await commentsRepo.AddComment(comment);
        }
    }
}
