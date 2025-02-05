using System.ComponentModel.DataAnnotations;

namespace lab1_nour_kassem.Models
{
    public class User
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}

public class UserUpdateRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
    public string NewName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
}