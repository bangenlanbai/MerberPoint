using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLB.MerberPoint.Common
{
    public class EnumHelper
    {
        /// <summary>
        /// 枚举转字典集合
        /// </summary>
        /// <typeparam name="T">枚举类名称</typeparam>
        /// <param name="keyDefault">默认key值</param>
        /// <param name="valueDefault">默认value值</param>
        /// <returns>返回生成的字典集合</returns>
        public static Dictionary<string ,object> EnumListDic<T>(string keyDefault,string valueDefault = "")
        {
            Dictionary<string, object> dicEnum = new Dictionary<string, object>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrWhiteSpace(keyDefault))
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            string[] fieldstrs = Enum.GetNames(enumType);
            foreach (var item in fieldstrs)
            {
                string description = string.Empty;
                var field = enumType.GetField(item);
                object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;//属性描述
                }
                else
                {
                    description = item;
                }
                dicEnum.Add(description, (int)Enum.Parse(enumType, item));
            }
            return dicEnum;

        }
    }
}
