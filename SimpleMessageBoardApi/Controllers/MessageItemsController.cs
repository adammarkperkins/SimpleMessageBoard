using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMessageBoardApi.Models;

namespace SimpleMessageBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageItemsController : ControllerBase
    {
        private readonly SimpleMessageBoardContext _context;

        public MessageItemsController(SimpleMessageBoardContext context)
        {
            _context = context;
        }

        // GET: api/MessageItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageItem>>> GetMessages()
        {
            return await _context.MessageItems.ToListAsync();
        }

        // GET: api/MessageItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageItem>> GetMessageItem(int id)
        {
            var messageItem = await _context.MessageItems.FindAsync(id);

            if (messageItem == null)
            {
                return NotFound();
            }

            return messageItem;
        }

        // PUT: api/MessageItems/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageItem(int id, MessageItem messageItem)
        {
            if (id != messageItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(messageItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MessageItems
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageItem>> PostMessageItem(MessageItem messageItem)
        {
            _context.MessageItems.Add(messageItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessageItem), new { id = messageItem.Id }, messageItem);
        }

        // DELETE: api/MessageItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageItem(int id)
        {
            var messageItem = await _context.MessageItems.FindAsync(id);
            if (messageItem == null)
            {
                return NotFound();
            }

            _context.MessageItems.Remove(messageItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageItemExists(int id)
        {
            return _context.MessageItems.Any(e => e.Id == id);
        }
    }
}
