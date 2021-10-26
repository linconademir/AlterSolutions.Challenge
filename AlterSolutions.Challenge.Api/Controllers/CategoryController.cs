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
    public class CategoryController : ControllerBase
    {
        private IRepositoryAlterSolutions<Category, int> _repositoryCategory
            = new RepositoryCategory(new ChallengeDbContext());

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _repositoryCategory.Get();
        }

        [HttpGet]
        public Category Get(int? id)
        {
            return _repositoryCategory.GetById(id.Value);
        }

    }
}
