namespace IdentityEmail.Entities
{
    public class MessageCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Icon { get; set; }      
        public string UserId { get; set; }
        public string Color { get; set; } = "primary";
        public virtual AppUser User { get; set; }

        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}
