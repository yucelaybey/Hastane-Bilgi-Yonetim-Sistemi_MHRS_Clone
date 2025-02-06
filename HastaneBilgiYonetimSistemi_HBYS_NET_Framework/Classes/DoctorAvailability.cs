using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Classes
{
    public class DoctorAvailability
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string DoctorNameSurname { get; set; }
        public string Gender { get; set; }
        public Image GenderImage { get; set; }
        public int PoliclinicId { get; set; }
        public string PoliclinicName { get; set; }
        public DateTime AvailableDate { get; set; }
    }
}
