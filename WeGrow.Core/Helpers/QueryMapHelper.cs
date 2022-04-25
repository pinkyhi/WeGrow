using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeGrow.Core.Helpers
{
    public static class QueryMapHelper
    {
        public static Dictionary<string, string> GetDictionaryFromModel<T>(T model) where T : new()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var modelType = typeof(T);
            var modelProperties = modelType.GetProperties();
            foreach(var property in modelProperties)
            {
                var value = property.GetValue(model);
                if (value != null)
                {
                    var valueType = property.PropertyType;
                    if (valueType != typeof(string) && (valueType.GetInterfaces().Contains(typeof(IEnumerable)) || valueType is IEnumerable))
                    {
                        var enumerableValue = value as IEnumerable<object>;
                        if(enumerableValue?.Count() > 0)
                        {
                            string joinedArr = string.Join(',', enumerableValue);
                            result.Add(property.Name, joinedArr);
                        }
                    }
                    else
                    {
                        result.Add(property.Name, value.ToString());
                    }
                }
            }

            return result;
        }
        public static T GetModelFromQueryUrl<T>(string url) where T : new()
        {
            Uri uri = new Uri(url);
            var keyValues = HttpUtility.ParseQueryString(uri.Query);
            return GetModelFromDictionary<T>(NameValuesToDictionary(keyValues));
        }

        public static T GetModelFromDictionary<T>(Dictionary<string, string> valuesDict) where T : new()
        {
            T result = new T();
            var modelType = typeof(T);
            var modelProperties = modelType.GetProperties();
            foreach(var property in modelProperties)
            {
                var propVal = valuesDict.FirstOrDefault(x => string.Equals(property.Name, x.Key, StringComparison.OrdinalIgnoreCase)).Value;
                if (property.PropertyType?.GetInterfaces().Contains(typeof(IEnumerable<>)) == true && string.IsNullOrWhiteSpace(propVal))
                {
                    var splittedArr = propVal.Split(',').ToList();
                    property.SetValue(result, propVal);
                }
                else if (string.IsNullOrWhiteSpace(propVal))
                {
                    property.SetValue(result, propVal);
                }
            }
            return result;
        }

        public static Dictionary<string, string> NameValuesToDictionary(NameValueCollection nameValues)
        {
            var dict = new Dictionary<string, string>();

            if (nameValues != null)
            {
                foreach (string key in nameValues.AllKeys)
                {
                    dict.Add(key, nameValues[key]);
                }
            }

            return dict;
        }
    }
}
