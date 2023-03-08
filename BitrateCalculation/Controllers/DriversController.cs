using BitrateCalculation.Models;
using BitrateCalculation.Services;
using BitrateCalculation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BitrateCalculation.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost]
        public IActionResult GetDeviceInformation([FromBody] Driver<NetworkInterfaceCard> driver)
        {            
            return Ok(_driverService.CalculateBitrate(driver));
        }

    }
}
