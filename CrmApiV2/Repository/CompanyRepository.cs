using CrmApiV2.Data;
using CrmApiV2.Interface;
using CrmApiV2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CrmApiV2.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Company> CreateAsync(Company company)
        {
            var companyExists = await _db.Companies.AnyAsync(u => u.Name == company.Name && !u.IsDeleted);
            if (companyExists)
            {
                throw new InvalidOperationException("Company name already exists");
            }

            await _db.Companies.AddAsync(company);
            await _db.SaveChangesAsync();
            return company;
        }

        public async Task<Company?> DeleteAsync(int id)
        {
            var company = await _db.Companies.FirstOrDefaultAsync(e  => e.Id == id && !e.IsDeleted);
            if (company == null)
            {
                return null;
            }

            var userExists = await _db.applicationUsers.AnyAsync(u => u.CompanyId == id && !u.IsDeleted);
            if (userExists)
            {
                throw new InvalidOperationException("Cannot delete company with associated users.");
            }

            company.IsDeleted = true;
            _db.Companies.Update(company);
            await _db.SaveChangesAsync();

            return company;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _db.Companies.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _db.Companies.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<Company?> UpdateAsync(Company company)
        {
            var existingCompany = await _db.Companies.FirstOrDefaultAsync(e => e.Id == company.Id && !e.IsDeleted);
            if (existingCompany == null)
            {
                return null;
            }
            var companyExists = await _db.Companies.AnyAsync(u => u.Name == company.Name && !u.IsDeleted && u.Id != company.Id);
            if (companyExists)
            {
                throw new InvalidOperationException("Company name already exists");
            }

            _db.Entry(existingCompany).CurrentValues.SetValues(company);
            await _db.SaveChangesAsync();
            return existingCompany;
        }
    }
}