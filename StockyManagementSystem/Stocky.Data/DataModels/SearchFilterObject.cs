using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Data
{
    public class SearchFilterObject
    {
        private string _ObjectID;
      
        public string ObjectID
        {
            get { return _ObjectID; }
            set
            {
                _ObjectID = value == "" ? null : value; 
            }
        }
        public string ObjectName { get; set; } = null;
        public string ObjectDescription { get; set; } = null;
        public DateTime ObjectCreatedFrom { get; set; } = DateTime.Parse("01/01/2010");
        public DateTime ObjectCreatedTo { get; set; } = DateTime.Now.AddDays(1);
        public int RecordsToReturn { get; set; } = 50;
        public object ExtraSearchObject1 { get; set; } = null;
    }
}
