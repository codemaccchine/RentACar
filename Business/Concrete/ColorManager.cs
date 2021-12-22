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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        [SecuredOperation(roles: "color.add", Priority = 1)]
        [ValidationAspect(typeof(ColorValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IColorService.Get", Priority = 3)]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "color.delete", Priority = 1)]
        [CacheRemoveAspect(pattern: "IColorService.Get", Priority = 2)]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult();
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }


        //[SecuredOperation(roles: "user", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == colorId));
        }



        [SecuredOperation(roles: "color.update", Priority = 1)]
        [ValidationAspect(typeof(ColorValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IColorService.Get", Priority = 3)]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult();
        }
    }
}
