using Comments.DB.Models;
using Comments.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comments.Implementation
{
    public class CommentsRepo : ICommentsRepo
    {
        protected CommentsContext db;
        protected ICommentLogger logger;

        public CommentsRepo(CommentsContext db, ICommentLogger logger)
        {
            this.db = db;
            this.logger = logger;
        }        

        public async Task<IEnumerable<IComment>> CommentsAsync()
        {
            try
            {
                Comment aaa = null;
                var b = aaa.Text;
                return await db.Comments.ToArrayAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex);                
                throw ex;
            }
        }

        public async Task<IEnumerable<IComment>> AddComment(IComment comment)
        {
            try
            {
                await db.Comments.AddAsync((Comment)comment);
                await db.SaveChangesAsync();
                return await CommentsAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw ex;
            }
        }
    }
}
