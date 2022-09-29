using Microsoft.EntityFrameworkCore;
using MiniProject02.Context;
using MiniProject02.Models;
using MiniProject02.Repository.Interface;
using MiniProject02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Repository.Data
{
    public class ManagerRepository : IManagerRepository
    {

        private readonly ApplicationDbContext _context;
        public ManagerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DailyProfit> DailyProfits()
        {
            var data = _context.Details.
                Include(x => x.Masters).
                Include(x => x.Products).
                ToList().GroupBy(x => x.Masters.TransactionDate.ToShortDateString()).
                Select(x => new DailyProfit
                {
                    Date = x.Key,
                    ItemSold = x.Sum(i => i.Quantity),
                    Profit = x.Sum(i => i.Products.Price * i.Quantity)
                }) ;

            var result = data.ToList();

            return result;
        }

        public List<Employee> GetEmployees()
        {
            var data = _context.Employee.ToList();

            return data;
        }
    }
}
