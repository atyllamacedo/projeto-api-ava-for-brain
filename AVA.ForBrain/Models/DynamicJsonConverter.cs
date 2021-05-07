using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace AVA.ForBrain.Models
{
    public sealed class DynamicJsonConverter
    {
        public static TEntity Deserialize<TEntity>(object param)
        {
            return JsonConvert.DeserializeObject<TEntity>(param.ToString());
        }
        public static string Serialize(object param)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var jsonString = System.Text.Json.JsonSerializer.Serialize(param, options);
            return jsonString;
        }
    }
}
