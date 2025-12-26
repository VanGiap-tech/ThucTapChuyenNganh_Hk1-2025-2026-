using System.ComponentModel.DataAnnotations;


namespace DoanThucTapChuyenNganh.ViewModels
    {
        public class RegisterViewModel // Lưu ý: Nên đặt tên số ít là ViewModel
        {
            [Display(Name = "Tên đăng nhập")]
            [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 50 ký tự")]
            public string Username { get; set; }

            [Display(Name = "Họ và tên")]
            [Required(ErrorMessage = "Vui lòng nhập họ tên")]
            public string FullName { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Vui lòng nhập Email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; }

            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
            public string Password { get; set; }

            [Display(Name = "Nhập lại mật khẩu")]
            [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
            public string ConfirmPassword { get; set; }
       
    }
    }

