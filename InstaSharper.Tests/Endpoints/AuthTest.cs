using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class AuthTest
    {
        public AuthTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;
        [Fact]
        public async void CreateUserTest()
        {
            var username = "kajokoleha";
            var password = "ramtinjokar";
            var email = "ramtinak@live.com";
            var firstName = "Ramtin";
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = username,
                    Password = password
                });

            var createResult = await apiInstance.CreateNewAccount(username, password, email, firstName);
            Assert.True(createResult.Succeeded);
            Assert.True(createResult.Value.AccountCreated);
        }

        [Fact]
        public async void UserLoginFailTest()
        {
            var username = "alex_codegarage";
            var password = "boombaby!";

            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = username,
                    Password = password
                });
            _output.WriteLine("Got API instance");

            var loginResult = await apiInstance.LoginAsync();
            Assert.False(loginResult.Succeeded);
            Assert.False(apiInstance.IsUserAuthenticated);
        }

        [Fact]
        public async void UserLoginSuccessTest()
        {
            var username = "";
            var password = "";

            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });

            Assert.False(apiInstance.IsUserAuthenticated);
            var loginResult = await apiInstance.LoginAsync();
            Assert.True(loginResult.Succeeded);
            Assert.True(apiInstance.IsUserAuthenticated);

            var apiInstance2 = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });
            var loginResult2 = await apiInstance2.LoginAsync();
            Assert.True(loginResult2.Succeeded);
            Assert.Equal(loginResult2.Info.Message, "Session is valid");
        }
    }
}