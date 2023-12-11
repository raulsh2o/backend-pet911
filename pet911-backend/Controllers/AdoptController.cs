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
        public AdoptController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("AdopList")]
        public string GetAdopt()
        {
            //comentario

            List<Adopt> adopt = _context.Adopt.ToList();


            return JsonConvert.SerializeObject(adopt);
        }
    }
}
