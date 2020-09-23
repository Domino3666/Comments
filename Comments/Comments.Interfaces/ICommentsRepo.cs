using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Interfaces
{
    public interface ICommentsRepo
    {
        Task<IEnumerable<IComment>> CommentsAsync();

        Task<bool> AddComment(IComment comment);
    }
}
