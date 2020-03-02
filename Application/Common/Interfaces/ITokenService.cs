
using System;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GetUserName();

        int GetUserId();

        (string, DateTime) GetSecurityToken(int id, ERole role, string username);

        Task<bool> IsCurrentTokenActive();

        Task DeactivateCurrentTokenAsync();
    }
}
