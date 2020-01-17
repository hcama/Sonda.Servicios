using FluentValidation;
using Sonda.Api.Resources;

namespace Sonda.Api.Validators
{
    public class SaveClienteResourceValidator : AbstractValidator<SaveClienteResource>
    {
        public SaveClienteResourceValidator()
        {
            RuleFor(m => m.ApellidoPaterno)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(m => m.ApellidoMaterno)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(m => m.Nombre)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(m => m.TipoClienteId)
                .NotEmpty()
                ;
        }
    }
}
