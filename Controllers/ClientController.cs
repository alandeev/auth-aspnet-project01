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
using backend_aspnet_crud.Data;

namespace backend_aspnet_crud.Controller
{
    // public class FileUPloadAPI {
    //     public IFormFile file { get; set; }
    // }

    [ApiController]
    [Route("v1/users")]
    [Authorize]
    public class ClientController: ControllerBase {
        [HttpGet]
        [Route("oauth")]
        public async Task<ActionResult<User>> findAll(
            [FromServices] IUserRepository userContext,
            [FromServices] IFileRepository fileContext
        ){
            var username = this.User.Identity.Name;
            var user = await userContext.findByUsername(username);
            
            if(user == null){
                return new ObjectResult(new { errors = new List<string> {"user not found"} }) { StatusCode = 401 };
            }

            var image = fileContext.findFileByUserId(user.id);
            if(image != null) {
                user.Image = image;
            }

            return user;
        }

        [HttpPost]
        [Route("uploads")]
        public async Task<ActionResult> UploadFileAsync(
            [FromServices] IFileRepository fileContext,
            [FromServices] IUserRepository userContext,
            [FromServices] DataContext dataContext,
            [FromServices] IWebHostEnvironment environment
        ){
            var username = this.User.Identity.Name;
            var user = await userContext.findByUsername(username);
            if(user == null){
                return BadRequest("user not found");
            }
            
            var files = HttpContext.Request.Form.Files;
            if(files.Count != 1) {
                return BadRequest("problem 01");
            }

            var ImagePath = environment.WebRootPath+"\\Upload\\";
            var Extension = Path.GetExtension(files[0].FileName);
            var fileName = user.username + "-" + user.id + Extension;
            var RelativeImagePath = ImagePath + fileName;

            try{
                var hasFile = fileContext.findFileByUserId(user.id);
                if(hasFile != null){
                    fileContext.deleteFile(hasFile);
                    System.IO.File.Delete(hasFile.path);
                }
                using (FileStream fileStream = System.IO.File.Create(RelativeImagePath)) {
                    files[0].CopyTo(fileStream);
                    fileStream.Flush();

                    var file = new FileM() { 
                        filename = fileName,
                        path = RelativeImagePath,
                        UserId = user.id,
                        link = "https://localhost:5001/upload/"+fileName
                    };

                    user.Image = file;
                    fileContext.addFile(file);
                    await dataContext.SaveChangesAsync();
                    return Ok(fileName);
                }
            }catch (Exception ex){
                Console.WriteLine("PROBLEM FINAL ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}