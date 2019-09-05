using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pocListMerchantCache.Model.Entities
{
    [Table("Merchant")]
    public class Merchant
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public void ChangeDocument(string document)
        {
            Document = document;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
