﻿namespace CrmApiV2.Dtos.Account
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CompanyId { get; set; }
        public List<string> Roles { get; set; }
    }
}
