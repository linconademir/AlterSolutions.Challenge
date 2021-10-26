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
    /// Endpoint responsible for controlling all Http verbs in the CATEGORY domain application
    /// In that Endpoin are contained Post; Put; Get; Delete and Patch
    /// </summary>
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private readonly IRepositoryAlterSolutions<Category, int> _repositoryCategory
           = new RepositoryCategory(new ChallengeDbContext());

        public CategoryController() { }
        public CategoryController(ChallengeDbContext context)
        {
            _repositoryCategory = new RepositoryCategory(context);
        }

        /// <summary>
        /// Getting category from the database.
        /// </summary>
        /// <param name="page">Number Page to view.</param>
        /// <param name="countReg">Quantity of registers for view.</param>
        /// <returns>Json with list os Category.</returns>
        [Route("Get")]
        public IHttpActionResult Get(int page = 1, int countReg = 10)
        {
            List<Category> categorysTotal = _repositoryCategory.Get();
            int totalPages = (int)Math.Ceiling(categorysTotal.Count() / Convert.ToDecimal(countReg));
            if (page < totalPages)
            {
                System.Web.HttpContext.Current.Response.AddHeader("X-Pages-NextPage:", (page + 1).ToString());
            }
            System.Web.HttpContext.Current.Response.AddHeader("X-Pages-TotalPages", totalPages.ToString());
            var categoryReturn = categorysTotal.OrderBy(p => p.Id).Skip(countReg * (page - 1)).Take(countReg);

            return Ok(categoryReturn);
        }

        /// <summary>
        /// Getting Specific category by Id in database.
        /// </summary>
        /// <param name="id">Category's Primary key for identify the register in database.</param>
        /// <returns>Object Category in Json Format.</returns>
        [Route("Get{id}")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Category category = _repositoryCategory.GetById(id.Value);

            if (category == null)
            {
                return NotFound();
            }
            return Content(HttpStatusCode.Found, category);
        }

        /// <summary>
        /// Route for Insert Category object in database.
        /// </summary>
        /// <param name="category">Object Category.</param>
        /// <returns>Category inserted in Json Format.</returns>
        [Route("Post")]
        public IHttpActionResult Post([FromBody] Category category)
        {
            try
            {
                _repositoryCategory.Save(category);
                return Created($"{Request.RequestUri}/{category.Id}", category);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Route for Update Category object in database.
        /// </summary>
        /// <param name="id">Category's Primary key for identify the register in database.</param>
        /// <param name="category">Object Category with Modified fields.</param>
        /// <returns>Category updated in Json Format.</returns>
        [Route("Put")]
        public IHttpActionResult Put(int? id, [FromBody] Category category)
        {
            try
            {
                if (!id.HasValue) return BadRequest();

                category.Id = id.Value;
                _repositoryCategory.Update(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Route for Delete Category object in database.
        /// </summary>
        /// <param name="id">Category's Primary key for identify the register in database.</param>
        /// <returns>Code 200 with delete's confirmation.</returns>
        [Route("Delete")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue) return BadRequest();

                Category category = _repositoryCategory.GetById(id.Value);
                if (category == null) return NotFound();

                _repositoryCategory.DeleteById(id.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Route for Update Category Specific fields in database.
        /// </summary>
        /// <param name="id">Category's Primary key for identify the register in database.</param>
        /// <param name="patchCategory">The object path containing value, path and operation.</param>
        /// <returns>Category updated in Json Format.</returns>
        [Route("Patch")]
        public IHttpActionResult Patch(int id, [FromBody] JsonPatchDocument<Category> patchCategory)
        {
            var category = _repositoryCategory.GetById(id);

            if (category != null)
            {
                patchCategory.ApplyTo(category);
            }
            _repositoryCategory.Update(category);
            return Ok(category);
        }
    }
}
