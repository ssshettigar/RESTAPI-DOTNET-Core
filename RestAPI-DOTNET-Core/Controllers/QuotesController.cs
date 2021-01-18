using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI_DOTNET_Core.Data;
using RestAPI_DOTNET_Core.Models;

namespace RestAPI_DOTNET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private QuoteDbContext _quoteContext;

        public QuotesController(QuoteDbContext quoteContext)
        {
            _quoteContext = quoteContext;
        }
        // GET: api/Quotes
        //[HttpGet]
        //public IEnumerable<Quote> Get()
        //{
        //    //return new string[] { "value1", "value2" };
        //    return _quoteContext.Quotes;
        //}

        //[HttpGet]
        //public IActionResult Get()
        //{

        //    //return Ok(_quoteContext.Quotes);
        //    return Ok(_quoteContext.Quotes);
        //}

        // GET: api/Quotes/5
        //[HttpGet("{id}", Name = "Get")]
        //public Quote Get(int id)
        //{
        //    return _quoteContext.Quotes.Find(id);
        //}

        [HttpGet]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;
            switch (sort)
            {
                case "ASC":
                    quotes= _quoteContext.Quotes.OrderBy(q => q.CreatedDateTime);
                    break;
                case "DESC":
                    quotes = _quoteContext.Quotes.OrderByDescending(q => q.CreatedDateTime);
                    break;
                default:
                    quotes = _quoteContext.Quotes;
                    break;
            }
            return Ok(quotes);
        }

        [HttpGet("[action]")]
        public IActionResult pageQuotes(int? pageNumber,int? pageSize)
        {
            IQueryable <Quote> quotes = _quoteContext.Quotes;

            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 1;

            return Ok(quotes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult searchQuotes(string Type)
        {
            IQueryable<Quote> quotes = _quoteContext.Quotes.Where(q=>q.Type.StartsWith(Type));

            

            return Ok(quotes);
        }

        // POST: api/Quotes
        //[HttpPost]
        //public void Post([FromBody] Quote quote)
        //{
        //    _quoteContext.Quotes.Add(quote);
        //    _quoteContext.SaveChanges();
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quoteContext.Quotes.Add(quote);
            _quoteContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Quote quote)
        //{
        //   Quote retrivedRecord= _quoteContext.Quotes.Find(id);
        //    retrivedRecord.Author = quote.Author;
        //    retrivedRecord.Description = quote.Description;
        //    retrivedRecord.Title = quote.Title;
        //    _quoteContext.SaveChanges();
        //}

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            Quote retrivedRecord = _quoteContext.Quotes.Find(id);
            if (retrivedRecord==null)
            {
                return NotFound($"No records present for the ID= {id}");
            }
            else
            {
                retrivedRecord.Author = quote.Author;
                retrivedRecord.Description = quote.Description;
                retrivedRecord.Title = quote.Title;
                retrivedRecord.Type = quote.Type;
                retrivedRecord.CreatedDateTime = quote.CreatedDateTime;
                _quoteContext.SaveChanges();
                return Ok("Record Updated Successfully");
            }
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    Quote retrivedRecord = _quoteContext.Quotes.Find(id);
        //    _quoteContext.Quotes.Remove(retrivedRecord);
        //    _quoteContext.SaveChanges();
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Quote retrivedRecord = _quoteContext.Quotes.Find(id);
            if (retrivedRecord == null)
            {
                return NotFound($"No records present for the ID= {id}");
            }
            else
            {
                //Quote retrivedRecord = _quoteContext.Quotes.Find(id);
                _quoteContext.Quotes.Remove(retrivedRecord);
                _quoteContext.SaveChanges();
                return Ok("Record Deleted Successfully");
            }
            
        }

        //api/Quotes/test/{id}
        //testing attribute based routing
        [HttpGet("[action]/{id}")]
        public int test(int id)
        {
            return id;
        }

    }
}
