using FluentValidation;

public class CartItemValidator : AbstractValidator<CartItemDto>
{
    public CartItemValidator()
    {
        RuleFor(x => x.ProductoId).GreaterThan(0).WithMessage("ID de producto inválido");
        RuleFor(x => x.Cantidad).GreaterThan(0).WithMessage("Cantidad debe ser mayor a 0");
        RuleFor(x => x.Cantidad).LessThanOrEqualTo(100).WithMessage("Cantidad máxima 100 unidades");
    }
}
