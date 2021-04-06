using Microsoft.AspNetCore.Mvc;
using sme.business.Interfaces;

namespace sme.app.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            //Sempre deve ser a negação do tem notificação
            return !_notificador.TemNotificacao();
        }
    }
}
