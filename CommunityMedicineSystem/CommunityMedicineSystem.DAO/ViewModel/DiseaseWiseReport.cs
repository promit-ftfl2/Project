using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityMedicineSystem.DAO.ViewModel
{
    public class DiseaseWiseReport
    {
        public string DistrictName { set; get; }
        public int TotalPatient { set; get; }
        public double PercentageOfPopulation { set; get; }
    }
}
