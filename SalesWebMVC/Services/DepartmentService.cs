using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;
        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }
         
        //Async -> Assincrono
        public async Task<List<Department>> FindAllAsync()
        { //Retorna lista assincrona agilizando a execução do código
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();

        }


    }

}
