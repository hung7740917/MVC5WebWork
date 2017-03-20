using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5WebWork.Models
{
    public class LoginVM : IValidatableObject
    {

        [StringLength(15, ErrorMessage = "欄位長度不得大於 15 個字元")]
        [Required]
        public string 帳號 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 密碼 { get; set; }



        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string str = repo.LoginCheck(this.帳號, this.密碼);
            if (str.Contains("密碼輸入錯誤"))
            {
                yield return new ValidationResult(str, new string[] { "Password" });
                yield break;
            }
            else if (str.Contains("查無此帳號"))
            {
                yield return new ValidationResult(str, new string[] { "Account" });
                yield break;
            }
            else
            {
                yield return ValidationResult.Success;
            }

        }
    }
}