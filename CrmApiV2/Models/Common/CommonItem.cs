using System.ComponentModel.DataAnnotations.Schema;

namespace CrmApiV2.Models.Common
{
    public class CommonItem
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
