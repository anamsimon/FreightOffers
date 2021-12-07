using FreightOffers.ExternalService.Api.Attributes;
using FreightOffers.ExternalServie.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Xml.Serialization;

namespace FreightOffers.ExternalServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class UpsController : ControllerBase
    {
        [HttpPost]
        [Route("offer")]
        [Consumes(MediaTypeNames.Application.Xml)]
        public ServiceUpsResponse Post(ServiceUpsRequest requestBody)
        {
            var result = new ServiceUpsResponse { Quote = 30m };
            return result;
        }

    }
}
