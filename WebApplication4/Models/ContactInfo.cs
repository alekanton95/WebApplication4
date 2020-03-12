namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("ContactInfo")]
    public partial class ContactInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Введите корректный номер телефона")]
        //[RegularExpression(@"/^([0-9]{0,11})?(\+[0-9]{1,3})?(\([0-9]{1,3})?(\)[0-9]{1})?([-0-9]{0,8})?([0-9]{0,1})?$/", ErrorMessage = "Введите корректный номер телефона")]
        //[Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введите корректный e-mail")]
        //[RegularExpression(@"/^(?!.*@.*@.*$)(?!.*@.*\-\-.*\..*$)(?!.*@.*\-\..*$)(?!.*@.*\-$)(.*@.+(\..{1,11})?)$/", ErrorMessage = "Введите корректный e-mail")]
        //[EmailAddress(ErrorMessage = "Введите корректный e-mail")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Skype")]
        public string Skype { get; set; }

        [StringLength(500)]
        [Display(Name = "Другое")]
        public string Other { get; set; }

        public int? ContactListId { get; set; }

        public virtual ContactList ContactList { get; set; }
    }
}
