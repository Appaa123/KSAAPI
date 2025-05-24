using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace KSAApi.Services;

public class UserService: IUserService {

private UserRepository _userRepository;

public UserService(UserRepository authRepository){

    this._userRepository = authRepository;

}
public List<User> GetUserAsync(){
    return _userRepository.GetAllAsync();
}

public async Task AddUserAsync(User user){
    await _userRepository.CreateAsync(user);
} 

public async Task UpdateUserAsync(string Id, User user){
    await _userRepository.UpdateAsync(Id, user);
} 

public async Task DeleteUserAsync(string Id){
    await _userRepository.DeleteAsync(Id);
} 

}