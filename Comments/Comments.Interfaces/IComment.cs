using System;
using System.Collections.Generic;
using System.Text;

namespace Comments.Interfaces
{
    public interface IComment
    {
        int Id { get; set; }
        string Text { get; set; }
        DateTime Date { get; set; }
    }
}
