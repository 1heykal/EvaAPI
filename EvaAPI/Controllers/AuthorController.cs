using EvaAPI.Entities;
using EvaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EvaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepository;
        
        public AuthorController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _authorRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return Ok(author);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Author author)
        {
            await _authorRepository.AddAsync(author);
            
            return CreatedAtAction(nameof(GetById), new { id = author.Id });
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Author author)
        {
            var authorEntity = await _authorRepository.GetByIdAsync(id);

            if (authorEntity is null)
                return NotFound($"There is no Author with the provided id: {id}");
            
            await _authorRepository.UpdateAsync(author);
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                return NotFound($"There is no Author with the provided id: {id}");
            
            await _authorRepository.DeleteAsync(author);
            return NoContent();
        }

    }
}
