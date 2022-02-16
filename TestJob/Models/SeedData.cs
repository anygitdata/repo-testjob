﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models
{
    public partial class SeedData
    {
        static partial void ModifyDataBase(DataContext cont);
        static partial void ModifyDataBaseNext(DataContext cont);

        static partial void InitializationServProcedure(DataContext cont, string PathDir_txt);

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            IAnyUserData userData = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<IAnyUserData>();

            DataContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<DataContext>();

            string PathDir_txt = userData.PathDir_txt;
            

            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            // Параметр seedData сбрасывается if seedData == on then seedData = off
            if (DataSettings_read.GetSettings(PathDir_txt).seedData == "on")
            {               
                context.RemoveRange(context.Set<Project>());

                context.SaveChanges();
            }


            if (!context.Projects.Any())
            {
                ModifyDataBase(context);
                ModifyDataBaseNext(context);

                context.SaveChanges();
                

                InitializationServProcedure(context, PathDir_txt);

            }

        }

    }
}
