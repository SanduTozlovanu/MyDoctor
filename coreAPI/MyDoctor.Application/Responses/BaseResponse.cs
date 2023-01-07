using Microsoft.AspNetCore.Mvc;

namespace MyDoctor.Application.Responses
{
    public class BaseResponse
    {
        protected ActionResult statusResult = new OkObjectResult("");
        public virtual void SetStatusResult(ActionResult result)
        {
            statusResult = result;
        }
        public virtual ActionResult GetStatusResult() { return statusResult; }
        public virtual bool IsStatusOk()
        {
            return statusResult.Equals(new OkObjectResult(""));
        }
    }
}

