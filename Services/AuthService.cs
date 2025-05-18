using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace KSAApi.Services;

public class AuthService: IAuthService {

private AuthRepository _authRepository;

public AuthService(AuthRepository authRepository){

    this._authRepository = authRepository;

}
public List<User> GetUserAsync(){
    return _authRepository.GetAllAsync();
}

public async Task AddUserAsync(User user){
    await _authRepository.CreateAsync(user);
} 

public async Task UpdateUserAsync(string Id, User user){
    await _authRepository.UpdateAsync(Id, user);
} 

public async Task DeleteUserAsync(string Id){
    await _authRepository.DeleteAsync(Id);
} 

}