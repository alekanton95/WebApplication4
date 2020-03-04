namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactInfo")]
    public partial class ContactInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [StringLength(500)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(500)]
        [Display(Name = "Skype")]
        public string Skype { get; set; }

        [StringLength(500)]
        [Display(Name = "Другое")]
        public string Other { get; set; }

        public int? ContactListId { get; set; }

        public virtual ContactList ContactList { get; set; }
    }
}
