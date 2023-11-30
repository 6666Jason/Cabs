using AutoMapper;
using Cabs.Areas.Website.ModelDtos;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Repositories.Company;
using Cabs.Areas.Website.Repositories.UploadFileRepo;
using Cabs.Data;

namespace Cabs.Areas.Website.Services.Company
{
    public class CompanyService : ICompanyRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public CompanyService(DatabaseContext dbContext, IFileRepository fileRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDto> AddCompanyAsync(CompanyDto companyDto, IFormFile photo)
        {
            var comp = _mapper.Map<CompanyModel>(companyDto);

            if (photo != null && photo.Length > 0)
            {
                var fileName = await _fileRepository.UploadFile(photo, "Company");

                // Create a new ImageModel instance for each image
                var imageModel = new ImageModel
                {
                    // Set properties of ImageModel, e.g., FileName, URL, etc.
                    FileName = fileName,
                    Url = "http://localhost:5065/Company/" + fileName
                };

                // Create a collection for Images if it's not already initialized
                comp.Images ??= new List<ImageModel>();

                // Add the new ImageModel to the Images collection
                comp.Images.Add(imageModel);
            }

            await _dbContext.Companies.AddAsync(comp);
            await _dbContext.SaveChangesAsync();

            var compDto = _mapper.Map<CompanyDto>(comp);
            return compDto;
        }

        public Task<bool> DeleteCompanyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyModel>> GetCompanyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CompanyModel> GetCompanyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyModel> UpdateCompanyAsync(CompanyDto companyDto, int id, IFormFile? photo)
        {
            throw new NotImplementedException();
        }
    }
}
