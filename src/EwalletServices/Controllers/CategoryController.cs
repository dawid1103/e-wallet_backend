using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletCommon.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EwalletServices.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        [HttpPost]
        public async void Create([FromBody] CategoryDTO category)
        {
        }

        [HttpGet("{id}")]
        public async void Get(int id)
        {

        }
    }
}
