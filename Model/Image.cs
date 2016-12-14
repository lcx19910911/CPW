namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Image")]
    public class Image : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Path { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        [Required]
        public System.DateTime PushTime { get; set; }
    }
}
