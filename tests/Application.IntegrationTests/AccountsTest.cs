using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using MyBills.Api.Accounts;
using MyBills.Application.IntegrationTests.Common;
using MyBills.Application.IntegrationTests.Infrastructure;
using MyBills.Application.Shared.Accounts.Commands;
using MyBills.Domain.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MyBills.Application.IntegrationTests
{
    [TestFixture]
    public class AccountsTest
    {
        [OneTimeSetUp]
        public void Init()
        {
            var testHost = new TestHost();
            _mediatorService = testHost.ServiceProvider.GetRequiredService<IMediator>();
            _userId = new Guid("254e16b6-b254-40f3-a4e1-cd0f76ce4cb7");
        }

        private IMediator _mediatorService;
        private Guid _userId;

        [Test]
        [Order(1)]
        public async Task CreateAccount_Run_FailValidations()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createAccountFunction = new CreateAccount(_mediatorService);
            var createAccountRequest = new HttpRequestMessage
            {
                Headers =
                {
                    Authorization = HttpRequestMessageUtils.GetAuthorization()
                },
                Content = new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand
                {
                    Balance = 3,
                    BankAccountNumber = "123445544"
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result = await createAccountFunction.Run(createAccountRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<BadRequestObjectResult>(result); });
        }

        [Test]
        [Order(2)]
        public async Task CreateAccount_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createAccountFunction = new CreateAccount(_mediatorService);
            var createAccountRequest = new HttpRequestMessage
            {
                Headers =
                {
                    Authorization = HttpRequestMessageUtils.GetAuthorization()
                },
                Content = new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand
                {
                    Balance = 3,
                    UserId = _userId,
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

        [Test]
        [Order(3)]
        public async Task UpdateAccount_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var updateAccountFunction = new UpdateAccount(_mediatorService);
            var updateAccountRequest = new HttpRequestMessage
            {
                Headers =
                {
                    Authorization = HttpRequestMessageUtils.GetAuthorization()
                },
                Content = new StringContent(JsonConvert.SerializeObject(new UpdateAccountCommand
                {
                    BankAccountNumber = "CH9300762011623852957"
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result = await updateAccountFunction.Run(updateAccountRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<OkResult>(result); });
        }

        [Test]
        [Order(4)]
        public async Task CloseAccounts_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var closeAccount = new CloseAccounts(_mediatorService);
            var httpRequestMessage = HttpRequestMessageUtils.GetHttpRequestMessage();

            // act            
            var result = await closeAccount.Run(httpRequestMessage, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<OkResult>(result); });
        }

        [Test]
        [Order(5)]
        public async Task GetAccounts_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var getAccounts = new GetAccounts(_mediatorService);

            // act
            var result = await getAccounts.Run(HttpRequestMessageUtils.GetHttpRequestMessage(), mock.Object,
                CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okObjectResult = (OkObjectResult) result;
                var accounts = (List<Account>) okObjectResult.Value;
                Assert.IsNotNull(accounts);
                Assert.AreEqual(1, accounts.Count);
                Assert.AreEqual("CH9300762011623852957", accounts[0].BankAccountNumber);
                Assert.AreEqual(3.0M, accounts[0].Balance);
                Assert.AreEqual(true, accounts[0].Closed);
                Assert.AreEqual(_userId, accounts[0].UserId);
            });
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                {key, value}
            };
            return qs;
        }
    }
}