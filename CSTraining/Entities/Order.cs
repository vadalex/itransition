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
    public class Order
    {
        public Order()
        {
            Products = new HashSet<Product>();
            CreatedDate = DateTime.Now;
        }
        [LuceneSearch(Field.Index.NOT_ANALYZED, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public bool IsPaid { get; set; }
        [DataMember]
        public string PayerId { get; set; }
        [StringLength(20)]
        [DataMember]
        public string Status { get; set; }        
        [Display(Name="Order date")]
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [LuceneSearch(Field.Index.ANALYZED)]
        [DataMember]
        public string Details { get; set; }
        [LuceneSearch(Field.Index.NOT_ANALYZED)]
        [DataMember]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
