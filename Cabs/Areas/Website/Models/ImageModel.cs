using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabs.Areas.Website.Models
{
    public class ImageModel : BaseModel
    {
        [StringLength(152, MinimumLength = 0, ErrorMessage = "ImageName không quá 152 ký tự.")]
        public string ImageName { get; set; } = string.Empty;

        [StringLength(152, MinimumLength = 0, ErrorMessage = "ImagePath không quá 152 ký tự.")]
        [Required(ErrorMessage = "ImagePath là bắt buộc.")]
        public string ImagePath { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public int? ProgramId { get; set; }

        [ForeignKey("DriverFkId")]
        public int DriverFkId { get; set; }
        public DriverModel? Driver { get; set; }

        [ForeignKey("CompanyFkId")]
        public int CompanyFkId { get; set; }
        public CompanyModel? Company { get; set; }
        public virtual ICollection<AdvertiseImageModels>? ImageAdvertises { get; set; }
    }
}
