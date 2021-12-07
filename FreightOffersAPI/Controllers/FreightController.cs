using Microsoft.AspNetCore.Mvc;
using FreightOffers.IService;
using FreightOffers.Model;
using System;

namespace FreightOffers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        private readonly IFreightService _freightService;
        public FreightController(IFreightService freightService)
        {
            this._freightService = freightService;
        }

        [HttpPost]
        [Route("BestOffer")]
        public IActionResult BestOffer([FromBody] Consignment consignment)
        {
            if (!consignment.IsValid())
                return BadRequest("request is incorrect");

            var result = this._freightService.BestOffer(consignment);
            return Ok(new Offer() { Amount = result });
        }

    }
}
