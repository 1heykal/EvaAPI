using EvaAPI.Entities;
using EvaAPI.Repositories;
using EvaAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorrowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitOfWork.BorrowRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var borrow = await _unitOfWork.BorrowRepository.GetByIdAsync(id);
            return Ok(borrow);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Borrow borrow)
        {
            borrow.BorrowDate = DateTime.UtcNow;
            await _unitOfWork.BorrowRepository.AddAsync(borrow);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = borrow.Id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Borrow borrow)
        {
            var borrowEntity = await _unitOfWork.BorrowRepository.GetByIdAsync(id);

            if (borrowEntity is null)
                return NotFound($"There is no Borrow with the provided id: {id}");

            _unitOfWork.BorrowRepository.Update(borrow);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrow = await _unitOfWork.BorrowRepository.GetByIdAsync(id);

            if (borrow is null)
                return NotFound($"There is no Borrow with the provided id: {id}");

            _unitOfWork.BorrowRepository.Delete(borrow);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
