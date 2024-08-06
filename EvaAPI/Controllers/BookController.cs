using EvaAPI.Entities;
using EvaAPI.Repositories;
using EvaAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitOfWork.BookRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            return Ok(book);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = book.Id });
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            var bookEntity = await _unitOfWork.BookRepository.GetByIdAsync(id);

            if (bookEntity is null)
                return NotFound($"There is no Book with the provided id: {id}");
            
            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);

            if (book is null)
                return NotFound($"There is no Book with the provided id: {id}");
            
            _unitOfWork.BookRepository.Delete(book);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}