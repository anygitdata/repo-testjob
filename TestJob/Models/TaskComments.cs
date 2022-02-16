using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestJob.Models
{
    public class TaskComment
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        // false -> into file  else into database
        public System.Nullable<bool> CommentType { get; set; }
        
        public byte[] Content { get; set; }

        public Task Task { get; set; }
    }
}
