using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }


        [SecuredOperation(roles: "customer.add", Priority = 1)]
        [ValidationAspect(typeof(CustomerValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "ICustomerService.Get", Priority = 3)]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "customer.delete", Priority = 1)]
        [CacheRemoveAspect(pattern: "ICustomerService.Get", Priority = 2)]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == customerId));
        }

        public IDataResult<List<CustomerDetailsDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailsDto>>(_customerDal.GetCustomerDetails());
        }

        [SecuredOperation(roles: "customer.update", Priority = 1)]
        [ValidationAspect(typeof(CustomerValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "ICustomerService.Get", Priority = 3)]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult();
        }
    }
}
