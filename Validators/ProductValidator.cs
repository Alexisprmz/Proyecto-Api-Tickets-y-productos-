using FluentValidation;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.Nombre)  
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.");

        RuleFor(x => x.Precio)  
            .GreaterThan(0).WithMessage("El precio debe ser mayor que 0.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser 0 o positivo.");
    }
}
