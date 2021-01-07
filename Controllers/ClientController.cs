using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend_aspnet_crud.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using backend_aspnet_crud.Entities;

namespace backend_aspnet_crud.Controller
{
    [ApiController]
    [Route("v1/users")]
    [Authorize]
    public class ClientController: ControllerBase {
        [HttpGet]
        [Route("oauth")]
        public async Task<ActionResult<User>> findAll(
            [FromServices] IUserRepository context
        ){
            var username = this.User.Identity.Name;
            var user = await context.findByUsername(username);
            
            if(user == null){
                return new ObjectResult(new { errors = new List<string> {"user not found"} });
            }
            return user;
        }
    }
}