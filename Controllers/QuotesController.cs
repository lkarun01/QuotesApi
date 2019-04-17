using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        private QuotesDbContext _quotesContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesContext = quotesDbContext;
        }

        //static List<Quote> _quotes = new List<Quote>()
        //    {
        //        new Quote(){Id=0, Auther="Emily Dickson", Title="The bran is wider than sky", Description="Human brain related quotes"},
        //        new Quote(){Id=1, Auther="Lalanke Karunaratne", Title="Photography is the relaxation", Description="Quotes of photography"},
        //        new Quote(){Id=1, Auther="Jhone Doe", Title="Cars are the styles of the road", Description="Quotes related to vehicals"}
        //    };

        // GET: api/Quotes
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [AllowAnonymous]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;

            switch (sort)
            {
                case "desc":
                    quotes = _quotesContext.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quotesContext.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quotesContext.Quotes;
                    break;
            }
            return Ok(quotes);
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var quote = _quotesContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No records found against this id");
            }
            else
            {
                return Ok(quote);
            }
            
        }
        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        [HttpGet("[action]")]
        //[Route("[action]")]
        public IActionResult PagingQuote(int? pageNumber, int? pageSize)
        {
            var quotes = _quotesContext.Quotes;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 2;
            return Ok(quotes.Skip((currentPageNumber - 1)* currentPageSize).Take(currentPageSize));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SearchQuote(string type)
        {
            var quotes = _quotesContext.Quotes.Where(q => q.Type.StartsWith(type));
            return Ok(quotes);
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            quote.UserId = userId;
            _quotesContext.Quotes.Add(quote);
            _quotesContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote value)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var entity = _quotesContext.Quotes.Find(id);
            if (entity == null)
            {
                return NotFound("No records found against this id");
            }
            else if (userId != entity.UserId)
            {
                return BadRequest("Sorry you cant update this record..");
            }
            else
            {
                entity.Title = value.Title;
                entity.Auther = value.Auther;
                entity.Description = value.Description;
                entity.Type = value.Type;
                entity.CreatedAt = value.CreatedAt;
                _quotesContext.SaveChanges();
                return Ok("Record Updated Successfully.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = _quotesContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No records found against this id");
            }
            else
            {
                _quotesContext.Quotes.Remove(quote);
                _quotesContext.SaveChanges();
                return Ok("record releted");
            }
        }
    }
}