
using Cabs.Areas.Website.ModelDtos;
using Cabs.Areas.Website.Models;

namespace Cabs.Areas.Website.Repositories.Company
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyModel>> GetCompanyAsync();
        Task<CompanyModel> GetCompanyByIdAsync(int id);
        Task<CompanyDto> AddCompanyAsync(CompanyDto companyDto, IFormFile photo);
        Task<CompanyModel> UpdateCompanyAsync(CompanyDto companyDto, int id, IFormFile? photo);
        Task<bool> DeleteCompanyAsync(int id);
    }
}
