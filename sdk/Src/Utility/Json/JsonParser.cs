using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NimUtility.Json
{
    public class JsonParser
    {
        /// <summary>
        /// 依据对象反序列化json字串;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            try
            {
                if(!string.IsNullOrEmpty(json))
                {
                    var setting = new JsonSerializerSettings();
                    setting.NullValueHandling = NullValueHandling.Ignore;
                    setting.Converters.Add(new JExtConverter());
                    T ret = JsonConvert.DeserializeObject<T>(json, setting);
                    return ret;
                }
            }
            catch (JsonException jsonException)
            {
                System.Diagnostics.Debug.WriteLine("Deserialize JsonException:" + jsonException.Message);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("JsonParser Deserialize error:\n" + e.Message + "Json string: " + json);
            }
            return default(T);
        }

        /// <summary>
        ///  反序列化JSON对象为经典的字典模式
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IDictionary<string, object> FromJson(string json)
        {
            try
            {
                var setting = new JsonSerializerSettings();
                setting.NullValueHandling = NullValueHandling.Ignore;
                setting.Converters.Add(new JExtConverter());
                setting.Converters.Add(new JsonKeyValuePairConverter());
                var ret = JsonConvert.DeserializeObject<IDictionary<string, object>>(json, setting);
                return ret;
            }
            catch (JsonException jsonException)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("JsonParser Deserialize error:{0}\nJson string:{1}", jsonException.Message, json));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("JsonParser Deserialize error:{0}\nJson string:{1}", e.Message, json));
            }
            return null;
        }

        /// <summary>
        /// 反序列化对象;
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object Deserialize(string json)
        {
            return Deserialize<object>(json);
        }

        public static object DeserializeObject(string json)
        {
            return Deserialize(json);
        }

        /// <summary>
        /// Deserializes the JSON to the given anonymous type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="anonymousType"></param>
        /// <returns></returns>
        public static T DeserializeAnonymousType<T>(string value, T anonymousType)
        {
            return JsonConvert.DeserializeAnonymousType(value, anonymousType);
        }

        /// <summary>
        /// 序列化对象;
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj,bool ignoreDefaultValue = false)
        {
            if(obj != null)
            {
                JsonSerializerSettings setting = new JsonSerializerSettings();
                setting.NullValueHandling = NullValueHandling.Ignore;
                if(ignoreDefaultValue)
                {
                    setting.DefaultValueHandling = DefaultValueHandling.Ignore;
                }
                setting.Converters.Add(new JExtConverter());
                return JsonConvert.SerializeObject(obj, Formatting.None, setting);
            }
            return string.Empty;
        }

        public static string SerializeWithIndented(object obj)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.Converters.Add(new JExtConverter());
            return JsonConvert.SerializeObject(obj, Formatting.Indented, setting);
        }

        /// <summary>
        /// 序列化字典对象;
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        public static string ToJson(IDictionary<string, object> bag)
        {
            return Serialize(bag);
        }
    }
}
