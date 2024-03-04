using evoting.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace evoting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SqlHelper _sqlHelper;

        public HomeController(ILogger<HomeController> logger, SqlHelper sqlHelper)
        {
            _logger = logger;
            _sqlHelper = sqlHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                string query = "Select vusername,vdisplayname from [User] where vusername=@username and HASHBYTES('SHA1',@password) = vpassword";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@username", model.Username));
                parameters.Add(new SqlParameter("@password", model.Password));

                DataSet ds = _sqlHelper.ExecuteDataset(query, parameters, CommandType.Text);
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0) 
                {
                    //user valid


                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    //redirect to secure page
                    return RedirectToAction("choosehouse");

                }
                else
                {
                    //invalid
                    TempData["ErrorMessage"] = "Login Failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                string query = "insert into [User](vusername,vdisplayname,vpassword)Values(@username,@displayname,HASHBYTES('SHA1',@password))";
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(new SqlParameter("@username", model.Username));
                p.Add(new SqlParameter("@displayname", model.DisplayName));
                p.Add(new SqlParameter("@password", model.Password));

                _sqlHelper.ExecuteNonQuery(query, p, System.Data.CommandType.Text);

                return RedirectToAction("Login");
            }
            return View("_Register");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new  { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult choosehouse()
        {
            return View();
        }
    }
}



