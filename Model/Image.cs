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
        /// ����ʱ��
        /// </summary>
        [Display(Name = "����ʱ��")]
        [Required]
        public System.DateTime PushTime { get; set; }
    }
}
