using Microsoft.AspNetCore.Identity;

namespace IdentityEmail.Entities
{
    public class AppUser :IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public string? ConfirmCode { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        public DateTime ConfirmDate { get; set; } = DateTime.Now;

        public virtual ICollection<UserMessage> SendMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }

    }
}
