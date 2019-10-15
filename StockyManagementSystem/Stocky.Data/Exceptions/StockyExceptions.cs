using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Exceptions
{
    public class NoRecordException : Exception
    {
        public Type RecordTypeProperty { get; set; }
        public NoRecordException(Type RecordType):base("No matching record has been found.")
        {
            RecordTypeProperty = RecordType;
            
        }
        public NoRecordException(Type RecordType, Exception InnerException) : base("No matching record has been found.", InnerException)
        {
            RecordTypeProperty = RecordType;
        }
    }
    public class InvalidObjectException : Exception
    {
        public List<Stocky.Data.PropertyValidation> InvalidPropertys { get; set; }
        public InvalidObjectException(List<Stocky.Data.PropertyValidation> InvalidObjects) : base()
        {
            InvalidPropertys = InvalidObjects;
        }
    }
}
