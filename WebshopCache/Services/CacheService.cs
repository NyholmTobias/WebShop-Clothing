using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;

namespace WebshopCache.Services
{
    public class CacheService : ICacheService
    {
        /// <summary>
        /// Från ByteArray till List av Int
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// 

        public List<int> DeserialiseToListOfIntsAsync(byte[] bytes)
        {
            var asString = Encoding.Unicode.GetString(bytes);
            return JsonConvert.DeserializeObject<List<int>>(asString);
            
        }
        /// <summary>
        /// Från ByteArray till List av string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public List<LineItemResponse> DeserialiseToListOfLineItems(byte[] bytes)
        {
            var asString = Encoding.Unicode.GetString(bytes);
            return JsonConvert.DeserializeObject<List<LineItemResponse>>(asString);

        }


        /// <summary>
        /// Från List av Ints till ByteArray
        /// </summary>
        /// <param name="listOfInts"></param>
        /// <returns></returns>
        public byte[] SerialiseListOfLineItemsToBytes(List<LineItemResponse> listOfLineItems)
        {
            var asString = JsonConvert.SerializeObject(listOfLineItems, SerializerSettings);
            return Encoding.Unicode.GetBytes(asString);
        }

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
        };

    }
}
