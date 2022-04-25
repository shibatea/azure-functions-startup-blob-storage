using System.Threading.Tasks;
using FunctionApp4.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp4
{
    public class Function1
    {
        private readonly IRepository _repository;

        public Function1(IRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var photoName = "icon.png";
            var result = await _repository.DeletePhotoAsync(photoName);
            return new OkObjectResult(result);
        }
    }
}
