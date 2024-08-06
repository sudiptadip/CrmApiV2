namespace CrmApiV2.Models
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
    }
}
