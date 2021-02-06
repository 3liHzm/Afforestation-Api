using Afforestation.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Afforestation.App.Posts
{
    [Service]
    public class GetImage
    {
        private readonly IPostManager _postManager;

        public GetImage(IPostManager postManager)
        {
            _postManager = postManager;
        }
        //public async Task<FileResult> Do(string fileName)
        //{
        //    byte[] fileContent = await _postManager.GetFile(fileName);
        //    return File(fileContent, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}
    }
}
