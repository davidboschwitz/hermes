using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hermes.Server
{

    public  class DatabaseItem
    {
        [Key]
        public Guid MessageID { get; set; }

        public Guid SenderID { get; set; }
        public Guid RecipientID { get; set; }

        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }

        public string MessageNamespace { get; set; }
        public string MessageName { get; set; }
    }

    public class ItemDbContext : DbContext
    {
        public DbSet<DatabaseItem> DatabaseItems { get; set;}
    }

        class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
