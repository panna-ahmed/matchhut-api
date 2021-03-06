using System.ComponentModel.DataAnnotations;

namespace MatchHut.Core.Dtos.UserRole
{
    public class CreateUserRoleDto
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}