using Comments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.DB.Models
{
    public class Comment : IComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
