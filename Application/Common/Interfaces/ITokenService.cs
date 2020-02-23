
using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GetUserName();

        int GetUserId();

        (string, DateTime) GetSecurityToken(int id, string username);

        Task<bool> IsCurrentTokenActive();

        Task DeactivateCurrentTokenAsync();
    }
}
