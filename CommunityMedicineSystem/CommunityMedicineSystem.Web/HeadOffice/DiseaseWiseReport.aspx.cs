using System;
using System.Data;
using System.Web.UI.WebControls;
using CommunityMedicineSystem.BLL;

namespace CommunityMedicineSystem.Web.HeadOffice
{
    public partial class DiseaseWiseReport : System.Web.UI.Page
    {
        private DiseaseManager aDiseaseManager=new DiseaseManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                diseaseDropDownList.DataSource = aDiseaseManager.GetAll();
                diseaseDropDownList.DataTextField = "Name";
                diseaseDropDownList.DataBind();
            }
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            reportGridView.DataSource = aDiseaseManager.GetDiseaseWiseReportsBetweenDates(diseaseDropDownList.Text,
                DateTime.Parse(firstDateTextBox.Value), DateTime.Parse(lastDateTextBox.Value));
            reportGridView.DataBind();
            ViewState["data"] = aDiseaseManager.GetDiseaseWiseReportsBetweenDates(diseaseDropDownList.Text,
                DateTime.Parse(firstDateTextBox.Value), DateTime.Parse(lastDateTextBox.Value));
            
        }

        protected void reportGridView_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            //DataTable aDataTable = ViewState["data"] as DataTable;
            //DataView aDataView=new DataView(aDataTable);
            
            //if (e.SortDirection == SortDirection.Ascending)
            //{
            //    aDataView.Sort = e.SortExpression + " " + "DESC";
            //}
            //else
            //{
            //    aDataView.Sort = e.SortExpression + " " + "ASC";
            //}
            //reportGridView.DataSource = aDataView;
            //reportGridView.DataBind();
        }
    }
}