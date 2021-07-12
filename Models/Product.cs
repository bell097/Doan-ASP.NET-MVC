namespace Doan_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            Image_detail = new HashSet<Image_detail>();
        }

        [Key]
        public int product_id { get; set; }

        public int category_id { get; set; }

        [Required]
        [StringLength(255)]
        public string product_name { get; set; }

        [Required]
        [StringLength(255)]
        public string product_image { get; set; }

        public long product_price { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string product_description { get; set; }

        public int product_quantity { get; set; }

        public int brand_id { get; set; }

        [StringLength(50)]
        public string product_size { get; set; }

        public int? origin_id { get; set; }

        public bool hot_product { get; set; }

        public int? sale_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image_detail> Image_detail { get; set; }

        public virtual Origin Origin { get; set; }

        public virtual sale sale { get; set; }
        public long productsale
        {
            get
            {
                if (sale_id != 0 && sale_id != null)
                {
                    return (long)(product_price - product_price * sale.sale_name / 100);
                }
                else
                    return 0;
            }
        }
    }
}
