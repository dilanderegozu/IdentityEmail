namespace IdentityEmail.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public MessageFolder Folder { get; set; }
        public string SenderId { get; set; }
        public virtual AppUser Sender { get; set; }
        public string? ReceiverId { get; set; }
        public virtual AppUser? Receiver { get; set; } //taslakta alıcı olmayabilir
        public bool IsStarred { get; set; }         // Klasörden bağımsız yıldızlanabilir
        public int? CategoryId { get; set; }
        public virtual MessageCategory? Category { get; set; }

    }
    public enum MessageFolder
    {
        Inbox,
        Sent,
        Draft,//taslaklar
        Archive,
        Schedule,//planlanmış mesajlar
        Trash, //çöp kutusu
        Spam
    }
}
