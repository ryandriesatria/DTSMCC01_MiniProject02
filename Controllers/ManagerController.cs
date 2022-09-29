using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject02.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ManagerRepository _repository;
        public ManagerController(ManagerRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == null || !role.Equals("Manager"))
            {
                return Content("Unauthorized");
            }
            var data = _repository.GetEmployees();
            return View(data);
        }

        public IActionResult Profit()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == null || !role.Equals("Manager"))
            {
                return Content("Unauthorized");
            }
            var data = _repository.DailyProfits();
            return View(data);
        }
    }
}
