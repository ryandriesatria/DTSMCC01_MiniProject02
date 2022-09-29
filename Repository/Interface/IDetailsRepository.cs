using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniProject02.Models;
using MiniProject02.ViewModels;

namespace MiniProject02.Repository.Interface
{
    interface IDetailsRepository
    {
        List<DetailsViewModel> Get();
        List<string> GetProductsName();
        List<string> GetEmployeesName();
        DetailsViewModel Get(int id);
        int Put(DetailsEdit detail);
        int Delete(DetailsViewModel detail);
        int BuyTransaction(DetailsViewModel detail);
        
    }
}
