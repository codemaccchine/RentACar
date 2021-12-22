using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;

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


        [SecuredOperation(roles: "car.add", Priority = 1)]
        [ValidationAspect(typeof(CarValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "ICarService.Get", Priority = 3)]
        public IResult Add(Car car)
        {
            BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfBrandLimitExceded());

            _carDal.Add(car);

            return new SuccessResult(SuccessMessages.CarAdded);
        }


        [SecuredOperation(roles: "car.update", Priority = 1)]
        [ValidationAspect(typeof(CarValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "ICarService.Get", Priority = 3)]
        public IResult Update(Car car)
        {
            if (car.Description.Length < 3 || car.DailyPrice <= 0)
            {
                return new ErrorResult(ErrorMessages.CarDescriptionInvalid);
            }

            _carDal.Update(car);
            return new SuccessResult(SuccessMessages.CarUpdated);
        }

        [SecuredOperation(roles: "car.delete", Priority = 1)]
        [CacheRemoveAspect(pattern: "ICarService.Get", Priority = 2)]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(SuccessMessages.CarDeleted);
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        public IDataResult<List<Car>> GetAll()
        {
            Thread.Sleep(5000);
            if (DateTime.Now.Hour == 0)
            {
                return new ErrorDataResult<List<Car>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), SuccessMessages.CarsListed);
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<Car> GetById(int id)
        {
            if (DateTime.Now.Hour == 6)
            {
                return new ErrorDataResult<Car>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), SuccessMessages.CarListed);
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 60, Priority = 2)]
        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails(), SuccessMessages.CarsListed);
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 60, Priority = 2)]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<Car>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), SuccessMessages.CarsListed);
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 60, Priority = 2)]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<Car>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), SuccessMessages.CarsListed);
        }



        /// <summary>
        /// Araba markası sayısı 15'i geçtiyse yeni araba eklenemez
        /// </summary>
        /// <returns></returns>
        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(ErrorMessages.BrandLimitExceded);
            }
            return new SuccessResult();
        }


        /// <summary>
        /// Bir markada bulunan araç sayısı 5'ten fazlaysa yeni araç ekleme
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result > 5)
            {
                return new ErrorResult(ErrorMessages.CarCountOfBrandCorrect);
            }
            return new SuccessResult();
        }



        /// <summary>
        /// Transaction Test
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(SuccessMessages.CarUpdated);
        }
    }
}
