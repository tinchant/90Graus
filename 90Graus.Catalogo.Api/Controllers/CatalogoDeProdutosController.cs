using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _90Graus.Catalogo.Domain;

namespace _90Graus.Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoDeProdutosController : ControllerBase
    {
        private readonly CatalogoDbContext _context;

        public CatalogoDeProdutosController(CatalogoDbContext context)
        {
            _context = context;
        }

        // GET: api/CatalogoDeProdutoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogoDeProduto>>> GetCatalogoDeProdutos()
        {
          if (_context.CatalogoDeProdutos == null)
          {
              return NotFound();
          }
            return await _context.CatalogoDeProdutos.ToListAsync();
        }

        // GET: api/CatalogoDeProdutoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoDeProduto>> GetCatalogoDeProduto(int id)
        {
          if (_context.CatalogoDeProdutos == null)
          {
              return NotFound();
          }
            var catalogoDeProduto = await _context.CatalogoDeProdutos.Include(catalogo => catalogo.Produtos).SingleOrDefaultAsync(catalogo => catalogo.Id == id);

            if (catalogoDeProduto == null)
            {
                return NotFound();
            }

            return catalogoDeProduto;
        }

        // PUT: api/CatalogoDeProdutoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogoDeProduto(int id, CatalogoDeProduto catalogoDeProduto)
        {
            if (id != catalogoDeProduto.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalogoDeProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoDeProdutoExists(id))
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

        [HttpPut("{id}/produtos")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {

            var catalogo = await _context.CatalogoDeProdutos.Include(c => c.Produtos).SingleOrDefaultAsync(catalogo => catalogo.Id == id);
            if (catalogo == null || !catalogo.Produtos.Any(produto => produto.Id == produto.Id))
                return NotFound();

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoDeProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost("{id}/produtos")]
        public async Task<IActionResult> PostProduto(int id, Produto produto)
        {

            var catalogo = await _context.CatalogoDeProdutos.Include(c=> c.Produtos).SingleOrDefaultAsync(catalogo => catalogo.Id == id);
            if(catalogo == null)
                return NotFound();

            catalogo.Produtos.Add(produto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoDeProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}/produtos")]
        public async Task<IActionResult> DeleteProduto(int id, int produtoId)
        {

            var catalogo = await _context.CatalogoDeProdutos.Include(c => c.Produtos).SingleOrDefaultAsync(catalogo => catalogo.Id == id);
            if (catalogo == null || !catalogo.Produtos.Any(produto => produto.Id == produtoId))
                return NotFound();

            _context.Produtos.Remove(catalogo.Produtos.Single(produto => produto.Id == produtoId));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoDeProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/CatalogoDeProdutoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatalogoDeProduto>> PostCatalogoDeProduto(CatalogoDeProduto catalogoDeProduto)
        {
          if (_context.CatalogoDeProdutos == null)
          {
              return Problem("Entity set 'CatalogoDbContext.CatalogoDeProdutos'  is null.");
          }
            _context.CatalogoDeProdutos.Add(catalogoDeProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalogoDeProduto", new { id = catalogoDeProduto.Id }, catalogoDeProduto);
        }

        // DELETE: api/CatalogoDeProdutoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogoDeProduto(int id)
        {
            if (_context.CatalogoDeProdutos == null)
            {
                return NotFound();
            }
            var catalogoDeProduto = await _context.CatalogoDeProdutos.FindAsync(id);
            if (catalogoDeProduto == null)
            {
                return NotFound();
            }

            _context.CatalogoDeProdutos.Remove(catalogoDeProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoDeProdutoExists(int id)
        {
            return (_context.CatalogoDeProdutos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
