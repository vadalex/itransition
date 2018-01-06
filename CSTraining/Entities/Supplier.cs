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
    public class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }
        [LuceneSearch(Field.Index.NOT_ANALYZED, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int SupplierID { get; set; }
        [LuceneSearch(Field.Index.ANALYZED)]
        [StringLength(100)]
        [Required]
        [DataMember]
        public string Name { get; set; }
        [StringLength(20)]
        [DataMember]
        public string Phone { get; set; }
        [LuceneSearch(Field.Index.ANALYZED)]
        [DataMember]
        public string Details { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
