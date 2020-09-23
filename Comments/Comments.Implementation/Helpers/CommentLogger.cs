using Comments.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comments.Implementation.Helpers
{
    public class CommentLogger: ICommentLogger
    {
        private ILogger logger;
        public ILogger Logger { get => logger; }

        public CommentLogger(IServiceProvider serviceProvider)
        {
            var loggerFactory = (ILoggerFactory)serviceProvider.GetService(typeof(ILoggerFactory));
            logger = loggerFactory.CreateLogger("CommentsWebApp");
        }

        public void LogError(Exception ex)
        {
            logger.LogError(ex.Message);
            if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
            {
                logger.LogError(ex.InnerException.Message);
            }
            logger.LogError(ex.StackTrace);
            string err = JsonConvert.SerializeObject(ex, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            logger.LogError(err);
        }

        public void LogError(string Message)
        {
            logger.LogError(Message);
        }

        public void LogInformation(string Message)
        {
            logger.LogError(Message);
        }

        public void LogWarning(string Message)
        {
            logger.LogError(Message);
        }

        public void LogDebug(string Message)
        {
            logger.LogError(Message);
        }
    }
}
