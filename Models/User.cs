using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public string Username { get; set;}

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set;}
}