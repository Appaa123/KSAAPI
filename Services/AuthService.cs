using System.Collections;
using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace KSAApi.Services;

public class AuthService : IAuthService
{

    private UserRepository _userRepository;
    private bool isValidated = false;

    private User _user;

    public AuthService(UserRepository userRepository)
    {

        _userRepository = userRepository;

    }
    public bool validateUser(User user)
    {
        int count = 0;
        if (user != null)
        {
            Console.WriteLine("validation started");

            this._userRepository.GetAllAsync().ForEach(x =>
            {
                if (x.username == user.username && x.password == user.password)
                {
                    this._user = user;
                    count++;
                }
            });

            if (count == 1)
            {
                this.isValidated = true;
            }
            else if (count > 1)
            {
                new Exception("Duplicate records found!!");
            }
            else
            {
                this.isValidated = false;
            }
        }
        
        Console.WriteLine("validation started" + this._user.username);
        
        return this.isValidated;
        
    }
}