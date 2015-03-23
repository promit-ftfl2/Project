using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityMedicineSystem.DAO;
using CommunityMedicineSystem.DAO.ViewModel;

namespace CommunityMedicineSystem.DAL
{
    public class DiseaseGateway:Gateway
    {
        public void Save(Disease aDisease)
        {
            SqlQuery = "INSERT INTO tbl_diseases VALUES('" + aDisease.Name + "','" + aDisease.Description + "','" +
                           aDisease.TreatementProcedure + "','" + aDisease.PreferedMedicine + "')";
            DbSqlConnection = new SqlConnection(ConnectionString);
            DbSqlConnection.Open();
            DbSqlCommand = new SqlCommand(SqlQuery, DbSqlConnection);
            DbSqlCommand.ExecuteNonQuery();
            DbSqlConnection.Close();
        }

        public List<Disease> GetAll()
        {
            List<Disease> diseases=new List<Disease>();
            SqlQuery = "SELECT * FROM tbl_diseases;";
            DbSqlConnection=new SqlConnection(ConnectionString);
            DbSqlConnection.Open();
            DbSqlCommand=new SqlCommand(SqlQuery,DbSqlConnection);
            DbSqlDataReader = DbSqlCommand.ExecuteReader();
            while (DbSqlDataReader.Read())
            {
                Disease aDisease = new Disease()
                {
                    Id = (int)DbSqlDataReader["id"],
                    Description = DbSqlDataReader["description"].ToString(),
                    Name = DbSqlDataReader["name"].ToString(),
                    PreferedMedicine = DbSqlDataReader["prefered_medicine"].ToString(),
                    TreatementProcedure = DbSqlDataReader["treatement_procedure"].ToString()
                };
                diseases.Add(aDisease);
            }
            DbSqlConnection.Close();
            return diseases;
        }
        public Disease Find(string name)
        {
            SqlQuery = "SELECT * FROM tbl_diseases WHERE name='" + name + "'";
            DbSqlConnection = new SqlConnection(ConnectionString);
            DbSqlConnection.Open();
            DbSqlCommand = new SqlCommand(SqlQuery, DbSqlConnection);
            DbSqlDataReader = DbSqlCommand.ExecuteReader();
            if (DbSqlDataReader.HasRows)
            {
                Disease aDisease = new Disease();
                while (DbSqlDataReader.Read())
                {
                    aDisease.Id = Convert.ToInt32(DbSqlDataReader["id"]);
                    aDisease.Name = DbSqlDataReader["name"].ToString();
                    aDisease.Description = DbSqlDataReader["description"].ToString();
                    aDisease.TreatementProcedure = DbSqlDataReader["treatement_procedure"].ToString();
                    aDisease.PreferedMedicine = DbSqlDataReader["prefered_medicine"].ToString();
                }
                DbSqlConnection.Close();
                return aDisease;
            }
            DbSqlConnection.Close();
            return null;
        }

        public List<DiseaseWiseReport> GetReportBetweenDates(string diseaseName,DateTime startDate,DateTime endDate)
        {
            SqlQuery =
                "SELECT t1.district_name,COUNT(t1.district_name) AS total_patient,t1.population" +
                " FROM (" +
                "SELECT v1.district_name,v1.voter_id,v1.population " +
                "FROM view_district_wise_patients v1 " +
                "WHERE v1.disease_name='"+diseaseName+"' and v1.date BETWEEN '"+startDate+"' AND '"+endDate+"' " +
                "GROUP BY v1.district_name,v1.voter_id,v1.population) t1 " +
                "GROUP BY t1.district_name,t1.population";
            DbSqlConnection=new SqlConnection(ConnectionString);
            DbSqlConnection.Open();
            DbSqlCommand = new SqlCommand(SqlQuery, DbSqlConnection);
            DbSqlDataReader = DbSqlCommand.ExecuteReader();
            List<DiseaseWiseReport> reports=new List<DiseaseWiseReport>();
            if (DbSqlDataReader.HasRows)
            {
                while (DbSqlDataReader.Read())
                {
                    DiseaseWiseReport aReport=new DiseaseWiseReport();
                    aReport.DistrictName = DbSqlDataReader["district_name"].ToString();
                    aReport.TotalPatient = Convert.ToInt32(DbSqlDataReader["total_patient"].ToString());
                    aReport.PercentageOfPopulation =(aReport.TotalPatient*100/Convert.ToInt32(DbSqlDataReader["population"]));
                    reports.Add(aReport);
                }
            }
            DbSqlConnection.Close();
            return reports;
        }
    }
}
