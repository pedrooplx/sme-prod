using FluentValidation;
using FluentValidation.Results;
using sme.business.Interfaces;
using sme.business.Models;
using sme.business.Notifications;

namespace sme.business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                Notificar(erro.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
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
