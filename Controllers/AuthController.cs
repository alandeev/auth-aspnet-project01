using Microsoft.AspNetCore.Mvc;

using backend_aspnet_crud.Entities;
using backend_aspnet_crud.Entities.UserDTO;
using backend_aspnet_crud.Repositories;

using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using backend_aspnet_crud.Services;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace backend_aspnet_crud.Controller
{

    public class FileUPloadAPI {
        public IFormFile files { get; set; }
    }

    [ApiController]
    [Route("v1")]
    public class AuthController : ControllerBase {
        public static IWebHostEnvironment _environment;
        public AuthController(IWebHostEnvironment environment){ _environment = environment; }

        [HttpPost]
        [Route("uploads")]
        public string UploadFile([FromForm] FileUPloadAPI objFile){
            try{
                Console.WriteLine(_environment.WebRootPath);
                if(objFile.files.Length > 0){
                    Console.WriteLine("0");
                    if(!Directory.Exists(_environment.WebRootPath+"\\Upload\\")) {
                        Directory.CreateDirectory(_environment.WebRootPath+"\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath+"\\Upload\\"+objFile.files.FileName)) {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Upload\\"+objFile.files.FileName;
                    }
                }
                Console.WriteLine("salvou");
                return "uploaded";
            }catch (Exception ex) {
                Console.WriteLine("error");
                return ex.Message;
            }
        }

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

                return NoContent();
            }catch(Exception error){
                var errors = new List<string> {error.Message};
                return new ObjectResult(new { errors = errors }) { StatusCode = 401 };
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

            var token = TokenService.GenerateToken(user);

            return new ObjectResult(new { token = $"Bearer {token}" });
        }
    }
}