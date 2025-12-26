using DoanThucTapChuyenNganh.Areas.Admin.Models;
using DoanThucTapChuyenNganh.Models;
using DoanThucTapChuyenNganh.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DoanThucTapChuyenNganh.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class UserController : Controller
    {

        private ApplicationDbContext db=new ApplicationDbContext(); 
        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Hiển thị form thêm mới
        [HttpGet]
        public IActionResult formAddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Đổi tên tham số thành 'model' cho dễ phân biệt với Entity 'User'
        public IActionResult AddUser(UserViewModel model)
        {
            // 1. Kiểm tra Validation cơ bản (Required, Length...)
            if (ModelState.IsValid)
            {
                // --- CHECK TRÙNG LẶP ---
                // Kiểm tra Email
                var checkEmail = db.Users.FirstOrDefault(x => x.Email == model.Email);
                if (checkEmail != null)
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng!");
                    return View("formAddUser", model); // Trả lại model để giữ dữ liệu cũ
                }

                // Kiểm tra UserName
                var checkUser = db.Users.FirstOrDefault(x => x.UserName == model.UserName);
                if (checkUser != null)
                {
                    ModelState.AddModelError("UserName", "Tên đăng nhập này đã tồn tại!");
                    return View("formAddUser", model); // Trả lại model để giữ dữ liệu cũ
                }

                // --- MAPPING (CHUYỂN ĐỔI) TỪ VIEWMODEL SANG ENTITY ---
                // Đây là bước quan trọng nhất mà bạn bị thiếu
                var newUser = new User(); // Tạo đối tượng User của Database

                newUser.UserName = model.UserName;
                newUser.FullName = model.FullName;
                newUser.Email = model.Email;
                newUser.Phone = model.PhoneNumber; // Hoặc model.Phone tùy bạn đặt
                newUser.IsAdmin = model.IsAdmin; // Gán quyền Admin từ Checkbox
                newUser.Address = model.Address;
                // --- XỬ LÝ MẬT KHẨU ---
                // Lấy Password thô từ Model -> Mã hóa -> Gán vào PasswordHash của Entity
                // Lưu ý: Dùng model.Password (người dùng nhập) chứ không phải PasswordHash
                newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // --- THIẾT LẬP MẶC ĐỊNH ---
                newUser.CreatedAt = DateTime.Now;
                newUser.IsLocked = false;

                // --- LƯU VÀO DB ---
                try
                {
                    db.Users.Add(newUser); // Thêm Entity (newUser) chứ không phải model
                    db.SaveChanges();

                    // Thành công thì quay về danh sách hoặc hiện thông báo
                    // TempData["Success"] = "Thêm mới thành công";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Nếu lỗi DB
                    ModelState.AddModelError("", "Lỗi lưu dữ liệu: " + ex.Message);
                    return View("formAddUser", model);
                }
            }

            // Nếu Validation cơ bản sai (chưa nhập, sai định dạng...)
            return View("formAddUser", model);
        }

        [HttpGet]
        public IActionResult formEditUser(int id)
        {
            var a= db.Users.Find(id);
            return View(a);

        }
        [HttpPost]
        public IActionResult EditUser(User b) {

            User a = db.Users.Find(b.Id);
            if (a != null)
            {
                a.IsAdmin = b.IsAdmin;
                a.Email = b.Email;
                a.Phone = b.Phone;
                a.FullName = b.FullName;
                a.IsLocked = b.IsLocked;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("!Mã số nhà sản xuất chưa có");
            }
        }
        public IActionResult ToggleBlock(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                // Đảo ngược trạng thái: Nếu đang khóa thì mở, đang mở thì khóa
                user.IsLocked = !user.IsLocked;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult formRemoveUser(int id)
        {
            // Tìm người dùng theo id
            var user = db.Users.Find(id);
            if (user == null)
            {
                return Content("Không tìm thấy người dùng này.");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult RemoveUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                try
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Trường hợp người dùng này đã có đơn hàng (Orders), SQL sẽ chặn không cho xóa
                    // Lúc này nên thông báo lỗi hoặc chuyển hướng về trang lỗi
                    return Content("Không thể xóa tài khoản này vì họ đã có dữ liệu đơn hàng liên quan! Hãy dùng chức năng Khóa tài khoản thay vì Xóa.");
                }
            }
            else
            {
                return Content("!Mã số người dùng này chưa có. Không xóa được");
            }
        }
    }
}
