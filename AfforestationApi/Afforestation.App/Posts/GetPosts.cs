using Afforestation.Domain.Enums;
using Afforestation.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afforestation.App.Posts
{
    [Service]
    public class GetPosts
    {
        private readonly IPostManager _postManager;

        public GetPosts(IPostManager postManager)
        {
            _postManager = postManager;
        }

        public IEnumerable<Response> Do()
        {
            var Response = _postManager.GetPosts(s => new Response
            {
                Id = s.Id,
                TreeCount = s.TreeCount,
                Category = s.Category,
                Long = s.Long,
                Lat = s.Lat,
                TimeStamps = s.TimeStamps,
                UserId =s.UserId

            });
            


            return Response;

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
        }


    }

}

