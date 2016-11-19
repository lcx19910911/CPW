//------------------------------------------------------------------------------
namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductType")]
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        public Nullable<int> Sort { get; set; }
    }
}
