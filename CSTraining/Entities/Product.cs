using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Entities.Attributes;

namespace Entities
{

    [DataContract]
    public class Product
    {
        public Product() 
        {
            Orders = new HashSet<Order>();
        }
        [LuceneSearch(Field.Index.NOT_ANALYZED, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int ProductID { get; set; }
        [LuceneSearch(Field.Index.ANALYZED)]
        [StringLength(100)]
        [Required]
        [DataMember]
        public string Name { get; set; }
        [LuceneSearch(Field.Index.NO)]
        [DataMember]
        public double Price { get; set; }
        [LuceneSearch(Field.Index.ANALYZED)]
        [StringLength(30)]
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
