using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend_aspnet_crud.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using backend_aspnet_crud.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using backend_aspnet_crud.Entities.UserDTO;
using System.Linq;

namespace backend_aspnet_crud.Controller
{
    [ApiController]
    [Route("v1/users/posts")]
    [Authorize]
    public class PostController: ControllerBase {
        [HttpGet]
        [Route("{id:int}")]
        async public Task<ActionResult<List<Post>>> getPostsByUsername(
            [FromServices] IUserRepository userContext,
            [FromServices] IPostRepository postContext,
            int id
        ){
            return await postContext.getPostsByUserId(id);
        }

        [HttpGet]
        async public Task<ActionResult<List<Post>>> getPosts(
            [FromServices] IUserRepository userContext,
            [FromServices] IPostRepository postContext
        ){
            var username = this.User.Identity.Name;
            var user = await userContext.findByUsername(username);
            if(user == null){
                return BadRequest("user not found");
            }

            var posts = await postContext.GetPosts();
            var a = posts.Select((post) => {
                Console.WriteLine(post.User.Image);
                return post;
            });
            return posts;
        }

        [HttpPost]
        async public Task<ActionResult> storePost(
            [FromServices] IUserRepository userContext,
            [FromServices] IPostRepository postContext,
            [FromBody] PostCreateDTO model
        ){
            var username = this.User.Identity.Name;
            var user = await userContext.findByUsername(username);
            if(user == null){
                return BadRequest("user not found");
            }
            
            var post = new Post {
                title = model.title,
                description = model.description,
                UserId = user.id,
                date = new DateTime()
            };

            postContext.AddAsync(post);
            
            post.User = user;
            
            return Ok(post);
        }
    }
}