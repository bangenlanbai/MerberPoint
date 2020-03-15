using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLB.MerberPoint.Common
{
    public static class JsonExtension
    {
        /// <summary>
        /// 将对象序列化为json字符串
        /// </summary>
        public static string ToJson(this object obj)
        {
           return  JsonConvert.SerializeObject(obj);
        }
       /// <summary>
       /// json字符串转对象
       /// </summary>
        public static T ToObject<T>(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            else
            {
                return default(T); 
            }
        }
    }
}
