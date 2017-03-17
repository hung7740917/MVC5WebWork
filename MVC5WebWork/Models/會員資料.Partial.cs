namespace MVC5WebWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(會員資料MetaData))]
    public partial class 會員資料
    {
    }

    public partial class 會員資料MetaData
    {

        [StringLength(15, ErrorMessage = "欄位長度不得大於 15 個字元")]
        [Required]
        public string Account { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string Password { get; set; }

        [StringLength(15, ErrorMessage = "欄位長度不得大於 15 個字元")]
        [Required]
        public string 姓名 { get; set; }
    }
}
