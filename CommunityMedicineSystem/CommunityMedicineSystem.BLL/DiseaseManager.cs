﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityMedicineSystem.DAL;
using CommunityMedicineSystem.DAO;
using CommunityMedicineSystem.DAO.ViewModel;

namespace CommunityMedicineSystem.BLL
{
    public class DiseaseManager
    {
       private DiseaseGateway aDiseaseGateway=new DiseaseGateway();
       public string Save(Disease aDisease)
       {
           Disease disease = aDiseaseGateway.Find(aDisease.Name);
           if (disease == null)
           {
               aDiseaseGateway.Save(aDisease);
               return "Disease saved.";
           }
           return "Duplicate Name.";
       }

        public List<Disease> GetAll()
        {
            return aDiseaseGateway.GetAll();
        }

        public Disease Find(string name)
        {
            return aDiseaseGateway.Find(name);
        }

        public List<DiseaseWiseReport> GetDiseaseWiseReportsBetweenDates(string diseaseName, DateTime startDate,
            DateTime endDate)
        {
            return aDiseaseGateway.GetReportBetweenDates(diseaseName, startDate, endDate);
        }
    }
}
