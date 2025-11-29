using System;
using LoginApp.Models;

namespace LoginApp.Services.Interfaces;

public interface ITokenService
{
    public string CreateToken(User user, IList<string> roles);
}
