namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("News")]
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Ico { get; set; }
        public string Image { get; set; }
        public string Describe { get; set; }
    }
}
