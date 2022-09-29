using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniProject02.Context;
using MiniProject02.ViewModels;
using MiniProject02.Models;

namespace MiniProject02.Repository.Data
{
    public class AccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseLogin Login(Login login)
        {

            var data = _context.UserRole.
                Include(x => x.User.Employee).
                Include(x => x.User).
                Include(x => x.Role).
                Where(x => x.User.Employee.Username == login.Username).FirstOrDefault();

            if (data == null)
                return null;
            else if (data != null && !ValidatePassword(login.Password, data.User.Password))
                return null;

            return new ResponseLogin
            {
                Id = data.UserId,
                Username = data.User.Employee.Username,
                FullName = data.User.Employee.FullName,
                Role = data.Role.Name

            };
        }

        //Register, if successful redirect to login.
        public ResponseLogin Register(Register register)
        {

            _context.Employee.Add(new Employee
            {
                FullName = register.FullName,
                Username = register.Username

            });

            var result = _context.SaveChanges();

            if (result > 0)
            {
                //Get employeeId from registered user
                int id = _context.Employee.Where(x => x.Username == register.Username).Select(x => x.Id).FirstOrDefault();

                //Insert registered user to table User and UserRole
                _context.User.Add(new User
                {
                    Id = id,
                    Password = HashPassword(register.Password)
                });
                _context.UserRole.Add(new UserRole
                {
                    UserId = id,
                    RoleId = _context.Role.Where(x => x.Name == register.Role).Select(x => x.Id).FirstOrDefault()
                });

                var result2 = _context.SaveChanges();

                //Login process
                if (result2 > 1)
                {
                    var data = Login(new Login
                    {
                        Username = register.Username,
                        Password = register.Password
                    });

                    return data;
                }
            }

            return null;
        }

        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        private bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
