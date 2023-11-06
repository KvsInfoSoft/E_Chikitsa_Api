using E_Chikitsa_Interfaces.InterfacesResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Chikitsa_Api.Controllers.Doctor
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        #region Variables
        private readonly IDoctors _doctors; 
        #endregion

        #region Constructor
        public DoctorsController(IDoctors doctors)
        {
            _doctors = doctors;
        } 
        #endregion

        #region GetDoctorDetail
        [HttpPost("GetDoctorDetail")]
        public async Task<IActionResult> GetDoctorDetail()
        {
            return Ok();
        } 
        #endregion
    }
}
