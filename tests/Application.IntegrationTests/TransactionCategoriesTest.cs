using System;
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
using MyBills.Api.TransactionCategories;
using MyBills.Application.Accounts.Commands.CreateAccount;
using MyBills.Application.IntegrationTests.Infrastructure;
using MyBills.Application.Shared.Accounts.Commands;
using MyBills.Application.Shared.TransactionCategories.Commands;
using MyBills.Application.TransactionCategories.Commands.CreateTransactionCategory;
using MyBills.Application.TransactionCategories.Commands.UpdateTransactionCategory;
using MyBills.Domain.Entities;
using MyBills.Domain.Enums;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MyBills.Application.IntegrationTests
{
    [TestFixture]
    public class TransactionCategoriesTest
    {
        [OneTimeSetUp]
        public async Task Init()
        {
            var testHost = new TestHost();
            _mediatorService = testHost.ServiceProvider.GetRequiredService<IMediator>();
            await _mediatorService.Send(new CreateAccountCommand
            {
                Balance = 0,
                ExternalId = Guid.NewGuid(),
                BankAccountNumber = "DE89370400440532013000"
            });
        }

        private IMediator _mediatorService;

        [Test]
        [Order(1)]
        public async Task CreateTransactionCategory_Run_FailValidations()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createTransactionCategoryFunction = new CreateTransactionCategory(_mediatorService);
            var createTransactionCategoryRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                    new CreateTransactionCategoryCommand
                    {
                        Description = "Example description",
                        RecurringPeriod = RecurringPeriod.Month
                    }), Encoding.UTF8, "application/json")
            };

            // act
            var result = await createTransactionCategoryFunction.Run(createTransactionCategoryRequest, mock.Object,
                CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<BadRequestObjectResult>(result); });
        }

        [Test]
        [Order(2)]
        public async Task CreateTransactionCategory_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var createTransactionCategoryFunction = new CreateTransactionCategory(_mediatorService);
            var createTransactionCategoryRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new CreateTransactionCategoryCommand
                {
                    Name = "Supermarket",
                    Description = "Groceries from supermarket",
                    AccountId = 1,
                    RecurringPeriod = RecurringPeriod.Month
                }), Encoding.UTF8, "application/json")
            };

            // act
            var result = await createTransactionCategoryFunction.Run(createTransactionCategoryRequest, mock.Object,
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
        public async Task UpdateTransactionCategory_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var updateTransactionCategoryFunction = new UpdateTransactionCategory(_mediatorService);
            var updateTransactionCategoryRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                    new UpdateTransactionCategoryCommand
                    {
                        Id = 1,
                        Name = "Drugs",
                        Description = "Drug Store",
                        RecurringPeriod = RecurringPeriod.Year
                    }), Encoding.UTF8, "application/json")
            };

            // act
            var result = await updateTransactionCategoryFunction.Run(updateTransactionCategoryRequest, mock.Object,
                CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<OkResult>(result); });
        }

        [Test]
        [Order(4)]
        public async Task DeletedTransactionCategories_Run_Success()
        {
            // arrange
            var mock = new Mock<ILogger>();
            var getTransactionCategories = new DisableTransactionCategories(_mediatorService);
            var httpRequest = CreateHttpRequest("id", "1");

            // act
            var result = await getTransactionCategories.Run(httpRequest, mock.Object, CancellationToken.None);

            // assert
            Assert.Multiple(() => { Assert.IsInstanceOf<OkResult>(result); });
        }

        [Test]
        [Order(5)]
        public async Task GetTransactionCategories_Run_Success()
        {
            // arrange
            var getTransactionCategories = new GetTransactionCategories(_mediatorService);
            var httpRequest = CreateHttpRequest("id", "1");

            // act
            var result = await getTransactionCategories.Run(httpRequest, CancellationToken.None);

            // assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okObjectResult = (OkObjectResult) result;
                var transactionCategories = (List<TransactionCategory>) okObjectResult.Value;
                Assert.IsNotNull(transactionCategories);
                Assert.AreEqual(1, transactionCategories.Count);
                Assert.AreEqual("Drugs", transactionCategories[0].Name);
                Assert.AreEqual("Drug Store", transactionCategories[0].Description);
                Assert.AreEqual(true, transactionCategories[0].Disabled);
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