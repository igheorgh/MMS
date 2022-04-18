using AutoFixture;
using AutoFixture.Kernel;
using DataLibrary.Models;
using DataLibrary.StatePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSApi.Tests.Users
{
    public static class UserHelpers
    {
        public static Fixture GetFixtureForUsers()
        {
            var fixture = new Fixture();
            fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(DataLibrary.StatePattern.State),
                    typeof(AssignedState))
            );
            return fixture;
        }

        public static List<User> GenerateUsers(int count)
        {
            var users = new List<User>();
            var fixture = GetFixtureForUsers();
            for(int i = 0; i < count; i++)
            {
                users.Add(fixture.Create<User>());
            }
            return users;
        }
    }
}
