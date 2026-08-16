using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [ApiController]
    [Route("ui")]
    public class ReactUiController : ControllerBase
    {
        #region Fields
        private const string GalagaIndexPath = "/galaga/index.html";
        #endregion

        #region Public Methods
        [HttpGet("galaga")]
        public IActionResult GetGalagaUi()
        {
            return Redirect(GalagaIndexPath);
        }
        #endregion
    }
}
