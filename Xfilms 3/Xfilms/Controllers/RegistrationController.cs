using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xfilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<RegistrationController> _logger;
        public RegistrationController(IRegistrationService registrationService,ILogger<RegistrationController> logger)
        {

            _logger = logger;
            _registrationService = registrationService;
        }


        [HttpPost]
        public async Task<ActionResult> RegistrationAccount(UserDTO account)
        {


            if (ModelState.IsValid)
            {


                _logger.LogInformation("OK");

            }
            else {

                _logger.LogInformation("!OK");
            
            
            }



            if (account != null)
            {

                await _registrationService.AccountRegistration(account);

                return Ok(new { user = true });

            }
            else {


                return Ok(new { user = false });
            }

            

        }
        
    }
}
