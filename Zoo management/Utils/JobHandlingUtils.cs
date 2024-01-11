using Microsoft.AspNetCore.Mvc;

namespace Zoo_management.Utils
{
    public static class JobHandlingUtils
    {
        public static async Task<ActionResult> HandleJobAsync(Func<Task<ActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                return new ObjectResult($"Internal Server Error. Exception message: {e.Message}, Inner exception: {e.InnerException?.Message}")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
