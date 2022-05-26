using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _90Graus.Catalogo.Domain
{
    public class CatalogoDbContext : DbContext
    {
        public DbSet<CatalogoDeProduto> CatalogoDeProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
            : base(options)
        {

        }
    }
}
