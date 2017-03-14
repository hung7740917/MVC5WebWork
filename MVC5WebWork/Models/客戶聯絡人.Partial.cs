namespace MVC5WebWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {

        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (repo.EmailCheck(this.Id,this.客戶Id, this.Email))
            {
                yield return new ValidationResult("此客戶聯絡人Eamil已存在", new string[] { "Email" });
                yield break;
            }
            yield return ValidationResult.Success;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [手機格式驗證(ErrorMessage ="手機格式輸入錯誤")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
