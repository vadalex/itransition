using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Lucene.Net.Documents;
using Entities.Attributes;

namespace Entities
{
    [DataContract]
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        [LuceneSearch(Field.Index.NOT_ANALYZED, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int CustomerID { get; set; }
        [LuceneSearch(Field.Index.ANALYZED)]
        [StringLength(100)]
        [Required]
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [StringLength(150)]
        [LuceneSearch(Field.Index.ANALYZED)]
        [DataMember]
        public string Address { get; set; }        
        [StringLength(20)]
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
