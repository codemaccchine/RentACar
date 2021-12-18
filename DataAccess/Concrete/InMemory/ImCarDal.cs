using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class ImCarDal : ICarDal
    {
        List<Car> _cars;
        public ImCarDal()
        {
            _cars = new List<Car>
            {
                new Car { Id = 1, BrandId = 1, ColorId =3, ModelYear=2021, DailyPrice =120, Description="320i Sport Line"},
                new Car { Id = 2, BrandId = 1, ColorId =4, ModelYear=2018, DailyPrice =230, Description="520d xDrive M Sport"},
                new Car { Id = 3, BrandId = 3, ColorId =4, ModelYear=2008, DailyPrice =120, Description="1.5 dCi Joy"},
                new Car { Id = 4, BrandId = 4, ColorId =2, ModelYear=2021, DailyPrice =565, Description="AMG GT43 4MATIC"},
                new Car { Id = 5, BrandId = 4, ColorId =6, ModelYear=2021, DailyPrice =585, Description="S400d"},
                new Car { Id = 6, BrandId = 3, ColorId =1, ModelYear=2018, DailyPrice =320, Description="1.6 Joy"},
                new Car { Id = 7, BrandId = 2, ColorId =5, ModelYear=2020, DailyPrice =520, Description="1.5 TDCi Trend X"}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var deletedCar = _cars.SingleOrDefault(c => c.Id == car.Id);
            if (deletedCar != null)
            {
                _cars.Remove(deletedCar);
            }
            else
            {
                throw new Exception("Böyle bir araba bulunamadı.");
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            return _cars.AsQueryable().Where(filter).SingleOrDefault();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return filter == null ? _cars : _cars.AsQueryable().Where(filter).ToList();
        }

        public List<CarDetailsDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            var updatedCar = _cars.SingleOrDefault(c => c.Id == car.Id);
            if (updatedCar != null)
            {
                updatedCar.BrandId = car.BrandId;
                updatedCar.ColorId = car.ColorId;
                updatedCar.DailyPrice = car.DailyPrice;
                updatedCar.ModelYear = car.ModelYear;
                updatedCar.Description = car.Description;
            }
            else
            {
                throw new Exception("Böyle bir araba bulunamadı.");
            }
        }
    }
}
