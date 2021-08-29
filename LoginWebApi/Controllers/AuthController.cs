using LoginWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LoginWebApi.Controllers.LoginController;

namespace LoginWebApi.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private AuthManager _authManager;
        public AuthController()
        {
            _authManager = new AuthManager();
        }

        //登入
        [HttpPost]
        [Route("signIn")]
        public void SignIn(UserInfo model)
        {
            //模擬從資料庫取得資料
            if (!(model.UserId == "abc" && model.Password == "123"))
            {
                throw new Exception("登入失敗，帳號或密碼錯誤");
            }

            var user = new UserInfo
            {
                Id = 1,
                UserId = "abc",
                UserName = "小明",
                Identity = Identity.User
            };
            _authManager.SignIn(user);
        }

        //登出
        [HttpPost]
        [Route("signOut")]
        public void SignOut()
        {
            _authManager.SignOut();
        }

        //測試是否通過驗證
        [HttpPost]
        [Route("isAuthenticated")]
        public bool IsAuthenticated()
        {
            var user = _authManager.GetUser();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
    
}
