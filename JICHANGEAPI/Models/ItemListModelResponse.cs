using System.Collections.Generic;

namespace JichangeApi.Models
{
    public class ItemListModelResponse
    {
        public List<ItemListModel> Response { get; set; }
        public string Message { get; set; }
    }
}