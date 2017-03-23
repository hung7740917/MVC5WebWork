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

        public IQueryable<客戶資料> Get篩選後客戶資料(string SortBy, string search)
        {
            var data = this.All().AsQueryable();

            data = this.SortBy(SortBy);

            if (!String.IsNullOrEmpty(search))
            {
                data = data.Where(p => p.客戶名稱.Contains(search));
            }


            return data;
        }

        public override void Delete(客戶資料 entity)
        {
            entity.IsDelete = true;
        }

        public IQueryable<客戶資料> SortBy(string SortBy)
        {

            if (SortBy == "+客戶名稱")
            {
                return this.All().OrderBy(p => p.客戶名稱);
            }
            else if (SortBy == "-客戶名稱")
            {
                return this.All().OrderByDescending(p => p.客戶名稱);
            }
            if (SortBy == "+統一編號")
            {
                return this.All().OrderBy(p => p.統一編號);
            }
            else if (SortBy == "-統一編號")
            {
                return this.All().OrderByDescending(p => p.統一編號);
            }
            else if (SortBy == "+電話")
            {
                return this.All().OrderBy(p => p.電話);
            }
            else if (SortBy == "-電話")
            {
                return this.All().OrderByDescending(p => p.電話);
            }
            else if (SortBy == "+傳真")
            {
                return this.All().OrderBy(p => p.傳真);
            }
            else if (SortBy == "-傳真")
            {
                return this.All().OrderByDescending(p => p.傳真);
            }
            else if (SortBy == "+地址")
            {
                return this.All().OrderBy(p => p.地址);
            }
            else if (SortBy == "-地址")
            {
                return this.All().OrderByDescending(p => p.地址);
            }
            else
            {
                return this.All().OrderBy(p => p.Id);
            }
        }

        public bool AccountCheck(string 帳號,int id)
        {
            var data = this.All().FirstOrDefault(p => p.帳號 == 帳號);
            if(data != null && data.Id != id)
            {
                return true;
            }
            return false;
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