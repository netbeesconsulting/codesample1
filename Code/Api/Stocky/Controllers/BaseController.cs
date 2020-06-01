using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stocky.Application.Utility;
using Stocky.Common;
using Stocky.Model.Security;
using Stocky.Model.Utility;

namespace Stocky.Controllers
{
    public class BaseController : Controller
    {
        protected const string API_VERSION = "api/V1";
        public BaseController()
        {
        }

        public BaseController(IBaseInjector inject) : base()
        {
        }

        protected int UserId
        {
            get
            {
                try
                {
                    return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
                }
                catch (Exception)
                {
                    throw new UnAuthorizedException("user id not found");
                }
            }
        }

        protected Enums.UserType UserType
        {
            get
            {
                try
                {
                    if (int.TryParse(this.User.Claims.First(c => c.Type == "UserType").Value, out var usertype))
                    {
                        var ut = (Enums.UserType)usertype;
                        if (ut == Enums.UserType.None)
                        {
                            throw new UnAuthorizedException("UserType not found");
                        }
                        return ut;
                    }

                    throw new UnAuthorizedException("UserType not found");
                }
                catch (Exception)
                {
                    throw new UnAuthorizedException("user type not found");
                }
            }
        }

        protected Request<T> Request<T>(T c)
        {
            return new Request<T>
            {
                Item = c,
                UserId = UserId,
                UserType = UserType
            };
        }
        protected Request<T> Request<T>(T c, bool allowAnonymas)
        {
            return new Request<T>
            {
                Item = c,
                UserId = 0,
            };
        }

        protected async Task<object> GenerateJwtToken(AuthResponse authresponse)
        {
            try
            {
                var claims = new List<Claim>{
                  new Claim(JwtRegisteredClaimNames.Sub, authresponse.Email),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim("UserId", authresponse.UserId.ToString()),
                  new Claim("UserType", ((int)authresponse.UserType).ToString())
               };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalConfig.JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(GlobalConfig.JwtExpireDays));
                var token = new JwtSecurityToken(GlobalConfig.JwtIssuer, GlobalConfig.JwtIssuer, claims,
                    expires: expires, signingCredentials: creds
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected async Task<IActionResult> HandleException(Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            if (ex is PrimaryKeyViolationException)
            {
                return BadRequest(ex.Message);
            }
            else
                switch (ex)
                {
                    case ForbiddenException _:
                        return StatusCode(403);
                    case UnAuthorizedException _:
                        return Unauthorized(ex.Message);
                    case RecordNotFoundException _:
                        return BadRequest(ex.Message);
                    case InvalidProcessException _:
                        return BadRequest(ex.Message);
                    case NullObjectException _:
                        return BadRequest(ex.Message);
                    case EntityValidationException _:
                        return BadRequest(ex.Message);
                    case EatException _:
                        return Ok();
                    default:
                        return StatusCode(500, ex.Message);
                }
        }
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task<FileSaveResult> SaveImage(string folderPath)
        {
            try
            {
                var fileSaveResult = new FileSaveResult();
                if (HttpContext.Request.Form.Files == null || HttpContext.Request.Form.Files.Count == 0) return fileSaveResult;

                var files = HttpContext.Request.Form.Files;
                var filenames = new List<string>();
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(GlobalConfig.WebRootPath, folderPath);
                        if (file.Length > 0)
                        {
                            //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            var fileName = file.FileName.Replace(" ", "_");
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                filenames.Add(fileName);
                                fileSaveResult.FileIsThere = true;
                            }

                        }
                    }
                }
                fileSaveResult.FileNames = filenames;
                return fileSaveResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}