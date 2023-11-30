using Cabs.Areas.Website.ModelDtos;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Repositories.Company;
using Cabs.CustomStatusCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cabs.Areas.Website.Controllers
{
    [Route("api/[controller]/[action]")]
    [Area("Website")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies()
        {
            try
            {
                var resources = await _companyRepository.GetCompanyAsync();
                if (resources != null && resources.Any())
                {
                    var response = new CustomStatusResult<IEnumerable<CompanyModel>>(
                        StatusCodes.Status200OK,
                        "Get list category successfully",
                        resources,
                        null
                    );
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<IEnumerable<CompanyModel>>(
                        StatusCodes.Status404NotFound,
                        "Not found result or result empty",
                        null,
                        null
                    );
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomStatusResult<IEnumerable<CompanyModel>>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }.ToString()
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompany(int id)
        {
            try
            {
                var resource = await _companyRepository.GetCompanyByIdAsync(id);
                if (resource == null)
                {
                    var response = new CustomStatusResult<CompanyModel>(404, "Resource not found", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomStatusResult<CompanyModel>(200, "Get category successfully", resource, null);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomStatusResult<CompanyModel>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }.ToString() // Convert the list to a single string
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<CompanyDto>> AddCompany([FromForm] CompanyDto companyDto, IFormFile? photo)
        {
            try
            {
                var resource = await _companyRepository.AddCompanyAsync(companyDto, photo);
                if (resource != null)
                {
                    var response = new CustomStatusResult<CompanyDto>(StatusCodes.Status201Created, "Resource created", resource, null);
                    return Ok(response);
                }
                else
                {
                    var reponse = new CustomStatusResult<CompanyModel>(StatusCodes.Status400BadRequest, "Unable to create resource", null, null);
                    return BadRequest(reponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyModel>> UpdateCompany([FromForm] CompanyDto companyDto, int id, IFormFile? photo)
        {
            try
            {
                var resource = await _companyRepository.UpdateCompanyAsync(companyDto, id, photo);
                if (resource != null)
                {
                    var response = new CustomStatusResult<CompanyModel>(StatusCodes.Status200OK, "update category successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<CompanyModel>(StatusCodes.Status400BadRequest, "No category to update", resource, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCompany(int id)
        {
            bool resourceDeleted = false;
            var resource = await _companyRepository.GetCompanyByIdAsync(id);
            if (resource != null)
            {
                resourceDeleted = await _companyRepository.DeleteCompanyAsync(id);
            }
            if (resourceDeleted)
            {
                var response = new CustomStatusResult<string>(200,
                    "Resource deleted successfully", null, null);
                return Ok(response);

            }
            else
            {
                var response = new CustomStatusResult<string>(404,
                    "Resource not found or unable to delete", null, null);
                return NotFound(response);
            }
        }
    }
}
