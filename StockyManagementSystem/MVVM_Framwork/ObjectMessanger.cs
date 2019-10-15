using System.Collections.Generic;
using System.Linq;
namespace MVVM_Framework
{
    public class ObjectMessenger
    {
        private static Dictionary<string, object> ObjectLibery = new Dictionary<string, object>();

        private static object ReturnObj;

        public void Send(string ObjectKey, object ObjectToSend)
        {
            if(ObjectLibery.ContainsKey(ObjectKey))
            {
                ObjectLibery.Remove(ObjectKey);
            }
            ObjectLibery.Add(ObjectKey, ObjectToSend);
        }

        public dynamic FindObject(string ObjectKey)
        {
            dynamic obj = ObjectLibery.Where(x => x.Key == ObjectKey).FirstOrDefault().Value;
            if (obj != null)
            {

                ReturnObj = obj;
                ObjectLibery.Remove(ObjectKey);
                return ReturnObj;               
            }
            else
            {
                return "No Object";
            }
            
        }
    }
}
