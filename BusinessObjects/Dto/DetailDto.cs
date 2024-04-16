
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Dto
{
    public class DetailDto
    {
        [Key]
        public int Id { set; get; }
        public string Description{ set; get; }
        public int ModelYear { set; get; }
        public int MaxPower { set; get; }
        public int Torque { set; get; }
        public int Length { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        public int CurbWeight { set; get; }
        public double BatteryEnergy { set; get; }
        public int CLTC_CruisingRange { set; get; }
        public int MaximumSspeed { set; get; }
        public int Cameras { set; get; }
        public double Acceleration { set; get; }
        public string Panoramic { set; get; }
        public int CarId { set; get; }


    }
}
