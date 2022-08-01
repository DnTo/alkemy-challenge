using alkemy_challenge.BLL;
using alkemy_challenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_challenge.Controllers
{
    public class AuthController : Controller
    {
        private readonly Context _context;
        private readonly IEmail _email;
        private readonly AuthBusiness _auth;
        public AuthController(Context context, IEmail email, AuthBusiness auth)
        {
            _context = context;
            _email = email;
            _auth = auth;
        }

        // GET: Auth

        public async Task<IActionResult> Index()
        {

            await Task.Run(() => _email.SendEmail("test body", "subjecy", "email@server.com", "", false));
            return View();
        }


        // GET: Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        // POST: Auth/Register
        /// <summary>
        /// Creo el usuario de la api, recibo el email y le devulevo por correo el password
        /// con el password puede logearse para crear un token para consumir la api
        /// </summary>
        /// <param name="user0"></param>
        /// <returns></returns>
        [Route("auth/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterPost(string Email)
        {
            //validate email format
            //if(!IsEmail(Email)) throw ,,,

            var user = new User();
            var password = "123456";
            user.Password = password;
            user.Role = "user";
            user.Email = Email;

            try
            {
                //si existe
                if (await _context.FindAsync(typeof(User), Email) != null)
                {
                    ViewBag.existe = "usuario ya existe";
                }
                else
                {
                    await _context.Users.AddAsync(user);
                    var added = await _context.SaveChangesAsync();

                    if (added > 0)
                    {
                        _email.SendEmail(($"Gracias por tu registro.\nTu usuario es {user.Email} y tu password es {user.Password}.\n" +
                             $"El endopoint para autenticacion es {Request.Host}/auth/login")
                            , "Credenciales",
                            user.Email,
                            "",
                            false);
                        ViewBag.existe = "usuario creado, Revisa tu correo para instrucciones";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("Register", user);
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = new User();
            var token = "";
            try
            {
                user = _auth.AuthenticateUser(email, password);
                if (user == null)
                {
                    ViewBag.existe = "credenciales incorrectas";
                }
                else
                {
                    //obtengo el token
                    token = _auth.GenereteToken(user);
                    ViewBag.token = token;
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }
            return View("Login", user);
        }
    }
}
