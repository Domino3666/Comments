using System;
using System.Collections.Generic;
using System.Text;

namespace Comments.Interfaces
{
    public interface ICommentLogger
    {
        void LogError(Exception ex);
        void LogError(string Message);
        void LogInformation(string Message);
        void LogDebug(string Message);
        void LogWarning(string Message);
    }
}
