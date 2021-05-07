using AVA.ForBrain.ApplicationCore.Entities;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVA.ForBrain.Validators
{
    public class UsuariosValidator : AbstractValidator<Usuarios>
    {
        static UsuariosValidator _instancia;
        public static UsuariosValidator Instancia
        {
            get { return _instancia ?? (_instancia = new UsuariosValidator()); }
        }

        public UsuariosValidator()
        {
            RuleFor(x => x.User).NotEmpty().WithMessage("campo obrigatório");
            RuleFor(x => x.Password).NotEmpty().WithMessage("campo obrigatório");
            RuleFor(x => x.Sobrenome).NotEmpty().WithMessage("campo obrigatório");
            RuleFor(x => x.Genero).NotEmpty().WithMessage("campo obrigatório");
            RuleFor(p => p.DtNascimento)
             .NotEmpty().WithMessage("campo obrigatório")
             .LessThan(p => DateTime.Now).WithMessage("a data deve estar no passado");
            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("campo obrigatório")
            .EmailAddress().WithMessage("email inválido")
            .OnAnyFailure(p => p.Email = "");
        }
    }
}
