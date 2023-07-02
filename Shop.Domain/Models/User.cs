using Shop.Domain.Models.Common;

namespace Shop.Domain.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string EmailActiveCode { get; set; } = string.Empty;
        public bool IsEmailActive { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsBan { get; set; }

    }
}
