using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Models
{
    public class User
    {
        public Employee Employee { get; set; }
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public string Password { get; set; }

    }
}
