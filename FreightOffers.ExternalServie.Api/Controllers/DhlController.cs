using FreightOffers.ExternalServie.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreightOffers.ExternalService.Api.Attributes;

namespace FreightOffers.ExternalServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class DhlController : ControllerBase
    {

        [HttpPost]
        [Route("offer")]
        public DhlResponse Post([FromBody] DhlRequest requestBody)
        {
            return new DhlResponse { Total = 100m };
        }
    }
}
