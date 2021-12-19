using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfBrandLimitExceded());

            _carDal.Add(car);

            return new SuccessResult(SuccessMessages.CarAdded);
        }

        [ValidationAspect(typeof(CarValidator))]  
        public IResult Update(Car car)
        {
            if (car.Description.Length < 3 || car.DailyPrice <= 0)
            {
                return new ErrorResult(ErrorMessages.CarDescriptionInvalid);
            }

            _carDal.Update(car);
            return new SuccessResult(SuccessMessages.CarUpdated);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(SuccessMessages.CarDeleted);
        }


        //[SecuredOperation("admin")]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 0)
            {
                return new ErrorDataResult<List<Car>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), SuccessMessages.CarsListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            if (DateTime.Now.Hour == 6)
            {
                return new ErrorDataResult<Car>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), SuccessMessages.CarListed);
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 6)
            {
                return new ErrorDataResult<List<CarDetailsDto>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails(), SuccessMessages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<Car>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), SuccessMessages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<Car>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), SuccessMessages.CarsListed);
        }


        //Araba markası sayısı 15'i geçtiyse yeni araba eklenemez
        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(ErrorMessages.BrandLimitExceded);
            }
            return new SuccessResult();
        }


        //Bir markada bulunan araç sayısı 5'ten fazlaysa yeni araç ekleme
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result > 5)
            {
                return new ErrorResult(ErrorMessages.CarCountOfBrandCorrect);
            }
            return new SuccessResult();
        }
    }
}
