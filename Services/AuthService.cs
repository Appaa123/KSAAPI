using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace KSAApi.Services;

public class AuthService : IAuthService
{

    private UserRepository _userRepository;
    private bool isValidated = false;

    public AuthService(UserRepository userRepository)
    {

        _userRepository = userRepository;

    }
    public async Task<bool> validateUser(User user)
    {
         Console.WriteLine("validation started");

        var valUser = await this._userRepository.GetByIdAsync(user.Id);
        
        Console.WriteLine("validation started" + valUser.username);

        if (valUser.username == user.username && valUser.password == user.password)
        {
            Console.WriteLine("Succesfully validated!!" + valUser.username);
            this.isValidated = true;
        }
        else
        {
            Console.WriteLine("validation failed" + valUser.username);
            this.isValidated = false;

        }

        
        return this.isValidated;
        
    }
}