using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoanThucTapChuyenNganh.ViewModels
{
    
        public class LoginViewModel
        {
            [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [DisplayName("Ten dang nhap")]    
            public string Username { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DisplayName("Mat Khau")]
        public string Password { get; set; }
        }
    
}
