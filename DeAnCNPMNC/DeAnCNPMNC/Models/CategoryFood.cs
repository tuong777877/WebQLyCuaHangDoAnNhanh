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

    public partial class CategoryFood
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryFood()
        {
            this.Foods = new HashSet<Food>();
        }
        [NotMapped]
        public List<CategoryFood> ListCate { get; set; }
        [Display(Name = "Mã loại món ăn: ")]
        [Required(ErrorMessage = "Mã loại món ăn không được để trống ...")]
        public string IDCFood { get; set; }
        [Display(Name = "Tên loại món ăn: ")]
        [Required(ErrorMessage = "Tên loại món ăn không được để trống ...")]
        public string NameCategoryFood { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Foods { get; set; }
    }
}