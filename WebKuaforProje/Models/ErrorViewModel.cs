using Microsoft.EntityFrameworkCore;
using WebKuaforProje.Models;  // ErrorViewModel ve diðer modeller için

namespace WebKuaforProje.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
