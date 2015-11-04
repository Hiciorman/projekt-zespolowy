using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace ProjectManager.Domain.Seed
{
    public class AppContextSeedInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            #region Statuses
            context.Statuses.Add(new Status() { Type = StatusType.Backlog, Description = "Backlog" });
            context.Statuses.Add(new Status() { Type = StatusType.Todo, Description = "Todo" });
            context.Statuses.Add(new Status() { Type = StatusType.InProgress, Description = "In progress" });
            context.Statuses.Add(new Status() { Type = StatusType.ReadyForReview, Description = "Ready to review" });
            context.Statuses.Add(new Status() { Type = StatusType.Done, Description = "Done" });

            context.SaveChanges();
            #endregion

            #region Priorities
            context.Priorities.Add(new Priority() { Type = PriorityType.Criticial, Description = "Critical" });
            context.Priorities.Add(new Priority() { Type = PriorityType.High, Description = "High" });
            context.Priorities.Add(new Priority() { Type = PriorityType.Low, Description = "Low" });
            context.Priorities.Add(new Priority() { Type = PriorityType.Minor, Description = "Minor" });
            context.Priorities.Add(new Priority() { Type = PriorityType.Normal, Description = "Normal" });
            context.SaveChanges();
            #endregion

            #region Categories

            context.Categories.Add(new Category() { Type = CategoryType.Bug, Description = "Bug" });
            context.Categories.Add(new Category() { Type = CategoryType.Improvment, Description = "Improvment" });
            context.Categories.Add(new Category() { Type = CategoryType.Task, Description = "Task" });
            context.SaveChanges();
            #endregion

            #region Users
            User user = new User
            {
                UserName = "Tester",
                Email = "test@test.com"
            };

            context.Users.Add(user);
            context.SaveChanges();
            #endregion

            #region Projects
            Project project = new Project
            {
                Name = "Test",
                Description = "Projekt testowy",
                OwnerId = context.Users.FirstOrDefault().Id,
                Members = new List<User>() { user}
                
            };

            context.Projects.Add(project);
            context.SaveChanges();
            #endregion

            #region Assignments
            for (int i = 0; i < 5; i++)
            {
                Assignment assignment = new Assignment
                {
                    Name = "Task " + i,
                    Description = "Opis taska " + i,
                    DueDate = new DateTime(2015,11,10),
                    OwnerId = context.Users.FirstOrDefault().Id,
                    ProjectId = context.Projects.FirstOrDefault().Id,
                    StatusId = context.Statuses.FirstOrDefault().Id,
                    PriorityId = context.Priorities.FirstOrDefault().Id,
                    CategoryId = context.Categories.FirstOrDefault().Id
                };

                context.Assignments.Add(assignment);
            }

            context.SaveChanges();
            #endregion
        }
    }
}