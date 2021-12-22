using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        [SecuredOperation(roles: "user.add", Priority = 1)]
        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IUserService.Get", Priority = 3)]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "user.update", Priority = 1)]
        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        [CacheRemoveAspect(pattern: "IUserService.Get", Priority = 3)]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "user.delete", Priority = 1)]
        [CacheRemoveAspect(pattern: "IUserService.Get", Priority = 2)]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }


        [SecuredOperation(roles: "admin", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }


        [SecuredOperation(roles: "admin", Priority = 1)]
        [CacheAspect(duration: 120, Priority = 2)]
        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }
        

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }
    }
}
