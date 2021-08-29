using Login.BizBase.Dao;
using Login.BizBase.Models;
using Login.BizBase.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.BizBase.Service
{
    public class ValidateUserService
    {
        public string ConnectionString { get; set; }
        public ValidateUserService(string conn)
        {
            ConnectionString = conn;
        }

        public ResultViewModel ValidateUserAccount(UserInfo userInfo)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.IsSuccess = false;

            ValidateUserDao validateUserDao = new ValidateUserDao(ConnectionString);
            var rtnUser = validateUserDao.ValidateUserAccount(userInfo);
            if(rtnUser == null)
            {
                resultViewModel.Msg = "Account not found!";
            }
            else
            {
                resultViewModel.IsSuccess = true;
                resultViewModel.Msg = "Login success!";
            }

            return resultViewModel;
        }


    }
}
