using AlterSolutions.Challenge.DataAccess.Context; 
using AlterSolutions.Challenge.Domain;
using AlterSolutions.Challenge.NetApi.Controllers;
using AlterSolutions.Challenge.Repository.Entity;
using AlterSolutions.Comum.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace AlterSolutions.Challenge.NetApi.Test.Controller
{
    [TestClass]
    public class CategoryTest
    {
        private readonly IRepositoryAlterSolutions<Category, int> _repositoryCategory
         = new RepositoryCategory(new ChallengeDbContext());

        private CategoryController _controller = new CategoryController();
        private IRepositoryAlterSolutions<Category, int> _repository;


        [TestMethod]
        public void TestMethod1()
        {
            ////Arrange
            //_controller = new CategoryController();
            //var testCategory = GetTestCategory();

            //var controller = new CategoryController();
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            //var result = controller.Get() as OkNegotiatedContentResult<IEnumerable<Category>>;

            //// Assert.IsNotNull(result);
            //// Assert.IsNotNull(result.Content);
            //// Assert.AreEqual(3, result.Content.Count());

            // var result = controller.GetAllProducts() as List<Product>;
            //    Assert.AreEqual(testCategory.Count, result.Count);

            //Act

            //Asset
        }

        private List<Category> GetTestCategory()
        {
            var testCategory = new List<Category>();
            testCategory.Add(new Category { Id = 1, Description = "Category1", Active = 1 });
            testCategory.Add(new Category { Id = 2, Description = "Category2", Active = 1 });
            testCategory.Add(new Category { Id = 3, Description = "Category3", Active = 1 });
            testCategory.Add(new Category { Id = 4, Description = "Category4", Active = 0 });

            _repository = new RepositoryCategory(new ChallengeDbContext());

            return testCategory;
        }
    }
}
