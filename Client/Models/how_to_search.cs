using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    class how_to_search
    {
        public string value { get; set; }
        public string key { get; set; }
        
        public how_to_search() { }
        public how_to_search(string value, string key)
        {
            this.value = value;
            this.key = key;
        }
    }
}
