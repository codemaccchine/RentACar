using Entities.Concrete;
using System.Collections.Generic;
using Entities.Dtos;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface ICarService 
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int id);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);

        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);

        IDataResult<List<CarDetailsDto>> GetCarDetails();


        IResult TransactionalOperation(Car car);
    }
}
