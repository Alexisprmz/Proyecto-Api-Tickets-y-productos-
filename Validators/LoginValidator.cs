using FluentValidation;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El usuario es obligatorio.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.");
    }
}
