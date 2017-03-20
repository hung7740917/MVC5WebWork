using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5WebWork.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => false == p.IsDelete);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.IsDelete = true;
        }

        public string LoginCheck(string account, string password)
        {

            var data = this.All().FirstOrDefault(p => p.帳號 == account);
            if (data != null)
            {
                if (data.密碼 == password)
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

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}