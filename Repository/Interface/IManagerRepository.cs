using MiniProject02.Models;
using MiniProject02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Repository.Interface
{
    interface IManagerRepository
    {
        List<Employee> GetEmployees();

        List<DailyProfit> DailyProfits();
    }
}
