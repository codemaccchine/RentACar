using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTestController : ControllerBase
    {
        ICarService _carService;
        public TransactionTestController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpPost("test")]
        public IActionResult TransactionTest(Car car)
        {
            var result = _carService.TransactionalOperation(car);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
