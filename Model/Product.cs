namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public class Product : BaseEntity
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Ico { get; set; }
        public string Image { get; set; }
        public string Describe { get; set; }
    }
}
