using CORE.Models;
using DAL.Contexts;
using DAL.Repository.Absractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concretes
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<SelectListItem>> SelectDepartmentsAsync()
        {
            return await _context.Departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
            }).ToListAsync();
        }
    }
}
