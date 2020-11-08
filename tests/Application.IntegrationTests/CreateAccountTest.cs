using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MyBills.Api.Accounts;
using MyBills.Application.Accounts.Commands.CreateAccount;
using MyBills.Application.IntegrationTests.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MyBills.Application.IntegrationTests
{
    [TestFixture]
    public class CreateAccountTest
    {
        private readonly CreateAccount _createAccountFunction;

        public CreateAccountTest()
        {
            var testHost = new TestHost();
            _createAccountFunction = new CreateAccount(testHost.ServiceProvider.GetRequiredService<IMediator>());
        }

        [Test]
        public async Task Run_CreateAccount_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand
                {
                    Balance = 1,
                    OwnerName = "Joao",
                    BankAccountNumber = "DE89370400440532013000"
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result = await _createAccountFunction.Run(httpRequestMessage, mock.Object);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                Assert.AreEqual(1, ((OkObjectResult) result).Value);
            });
        }
    }
}