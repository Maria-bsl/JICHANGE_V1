using JichangeApi.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Web.Http;

namespace JichangeApi.Controllers.setup
{
    public class SetupBaseController : ApiController
    {
        public static string ERROR_OCCURED_ON_SERVER_MESSAGE = "An error occured on the server.";
        public static string ALREADY_EXISTS_MESSAGE = "Already exists.";
        public static string NO_DATA_FOUND_MESSAGE = "No data found.";
        public static string NOT_FOUND_MESSAGE = "Not found.";
        public static string INSERT_FAILED = "Failed to insert data into database.";

        protected HttpResponseMessage GetList<T, D>(T results)
        {
            if (results != null)
            {
                return GetSuccessResponse(results);
            }
            else
            {
                return GetNoDataFoundResponse();
            }
        }
        protected HttpResponseMessage GetServerErrorResponse(string errorMessage,HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            ServiceErrorLogsMaster serviceErrorLogsMaster = new ServiceErrorLogsMaster();
            serviceErrorLogsMaster.AddErrorLogs(errorMessage);
            var res = Request.CreateResponse(new { response = 0, message = new List<string> { SetupBaseController.ERROR_OCCURED_ON_SERVER_MESSAGE, errorMessage } });
            res.StatusCode = code;
            return res;
        }
        protected HttpResponseMessage GetAlreadyExistsErrorResponse()
        {
            return Request.CreateResponse(new { response = 0, message = new List<string> { SetupBaseController.ALREADY_EXISTS_MESSAGE } });
        }
        protected HttpResponseMessage GetInvalidModelStateResponse(List<string> errorMessages)
        {
            return Request.CreateResponse(new { response = 0, message = errorMessages });
        }
        protected HttpResponseMessage GetSuccessResponse<T>(T response)
        {
            return Request.CreateResponse(new { response = response, message = new List<string>() });
        }
        protected HttpResponseMessage GetNoDataFoundResponse()
        {
            return Request.CreateResponse(new { response = new List<string>(), message = new List<string> { SetupBaseController.NO_DATA_FOUND_MESSAGE } });
        }
        protected HttpResponseMessage GetNotFoundResponse()
        {
            return Request.CreateResponse(new { response = 0, message = new List<string> { SetupBaseController.NOT_FOUND_MESSAGE } });
        }
        protected HttpResponseMessage GetCustomErrorMessageResponse(List<string> messages, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            var res = Request.CreateResponse(new { response = 0, message = messages });
            res.StatusCode = code;
            return res;
        }

        protected HttpResponseMessage SuccessJsonResponse(JsonObject data,HttpStatusCode code = HttpStatusCode.OK)
        {
            JsonObject json = new JsonObject();
            json.Add("response", data);
            json.Add("message", new JsonArray());
            var response = GetSuccessResponse(code);
            response.Content = new StringContent(json.ToJsonString(), Encoding.UTF8, "application/json");
            return response;
        }
        protected HttpResponseMessage SuccessJsonResponse(JsonArray array, HttpStatusCode code = HttpStatusCode.OK)
        {
            JsonObject json = new JsonObject();
            json.Add("response", array);
            json.Add("message", new JsonArray());
            var response = GetSuccessResponse(code);
            response.Content = new StringContent(json.ToJsonString(), Encoding.UTF8, "application/json");
            return response;
        }
        protected List<string> ModelStateErrors()
        {
            if (ModelState.IsValid)
            {
                return new List<string>();
            }
            var messages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return messages;
        }
    }
}