using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly IBrainstormSessionRepository _sessionRepository;

        public SessionController(ILogger<SessionController> logger, IBrainstormSessionRepository sessionRepository)
        {
            _logger = logger;
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var session = await _sessionRepository.GetByIdAsync(id.Value);
            _logger.LogDebug("Session with ID: {Id}, {session}", id.Value, session);
            if (session == null)
            {
                _logger.LogDebug("Session not found with ID: {Id}", id.Value);
                return Content("Session not found.");
            }

            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };
            _logger.LogDebug("Session view model: {viewModel}", viewModel);

            return View(viewModel);
        }
    }
}
