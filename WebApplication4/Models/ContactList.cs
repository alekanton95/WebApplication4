namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("ContactList")]
    public partial class ContactList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactList()
        {
            ContactInfoes = new HashSet<ContactInfo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "��������� �������")]
        [StringLength(100)]
        [Display(Name ="�������")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "��������� ���")]
        [StringLength(100)]
        [Display(Name = "���")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "��������")]
        public string MiddleName { get; set; }

        [StringLength(1000)]
        [Display(Name = "�����������")]
        public string Organization { get; set; }

        [StringLength(1000)]
        [Display(Name = "���������")]
        public string Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //[UIHint("Collection")]
        public virtual ICollection<ContactInfo> ContactInfoes { get; set; }
    }
}
