using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var store = new UserStore<User>(context);
            var manager = new ApplicationUserManager(store);
            var user = new User()
            {
                Email = "test@test.com",
                UserName = "Tester",
            };

            manager.Create(user, "test12345");

            var user2 = new User()
            {
                Email = "test2@test.com",
                UserName = "Manager"
            };

            manager.Create(user2, "test12345");
            #endregion

            #region Projects
            Project project = new Project
            {
                Name = "X",
                Description = "Project X is a 2012 American comedy film directed by Nima Nourizadeh and written by Michael Bacall and Matt Drake" +
                              " based on a story by Bacall, and produced by director Todd Phillips. The film follows three friends—Thomas (Thomas Mann)," +
                              " Costa (Oliver Cooper) and J.B. (Jonathan Daniel Brown)—who plan to gain popularity by throwing a party, a plan which quickly" +
                              " escalates out of their control.",
                Members = new HashSet<User>() { user, user2 },
            };

            Project project2 = new Project
            {
                Name = "Manhattan",
                Description = "The Manhattan Project was a research and development project that produced the first nuclear weapons" +
                              " during World War II. It was led by the United States with the support of the United Kingdom and Canada." +
                              " From 1942 to 1946, the project was under the direction of Major General Leslie Groves of the U.S. Army Corps of Engineers;" +
                              " physicist J. Robert Oppenheimer was the director of the Los Alamos National Laboratory that designed the actual bombs.",
                Members = new HashSet<User>() { user },
            };

            context.Projects.Add(project);
            context.Projects.Add(project2);
            context.Users.First(x => x.UserName == "Tester").ActiveProjectId = project.Id;
            context.Users.First(x => x.UserName == "Manager").ActiveProjectId = project.Id;
            context.SaveChanges();
            #endregion

            #region Assignments
            for (int i = 0; i < 5; i++)
            {
                Assignment assignment = new Assignment
                {
                    Name = "Task " + i,
                    Description = "Opis taska " + i,
                    DueDate = new DateTime(2015, 11, 10),
                    OwnerId = context.Users.First(x => x.UserName == "Tester").Id,
                    ProjectId = context.Projects.First(x => x.Name == "X").Id,
                    StatusId = context.Statuses.First(x => x.Type == StatusType.Todo).Id,
                    PriorityId = context.Priorities.First(x => x.Type == PriorityType.Normal).Id,
                    CategoryId = context.Categories.First(x => x.Type == CategoryType.Task).Id,
                };

                context.Assignments.Add(assignment);
            }

            context.SaveChanges();
            #endregion

            #region Sprints
            Sprint sprint1 = new Sprint
            {
                Name = "Sprint 1",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Assignemnts = new List<Assignment>()
                {
                    context.Assignments.First(x => x.Name == "Task 1")
                }
            };

            var projectX = context.Projects.First(x => x.Name == "X");
            projectX?.Sprints.Add(sprint1);

            context.SaveChanges();
            #endregion
        }
    }
}