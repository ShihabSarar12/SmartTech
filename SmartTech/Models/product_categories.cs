//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace SmartTech.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product_categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product_categories()
        {
            this.products = new HashSet<product>();
        }
    
        public long id { get; set; }

        [Required]
        [StringLength(100)]
        public string category_name { get; set; }

        [Required]
        [StringLength(100)]
        public string category_image { get; set; }

        [Required]
        [StringLength(100)]
        public string category_icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
