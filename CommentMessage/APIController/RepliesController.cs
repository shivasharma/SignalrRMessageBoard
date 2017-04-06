using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommentMessage.Data;
using CommentMessage.Hub;

namespace CommentMessage.APIController
{
    public class RepliesController :ApiControllerWithHub<ToDoHub>
    {
        private IMessageBoardRepository _repo;
        public RepliesController(IMessageBoardRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Reply> Get(int topicId)
        {
            return _repo.GetRepliesByTopic(topicId).OrderByDescending(t => t.Created).ToList(); ;
        }

        public HttpResponseMessage Post(int topicid, [FromBody] Reply newReply)
        {
            if (newReply.Created == default(DateTime))
            {
                newReply.Created = DateTime.UtcNow;
            }
            newReply.TopicId = topicid;

            if (_repo.AddReply(newReply) && _repo.Save())
            {
              // IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<ToDoHub>();
                //Notify the connected clients
                Hub.Clients.All.addItem(newReply);
               // hub.Clients.All.addItem(newReply);
                return Request.CreateResponse(HttpStatusCode.Created, newReply);

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }


        public HttpResponseMessage Delete(Reply reply)
        {

            if (_repo.DeleteReply(reply.Id, reply.TopicId) && _repo.Save())

            {
                // Notify the connected clients
                 Hub.Clients.All.deleteItem(reply);
                return Request.CreateResponse(HttpStatusCode.OK, reply);

            }
            return Request.CreateResponse(HttpStatusCode.OK, reply);
        }


        public HttpResponseMessage Edit(Reply reply)
        {

            if (_repo.EditReply(reply) && _repo.Save())

            {
                // Notify the connected clients
                Hub.Clients.All.editItem(reply);
                return Request.CreateResponse(HttpStatusCode.OK, reply);

            }
            return Request.CreateResponse(HttpStatusCode.OK, reply);
        }

    }
}
