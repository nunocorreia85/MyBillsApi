using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using MyBills.Api.BankTransactions;
using MyBills.Application.Accounts.Commands.CreateAccount;
using MyBills.Application.BankTransactions.Commands.CreateBankTransaction;
using MyBills.Application.BankTransactions.Commands.UpdateBankTransaction;
using MyBills.Application.IntegrationTests.Infrastructure;
using MyBills.Domain.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MyBills.Application.IntegrationTests
{
    [TestFixture]
    public class BankTransactionsTest
    {
        [OneTimeSetUp]
        public async Task Init()
        {
            var testHost = new TestHost();
            _mediatorService = testHost.ServiceProvider.GetRequiredService<IMediator>();
            await _mediatorService.Send(new CreateAccountCommand
            {
                Balance = 0,
                ExternalId = "Nuno",
                BankAccountNumber = "DE89370400440532013000"
            });
        }

        private IMediator _mediatorService;

        [Test]
        [Order(1)]
        public async Task CreateBankTransaction_Run_FailValidations()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createBankTransactionFunction = new CreateBankTransaction(_mediatorService);
            var createBankTransactionRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new CreateBankTransactionCommand
                {
                    Amount = 0
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result =
                await createBankTransactionFunction.Run(createBankTransactionRequest, mock.Object,
                    CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<BadRequestObjectResult>(result); });
        }

        [Test]
        [Order(2)]
        public async Task CreateBankTransaction_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createBankTransactionFunction = new CreateBankTransaction(_mediatorService);
            var createBankTransactionRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new CreateBankTransactionCommand
                {
                    Amount = 3,
                    Memo = "Groceries",
                    CategoryId = 1,
                    AccountId = 1
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result =
                await createBankTransactionFunction.Run(createBankTransactionRequest, mock.Object,
                    CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                Assert.AreEqual(1, ((OkObjectResult) result).Value);
            });
        }

        [Test]
        [Order(3)]
        public async Task UpdateBankTransaction_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var updateBankTransactionFunction = new UpdateBankTransaction(_mediatorService);
            var updateBankTransactionRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new UpdateBankTransactionCommand
                {
                    Id = 1,
                    Memo = "Groceries",
                    Amount = 6
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result =
                await updateBankTransactionFunction.Run(updateBankTransactionRequest, mock.Object,
                    CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<OkResult>(result); });
        }

        [Test]
        [Order(4)]
        public async Task DeletedBankTransactions_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var getBankTransactions = new DeleteBankTransactions(_mediatorService);
            var httpRequest = CreateHttpRequest("id", "1");

            // act
            var result = await getBankTransactions.Run(httpRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<OkResult>(result); });
        }

        [Test]
        [Order(5)]
        public async Task GetBankTransactions_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var getBankTransactions = new GetBankTransactions(_mediatorService);
            var httpRequest = CreateHttpRequest("id", "1");

            // act
            var result = await getBankTransactions.Run(httpRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okObjectResult = (OkObjectResult) result;
                var bankTransactions = (List<BankTransaction>) okObjectResult.Value;
                Assert.IsNotNull(bankTransactions);
                Assert.AreEqual(1, bankTransactions.Count);
                Assert.AreEqual("Groceries", bankTransactions[0].Memo);
                Assert.AreEqual(6.0M, bankTransactions[0].Amount);
                Assert.AreEqual(true, bankTransactions[0].Deleted);
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

        private static HttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue));
            return request;
        }
    }
}