using Afforestation.Domain.Enums;
using Afforestation.Domain.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Afforestation.App.Posts
{
    [Service]
    public class CreatePost
    {
        private readonly IPostManager _postManager;

        public CreatePost(IPostManager postManager)
        {
            _postManager = postManager;
        }

        public async Task<Response> Do(Request request)
        {
            var post = new Domain.Models.Posts
            {
                
                UserId = request.UserId,
                TreeCount = request.TreeCount,
                Category = request.Category,
               
                Long = request.Long,
                Lat = request.Lat,
                TimeStamps = DateTime.Now,
            };
            await _postManager.CreatPost(post);

            if (request.Files != null)
            {
                //List<string> filesNames = new List<string>();
                
                //foreach (var file in request.Files)
                //{
                //    string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.fff") + file.FileName;
                //    filesNames.Add(fileName);
                //    using (var reader = new StreamReader(file.OpenReadStream()))
                //    {
                //        var bytes = default(byte[]);
                //        using (var memstream = new MemoryStream())
                //        {
                //            reader.BaseStream.CopyTo(memstream);
                //            bytes = memstream.ToArray();
                //        }
                //        await _postManager.WriteFile(fileName, bytes);
                //    }
                //}



               await _postManager.UploadGalleryFiles(post.Id, request.Files);
            }


            return new Response
            {
                Id = post.Id,
                UserId = post.UserId,
                TreeCount = post.TreeCount,
                Category = post.Category,
                Long = post.Long,
                Lat = post.Lat,
                TimeStamps = post.TimeStamps,

            };
 
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

       public class Request
        {
   
            public int UserId { get; set; }
            public int TreeCount { get; set; }
            public Category Category { get; set; }

            public double Long { get; set; }
            public double Lat { get; set; }

            public ICollection<IFormFile> Files { get; set; }
        }
    }
}
