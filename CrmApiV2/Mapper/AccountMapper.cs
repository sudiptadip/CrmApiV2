using CrmApiV2.Dtos.Account;
using CrmApiV2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.Design;
using System.Net;

namespace CrmApiV2.Mapper
{
    public static class AccountMapper
    {
        public static UserDto ToUserDto(this ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                CreatedOn = user.CreatedOn,
                CompanyId = user.CompanyId
            };

        }
    }
}
