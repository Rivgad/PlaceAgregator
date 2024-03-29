﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Enums;
using PlaceAgregator.Shared.Models.Types;

namespace PlaceAgregator.EntityFramework
{

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext appDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            bool recreate
            )
        {
            if (appDbContext.Database.IsNpgsql())
            {
                appDbContext.Database.Migrate();
            }

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager, recreate: recreate);
            await SeedDefaultTypes(appDbContext);
            await SeedDefaultPlaces(appDbContext, userManager);
            await SeedDefaultComments(appDbContext, userManager);
        }
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var rolesNames = Enum.GetValues(typeof(Role))
                .Cast<Role>()
                .Select(roleEnum => roleEnum.GetDescriptionAttribute().ToLower())
                .ToList();

            foreach (string role in rolesNames)
            {
                if (!roleManager.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = role.ToLower(), NormalizedName = role.ToUpper() });
                }
                else
                {
                    var roleToUpdate = await roleManager.FindByNameAsync(role);
                    roleToUpdate.NormalizedName = role.ToUpper();
                    roleToUpdate.Name = role.ToLower();

                    await roleManager.UpdateAsync(roleToUpdate);
                }
            }
        }
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, bool recreate)
        {
            string defaultPassword = "P@ssword1";

            string defaultLessorUserName = "lessor";
            string defaultLessorEmail = "lessor@test.com";
            var defaultLessor = new AppUser()
            {
                UserName = defaultLessorUserName,
                Email = defaultLessorEmail,
                EmailConfirmed = true
            };

            string defaultRenterUserName = "renter";
            string defaultRenterEmail = "renter@test.com";
            var defaultRenter = new AppUser()
            {
                UserName = defaultRenterUserName,
                Email = defaultRenterEmail,
                EmailConfirmed = true
            };

            string defaultModeratorUserName = "moderator";
            string defaultModeratorEmail = "moderator@test.com";
            var defaultModerator = new AppUser
            {
                UserName = defaultModeratorUserName,
                Email = defaultModeratorEmail,
                EmailConfirmed = true
            };

            string defaultAdminEmail = "admin@test.com";
            string defaultAdminUserName = "Admin";
            var defaultAdmin = new AppUser
            {
                UserName = defaultAdminUserName,
                Email = defaultAdminEmail,
                EmailConfirmed = true
            };

            List<KeyValuePair<Role, AppUser>> defaultUsersDict = new();
            defaultUsersDict.Add(new(Role.User, defaultLessor));
            defaultUsersDict.Add(new(Role.User, defaultRenter));
            defaultUsersDict.Add(new(Role.Moderator, defaultModerator));
            defaultUsersDict.Add(new(Role.Admin, defaultAdmin));

            foreach (var roleUserPair in defaultUsersDict)
            {
                var existedUser = await userManager.FindByNameAsync(roleUserPair.Value.UserName);
                if (existedUser != null)
                {
                    if (recreate)
                    {
                        await userManager.DeleteAsync(existedUser);
                        await userManager.CreateAsync(roleUserPair.Value, defaultPassword);
                        await userManager.AddToRoleAsync(roleUserPair.Value, roleUserPair.Key.GetDescriptionAttribute());
                    }
                }
                else
                {
                    await userManager.CreateAsync(roleUserPair.Value, defaultPassword);
                    await userManager.AddToRoleAsync(roleUserPair.Value, roleUserPair.Key.GetDescriptionAttribute());
                }
            }
        }
        public static async Task SeedDefaultTypes(ApplicationDbContext context)
        {
            var eventTypes = new List<string>()
            {
                "Корпоратив",
                "День рождения",
                "Банкет",
                "Вечеринка",
                "Квартирник",
                "Презентация"
            };

            var prohibionTypes = new List<string>()
            {
                "шуметь, включать громко музыку, петь, кричать",
                "употреблять алкоголь",
                "курить в помещении",
                "шуметь после 23:00",
                "использовать пачкающие материалы (вода, краска, химикаты и прочее)",
                "употреблять еду",
                "приносить свою еду",
                "употреблять чай, кофе и печеньки",
                "приводить детей 4-10 лет",
                "приносить свои напитки (включая алкоголь)"
            };

            foreach (var eventType in eventTypes)
            {
                if (!context.EventTypes.Any(item => item.Title == eventType))
                    await context.EventTypes.AddAsync(new() { Title = eventType });
            }
            foreach (var type in prohibionTypes)
            {
                if (!context.Prohibitions.Any(item => item.Title == type))
                    await context.Prohibitions.AddAsync(new() { Title = type });
            }

            await context.SaveChangesAsync();
        }
        public static async Task SeedDefaultPlaces(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            var lessorId = (await userManager.FindByNameAsync("lessor"))?.Id;
            var renterId = (await userManager.FindByNameAsync("renter"))?.Id;

            if (lessorId == null || renterId == null)
                return;

            if (await context.Places.AnyAsync())
                return;
            var eventTypes = await context.EventTypes.ToListAsync();
            var prohibitions = await context.Prohibitions.ToListAsync();
            if (!eventTypes.Any() || !prohibitions.Any())
                return;

            var defaultPlaces = new List<Place>()
            {
                new Place()
                {
                    Id = 1,
                    UserId = lessorId,
                    Title = "DefaultPlace 1",
                    City="DefaultCity",
                    Address="DefaultAddress 1",
                    Area= 20,
                    BaseRate =400,
                    BookingHorizonInDays = 20,
                    Prohibitions= new List<Prohibition>()
                    {
                        prohibitions[0],
                        prohibitions[1],
                        prohibitions[2],
                        prohibitions[3]
                    },
                    EventTypes = new List<EventType>()
                    {
                        eventTypes[0],
                        eventTypes[1],
                        eventTypes[2]
                    },
                    BookingRequests = new List<BookingRequest>()
                    {
                        new BookingRequest()
                        {
                            Id = 1,
                            PlaceId = 1,
                            UserId = renterId,
                            CreationDateTime = DateTime.UtcNow,
                            StartDateTime = DateTime.UtcNow.AddDays(15),
                            EndDateTime = DateTime.UtcNow.AddDays(15).AddHours(5),
                            GuestsQuantity = 5,
                            Status = BookingRequest.RequestStatus.Created,
                            Comment = "Хочу того да сего да побольше"
                        },
                        new BookingRequest()
                        {
                            Id = 2,
                            PlaceId = 1,
                            UserId = renterId,
                            CreationDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(10)),
                            StartDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(10)).AddDays(2),
                            EndDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(10)).AddHours(4),
                            GuestsQuantity = 10,
                            Status = BookingRequest.RequestStatus.Accepted
                        }
                    },
                    Discounts = new List<Discount>()
                    {
                        new Discount()
                        {
                            Id = 1,
                            PlaceId = 1,
                            Procents = 10,
                            FromHoursQuantity = 3
                        },
                        new Discount()
                        {
                            Id = 2,
                            PlaceId = 1,
                            Procents = 20,
                            FromHoursQuantity = 5
                        }
                    },
                    Charges = new List<Charge>()
                    {
                        new Charge()
                        {
                            PlaceId = 1,
                            FromGuestsQuantity = 10,
                            Procents = 10,
                            Comment = "Уборка от 10 человек"
                        },
                        new Charge()
                        {
                            PlaceId = 1,
                            FromGuestsQuantity = 20,
                            Procents = 20,
                            Comment = "Уборка от 20 человек"
                        }
                    },
                    IsActive = true,
                    Shedule = new Shedule()
                    {
                        Monday = true,
                        Thursday = true,
                        Wednesday = true,
                        Friday = true,
                        Thuesday = true
                    }
                },
                new Place()
                {
                    Id = 2,
                    UserId = lessorId,
                    Title = "DefaultPlace 2",
                    City="DefaultCity",
                    Address="DefaultAddress 2",
                    Area= 40,
                    BaseRate =800,
                    BookingHorizonInDays = 10,
                    Prohibitions= new List<Prohibition>()
                    {
                        prohibitions[4],
                        prohibitions[5],
                        prohibitions[6],
                        prohibitions[7]
                    },
                    EventTypes = new List<EventType>()
                    {
                        eventTypes[3],
                        eventTypes[4],
                        eventTypes[5]
                    },
                    BookingRequests = new List<BookingRequest>()
                    {
                        new BookingRequest()
                        {
                            Id = 3,
                            PlaceId = 2,
                            UserId = renterId,
                            CreationDateTime = DateTime.UtcNow,
                            StartDateTime = DateTime.UtcNow.AddDays(20),
                            EndDateTime = DateTime.UtcNow.AddDays(20).AddHours(2),
                            GuestsQuantity = 10,
                            Status = BookingRequest.RequestStatus.Created
                        },
                        new BookingRequest()
                        {
                            Id = 4,
                            PlaceId = 2,
                            UserId = renterId,
                            CreationDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(8)),
                            StartDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(5)).AddDays(14),
                            EndDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(5)).AddHours(18),
                            GuestsQuantity = 30,
                            Status = BookingRequest.RequestStatus.Rejected
                        }
                    },
                    Discounts = new List<Discount>()
                    {
                        new Discount()
                        {
                            Id = 3,
                            PlaceId = 2,
                            Procents = 40,
                            FromHoursQuantity = 8
                        }
                    },
                    Charges = new List<Charge>()
                    {
                        new Charge()
                        {
                            PlaceId = 2,
                            FromGuestsQuantity = 40,
                            Procents = 30,
                        }
                    },
                    IsActive = true,
                    Shedule = new Shedule()
                    {
                        Monday = false,
                        Thursday = false,
                        Wednesday = true,
                        Friday = true,
                        Thuesday = true,
                        Sunday = false,
                        Saturday = false
                    }
                }
            };

            foreach (var place in defaultPlaces)
            {
                await context.Places.AddAsync(place);
            }

            await context.SaveChangesAsync();
        }
        public static async Task SeedDefaultComments(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            var place1 = await context.Places.FirstOrDefaultAsync(item => item.Id == 1);
            var place2 = await context.Places.FirstOrDefaultAsync(item => item.Id == 2);
            var renterId = (await userManager.FindByNameAsync("renter"))?.Id;

            if (place1 == null || place2 == null || renterId == null)
                return;

            var defaultComments = new List<Comment>()
            {
                new()
                {
                    UserId = renterId,
                    PlaceId = place1.Id,
                    LastEditTime= DateTime.UtcNow,
                    Rating = 4,
                    Text="Нормальная площадка",
                    IsBlocked=false
                },
                new()
                {
                    UserId = renterId,
                    PlaceId = place2.Id,
                    LastEditTime= DateTime.UtcNow,
                    Rating = 5,
                    Text="Отличная площадка",
                    IsBlocked=false
                }
            };

            foreach (var comment in defaultComments)
            {
                if (!context.Comments.Any(item => item.PlaceId == comment.PlaceId && item.UserId == comment.UserId))
                    await context.Comments.AddAsync(comment);
            }

            await context.SaveChangesAsync();
        }
    }
}
