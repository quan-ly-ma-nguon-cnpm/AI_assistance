using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Application.Models.Users
{
    public class Users
    {
        public Guid Id { get; set; }
        [Key]
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

        // Navigation Property (1-to-1)
        public Users_Information? UsersInformation { get; set; }
    }

    public class Users_Information
    {
        public Guid ID { get; set; }
        [Key]
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string ColleagueID { get; set; }
        public required string Address { get; set; }
        public required string MediaLinked { get; set; }

        // Navigation Property
        public Users? User { get; set; }
    }


}