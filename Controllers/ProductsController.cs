using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles="admin")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private readonly ProductService _productService;
    public ProductsController(ProductService productService) { _productService = productService; }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductDto dto) => Ok(await _productService.AddAsync(dto));

    [HttpPut("{id}")]
public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
{
    var updated = await _productService.UpdateAsync(id, dto);

    return Ok(new
    {
        message = "Producto actualizado correctamente",
        productoActualizado = new
        {
            updated.Id,
            updated.Nombre,
            updated.Precio,
            updated.Stock
        }
    });
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var deleted = await _productService.DeleteAsync(id);

    return Ok(new
    {
        message = "Producto eliminado correctamente",
        productoEliminado = new
        {
            deleted.Id,
            deleted.Nombre,
            deleted.Precio,
            deleted.Stock
        }
    });
}

}
