namespace MyMVCTest01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶列表MetaData))]
    public partial class 客戶列表
    {
    }
    
    public partial class 客戶列表MetaData
    {
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
        public string 客戶分類 { get; set; }
    }
}
