namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Message")]
    public class Message : BaseEntity
    {
        public string Name { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TelePhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string ConnectPeople { get; set; }
        public string Content { get; set; }
    }
}
