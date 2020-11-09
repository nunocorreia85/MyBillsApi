using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using MyBills.Api.Accounts;
using MyBills.Application.Accounts.Commands.CreateAccount;
using MyBills.Application.IntegrationTests.Infrastructure;
using MyBills.Domain.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MyBills.Application.IntegrationTests
{
    [TestFixture]
    public class AccountsTest
    {
        
        private IMediator _mediatorService;


        [OneTimeSetUp]
        public void Init()
        {
            var testHost = new TestHost();
            _mediatorService = testHost.ServiceProvider.GetRequiredService<IMediator>();
        }

        [Test, Order(1)]
        public async Task CreateAccount_Run_FailValidations()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createAccountFunction = new CreateAccount(_mediatorService);
            var createAccountRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand
                {
                    Balance = 1,
                    OwnerName = "Joao",
                    BankAccountNumber = "123445544"
                }), Encoding.UTF8, "application/json")
            };
    
            // act
            var result = await createAccountFunction.Run(createAccountRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            });
        }
        
        [Test, Order(2)]
        public async Task CreateAccount_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createAccountFunction = new CreateAccount(_mediatorService);
            var createAccountRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand
                {
                    Balance = 1,
                    OwnerName = "Joao",
                    BankAccountNumber = "DE89370400440532013000"
                }), Encoding.UTF8, "application/json")
            };
    
            // act
            var result = await createAccountFunction.Run(createAccountRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                Assert.AreEqual(1, ((OkObjectResult) result).Value);
            });
        }
        
        [Test, Order(3)]
        public async Task Run_CloseAccounts_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var getAccounts = new CloseAccounts(_mediatorService);
            var httpRequest = CreateHttpRequest("id", "1");

            // act
            var result = await getAccounts.Run(httpRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkResult>(result);
                var okObjectResult = (OkResult) result;
            });
        }
        
        [Test, Order(4)]
        public async Task Run_GetAccounts_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var getAccounts = new GetAccounts(_mediatorService);
            var httpRequest = CreateHttpRequest("id", "1");

            // act
            var result = await getAccounts.Run(httpRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okObjectResult = (OkObjectResult) result;
                var accounts = (List<Account>)okObjectResult.Value;
                Assert.IsNotNull(accounts);
                Assert.AreEqual(1, accounts.Count);
                Assert.AreEqual("DE89370400440532013000", accounts[0].BankAccountNumber);
                Assert.AreEqual(1.0M, accounts[0].Balance );
                Assert.AreEqual(true, accounts[0].Closed );
            });
        }
        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }

        public static HttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue));
            return request;
        }
    }
}