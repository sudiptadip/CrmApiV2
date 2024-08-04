using CrmApiV2.Dtos.Company;
using CrmApiV2.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CrmApiV2.Mapper
{
    public static class CompanyMapper
    {

        public static CompanyDto ToCompanyDto(this Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                Address = company.Address,
            };
        }


        public static Company ToCompanyToCreateDto(this CreateCompanyDto company)
        {
            return new Company
            {
                Name = company.Name,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                Address = company.Address,
                IsDeleted = false,
                CreatedBy = "SuperAdmin",
                CreatedOn = DateTime.Now,
            };
        }

        public static Company ToCompanyToUpdateDto(this UpdateCompanyDto company)
        {
            return new Company
            {
                Name = company.Name,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                Address = company.Address,
                IsDeleted = false,
                ModifiedBy = "SuperAdmin",
                ModifiedOn = DateTime.Now,
            };
        }

    }
}
