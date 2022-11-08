using System.ComponentModel.DataAnnotations;

namespace FriendOrganizer.Model;

public class Friend
{
    // Here [Required] data annotation can be replaced with C# nullable reference type implicit validations
    // But since we want to use the annotations for WPF, we will use it here.

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [EmailAddress]
    public string? Email { get; set; }
}