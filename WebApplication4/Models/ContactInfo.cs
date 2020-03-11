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
        [Display(Name = "�������")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "������� ���������� ����� ��������")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "������� ���������� ����� ��������")]
        [Phone(ErrorMessage = "������� ���������� ����� ��������")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "������� ���������� e-mail")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,100}", ErrorMessage = "������� ���������� e-mail")]
        [EmailAddress(ErrorMessage = "������� ���������� e-mail")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Skype")]
        public string Skype { get; set; }

        [StringLength(500)]
        [Display(Name = "������")]
        public string Other { get; set; }

        public int? ContactListId { get; set; }

        public virtual ContactList ContactList { get; set; }
    }
}
