namespace TaxiDemo.Models
{
    public class AdvertiseImageModels
    {
        public int ImageId { get; set; }
        public ImageModel? Image { get; set; }

        public int AdvertiseId { get; set; }
        public AdvertiseModel? Advertise { get; set; }
    }
}
