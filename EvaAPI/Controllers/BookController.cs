using EvaAPI.Entities;
using EvaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;
        
        public BookController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bookRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return Ok(book);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            await _bookRepository.AddAsync(book);
            
            return CreatedAtAction(nameof(GetById), new { id = book.Id });
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            var bookEntity = await _bookRepository.GetByIdAsync(id);

            if (bookEntity is null)
                return NotFound($"There is no Book with the provided id: {id}");
            
            await _bookRepository.UpdateAsync(book);
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                return NotFound($"There is no Book with the provided id: {id}");
            
            await _bookRepository.DeleteAsync(book);
            return NoContent();
        }
    }
}