using Business.Abstract;
using Business.Constants.Messages;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
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


        [ValidationAspect(typeof(Rental))]
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
        

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }


        [ValidationAspect(typeof(Rental))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }



        private IResult CheckIfCarIsNotRented(int carId)
        {
            var carToBeRented = _rentalDal.Get(r => r.CarId == carId);
            if (String.IsNullOrEmpty(carToBeRented.ReturnDate.ToString()))
            {
                return new ErrorResult(ErrorMessages.RentalReturnDateNextTime);
            }
            return new SuccessResult();
        }

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
