using Afforestation.Domain.Enums;
using Afforestation.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Afforestation.App.Posts
{
    [Service]
    public class GetPost
    {
        private readonly IPostManager _postManager;

        public GetPost(IPostManager postManager)
        {
            _postManager = postManager;
        }

        public Response Do(int id)
        {
            return _postManager.GetPostById(id, s => new Response
            {
                Id = s.Id,
                TreeCount = s.TreeCount,
                Category = s.Category,
                Long = s.Long,
                Lat = s.Lat,
                TimeStamps = s.TimeStamps,
                UserId = s.UserId,
                Attachments = s.Attachments.Select(x => new AttachmentsV
                {
                    Id = x.Id,
                    ImgUrl = x.ImgUrl
                })

            });

        }

        public class Response
        {
            public int Id { get; set; }
            public int TreeCount { get; set; }
            public Category Category { get; set; }

            public double Long { get; set; }
            public double Lat { get; set; }
            public DateTime TimeStamps { get; set; }
            public int UserId { get; set; }

            public IEnumerable<AttachmentsV> Attachments { get; set; }
        }

        public class AttachmentsV
        {
            public int Id { get; set; }
            public string ImgUrl { get; set; }
        }

    }
}
