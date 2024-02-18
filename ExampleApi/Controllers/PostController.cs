using ExampleApi.Database;
using ExampleApi.Structures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("Post")]
    public class PostController : Controller
    {
        private readonly PostDataContext ctx;
        public PostController(PostDataContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Post> Create([FromBody] Post post)
        {
            var entry = await ctx.AddAsync(post);
            await ctx.SaveChangesAsync();
            return entry.Entity;
        }
        
        [HttpGet]
        [Route("List")]
        public Task<List<Post>> List()
        {
            return ctx.Posts.ToListAsync();
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Post>> List(int id)
        {
            var r = await ctx.Posts.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (r == null)
            {
                return NotFound();
            }

            return r;
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody] Post post)
        {
            var r = await ctx.Posts.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (r == null)
            {
                return NotFound();
            }

            r.Name = post.Name;
            r.Content = post.Content;
            await ctx.SaveChangesAsync();
            return ctx.Entry(r).Entity;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await ctx.Posts.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (r == null)
            {
                return NotFound();
            }

            ctx.Posts.Remove(r);
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
}