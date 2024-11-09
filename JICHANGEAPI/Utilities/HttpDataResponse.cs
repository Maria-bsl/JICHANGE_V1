using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Web;

namespace JichangeApi.Utilities
{
    public class HttpDataResponse
    {
        public JsonObject error { get; set; }
        public JsonObject response { get; set; }
    }
}