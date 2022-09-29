using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniProject02.Models;

namespace MiniProject02.Repository.Interface
{
    interface IProductsRepository
    {
        List<Products> Get();
        Products Get(int id);
        int Post(Products product);
        int Put(Products product);
        int Delete(int id);
    }
}
