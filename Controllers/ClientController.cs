using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend_aspnet_crud.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using backend_aspnet_crud.Entities.UserDTO;
using backend_aspnet_crud.Entities;

namespace backend_aspnet_crud.Controller
{
    [ApiController]
    [Route("v1/clients")]
    [Authorize]
    public class ClientController: ControllerBase {

        [HttpGet]
        [Route("byuser")]
        public async Task<ActionResult<User>> findByUsername(
            [FromServices] IUserRepository context,
            [FromBody] UserFindByUsernameDTO model
        ){
            var user = await context.findByUsername(model.username);
            if(user == null) {
                return NotFound();
            }
            return new ObjectResult(user) { StatusCode = 200 };
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> findAll(
            [FromServices] IUserRepository context
        ){
            var users = await context.GetUsers();
            return new ObjectResult(users);
        }
    }
}