using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestJob.Models
{
    public class Task
    {
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string TaskName { get; set; }

        public Guid ProjectId { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }


        [Column(TypeName = "datetime")]
        public Nullable<DateTime> StartDate { get; set; }


        [Column(TypeName = "datetime")]
        public Nullable<DateTime> UpdateDate { get; set; }

        [Column(TypeName = "datetime")]
        public Nullable<DateTime> CancelDate { get; set; }


        public Project Project { get; set; }

        public IEnumerable<TaskComment> TaskComments  { get; set; }
    }
}
