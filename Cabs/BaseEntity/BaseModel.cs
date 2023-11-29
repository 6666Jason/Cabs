using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cabs.BaseEntity
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            CreateDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Create At")]
        [DataType(DataType.Date, ErrorMessage = "CreateDate phải là kiểu dd-MM-yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date, ErrorMessage = "UpdateDate phải là kiểu dd-MM-yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Is Deleted")]

        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
