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

        public CommentsRepo(CommentsContext db)
        {
            this.db = db;
        }        

        public async Task<IEnumerable<IComment>> CommentsAsync()
        {
            return await db.Comments.ToArrayAsync();
        }

        public async Task<bool> AddComment(IComment comment)
        {
            try
            {
                await db.Comments.AddAsync((Comment)comment);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
