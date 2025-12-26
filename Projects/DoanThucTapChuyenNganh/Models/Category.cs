using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoanThucTapChuyenNganh.Models;

public partial class Category
{
    [Display(Name = "Mã danh mục")]
    public int Id { get; set; }

    [Display(Name = "Tên danh mục")]
    [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
    [StringLength(100, ErrorMessage = "Tên danh mục không được quá 100 ký tự")]
    public string Name { get; set; } = null!;

    // Thuộc tính này thường dùng khi hiển thị trang Details (Chi tiết)
    [Display(Name = "Sản phẩm thuộc danh mục")]
    public virtual ICollection<Product> Products { get; set; }
}
    
