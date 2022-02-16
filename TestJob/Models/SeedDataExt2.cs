using System;
using System.Linq;
using System.Text;

namespace TestJob.Models
{
    public partial class SeedData
    {
        static partial void ModifyDataBaseNext(DataContext context)
        {
            DateTime d1 = new DateTime(2022,1,23).AddHours(10);
            DateTime d2 = d1.AddDays(1).AddHours(3).AddMinutes(40);
            //DateTime d3 = d2.AddHours(3).AddMinutes(20);

            // Используется для продолжения добавления данных
            
            Project prStart =
                new Project { ProjectName = "Project refactoring", CreateDate = d1, UpdateDate = d1 };

            Task task1 =
                new Task { TaskName = "Partial Methods", CreateDate = d1, Project = prStart, StartDate = d1 };

            Task task2 =
                new Task { TaskName = "restAPI extension", CreateDate = d2, Project = prStart, StartDate = d2 };

            context.AddRange(
                new TaskComment
                {
                    CommentType = true,
                    Content = Encoding.ASCII.GetBytes("Using Partial Methods"),
                    Task = task1
                },

                new TaskComment
                {
                    CommentType = false,
                    Task = task2,
                    Content = Encoding.ASCII.GetBytes("ExtRestAPI.txt")
                });

        }

    }
}
