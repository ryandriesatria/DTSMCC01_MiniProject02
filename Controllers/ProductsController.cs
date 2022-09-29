using Microsoft.AspNetCore.Mvc;
using MiniProject02.Context;
using MiniProject02.Models;
using MiniProject02.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Controllers
{


    public class ProductsController : Controller
    {
        private readonly ProductsRepository _repository;
        public ProductsController(ProductsRepository repository)
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
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Products product)
        {
            var result = _repository.Post(product);
            if (result > 0)
                return RedirectToAction("Index");

            return View();
        }

        //GET
        public IActionResult Edit(int id)
        {
            var data = _repository.Get(id);
            return View(data);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Products product)
        {
            var result = _repository.Put(product);

            if(result > 0)
                return RedirectToAction("Index");

            return View();
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
        public IActionResult Delete(Products product)
        {
            var result = _repository.Delete(product.Id);

            if (result > 0)
                return RedirectToAction("Index");

            return View();
        }

        //GET
        public IActionResult Details(int id)
        {
            var data = _repository.Get(id);
            return View(data);
        }

    }
}

