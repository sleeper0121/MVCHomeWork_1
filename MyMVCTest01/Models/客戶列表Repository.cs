using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MyMVCTest01.Models
{   
	public  class 客戶列表Repository : EFRepository<客戶列表>, I客戶列表Repository
	{
    }

	public  interface I客戶列表Repository : IRepository<客戶列表>
	{

	}
}