using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC5WebWork.Models.ValidationAttributes
{
    public class 手機格式驗證Attribute : DataTypeAttribute
    {
        public 手機格式驗證Attribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            string str = value.ToString();

            var validation = Regex.IsMatch(str, @"\d{4}-\d{6}");

            if (validation)
            {
                return true;
            }

            return false;
        }
    }
}