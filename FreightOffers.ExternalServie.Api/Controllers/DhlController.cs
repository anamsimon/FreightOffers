using FreightOffers.ExternalServie.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightOffers.ExternalServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
