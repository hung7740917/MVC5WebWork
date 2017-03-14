using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5WebWork.Models
{   
	public  class 客戶清單檢視表Repository : EFRepository<客戶清單檢視表>, I客戶清單檢視表Repository
	{

	}

	public  interface I客戶清單檢視表Repository : IRepository<客戶清單檢視表>
	{

	}
}