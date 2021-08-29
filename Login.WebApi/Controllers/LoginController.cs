using Login.BizBase.Models;
using Login.BizBase.Service;
using Login.BizBase.ViewModels;
using Login.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login.WebApi.Controllers
{
    [AuthAttribute]
    public class LoginController : ApiController
    {
        public string ConnectionString = ConfigurationManager.AppSettings["OraConnectionString"].ToString();

        public ResultViewModel ResultViewModel { get; set; }
        public UserInfo ApiUserInfo { get; set; }

        public ResultViewModel Get()
        {
            return ResultViewModel;
        }

        public ResultViewModel Get(string account)
        {
            if (!ApiUserInfo.Account.Any(x => x.Equals(account)))
            {
                ResultViewModel.IsSuccess = false;
                ResultViewModel.Msg = "Login Error(Login-001)";
            }
            return ResultViewModel;
        }

        public void Post([FromBody] UserInfo userInfo)
        {
            ApiUserInfo = userInfo;
            ValidateUserService validateUserService = new ValidateUserService(ConnectionString);
            ResultViewModel = validateUserService.ValidateUserAccount(userInfo);
        }

        

        #region
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
        #endregion
    }
}
