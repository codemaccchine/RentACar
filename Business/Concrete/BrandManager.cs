using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }


        [SecuredOperation(roles: "brand.add", Priority = 1)]
        [ValidationAspect(typeof(BrandValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IBrandService.Get", Priority = 3)]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "brand.delete", Priority = 1)]
        [CacheRemoveAspect(pattern: "IBrandService.Get", Priority = 2)]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult();
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120,Priority =2)]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId));
        }


        [SecuredOperation(roles: "brand.update", Priority = 1)]
        [ValidationAspect(typeof(BrandValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IBrandService.Get", Priority = 3)]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult();
        }
    }
}
