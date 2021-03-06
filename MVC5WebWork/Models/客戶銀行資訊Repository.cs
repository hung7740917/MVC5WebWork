using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5WebWork.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => false == p.IsDelete);
        }

        public IQueryable<客戶銀行資訊> Get篩選後客戶銀行資訊(string SortBy, string search)
        {
            var 客戶銀行資訊 = this.All().Include(客 => 客.客戶資料);

            客戶銀行資訊 = this.SortBy(SortBy);

            if (!String.IsNullOrEmpty(search))
            {
                客戶銀行資訊 = 客戶銀行資訊.Where(p => p.帳戶名稱.Contains(search));
            }


            return 客戶銀行資訊;
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.IsDelete = true;
        }

        public IQueryable<客戶銀行資訊> SortBy(string SortBy)
        {

            if (SortBy == "+銀行名稱")
            {
                return this.All().OrderBy(p => p.銀行名稱);
            }
            else if (SortBy == "-銀行名稱")
            {
                return this.All().OrderByDescending(p => p.銀行名稱);
            }
            if (SortBy == "+銀行代碼")
            {
                return this.All().OrderBy(p => p.銀行代碼);
            }
            else if (SortBy == "-銀行代碼")
            {
                return this.All().OrderByDescending(p => p.銀行代碼);
            }
            else if (SortBy == "+分行代碼")
            {
                return this.All().OrderBy(p => p.分行代碼);
            }
            else if (SortBy == "-分行代碼")
            {
                return this.All().OrderByDescending(p => p.分行代碼);
            }
            else if (SortBy == "+帳戶名稱")
            {
                return this.All().OrderBy(p => p.帳戶名稱);
            }
            else if (SortBy == "-帳戶名稱")
            {
                return this.All().OrderByDescending(p => p.帳戶名稱);
            }
            else if (SortBy == "+帳戶號碼")
            {
                return this.All().OrderBy(p => p.帳戶號碼);
            }
            else if (SortBy == "-帳戶號碼")
            {
                return this.All().OrderByDescending(p => p.帳戶號碼);
            }
            else if (SortBy == "+客戶名稱")
            {
                return this.All().OrderBy(p => p.客戶資料.客戶名稱);
            }
            else if (SortBy == "-客戶名稱")
            {
                return this.All().OrderByDescending(p => p.客戶資料.客戶名稱);
            }
            else
            {
                return this.All().OrderBy(p => p.Id);
            }
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}