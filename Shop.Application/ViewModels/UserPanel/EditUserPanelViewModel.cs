using Microsoft.AspNetCore.Http;

namespace Shop.Application.ViewModels.UserPanel
{
    public class EditUserPanelViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string? Mobile { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
