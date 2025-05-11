using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public string Username { get; set;} = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set;} = string.Empty;
}