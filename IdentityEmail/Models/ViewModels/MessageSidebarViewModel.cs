using IdentityEmail.Entities;

namespace IdentityEmail.Models.ViewModels
{
    public class MessageSidebarViewModel
    {
        public int InboxCount { get; set; }
        public int StarredCount { get; set; }
        public int DraftCount { get; set; }
        public int SentCount { get; set; }
        public int ScheduleCount { get; set; }
        public int ArchiveCount { get; set; }
        public int SpamCount { get; set; }
        public int TrashCount { get; set; }

        public List<MessageCategory> Categories { get; set; } = new();
    }
}