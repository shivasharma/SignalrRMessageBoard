using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CommentMessage.Data
{
    public class MessageBoardMigrationsConfiguration:DbMigrationsConfiguration<MessageBoardContext>
    {
        public MessageBoardMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MessageBoardContext context)
        {
            base.Seed(context);
//#if DEBUG
            if (context.Topics.Count() == 0)
            {
                var topic = new Topic()
                {
                    Title = "I love MVC",
                    Created = DateTime.Now,
                    Body = "This is test body",
                    Replies = new List<Reply>()
                    {
                        new Reply()
                        {
                            Body="This is reply body",
                            Created = DateTime.Now
                        },
                        new Reply()
                        {
                            Body = "This is reply body2",
                            Created = DateTime.Now
                        },
                        new Reply()
                        {
                            Body = "this is body3",
                            Created = DateTime.Now
                        }
                    },

                };
                context.Topics.Add(topic);

                var anotherTopic = new Topic()
                {
                    Title = "THis is another topic",
                    Created = DateTime.Now,
                    Body = "Ruby on rail"
                };
                context.Topics.Add(anotherTopic);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                }

            }
//#endif
        }
    }
}