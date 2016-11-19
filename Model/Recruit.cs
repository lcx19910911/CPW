namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Recruit")]
    public class Recruit : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RequireMent { get; set; }
        public string Describe { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
