using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoanThucTapChuyenNganh.Models;

public partial class Brand
{
    [Display(Name = "Mã thương hiệu")]
    public int Id { get; set; }

    [Display(Name = "Tên thương hiệu")]
    [Required(ErrorMessage = "Vui lòng nhập tên thương hiệu")]
    [StringLength(100, ErrorMessage = "Tên thương hiệu không được quá 100 ký tự")]
    public string Name { get; set; } = null!;

    [Display(Name = "Quốc gia / Xuất xứ")]
    [StringLength(100, ErrorMessage = "Tên quốc gia không được quá 100 ký tự")]
    // Lưu ý: Không cần [Required] vì trong class gốc Country là "string?" (có thể null)
    public string? Country { get; set; }

    [Display(Name = "Danh sách sản phẩm")]
    public virtual ICollection<Product> Products { get; set; }
}
