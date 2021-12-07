using FreightOffers.ExternalService.Api.Attributes;
using FreightOffers.ExternalServie.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreightOffers.ExternalServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class FedExController : ControllerBase
    {
        [HttpPost]
        [Route("offer")]
        public FedExResponse Post([FromBody] FedExRequest requestBody)
        {
            return new FedExResponse { Amount = 50m };
        }
    }
}
