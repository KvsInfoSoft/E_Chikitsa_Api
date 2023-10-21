using E_Chikitsa_Utility.UtilityTools.APIResponse;
using E_Chikitsa_ViewModels.CommoModel;
using E_Chikitsa_ViewModels.ConfigurationModel;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json.Serialization;

namespace E_Chikitsa_Api.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        static readonly Serilog.ILogger _apiLogService = (Serilog.ILogger)Serilog.Log.ForContext<ExceptionMiddleware>();

        private readonly IOptions<AppSetting> _AppSetting;
        private readonly IConfiguration _Configuration;

        public ExceptionMiddleware(RequestDelegate next, IOptions<AppSetting> appSetting, IConfiguration configuration)
        {
            _next = next;
            _AppSetting = appSetting;
            _Configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var reqestTime = DateTime.UtcNow;
            var requestBodyContent = await ReadRequestBody(request);
            string responseBodyContent = string.Empty;
            var stopWatch = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);
            }
            catch (AccessViolationException avEx)
            {
                Log.Error("Request Time : {@requestTime}," +
                    "Request ElapsedMillisecond : {@ElapsedMillisecond}," +
                    "Request Method :{@Method}," +
                    "Request Path : {@Path}," +
                    "Request QueryString : {@QueryString}," +
                    "Request Body : {@Body}," +
                    "Response : {@ResponseBody}, Exception : {@Exception}", reqestTime, stopWatch.ElapsedMilliseconds, request.Method,
                    request.Path.Value, request.QueryString.ToString(), requestBodyContent, responseBodyContent, avEx.Message);
                await HandleExceptionAsync (httpContext, avEx);
               
            }
            catch(Exception Ex)
            {
                Log.Error("Request Time : {@requestTime}," +
                   "Request ElapsedMillisecond : {@ElapsedMillisecond}," +
                   "Request Method :{@Method}," +
                   "Request Path : {@Path}," +
                   "Request QueryString : {@QueryString}," +
                   "Request Body : {@Body}," +
                   "Response : {@ResponseBody}, Exception : {@Exception}", reqestTime, stopWatch.ElapsedMilliseconds, request.Method,
                   request.Path.Value, request.QueryString.ToString(), requestBodyContent, responseBodyContent, Ex.Message);
                await HandleExceptionAsync(httpContext, Ex);

            }

        }

        private async Task<string> ReadRequestBody (HttpRequest request)
        {
            request.EnableBuffering();
            string bodyAsText = string.Empty;
            if(request.ContentLength == null || !(request.ContentLength >0) || 
                !request.Body.CanSeek)
            {

                return bodyAsText;
            }
            request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {

                request.Body.Position= 0;
                bodyAsText = await reader.ReadToEndAsync();
            }
            request.Body.Position = 0;
            return bodyAsText;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string error = JsonConvert.SerializeObject(new SingleResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Result = "Fail"
            });

            bool _isSendErrorEmail = false;
            try
            {
                _isSendErrorEmail = Convert.ToBoolean(_Configuration.GetSection("ErrorLog:IsSendErrorEmail").Value);


            }
            catch
            {
                _isSendErrorEmail = false;
            }
            try
            {
                if(_isSendErrorEmail == true)
                {
                    //TODO - Implement Mail features if send error log through email.
                }
            }
            catch
            {                
            }
            await context.Response.WriteAsync(error);
        }
    }
}
