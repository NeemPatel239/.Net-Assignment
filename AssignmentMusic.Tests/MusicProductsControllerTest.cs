using AssignmentMusic.Controllers;
using AssignmentMusic.Data;
using AssignmentMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AssignmentMusic.Tests
{
    [TestClass]
    public class MusicProductsControllerTest
    {

        List<MusicProducts> Musicproducts;

        private ApplicationDbContext _context;

        public MusicProductsController MusicproductsController { get; private set; }

        [TestInitialize]
        public void testInitialize()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);
            Musicproducts = new List<MusicProducts>();

            // 2. create mock data and add to in-memory database
            Company mockCompany = new Company { ID = 3, CompanyName = "A Fake Company" };

            Musicproducts.Add(new MusicProducts
            {
                ProductId = 103,
                ProductName = "Some Music Product",
                Price = 100,
                Company = mockCompany
            });

            Musicproducts.Add(new MusicProducts
            {
                ProductId = 32,
                ProductName = "Another Music Product",
                Price = 88,
                Company = mockCompany
            });

            // add each fake product to the list
            Musicproducts.Add(new MusicProducts
            {
                ProductId = 14,
                ProductName = "A Lousy Music Product",
                Price = 43,
                Company = mockCompany
            });

            foreach (var p in Musicproducts)
            {
                // add each product to in-memory db
                _context.MusicProducts.Add(p);
            }
            _context.SaveChanges();

            // 3. this is the last step
            MusicproductsController = new MusicProductsController(_context);
        }

        [TestMethod]
        public void IndexLoadsCorrectView()
        {
            // act
            var result = MusicproductsController.Index().Result;
            var viewResult = (ViewResult)result;

            // ASSERT
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsMusicProducts()
        {
            // act
            var result = MusicproductsController.Index().Result;

            // get the view result
            var viewResult = (ViewResult)result;

            // assert - convert result to list of products & compare to mock product list
            CollectionAssert.AreEqual(Musicproducts.OrderBy(p => p.ProductName).ToList(), (List<MusicProducts>)viewResult.Model);
        }
        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            var result = MusicproductsController.Details(8879).Result;

            // assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DetailsValidIdLoadsProduct()
        {
            // act
            var result = MusicproductsController.Details(127).Result;
            var viewResult = (ViewResult)result;

            // assert
            Assert.AreEqual(Musicproducts[1], viewResult.Model);
        }

        [TestMethod]
        public void CreatePostInvalidData()
        {
            // arrange -> create product object
            var product = new MusicProducts
            {
                ProductId = 9,
                Price = 94,
                Company = new Company { ID = 4, CompanyName = "JIO" }
            };

            MusicproductsController.ModelState.AddModelError("Error", "Fake model error");

            // act
            var result = MusicproductsController.Create(product);
            var viewResult = (ViewResult)result.Result;

            // assert
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void CreatePostInvalidDataPopulatesCategories()
        {
            // arrange -> create product object
            var Musicproducts = new MusicProducts
            {
                ProductId = 9,
                Price = 94,
                Company = new Company { ID = 4, CompanyName = "JIO" }
            };

            // manually create error in model
            MusicproductsController.ModelState.AddModelError("Error", "Fake model error");

            // act
            var result = MusicproductsController.Create(Musicproducts);
            var viewResult = (ViewResult)result.Result;

            // assert
            Assert.IsNotNull(viewResult.ViewData["CategoryId"]);
        }

        [TestMethod]
        public void CreatePostAddsProduct()
        {
            // arrange -> create product object
            var Musicproducts = new MusicProducts
            {
                ProductId = 9,
                ProductName = "Latest Product",
                Price = 94,
                Company = new Company { ID = 4, CompanyName = "JIO" }
            };

            // act
            var result = MusicproductsController.Create(Musicproducts);

            // assert
            Assert.AreEqual(_context.MusicProducts.LastOrDefault(), Musicproducts);
        }

        [TestMethod]
        public void CreatePostRedirectsToIndex()
        {
            // arrange -> create product object
            var Musicproducts = new MusicProducts
            {
                ProductId = 9,
                ProductName = "New Products",
                Price = 94,
                Company = new Company { ID = 4, CompanyName = "JIO" }
            };

            // act
            var result = MusicproductsController.Create(Musicproducts);
            var redirectResult = (RedirectToActionResult)result.Result;

            // assert
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
  
