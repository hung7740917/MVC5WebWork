using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5WebWork.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => false == p.IsDelete);
        }

        public IQueryable<客戶聯絡人> Get篩選後客戶聯絡人(string SortBy, string search)
        {
            var 客戶聯絡人 = this.All().Include(客 => 客.客戶資料).AsQueryable();
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);

            if (!String.IsNullOrEmpty(search))
            {
                客戶聯絡人 = 客戶聯絡人.Where(p => p.姓名.Contains(search));
            }

            客戶聯絡人 = this.SortBy(SortBy);

            return 客戶聯絡人;
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDelete = true;
        }

        public IQueryable<客戶聯絡人> SortBy(string SortBy)
        {

            if (SortBy == "+職稱")
            {
                return this.All().OrderBy(p => p.職稱);
            }
            else if (SortBy == "-職稱")
            {
                return this.All().OrderByDescending(p => p.職稱);
            }
            if (SortBy == "+姓名")
            {
                return this.All().OrderBy(p => p.姓名);
            }
            else if (SortBy == "-姓名")
            {
                return this.All().OrderByDescending(p => p.姓名);
            }
            else if (SortBy == "+Email")
            {
                return this.All().OrderBy(p => p.Email);
            }
            else if (SortBy == "-Email")
            {
                return this.All().OrderByDescending(p => p.Email);
            }
            else if (SortBy == "+手機")
            {
                return this.All().OrderBy(p => p.手機);
            }
            else if (SortBy == "-手機")
            {
                return this.All().OrderByDescending(p => p.手機);
            }
            else if (SortBy == "+電話")
            {
                return this.All().OrderBy(p => p.電話);
            }
            else if (SortBy == "-電話")
            {
                return this.All().OrderByDescending(p => p.電話);
            }else if (SortBy == "+客戶名稱")
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

        public bool EmailCheck(int Id,int 客戶Id, string email)
        {
            var data = this.All().Where(p => p.客戶Id == 客戶Id && p.Email == email).FirstOrDefault();

            if (data != null && data.Id != Id)
            {
                return true;
            }
            return false;
                
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}