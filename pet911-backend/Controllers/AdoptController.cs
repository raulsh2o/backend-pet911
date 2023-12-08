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
    public class AdoptController : ControllerBase
    {
        private readonly DataContext _context;


        [HttpGet("AdopList")]
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
    }
}
