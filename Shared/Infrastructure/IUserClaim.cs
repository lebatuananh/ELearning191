using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Infrastructure
{
    public interface IUserClaim
    {
        Guid UserId { get; }
        string UserName { get; }
        string UserEmail { get; }
    }
}
