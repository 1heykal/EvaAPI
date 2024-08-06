using EvaAPI.Entities;
using EvaAPI.Repositories;
using EvaAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitOfWork.MemberRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);
            return Ok(member);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Member member)
        {
            member.JoinDate = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            await _unitOfWork.MemberRepository.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = member.Id });
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Member member)
        {
            var memberEntity = await _unitOfWork.MemberRepository.GetByIdAsync(id);

            if (memberEntity is null)
                return NotFound($"There is no Member with the provided id: {id}");
            
            _unitOfWork.MemberRepository.Update(member);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);

            if (member is null)
                return NotFound($"There is no Member with the provided id: {id}");
            
            _unitOfWork.MemberRepository.Delete(member);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
