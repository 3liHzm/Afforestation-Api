using Afforestation.Domain.Infrastructure;
using Afforestation.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Afforestation.Database.Managers
{
    public class PostManager : IPostManager
    {
        private readonly AppDbContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly IConfiguration _IConfiguration;

        public PostManager(AppDbContext ctx, IHostingEnvironment hosting, IConfiguration IConfiguration)
        {
            _ctx = ctx;
            _hosting = hosting;
            _IConfiguration = IConfiguration;
        }    
        public async Task<int> CreatPost(Posts post)
        {
            _ctx.Posts.Add(post);
            return await _ctx.SaveChangesAsync();
        }


        public async Task<int> UploadGalleryFiles(int id, IEnumerable<IFormFile> imgFiles)
        {
            if (imgFiles != null)
            {
                List<Attachments> imges = new List<Attachments>();
                foreach (var img in imgFiles)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"Images");
                    //string fileName = CreatImgRef() + img.FileName;
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.fff") + img.FileName;
                    string fullPath = Path.Combine(uploads, fileName);

                    using (var filestream = new FileStream(fullPath, FileMode.Create))
                    {
                        await img.CopyToAsync(filestream);

                    }

                    var imgGallery = new Attachments
                    {
                        ImgUrl = fileName,
                        PostId = id
                    };
                    imges.Add(imgGallery);

                }

                _ctx.Attachments.AddRange(imges);


                bool saveFailed; //to avoid DbUpdateConcurrencyException
                do
                {
                    saveFailed = false;
                    try
                    {
                        return await _ctx.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        var entry = ex.Entries.Single();

                        if (entry.State == EntityState.Deleted)
                            //When EF deletes an item its state is set to Detached           
                            entry.State = EntityState.Detached;
                        else
                            entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                    }
                } while (saveFailed);



            }
            return 0;
        }


        public TResult GetPostById<TResult>(int id, Func<Posts, TResult> selector)
        {
            return _ctx.Posts.Include(s=>s.Attachments)
                .Where(s => s.Id == id)
                .Select(selector).FirstOrDefault();
        }

        public IEnumerable<TResult> GetPosts<TResult>(Func<Posts, TResult> selector)
        {
            return _ctx.Posts
                .Select(selector).ToList();
        }

        public async Task<int> DeletePost(int id)
        {
            var post = _ctx.Posts.SingleOrDefault(s => s.Id == id);
            if(post == null)
            {
                return 0;
            }
            _ctx.Posts.Remove(post);
            await DeleteGalleryImgs(id);
           return await _ctx.SaveChangesAsync();
        }

        public Task DeleteGalleryImgs(int id)
        {
            var galleryImgs = _ctx.Attachments.Select(s => s).Where(s => s.PostId == id).ToList();

            _ctx.Attachments.RemoveRange(galleryImgs);

            string dir = Path.Combine(_hosting.WebRootPath, @"Images");
            foreach (var img in galleryImgs)
            {
                string fullPath = Path.Combine(dir, img.ImgUrl);

                if (File.Exists(fullPath))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {


                            File.Delete(fullPath);
                            break;


                        }
                        catch (IOException) when (i < 3)
                        {
                            Thread.Sleep(100);

                        }
                    }

                }

            }
            return Task.CompletedTask;
        }

        //public async Task<byte[]> GetFile(string File)
        //{
        //    string AccessToken = _IConfiguration.GetSection("DropBoxAccessToken").Value;

        //    using (var _dropBox = new DropboxClient(AccessToken))
        //    using (var response = await _dropBox.Files.GetThumbnailAsync("/" + File))
        //    {
        //        return await response.GetContentAsByteArrayAsync();
        //    }
        //}

        //public async Task WriteFile(string FileName, byte[] Content)
        //{
        //    string AccessToken = _IConfiguration.GetSection("DropBoxAccessToken").Value;
        //    using (var _dropBox = new DropboxClient(AccessToken))
        //    using (var _memoryStream = new MemoryStream(Content))
        //    {
        //        var updated = await _dropBox.Files.UploadAsync(
        //             "/" + FileName,
        //            WriteMode.Overwrite.Instance,
        //            body: _memoryStream);
        //    }
        //}

        //public async Task<int> UploadGalleryFiles(int id, IEnumerable<string> filesNames)
        //{
        //    List<Attachments> imges = new List<Attachments>();
        //    foreach (var fileName in filesNames)
        //    {
        //        var imgGallery = new Attachments
        //        {
        //            ImgUrl = fileName,
        //            PostId = id
        //        };
        //        imges.Add(imgGallery);
        //    }

        //    _ctx.Attachments.AddRange(imges);
        //    return await _ctx.SaveChangesAsync();
        //}
    }
}
                                                             