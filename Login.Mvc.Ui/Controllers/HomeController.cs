using Login.BizBase.Models;
using Login.BizBase.ViewModels;
using Login.Mvc.Ui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Login.Mvc.Ui.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserViewModel userInfo = new UserViewModel();
            return View(userInfo);
        }
        public static readonly Uri _baseAddress = new Uri("https://localhost:44313/");

        [HttpPost]
        public JsonResult ValidLoginInfo(UserViewModel userViewModel)
        {
            
            UserInfo userInfo = new UserInfo();
            ResultViewModel resultViewModel = new ResultViewModel();
            try
            {
                userInfo = ValidateUserInfo(userViewModel);

                if (userInfo.IsValidate)
                {
                    Uri postAddress = new Uri(_baseAddress, "/api/Login");

                    using (var httpClient = new HttpClient())
                    {
                        var response = httpClient.PostAsJsonAsync(postAddress, userInfo).Result;

                        if (!response.IsSuccessStatusCode)
                        {
                            resultViewModel.IsSuccess = false;
                            resultViewModel.Msg = "Login Error(Home-001)";
                        }
                        else
                        {
                            Uri getAddress = new Uri(_baseAddress, "/api/Login/?account=" + userInfo.Account);
                            response = httpClient.GetAsync(getAddress).Result;

                            if (response.IsSuccessStatusCode)
                                resultViewModel = response.Content.ReadAsAsync<ResultViewModel>().Result;
                        }
                        
                    }
                }
                else
                {
                    resultViewModel.IsSuccess = false;
                    resultViewModel.Msg = "Login Error(Home-002)";
                }
            }
            catch(Exception ex)
            {
                resultViewModel.IsSuccess = false;
                resultViewModel.Msg = "Login Error(Home-003)";
            }
            

            return Json(new { result  = resultViewModel } );

        }

        public UserInfo ValidateUserInfo(UserViewModel userViewModel)
        {
            UserInfo userInfo = new UserInfo();
            if (!string.IsNullOrEmpty(userViewModel.UserName) && !string.IsNullOrEmpty(userViewModel.UserPassword))
            {
                userInfo.Account = userViewModel.UserName;
                userInfo.Password = userViewModel.UserPassword;
                userInfo.IsValidate = true;
            }
            else
                userInfo.IsValidate = false;

            return userInfo;
        }
    }
}