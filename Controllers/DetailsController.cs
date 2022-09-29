using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject02.Context;
using MiniProject02.Models;
using MiniProject02.Repository.Data;
using MiniProject02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Controllers
{
    public class DetailsController : Controller
    {

        private readonly DetailsRepository _repository;
        public DetailsController(DetailsRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var data = _repository.Get();
            return View(data);
        }

        //GET
        public IActionResult Create()
        {
            var productsName = _repository.GetProductsName();
            var employeesName = _repository.GetEmployeesName();

            ViewBag.productsName = productsName;
            ViewBag.employeesName = employeesName;

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DetailsViewModel detail)
        {

            var result = _repository.BuyTransaction(detail);

            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(int id)
        {
            var data = _repository.Get(id);
            var employeesName = _repository.GetEmployeesName();
            ViewBag.employeesName = employeesName;

            return View( new DetailsEdit
            {
                Id = data.Id,
                ProductName = data.ProductName,
                TransactionDate = data.TransactionDate,
                OldQuantity = data.Quantity,
                Kasir = data.Kasir
            });
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DetailsEdit detail)
        {
            var result = _repository.Put(detail);
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Delete(int id)
        {
            var data = _repository.Get(id);
            return View(data);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DetailsViewModel detail)
        {
            int result = _repository.Delete(detail);
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Details(int id)
        {
            var data = _repository.Get(id);
            return View(data);
        }
    }
}
