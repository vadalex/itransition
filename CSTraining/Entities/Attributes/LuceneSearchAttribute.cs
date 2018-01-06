using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Documents;

namespace Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class LuceneSearchAttribute : Attribute
    {
        public LuceneSearchAttribute(Field.Index index)
        {
            this.Index = index;
            this.IsIdentity = false; 
        }

        public Field.Index Index  { get; set; }

        public bool IsIdentity { get; set; }
    }
}
