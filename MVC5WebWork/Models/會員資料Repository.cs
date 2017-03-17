using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5WebWork.Models
{
    public class 會員資料Repository : EFRepository<會員資料>, I會員資料Repository
    {
        public 會員資料 Find(string account)
        {
            return this.All().FirstOrDefault(p => p.Account == account);
        }

        public string LoginCheck(string account, string password)
        {

            var data = this.All().FirstOrDefault(p => p.Account == account);
            if (data != null)
            {
                if (data.Password == password)
                {
                    return "確認OK";
                }
                else
                {
                    return "密碼輸入錯誤";
                }
            }
            else
            {
                return "查無此帳號";
            }

        }
    }

    public interface I會員資料Repository : IRepository<會員資料>
    {

    }
}