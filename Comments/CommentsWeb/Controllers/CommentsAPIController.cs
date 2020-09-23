using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comments.DB.Models;
using Comments.Interfaces;
using CommentsWeb.Model;
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
        protected ICommentLogger logger;

        public CommentsAPIController(ICommentsRepo commentsRepo, ICommentLogger logger)
        {
            this.commentsRepo = commentsRepo;
            this.logger = logger;
        }

        [HttpGet]
        [Route("comments")]
        public async Task<Response<IEnumerable<IComment>>> Comments()
        {
            Response<IEnumerable<IComment>> response = new Response<IEnumerable<IComment>>();
            try
            {
                var model = await commentsRepo.CommentsAsync();
                response.Success = true;
                response.Model = model;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMsg = "Nastala neočakávaná chyba. Pre bližšie info pozrite prosím logy";
                logger.LogError(ex);
                response.Model = Enumerable.Empty<IComment>();
            }
            return response;
        }

        [HttpPost]
        [Route("addComments")]
        public async Task<Response<IEnumerable<IComment>>> AddComment([FromBody] JObject data)
        {            
            Response<IEnumerable<IComment>> response = new Response<IEnumerable<IComment>>();
            try
            {
                IComment comment = data.ToObject<Comment>(new Newtonsoft.Json.JsonSerializer { DateFormatString = "d.M.yyyy" });
                var model = await commentsRepo.AddComment(comment);
                response.Success = true;
                response.Model = model;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMsg = "Nastala neočakávaná chyba. Pre bližšie info pozrite prosím logy";
                logger.LogError(ex);
                response.Model = Enumerable.Empty<IComment>();
            }
            return response;
        }
    }
}
