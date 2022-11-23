using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Test.Model;
using Test.Model.DataBase;

namespace ASPProject.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Autorization()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Autorization(string login, string password, string ReturnUrl)
		{
			using (SchoolContext db = new SchoolContext()){
				var student = db.Students
					.Join(db.StudentsLogins, s => s.Id, sl => sl.StudentsId, (s, sl) => new {Students = s, StudentsLogins = sl})
					.FirstOrDefault(s => s.StudentsLogins.Login == login && s.StudentsLogins.Password == password);
				var teacher = db.Teachers
					.Join(db.TeachersLogins, t => t.Id, tl => tl.TeachersId, (s, sl) => new { Teachers = s, TeachersLogins = sl })
					.FirstOrDefault(t => t.TeachersLogins.Login == login && t.TeachersLogins.Password == password);

				if (login == "admin" && password == "admin")
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, "Admin")
					};
					var claimsIdentity = new ClaimsIdentity(claims, "Login");
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

					TempData["Role"] = "Admin";
					TempData["FullName"] = "Admin";
					TempData["Login"] = "Admin";

					return Redirect(ReturnUrl == null ? "/Home/PageDB" : ReturnUrl);
				}
				else if (student.StudentsLogins.Login == login && student.StudentsLogins.Password == password)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, student.Students.FullName)
					};
					var claimsIdentity = new ClaimsIdentity(claims, "Login");
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

					TempData["Role"] = "Student";
					TempData["FullName"] = student.Students.FullName;
					TempData["Login"] = student.StudentsLogins.Login;

					return Redirect(ReturnUrl == null ? "/Home/PageDB" : ReturnUrl);
				}
				else if (teacher.TeachersLogins.Login == login && teacher.TeachersLogins.Password == password)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, teacher.Teachers.FullName)
					};
					var claimsIdentity = new ClaimsIdentity(claims, "Login");
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

					TempData["Role"] = "Teacher";
					TempData["FullName"] = teacher.Teachers.FullName;
					TempData["Login"] = teacher.TeachersLogins.Login;

					return Redirect(ReturnUrl == null ? "/Home/PageDB" : ReturnUrl);
				}
				else
				{
					return View();
				}
			}	
		}
		public IActionResult PageDB()
		{
			ViewBag.Role = TempData["Role"];
			ViewBag.FullName = TempData["FullName"];
			ViewBag.Login = TempData["Login"];
			return View();
		}
		public IActionResult AdminDB()
		{
			return PartialView();
		}
		public IActionResult TeacherDB()
		{
			return PartialView();
		}
		public IActionResult StudentDB()
		{
			return PartialView();
		}
	}
}