using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Classes
{
    public class AppointmentDashboard
    {
        public int RandevuId { get; set; }
        public int DoktorId { get; set; }
        public string DoktorAdi { get; set; }
        public string DoktorSoyadi { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string PoliklinikAdi { get; set; }
        public string RandevuSaati { get; set; }
    }

}
