using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;

namespace WebshopCache.Services
{
    public interface ICacheService
    {
        List<int> DeserialiseToListOfIntsAsync(byte[] bytes);

        /// <summary>
        /// Från ByteArray till List av string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        List<LineItemResponse> DeserialiseToListOfLineItems(byte[] bytes);

        byte[] SerialiseListOfLineItemsToBytes(List<LineItemResponse> listOfLineItems);



    }
}
