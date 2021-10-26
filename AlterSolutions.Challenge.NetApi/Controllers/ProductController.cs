using AlterSolutions.Challenge.DataAccess.Context;
using AlterSolutions.Challenge.Domain;
using AlterSolutions.Challenge.Repository.Entity;
using AlterSolutions.Comum.Repository.Interface;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlterSolutions.Challenge.NetApi.Controllers
{
    /// <summary>
    /// Endpoint responsible for controlling all Http verbs in the PRODUCT domain application
    /// In that Endpoin are contained Post; Put; Get; Delete and Patch
    /// </summary>
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly IRepositoryAlterSolutions<Product, int> _repositoryProducts
           = new RepositoryProduct(new ChallengeDbContext());

        /// <summary>
        /// Getting products from the database.
        /// </summary>
        /// <param name="page">Number Page to view.</param>
        /// <param name="countReg">Quantity of registers for view.</param>
        /// <returns>Json with list os Products.</returns>
        [Route("Get")]
        public IHttpActionResult Get(int page = 1, int countReg = 10)
        {
            List<Product> productsTotal = _repositoryProducts.Get();
            int totalPages = (int)Math.Ceiling(productsTotal.Count() / Convert.ToDecimal(countReg));
            if (page < totalPages)
            {
                System.Web.HttpContext.Current.Response.AddHeader("X-Pages-NextPage:", (page + 1).ToString());
            }
            System.Web.HttpContext.Current.Response.AddHeader("X-Pages-TotalPages", totalPages.ToString());
            var productsReturn = productsTotal.OrderBy(p => p.Id).Skip(countReg * (page - 1)).Take(countReg);
         
            return Ok(productsReturn);
        }

        /// <summary>
        /// Getting Specific product by Id in database.
        /// </summary>
        /// <param name="id">Product's Primary key for identify the register in database.</param>
        /// <returns>Object Product in Json Format.</returns>
        [Route("Get{id}")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Product product = _repositoryProducts.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }
            return Content(HttpStatusCode.Found, product);
        }

        /// <summary>
        /// Route for Insert Product object in database.
        /// </summary>
        /// <param name="product">Object Product.</param>
        /// <returns>Product inserted in Json Format.</returns>
        [Route("Post")]
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                _repositoryProducts.Save(product);
                return Created($"{Request.RequestUri}/{product.Id}", product);
             }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Route for Update Product object in database.
        /// </summary>
        /// <param name="id">Product's Primary key for identify the register in database.</param>
        /// <param name="product">Object Product with Modified fields.</param>
        /// <returns>Product updated in Json Format.</returns>
        [Route("Put")]
        public IHttpActionResult Put(int? id, [FromBody]Product product)
        {
            try
            {
                if (!id.HasValue) return BadRequest();

                product.Id = id.Value;
                _repositoryProducts.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Route for Delete Product object in database.
        /// </summary>
        /// <param name="id">Product's Primary key for identify the register in database.</param>
        /// <returns>Code 200 with delete's confirmation.</returns>
        [Route("Delete")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue) return BadRequest();

                Product product = _repositoryProducts.GetById(id.Value);
                if (product == null) return NotFound();

                _repositoryProducts.DeleteById(id.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Route for Update Product Specific fields in database.
        /// </summary>
        /// <param name="id">Product's Primary key for identify the register in database.</param>
        /// <param name="patchProduct">The object path containing value, path and operation.</param>
        /// <returns>Product updated in Json Format.</returns>
        [Route("Patch")]
        public IHttpActionResult Patch(int id, [FromBody] JsonPatchDocument<Product> patchProduct)
        {
            var product = _repositoryProducts.GetById(id);

            if (product != null)
            {
                patchProduct.ApplyTo(product);
            }
            _repositoryProducts.Update(product);
            return Ok(product);
        }
    }
}
