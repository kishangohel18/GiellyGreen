using GiellyGreenApi.Models;

namespace GiellyGreenApi.Helper
{
    public class JsonResponseHelper
    {
        public static JsonResponse JsonResponseMessage(int ResponseStatus, string Message, dynamic Result)
        {
            var ObjResponse = new JsonResponse
            {
                ResponseStatus = ResponseStatus,
                Message = Message,
                Result = Result
            };

            return ObjResponse;
        }
    }
}