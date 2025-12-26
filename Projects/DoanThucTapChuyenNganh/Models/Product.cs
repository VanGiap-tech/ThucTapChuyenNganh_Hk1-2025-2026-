using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoanThucTapChuyenNganh.Models;

public partial class Product
{
    [Display(Name = "Mã sản phẩm")]
    public int Id { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
    [StringLength(200, ErrorMessage = "Tên sản phẩm không được quá 200 ký tự")]
    public string Name { get; set; } = null!;

    [Display(Name = "Mô tả sản phẩm")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Display(Name = "Giá bán")]
    [Required(ErrorMessage = "Vui lòng nhập giá bán")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn hoặc bằng 0")]
    [DisplayFormat(DataFormatString = "{0:#,##0} VNĐ")]
    public decimal Price { get; set; }

    [Display(Name = "Hình ảnh")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Số lượng tồn")]
    [Required(ErrorMessage = "Vui lòng nhập số lượng")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
    public int StockQuantity { get; set; }

    [Display(Name = "Khối lượng (kg)")]
    [Range(0, double.MaxValue, ErrorMessage = "Khối lượng phải lớn hơn 0")]
    public double? Weight { get; set; }

    [Display(Name = "Chất liệu")]
    public string? Material { get; set; }

    [Display(Name = "Danh mục")]
    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    public int CategoryId { get; set; }

    [Display(Name = "Thương hiệu")]
    [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
