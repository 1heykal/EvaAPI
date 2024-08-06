using EvaAPI.Entities;
using EvaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EvaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IRepository<Member> _memberRepository;
        
        public MemberController(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _memberRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            return Ok(member);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Member member)
        {
            member.JoinDate = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            await _memberRepository.AddAsync(member);
            
            return CreatedAtAction(nameof(GetById), new { id = member.Id });
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Member member)
        {
            var memberEntity = await _memberRepository.GetByIdAsync(id);

            if (memberEntity is null)
                return NotFound($"There is no Member with the provided id: {id}");
            
            await _memberRepository.UpdateAsync(member);
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member is null)
                return NotFound($"There is no Member with the provided id: {id}");
            
            await _memberRepository.DeleteAsync(member);
            return NoContent();
        }
        
    }
}
