namespace MyMVCTest01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new 客戶資料Entities();


            if (this.Id==0)
            {
                if (db.客戶聯絡人.Where( p => p.客戶Id ==this.客戶Id  &&  p.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email重複",new string[] { "Email"});
                }
            }
            else
            {
                if (db.客戶聯絡人.Where(p => p.Id != this.Id  &&  p.客戶Id == this.客戶Id && this.Email == p.Email).Any())
                {
                    yield return new ValidationResult("Email重複", new string[] { "Email" });
                }
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
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }


        //[CheckPhoneFormat(ErrorMessage = " Phone 格式不對")]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [RegularExpression(@"\d{4}-\d{6}",ErrorMessage = "Phone 格式不對")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        
        public string 電話 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }

    //internal class CheckPhoneFormatAttribute : DataTypeAttribute
    //{
    //    public CheckPhoneFormatAttribute():base(DataType.Text)
    //    {
    //    }

    //    public override bool IsValid(object value)
    //    {

    //        string pattern = @"\d{4}-\d{6}";
    //        var phone = (string)value;

    //        if (string.IsNullOrEmpty(phone))
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            Regex re = new Regex(pattern, RegexOptions.IgnoreCase);
    //            return re.IsMatch(phone) ? true : false;
    //        }
    //    }
    //}




    //internal class CheckDuplicateAttribute : DataTypeAttribute
    //{
    //    public string OtherProperty { get; private set; }
    //    public CheckDuplicateAttribute(string other) : base(DataType.Text)
    //    {
    //        OtherProperty = other;
    //    }
    //    protected override ValidationResult IsValid(object value , ValidationContext validationContext)
    //    {
    //        var email = (string)value;

    //        var property = validationContext.ObjectType.GetProperty(OtherProperty);
    //        var otherValue = property.GetValue(validationContext.ObjectInstance, null);

    //        //using (客戶資料Entities db = new 客戶資料Entities())
    //        //{
    //        //    var a = db.客戶聯絡人.Where(p => p.Email == email).FirstOrDefault() == null ? true : false;
    //        //    return a;
    //        //}

    //        using ( 客戶資料Entities db = new 客戶資料Entities())
    //        {
    //            var contacters = db.客戶聯絡人.Where(p => p.客戶Id == (int)otherValue);

    //            foreach (var item in contacters)
    //            {
    //                if (item.Email == email)
    //                {
    //                    return  new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    //                }
    //            }
    //        }
    //        return null;
    //    }





    //}
}
