using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CommentMessage.Hub;
using Microsoft.AspNet.SignalR;

namespace CommentMessage.Data
{
    public class MessageBoardRepository:IMessageBoardRepository
    {
        private MessageBoardContext _ctx;
        public MessageBoardRepository(MessageBoardContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Topic> GetTopics()
        {
            return _ctx.Topics;

        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<ToDoHub>();
            hub.Clients.All.getAllTopics();
          return _ctx.Topics.Include("Replies");
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _ctx.Replies.Where(r => r.TopicId == topicId);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception e)
            {//log error
                return false;
            }
        }


        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _ctx.Topics.Add(newTopic);
                return true;
            }
            catch (Exception e)
            {//log error
                return false;
            }
        }

        //public IQueryable<Topic> GetTopicsIncludingReplies()
        //{
        //    return _ctx.Topics.Include("Replies");
        //}
        public bool AddReply(Reply newReply)
        {
            try
            {
                _ctx.Replies.Add(newReply);
                return true;
            }
            catch (Exception e)
            {//log error
                return false;
            }
        }

        public bool DeleteReply(int replyId, int topicid)
        {
            try
            {
                var getallReplybyTopic = GetRepliesByTopic(topicid).FirstOrDefault(x => x.Id==replyId);
                if (getallReplybyTopic != null) _ctx.Replies.Remove(getallReplybyTopic);

                return true;
            }
            catch (Exception e)
            {//log error
                return false;
            }
        }

        public bool EditReply(Reply editReply)
        {
            try
            {
                var getTopicReply = GetRepliesByTopic(editReply.TopicId).FirstOrDefault(x => x.Id == editReply.Id);
                if (getTopicReply != null)
                {
                    getTopicReply.Body = editReply.Body;
                 }
                
                return true;
            }
            catch (Exception e)
            {//log error
                return false;
            }
        }
    }
}