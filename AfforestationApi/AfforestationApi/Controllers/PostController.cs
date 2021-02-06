using Afforestation.App.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AfforestationApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {


        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult GetPosts([FromServices] GetPosts getPosts) =>
        Ok(getPosts.Do());
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetPost(int id, [FromServices] GetPost getPost) =>
        Ok(getPost.Do(id));

        //[AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm]CreatePost.Request request, [FromServices] CreatePost createPost) =>
        Ok(await createPost.Do(request));

       // [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] DeletePost deletePost) =>
        Ok(await deletePost.Do(id));




    }
}
