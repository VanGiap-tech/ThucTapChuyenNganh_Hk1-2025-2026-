using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoanThucTapChuyenNganh.Models;

public partial class User
{
    public int Id { get; set; }

    [Display(Name = "Tên đăng nhập")]
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
    public string? UserName { get; set; } // Thêm ? để an toàn với DB

    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    public string? PasswordHash { get; set; } // Thêm ? để an toàn với DB

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự.")]
    public string? FullName { get; set; }

    [Display(Name = "Địa chỉ Email")]
    
    [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ.")]
    public string? Email { get; set; }

    [Display(Name = "Số điện thoại")]
   
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string? Phone { get; set; }

    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.Date)]
    public DateTime? CreatedAt { get; set; }


    [Display(Name = "Quản trị viên")]
    public bool? IsAdmin { get; set; } = false;

    [DisplayName("Trạng thái")] 
    [Required(ErrorMessage = "Vui lòng chọn trạng thái")] 
    public bool IsLocked { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}