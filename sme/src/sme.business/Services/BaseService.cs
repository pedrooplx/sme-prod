using FluentValidation;
using FluentValidation.Results;
using sme.business.Models;

namespace sme.business.Services
{
    public abstract class BaseService
    {
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                Notificar(erro.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem)
        {
            //Propagar o erro até a camada de apresentação
        }
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid)
            {
                return true;
            }
            else
            {
                Notificar(validator);
                return false;
            }
        }
    }
}
