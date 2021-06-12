﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeAnCNPMNC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CategoryUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryUser()
        {
            this.Employees = new HashSet<Employee>();
        }
        [NotMapped]
        public List<CategoryUser> ListCateUser { get; set; }
        [Display(Name = "Mã user: ")]
        [Required(ErrorMessage = "Mã user không được để trống ...")]
        public string IDCUser { get; set; }
        [Display(Name = "Tên loại user: ")]
        [Required(ErrorMessage = "Tên user không được để trống ...")]
        public string NameCateUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
