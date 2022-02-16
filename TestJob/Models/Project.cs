using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestJob.Models
{
    public class Project
    {
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string ProjectName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime")]
        public Nullable<DateTime> UpdateDate { get; set; }

        public IEnumerable<Task> Tasks { get; set; }

    }

    public class ProjectView
    {
        public Project Project { get; set; }
        public int key { get; set; }
        public string disabled { get; set; }
    }

}
