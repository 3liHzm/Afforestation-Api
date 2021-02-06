using Afforestation.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Afforestation.Domain.Infrastructure
{
    public interface IPostManager
    {
        Task<int> CreatPost(Posts post);
        Task<int> UploadGalleryFiles(int id, IEnumerable<IFormFile> imgFiles);
       // Task<int> UploadGalleryFiles(int id, IEnumerable<string> imgFiles);

        IEnumerable<TResult> GetPosts<TResult>(Func<Posts, TResult> selector);
        TResult GetPostById<TResult>(int id, Func<Posts, TResult> selector);
        Task<int> DeletePost(int id);
        Task DeleteGalleryImgs(int id);

        //File Manger

        //Task<byte[]> GetFile(string File);
        //Task WriteFile(string File, byte[] Content);

    }
}
                                                 