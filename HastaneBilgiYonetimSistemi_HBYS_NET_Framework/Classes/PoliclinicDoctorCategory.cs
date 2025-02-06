using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Classes
{
    public class PoliclinicDoctorCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PoliclinicDoctorCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
