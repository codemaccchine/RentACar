using Business.Abstract;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }


        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, SuccessMessages.UserRegistered);
        }


        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(ErrorMessages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(ErrorMessages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, SuccessMessages.SuccessfulLogin);
        }


        /// <summary>
        /// Kullanıcının var olup olmadığını kontrol eder
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(ErrorMessages.UserAlreadyExists);
            }
            return new SuccessResult();
        }


        /// <summary>
        /// AccessToken oluşturur
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, SuccessMessages.AccessTokenCreated);
        }
    }
}
