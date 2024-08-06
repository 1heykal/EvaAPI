using EvaAPI.Entities;
using EvaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IRepository<Borrow> _borrowRepository;

        public BorrowController(IRepository<Borrow> borrowRepository)
        {
            _borrowRepository = borrowRepository ?? throw new ArgumentNullException(nameof(borrowRepository));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _borrowRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var borrow = await _borrowRepository.GetByIdAsync(id);
            return Ok(borrow);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Borrow borrow)
        {
            borrow.BorrowDate = DateTime.UtcNow;
            await _borrowRepository.AddAsync(borrow);

            return CreatedAtAction(nameof(GetById), new { id = borrow.Id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Borrow borrow)
        {
            var borrowEntity = await _borrowRepository.GetByIdAsync(id);

            if (borrowEntity is null)
                return NotFound($"There is no Borrow with the provided id: {id}");

            await _borrowRepository.UpdateAsync(borrow);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrow = await _borrowRepository.GetByIdAsync(id);

            if (borrow is null)
                return NotFound($"There is no Borrow with the provided id: {id}");

            await _borrowRepository.DeleteAsync(borrow);
            return NoContent();
        }
    }
}
