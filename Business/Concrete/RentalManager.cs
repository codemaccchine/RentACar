using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
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
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }


        [SecuredOperation(roles: "rental.add", Priority = 1)]
        [ValidationAspect(typeof(RentalValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IRentalService.Get", Priority = 3)]
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(CheckIfCarIsNotRented(rental.CarId), 
                              CheckIfCarIsNotBack(rental.CarId));

            if (result != null)
            {
                return result;
            }

            rental.RentDate = DateTime.Now;
            _rentalDal.Add(rental);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "rental.delete", Priority = 1)]
        [CacheRemoveAspect(pattern: "IRentalService.Get", Priority = 2)]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }


        //[SecuredOperation(roles: "moderator,admin", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }


        //[SecuredOperation(roles: "moderator,admin", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }


        [SecuredOperation(roles: "rental.update", Priority = 1)]
        [ValidationAspect(typeof(RentalValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IRentalService.Get", Priority = 3)]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        public IDataResult<List<RentalDetailsDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailsDto>>(_rentalDal.GetRentalDetails());
        }



        /// <summary>
        /// Kiralanmak istenen aracın kiralanabilir olup olmadığını kontrol eder
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        private IResult CheckIfCarIsNotRented(int carId)
        {
            var carToBeRented = _rentalDal.Get(r => r.CarId == carId);
            if (String.IsNullOrEmpty(carToBeRented.ReturnDate.ToString()))
            {
                return new ErrorResult(ErrorMessages.RentalReturnDateNextTime);
            }
            return new SuccessResult();
        }


        /// <summary>
        /// Kiralanmak istenen aracın dönüş tarihini kontrol eder
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        private IResult CheckIfCarIsNotBack(int carId)
        {
            var carToBeRented = _rentalDal.Get(r => r.CarId == carId);

            if (carToBeRented.ReturnDate > DateTime.Now)
            {
                return new ErrorResult(ErrorMessages.RentalReturnDateNextTime);
            }
            return new SuccessResult();
        }

       
    }
}
