using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowShareBlogEngine.Models
{
    public class RowSerialized
    {
        public Guid Id { get; set; }
        public string ListId { get; set; }
        public Dictionary<string, string> Values { get; set; }
    }
}
