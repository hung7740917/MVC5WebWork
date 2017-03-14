using System;
using System.Linq;
using System.Collections.Generic;
	
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

        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDelete = true;
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