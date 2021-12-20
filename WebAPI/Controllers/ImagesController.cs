using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        IImageService _imageService;
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _imageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _imageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getimagesbycarid")]
        public IActionResult GetImagesByCarId(int id)
        {
            var result = _imageService.GetImagesByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] Image carImage)
        {
            var result = _imageService.Add(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] int id)
        {
            var carImage = _imageService.GetById(id).Data;
            var result = _imageService.Update(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int id)
        {
            var carImage = _imageService.GetById(id).Data;

            var result = _imageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
