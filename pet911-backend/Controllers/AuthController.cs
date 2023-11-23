using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pet911_backend.Helpers;
using pet911_backend.Models.Dto;
using pet911_backend.Models;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using pet911_backend.Hubs;

namespace pet911_backend.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private IConfiguration config;
        private readonly IEmailSender emailSender;
        private readonly HttpClient _httpClient;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        public AuthController(DataContext context, IConfiguration config, IEmailSender emailSender, HttpClient httpClient, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _context = context;
            this.emailSender = emailSender;
            this.config = config;
            this._httpClient = httpClient;
            _hubContext = hubContext;
        }

        [HttpPost("Login")]
        public ActionResult<User> PostLogin(Login model)
        {
            try
            {
                var user = _context.User.Where(us => us.Email == model.Email).ToList().FirstOrDefault();
                if (user == null )
                {
                    return Ok();
                }
                var confirm = CheckPassword(model.Password, user);

                if(!confirm)
                {
                    return Ok();
                }
                var token = GenerateToken(user);
                Role rol = _context.Role.Find(user.IdRole);
          
                return Ok(new {token = token,idUser=user.Id,name=user.Name,rol=rol.RoleType});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("LoginWithGoogle")]
        public ActionResult LoginWithGoogle([FromBody] GoogleUser model)
        {

            return Ok(model);
        }

        [HttpPost("LoginWithFacebook")]
        public async Task<IActionResult> LoginWithFacebook([FromBody] string credential)
        {
           return Ok(new { credential = credential });
            
        }

        private bool CheckPassword(string password, User user)
        {
            bool result;

            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }

            return result;
        }
        private string GenerateToken(User user)
        {
            var role = _context.Role.Find(user.IdRole);
            var claims = new[]
            {
                new Claim("id",user.Email),
                new Claim(ClaimTypes.Role,role.RoleType),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = false,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });
            return token;
        }
        [HttpPost("CheckUser")]
        public IActionResult CheckUser([FromBody] Email email)
        {
            var baseUser = _context.User.Where(us => us.Email == email.email).FirstOrDefault();
            
            if (baseUser != null)
            {
                return Ok(new { email });
            }

            return Ok("");
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] Register model)
        {
            var baseUser = _context.User.Where(us=>us.Email==model.Email).FirstOrDefault();
            if (baseUser != null)
            {
                return BadRequest("El correo ya esta registrado");
            }
            var user = new User { Email = model.Email };
            byte[] passwordHash, passwordSalt;
            if (model.ConfirmPassword == model.Password)
            {
                
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                }
            }
            else
            {
                return BadRequest("Las contraseñas no coinciden");
            }

            user.Id = Guid.NewGuid().ToString();
            user.Name = model.Name;
            user.Email = user.Email;
            user.Birthdate = DateOnly.Parse(model.Birthdate); 
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            Role role = _context.Role.Where(x => x.RoleType == "Usuario").ToList().FirstOrDefault(); 
            user.IdRole = role.Id;

            _context.User.Add(user);
            _context.SaveChanges();
          
            return Ok(new {model.Email,model.Password});
        }


        [HttpPost("Session")]
        public IActionResult RegisterSession([FromBody] Session model)
        {
            try
            {
                _context.Session.Add(model);
                _context.SaveChanges();
                return Ok(model);
            }catch(Exception ex)
            {
                return BadRequest("Error");
            }
            
            return BadRequest("No existe");
            //var user = new User { Email = model.Email };
            //byte[] passwordHash, passwordSalt;
            //if (model.ConfirmPassword == model.Password)
            //{

            //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
            //    {
            //        passwordSalt = hmac.Key;
            //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
            //    }
            //}
            //else
            //{
            //    return BadRequest("Las contraseñas no coinciden");
            //}

            //user.Id = Guid.NewGuid().ToString();
            //user.Name = model.Name;
            //user.Email = user.Email;
            //user.Birthdate = DateOnly.Parse(model.Birthdate);
            //user.PasswordSalt = passwordSalt;
            //user.PasswordHash = passwordHash;

            //Role role = _context.Role.Where(x => x.RoleType == "User").ToList().FirstOrDefault();
            //user.IdRole = role.Id;

            //_context.User.Add(user);
            //_context.SaveChanges();

            //return Ok(new { model.Email, model.Password });
        }

        [HttpDelete("Session/{id}")]
        public IActionResult DeleteSession( string id)
        {
            try
            {
                var session = _context.Session.Find(id);
                if (session == null)
                {
                    return BadRequest("No se econtro la sesion");

                }
                _context.Session.Remove(session);
                _context.SaveChanges();
                return Ok("Se elimino la sesion");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //User baseUser = _context.User.Include(us=>us.services).Where(us => us.Email == model.Email).FirstOrDefault();
            //var user = baseUser.services.Select(x=> new { x.Name,x.Type});
            //if (baseUser != null)
            //{
            //    Role rol = _context.Role.Find(baseUser.IdRole);
            //    var newRol = rol.RoleType;

            //    return Ok(rol);
            //}
        }
        [HttpDelete("Notification/{id}")]
        public IActionResult DeleteNotification(string id)
        {
            try
            {
                var notification = _context.Notification.Find(id);
                if (notification == null)
                {
                    return BadRequest("No se econtro la sesion");

                }
                _context.Notification.Remove(notification);
                _context.SaveChanges();
                return Ok("Se elimino la notificacion");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Admins")]
        public string GetAdmins()
        {
            List<Session> sessions = _context.Session.ToList();
            List<string> adminEmails = new List<string>();

            foreach (Session session in sessions)
            {
                User user = _context.User.FirstOrDefault(u => u.Email == session.Email);

                if (user != null)
                {
                    var role = _context.Role.Find(user.IdRole);

                    if (role != null && role.RoleType == "Admin")
                    {
                        adminEmails.Add(user.Email);
                    }
                }
            }

            return JsonConvert.SerializeObject(adminEmails);
        }

        [HttpPost("GetSession")]
        public string PostAdminNotifications([FromBody] Email model)
        {
            List<Session> session = _context.Session
                    .Where(n => n.Email == model.email)
                    .ToList();

            List<string> messages = session.Select(n => n.Id).ToList();

            return JsonConvert.SerializeObject(messages);

        }


        [HttpPost("GetNotifications")]
        public string PostAdminNotifications([FromBody] EmailNotification model)
        {
            List<Notification> notification = _context.Notification
                    .Where(n => n.Email_rx == model.Email_rx)
                    .ToList();

            //List<string> messages = notification.Select(n => n.Email_tx).ToList();
            //return JsonConvert.SerializeObject(messages);
            var notificationInfos = notification.Select(n => new Notification
            {
                Id = n.Id,
                Email_tx = n.Email_tx // Reemplaza con el nombre de la propiedad correcto si es diferente
            }).ToArray();

            return JsonConvert.SerializeObject(notificationInfos);
        }

        [HttpPost("ConfirfNotification")]
        public string PostConfirmNotification([FromBody] EmailNotification model)
        {
            List<Session> ses = _context.Session.ToList();

            var sel = ses.Select(s => new
            {
                s.Email
            });
            string retMessage = string.Empty;
            foreach (Session s in ses)
            {
                User use = _context.User.Where(us => us.Email == s.Email).FirstOrDefault();

                var role = _context.Role.Find(use.IdRole);
                if (role.RoleType == "Usuario")
                {

                    Message mew = new Message();
                    mew.Email_rx = model.Email_rx;
                    mew.Email_tx = model.Email_tx;
                    mew.Text = "Emergencia Aceptada!";
                    try
                    {
                        _hubContext.Clients.Client(s.Id).BroadcastMessage(mew);
                        retMessage = "Success";
                    }
                    catch (Exception e)
                    {
                        retMessage = e.ToString();
                    }
                    return retMessage;
                }

            }
            return retMessage;
        }

        [HttpPost("Notification")]
        public string PostUser([FromBody] EmailNotification model)
        {
            Notification notification = new Notification();
            notification.Id = Guid.NewGuid().ToString();
            notification.Email_rx = model.Email_rx;
            notification.Email_tx = model.Email_tx;
            notification.Message = "Mensaje de emergencia!"; ;

            _context.Notification.Add(notification);
            _context.SaveChanges();

            //List<Session> ses = _context.Session.ToList();
            List<Session> ses = _context.Session
            .Where(s => s.Email == model.Email_rx)
            .ToList();

            var sel = ses.Select(s => new
            {
                s.Email
            });
            string retMessage = string.Empty;
            foreach (Session s in ses)
            {
                User use = _context.User.Where(us => us.Email == s.Email).FirstOrDefault();

                var role = _context.Role.Find(use.IdRole);
                if (role.RoleType == "Admin")
                {

                    Message mew = new Message();
                    mew.Email_rx = model.Email_rx;
                    mew.Email_tx = model.Email_tx;
                    mew.Text = "Mensaje de emergencia!";
                    try
                    {
                        _hubContext.Clients.Client(s.Id).BroadcastMessage(mew);
                        retMessage = "Success";
                    }
                    catch (Exception e)
                    {
                        retMessage = e.ToString();
                    }
                    return retMessage;
                }

            }
            return retMessage;
        }
        [HttpGet("Mesa/{id}")]
        public ActionResult MessageUser(string id)
        {
            string retMessage = string.Empty;
            try
            {
                Message mew = new Message();
                mew.Text = "Un paciente esta en camino";
                _hubContext.Clients.AllExcept(id).BroadcastMessage(mew);
                return Ok ( new { mesagge = "Un paciente esta en camino" });
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return Ok(new { mesagge = "No funciona" });
        }


        [HttpPost("SendEmail")]
        public async  Task<IActionResult> SendEmail([FromBody] Email email)
        {
             var baseUser = _context.User.Where(us => us.Email == email.email).FirstOrDefault();
            if (baseUser == null)
            {
                return BadRequest("Correo invalido");
            }
            var role = _context.Role.Find(baseUser.IdRole);
            if ( role.RoleType== "Admin")
            {

                await emailSender.SendEmailAsync(email.email, "Recupera tu contraseña", "http://localhost:8100/two-part-auth/" + baseUser.Id);
                return Ok("Se envio el correo");

            }
            await emailSender.SendEmailAsync(email.email, "Recupera tu contraseña", "http://localhost:8100/password-change/"+baseUser.Id);

            return Ok("Se envio el correo");

        }
        [HttpPost("SendCode/{id}")]
        public async Task<IActionResult> SendCode([FromBody] Email email,string id)
        {
            var baseUser = _context.User.Find(id);
            
            await emailSender.SendEmailAsync(baseUser.Email, "Recupera tu contraseña", email.email);

            return Ok("Se envio el correo");

        }
        [HttpPost("ResetPassword/{id}")]
        public IActionResult ResetPassword([FromBody] ResetPassword reset,string id)
        {
            var baseUser = _context.User.Find(id);

            if (baseUser != null)
            {
                byte[] passwordHash, passwordSalt;
                if (reset.ConfirmPassword == reset.Password)
                {

                    using (var hmac = new System.Security.Cryptography.HMACSHA512())
                    {
                        passwordSalt = hmac.Key;
                        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(reset.Password));
                    }
                }
                else
                {
                    return BadRequest("Las contraseñas no coinciden");
                }

              
                baseUser.PasswordSalt = passwordSalt;
                baseUser.PasswordHash = passwordHash;

                _context.SaveChanges();
                return Ok(new { baseUser.Email,reset.Password });
            }

            return Ok("");
        }
    }
}
