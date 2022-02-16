using System;
using System.Linq;
using System.Text;

namespace TestJob.Models
{
    public partial class SeedData
    {
        static partial void ModifyDataBase(DataContext context)
        {

            DateTime d1 = new DateTime(2022,1,21); 
            DateTime d2 = d1.AddDays(1).AddHours(2.5);
            DateTime d3 = d2.AddDays(1).AddHours(3).AddMinutes(15);


            Project prStart =
                new Project { ProjectName = "Main project", CreateDate = d1, UpdateDate = null };

            Project prTest =
                new Project { ProjectName = "Test project", CreateDate = d2, UpdateDate = null };


            #region Loading data by tasks 

            Task task1 =
                new Task { TaskName = "CreatingProject", CreateDate = d1, Project = prStart, StartDate = d1 };

            Task task2 =
                new Task { TaskName = "Creating API", CreateDate = d2, Project = prStart, StartDate = d2 };

            Task task3 =
                new Task { TaskName = "Testing basic methods ", CreateDate = d2, Project = prTest, StartDate = d3 };
            #endregion


            #region Loading comments 
            context.AddRange(
                new TaskComment
                {
                    CommentType = false,
                    Content = Encoding.ASCII.GetBytes("Task1.txt"),
                    Task = task1
                },

                new TaskComment
                {
                    CommentType = true,
                    Task = task2,
                    Content = Encoding.ASCII.GetBytes("Creation of tools for processing user data")
                },

                new TaskComment
                {
                    CommentType = true,
                    Task = task3,
                    Content = Encoding.ASCII.GetBytes("Creating a Test Structure")
                });

            #endregion


        }

    }
}
