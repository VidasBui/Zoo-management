using Microsoft.AspNetCore.Mvc;
using Zoo_management.Utils;

namespace Zoo_management_tests.Tests.Utils
{
    public class JobHandlingUtilsTests
    {
        [Fact]
        public async Task HandleJobAsyncSuccessfulJobTest()
        {
            async Task<ActionResult> successfulAction()
            {
                return new OkResult();
            }

            var result = await JobHandlingUtils.HandleJobAsync(successfulAction);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task HandleJobAsyncUnsuccesfulJobTest()
        {
            async Task<ActionResult> unSuccessfulAction()
            {
                var arr = new int[4];
                var a = arr[5];
                return new OkResult();
            }

            var result = await JobHandlingUtils.HandleJobAsync(unSuccessfulAction);

            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)result;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().BeOfType<string>();
        }
    }
}
