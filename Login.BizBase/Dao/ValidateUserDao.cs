using Login.BizBase.Models;
using Login.BizBase.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.BizBase.Dao
{
    public class ValidateUserDao
    {
        public string ConnectionString { get; set; }
        public ValidateUserDao(string conn)
        {
            ConnectionString = conn;
        }

        public UserInfo ValidateUserAccount(UserInfo userInfo)
        {
            try
            {
                string sql = "select * from User where account=:userid and password=:pwd ";

                using (OracleConnection _conn = new OracleConnection(ConnectionString))
                {
                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }

                    OracleCommand cmd = new OracleCommand(sql, _conn);
                    //依參數名稱代入參數
                    cmd.BindByName = true;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(":userid", userInfo.Account);
                    cmd.Parameters.Add(":pwd", userInfo.Password);
                    return (UserInfo)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
