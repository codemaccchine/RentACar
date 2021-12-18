using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IImageService 
    {
        IDataResult<List<Image>> GetAll();
        IDataResult<Image> GetById(int id);
        IResult Add(IFormFile file, Image carImage);
        IResult Update(IFormFile file, Image carImage);
        IResult Delete(Image carImage);
        IDataResult<List<Image>> GetImagesByCarId(int carId);
    }
}
