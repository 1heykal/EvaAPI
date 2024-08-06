using EvaAPI.Entities;
using EvaAPI.Repositories;
using EvaAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace EvaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitOfWork.AuthorRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            return Ok(author);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Author author)
        {
            await _unitOfWork.AuthorRepository.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = author.Id });
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Author author)
        {
            var authorEntity = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (authorEntity is null)
                return NotFound($"There is no Author with the provided id: {id}");
            
            _unitOfWork.AuthorRepository.Update(author);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (author is null)
                return NotFound($"There is no Author with the provided id: {id}");
            
            _unitOfWork.AuthorRepository.Delete(author);
            await _unitOfWork.SaveChangesAsync();
            
            return NoContent();
        }

    }
}
