using Microsoft.AspNetCore.Mvc;

using backend_aspnet_crud.Entities;
using backend_aspnet_crud.Entities.UserDTO;
using backend_aspnet_crud.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace backend_aspnet_crud.Controller
{
    [ApiController]
    [Route("v1")]
    public class AuthController : ControllerBase {

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> store(
            [FromBody] UserRegisterDTO model,
            [FromServices] IUserRepository context
        ){
            var user = new User {
                name = model.name,
                username = model.username,
                password = model.password,
                role = model.role
            };

            try{
                await context.AddAsync(user);
                return Ok();
            }catch(Exception error){
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<User>> authenticate(
            [FromBody] UserConnectDTO model,
            [FromServices] IUserRepository context
        ){
            var user = await context.findByUsername(model.username);
            if(user == null) {
                var errors = new List<string> {"username not found"};
                return new ObjectResult(new { errors = errors }) { StatusCode = 401 };
            }
            if(user.password != model.password){
                var errors = new List<string> {"password is invalid"};
                return new ObjectResult(new { errors = errors }) { StatusCode = 401 };
            }

            return user;
        }
    }
}