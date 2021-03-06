using MatchHut.Core;
using MatchHut.Infrastructure.Services;
using MatchHut.Infrastructure.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MatchHut.Web.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private readonly ApplicationData _applicationData;

        public HomeController(IUnitOfWork unitOfWork,
            ILogger<HomeController> logger,
            IUrlHelper urlHelper,
            IPropertyMappingService propertyMappingService,
            IOptions<JwtIssuerOptions> jwtIssuerOptions,
            ApplicationData applicationData)
            : base(unitOfWork, logger, urlHelper, propertyMappingService, null)
        {
            _jwtIssuerOptions = jwtIssuerOptions?.Value;
            _applicationData = applicationData;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["ApiUrl"] = _jwtIssuerOptions.Audience.TrimEnd('/');
            ViewData["Version"] = _applicationData.Version;

            return View("Dashboard");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}