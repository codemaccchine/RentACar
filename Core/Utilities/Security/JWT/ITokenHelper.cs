using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User User, List<OperationClaim> operationClaims);
    }
}
