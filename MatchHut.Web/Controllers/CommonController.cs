using AutoMapper;
using MatchHut.Core;
using MatchHut.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MatchHut.Web.Controllers
{
    [Route("api/common")]
    public class CommonController : BaseController<CommonController>
    {
        private readonly IWebHostEnvironment _env;
        public CommonController(IUnitOfWork unitOfWork,
            ILogger<CommonController> logger,
            IUrlHelper urlHelper,
            IWebHostEnvironment env,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
            : base(unitOfWork, logger, urlHelper, propertyMappingService, mapper)
        {
            _env = env;
        }

        
    }
}
