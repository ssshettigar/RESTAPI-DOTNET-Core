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
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            //return new string[] { "value1", "value2" };
            return _quoteContext.Quotes;
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public Quote Get(int id)
        {
            return _quoteContext.Quotes.Find(id);
        }

        // POST: api/Quotes
        [HttpPost]
        public void Post([FromBody] Quote quote)
        {
            _quoteContext.Quotes.Add(quote);
            _quoteContext.SaveChanges();
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Quote quote)
        {
           Quote retrivedRecord= _quoteContext.Quotes.Find(id);
            retrivedRecord.Author = quote.Author;
            retrivedRecord.Description = quote.Description;
            retrivedRecord.Title = quote.Title;
            _quoteContext.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Quote retrivedRecord = _quoteContext.Quotes.Find(id);
            _quoteContext.Quotes.Remove(retrivedRecord);
            _quoteContext.SaveChanges();
        }
    }
}
