using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context) { _context = context; }

    public async Task<List<Producto>> GetAllAsync() =>
        await _context.Productos.ToListAsync();

    public async Task<Producto> GetByIdAsync(int id) =>
        await _context.Productos.FindAsync(id);

    public async Task<Producto> AddAsync(ProductDto dto)
    {
        var product = new Producto
        {
            Nombre = dto.Nombre,  
            Precio = dto.Precio, 
            Stock = dto.Stock
        };
        _context.Productos.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Producto> UpdateAsync(int id, ProductDto dto)
{
    var product = await _context.Productos.FindAsync(id);
    if (product == null) throw new Exception("Product not found");

    product.Nombre = dto.Nombre;
    product.Precio = dto.Precio;
    product.Stock = dto.Stock;

    await _context.SaveChangesAsync();

    return product;
}

public async Task<Producto> DeleteAsync(int id)
{
    var product = await _context.Productos.FindAsync(id);
    if (product == null) throw new Exception("Product not found");

    _context.Productos.Remove(product);
    await _context.SaveChangesAsync();

    return product;
}
}
