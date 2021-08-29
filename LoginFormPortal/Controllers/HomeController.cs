using LoginFormPortal.Models;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LoginFormPortal.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            UserInfo userInfo = new UserInfo();
            return View(userInfo);
        }
        public static readonly Uri _baseAddress = new Uri("https://localhost:44346/");

        [HttpPost]
        public JsonResult ValidLoginInfo(UserInfo userinfo)
        {
            bool createSuccess = true;
            Uri address = new Uri(_baseAddress, "/api/Login");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsJsonAsync(address, userinfo).Result;

                if (!response.IsSuccessStatusCode)
                    createSuccess = false;
            }

            return Json(createSuccess, JsonRequestBehavior.AllowGet);

            //var display = Userloginvalues().Where(m => m.Account == userinfo.Account && m.Password == userinfo.Password).FirstOrDefault();
            //if (display != null)
            //{
            //    ViewBag.Status = "CORRECT UserNAme and Password";
            //}
            //else
            //{
            //    ViewBag.Status = "INCORRECT UserName or Password";
            //}

        }




        public List<UserInfo> Userloginvalues()
        {
            List<UserInfo> objModel = new List<UserInfo>();
            objModel.Add(new UserInfo { Account = "user1", Password = "password1" });
            objModel.Add(new UserInfo { Account = "user2", Password = "password2" });
            objModel.Add(new UserInfo { Account = "user3", Password = "password3" });
            objModel.Add(new UserInfo { Account = "user4", Password = "password4" });
            objModel.Add(new UserInfo { Account = "user5", Password = "password5" });
            return objModel;
        }

        public class AuthManager
        {
            //登入
            public void SignIn(UserInfo user)
            {
                //新增表單驗證用的票證
                var ticket = new FormsAuthenticationTicket(1,   //版本
                                                                //使用者名稱
                    user.UserName,
                    //發行時間
                    DateTime.Now,
                    //有效期限
                    DateTime.Now.AddMinutes(60),
                    //是否將 Cookie 設定成 Session Cookie，如果是則會在瀏覽器關閉後移除
                    false,
                    //將要記錄的使用者資訊轉換為 JSON 字串
                    JsonConvert.SerializeObject(user),
                    //儲存 Cookie 的路徑
                    FormsAuthentication.FormsCookiePath);

                //將 Ticket 加密
                var encTicket = FormsAuthentication.Encrypt(ticket);

                //將 Ticket 寫入 Cookie
                System.Web.HttpContext.Current.Response.Cookies.Add(
                    new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }


            //取得使用者資訊
            public UserInfo GetUser()
            {
                //取得 ASP.NET 使用者
                var user = System.Web.HttpContext.Current.User;

                //是否通過驗證
                if (user?.Identity?.IsAuthenticated == true)
                {
                    //取得 FormsIdentity
                    var identity = (FormsIdentity)user.Identity;

                    //取得 FormsAuthenticationTicket
                    var ticket = identity.Ticket;

                    //將 Ticket 內的 UserData 解析回 User 物件
                    return JsonConvert.DeserializeObject<UserInfo>(ticket.UserData);
                }
                return null;
            }


        }

        public ActionResult Error()
        {
            return View();
        }
    }
}