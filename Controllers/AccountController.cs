using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject02.Repository.Data;
using MiniProject02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _repository;
        public AccountController(AccountRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var data = _repository.Login(login);

            if (data == null)
            {
                return View();
            }
            HttpContext.Session.SetString("Role", data.Role);
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            var data = _repository.Register(register);

            if (data == null)
            {
                return View();
            }
            HttpContext.Session.SetString("Role", data.Role);
            return RedirectToAction("Index", "Products");
        }
    }
}
