using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniProject02.Context;
using MiniProject02.Models;
using MiniProject02.ViewModels;
using MiniProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MiniProject02.Repository.Data
{
    public class DetailsRepository : IDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public DetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Delete(DetailsViewModel detail)
        {
            var data = _context.Details.Find(detail.Id);
            _context.Details.Remove(data);
            var product = _context.Products.
                    Where(x => x.Name == detail.ProductName).FirstOrDefault();
            product.Stock = product.Stock + detail.Quantity;
            _context.Products.Update(product);
            var result = _context.SaveChanges();
            return result;
        }

        public int Put(DetailsEdit detail)
        {
            // Get productId from field productName on the input form
            var product = _context.Products.
                Where(x => x.Name == detail.ProductName).FirstOrDefault();
            // Get employeeId from field kasir on the input form
            int employeeId = _context.Employee.
                Where(x => x.FullName == detail.Kasir).
                Select(x => x.Id).FirstOrDefault();

            var data = _context.Details.Find(detail.Id);
            data.EmployeeId = employeeId;
            data.Quantity = detail.Quantity;
            _context.Details.Update(data);

            // Adjust the quantity from product stock based on new transaction
            product.Stock = product.Stock + (detail.OldQuantity - detail.Quantity);
            
            _context.Products.Update(product);

            _context.SaveChanges();

            return 1;
        }

        public List<DetailsViewModel> Get()
        {
            var data = _context.Details.
                Include(d => d.Products).
                Include(d => d.Masters).
                Include(d => d.Employee).
                Select(x => new DetailsViewModel
                {
                    Id = x.Id,
                    ProductName = x.Products.Name,
                    TransactionDate = x.Masters.TransactionDate,
                    Total = x.Products.Price * x.Quantity,
                    Quantity = x.Quantity,
                    Kasir = x.Employee.FullName
                }).ToList();


            return data;
        }

        public DetailsViewModel Get(int id)
        {
            var data = _context.Details.
                Include(d => d.Products).
                Include(d => d.Masters).
                Include(d => d.Employee).
                Where(x => x.Id == id).
                Select(x => new DetailsViewModel
                {
                    Id = x.Id,
                    ProductName = x.Products.Name,
                    TransactionDate = x.Masters.TransactionDate,
                    Total = x.Products.Price * x.Quantity,
                    Quantity = x.Quantity,
                    Kasir = x.Employee.FullName
                }).FirstOrDefault();
            return data;
        }


        public int BuyTransaction(DetailsViewModel detail)
        {
            //Insert into table masters and get masterId from inserted master obj
            var master = new Masters()
            {
                TransactionDate = detail.TransactionDate
            };
            _context.Masters.Add(master);
            var result = _context.SaveChanges();

            if(result > 0)
            {
                // Get productId from field productName on the input form
                var product = _context.Products.
                    Where(x => x.Name == detail.ProductName).FirstOrDefault();
                // Get employeeId from field kasir on the input form
                int employeeId = _context.Employee.
                    Where(x => x.FullName == detail.Kasir).
                    Select(x => x.Id).FirstOrDefault();

                // Insert new transaction into detail table
                _context.Details.Add(new Details
                {
                    ProductId = product.Id,
                    MasterId = master.Id,
                    Quantity = detail.Quantity,
                    EmployeeId = employeeId

                });

                // Decrease the quantity from product stock based on new transaction

                product.Stock = product.Stock - detail.Quantity;
                _context.Products.Update(product);

                _context.SaveChanges();

                return 1;
            }
            return 0;
        }

        public List<string> GetProductsName()
        {
            var data = _context.Products.Select(x => x.Name).ToList();
            return data;
        }

        public List<string> GetEmployeesName()
        {
            var data = _context.Employee.Select(x => x.FullName).ToList();
            return data;
        }
    }
}
