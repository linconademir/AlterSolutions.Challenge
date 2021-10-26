using AlterSolutions.Challenge.DataAccess.Context;
using AlterSolutions.Challenge.Domain;
using AlterSolutions.Challenge.Repository.Entity;
using AlterSolutions.Comum.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterSolutions.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IRepositoryAlterSolutions<Product, int> _repositoryProducts
            = new RepositoryProduct(new ChallengeDbContext());

        //[HttpGet]
        //public IEnumerable<Product> Get()
        //{
        //    return _repositoryProducts.Get();
        //}

        [HttpGet]
        public IActionResult Get(int? id)
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
            return Ok(product);
        }

    }

}
