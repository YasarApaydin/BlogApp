using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>(); 
            if(context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag {Text= "Wep programlama", Url= "Wep", Color= TagColors.success},
                        new Tag {Text="Frontend programlama", Url= "Frontend", Color= TagColors.warning },
                        new Tag {Text= "backend programlama", Url= "backend", Color= TagColors.primary },
                            new Tag { Text = "php programlama", Url= "php", Color= TagColors.danger },
                                new Tag { Text = "fullstack programlama", Url= "fullstack", Color=TagColors.info }
                        );
                    context.SaveChanges();


                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User {UserName="yaşarapaydın",Name="Yaşar Apaydın",Email="apaydinyasar0@gmail.com",Password="123456", Image="p1.png" },
                        new User { UserName = "alialtın", Name = "Ali Altunar", Email = "alialtunar@gmail.com", Password = "123456", Image = "p2.png" }
                            
                        );
                    context.SaveChanges();

                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                       new Post
                       {
                           Title = "asp.net core",
                           Description = "Asp.net core dersleri",
                           Content = "Asp.net core dersleri",
                           Url = "aspnet-core",
                           IsAcive = true,
                           PublishedOn=DateTime.Now.AddDays(-10),
                           Tags=context.Tags.Take(3).ToList(),
                           Image="1.png",
                           UserId=1,
                           Comments = new List<Comment> {
                           new Comment {Text="iyi bir kurs",PublishedOn= DateTime.Now.AddDays(-5), UserId=1 },
                           new Comment {Text="normal bir kurs", PublishedOn=DateTime.Now.AddDays(-10),UserId=2 }
                           }

                       },
                        new Post
                        {
                            Title = "Php",
                            Description = "php dersleri",
                            Content = "php dersleri",
                            Url="php",
                            IsAcive = true,
                            Image="2.png",
                            PublishedOn = DateTime.Now.AddDays(-2),
                            Tags = context.Tags.Take(2).ToList(),
                            UserId = 1

                        },
                         new Post
                         {
                             Title = "Java",
                             Description="Java Dersleri",
                             Content = "java dersleri",
                             Url="Java",
                             IsAcive = true,
                             Image="3.png",
                             PublishedOn = DateTime.Now.AddDays(-5),
                             Tags = context.Tags.Take(4).ToList(),
                             UserId = 2

                         },
                         new Post
                         {
                             Title = "MsSql",
                             Description = "MsSql Dersleri",
                             Content = "MsSql server dersleri",
                             Url = "MsSql",
                             IsAcive = true,
                             Image = "1.png",
                             PublishedOn = DateTime.Now.AddDays(-7),
                             Tags = context.Tags.Take(4).ToList(),
                             UserId = 2

                         },
                         new Post
                         {
                             Title = "Django",
                             Description = "Django Dersleri",
                             Content = "Django dersleri",
                             Url = "Django",
                             IsAcive = true,
                             Image = "1.png",
                             PublishedOn = DateTime.Now.AddDays(-20),
                             Tags = context.Tags.Take(4).ToList(),
                             UserId = 2

                         },
                         new Post
                         {
                             Title = "React",
                             Description= "React Dersleri",
                             Content = "React dersleri",
                             Url = "React",
                             IsAcive = true,
                             Image = "2.png",
                             PublishedOn = DateTime.Now.AddDays(-30),
                             Tags = context.Tags.Take(4).ToList(),
                             UserId = 2

                         }
                        );
                    context.SaveChanges();
                    
                }

            }
        }
    }
}
