using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using alkemy_challenge.Models;
using alkemy_challenge.BLL;

namespace alkemy_challenge.Controllers
{
    [Route("auth")]
    public class UsersController : Controller
    {
        private readonly Context _context;
        private readonly IEmail _email;
        public UsersController(Context context, IEmail email)
        {
            _context = context;
            _email = email;
        }

        // GET: Users
        public ActionResult Index()
        {

            _email.SendEmail("hola", "subject", "danto@hotmail.com","alkemy challenge", false);
            return View("Index");
        }



        // GET: Users/Register
        [Route("Register")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

  


        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
