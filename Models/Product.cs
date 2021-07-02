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

        [Required]
        [StringLength(50)]
        public string product_size { get; set; }

        [Required]
        [StringLength(100)]
        public string product_origin { get; set; }

        public bool hot_product { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }
    }
}
