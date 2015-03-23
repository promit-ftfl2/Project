using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommunityMedicineSystem.BLL;
using CommunityMedicineSystem.DAO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
namespace CommunityMedicineSystem.Web
{
    public partial class HistoryOfPatient : System.Web.UI.Page
    {
        private PatientManager aPatientManager=new PatientManager();
        private PatientInfoManager aPatientInfoManager=new PatientInfoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] == "center")
                {
                    showButton.Visible = false;
                    string voterId = Request.QueryString["voterId"];
                    FillInfoOfVoter(voterId);
                }
                else
                {
                    showButton.Visible = true;
                }

            }
        }

        private void FillInfoOfVoter(string voterId)
        {
            Voter aVoter = aPatientInfoManager.GetPatientInfo(voterId);
            if (aVoter != null)
            {
                voterIdTextBox.Text = aVoter.id;
                addressTextBox.Text = aVoter.address;
                nameTextBox.Text = aVoter.name;
                //DataTable aDataTable=new DataTable();
                patientHistoryGridView.DataSource = aPatientManager.GetPatientHistoryList(voterId).AsEnumerable().All(p => p.Quantity == 10);
                //    aPatientManager.GetPatientHistoryList(voterId);
                patientHistoryGridView.DataBind();
            }
                
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (voterIdTextBox.Text != string.Empty)
            {
                FillInfoOfVoter(voterIdTextBox.Text);
            }
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    patientHistoryGridView.AllowPaging = false;
                    this.FillInfoOfVoter(voterIdTextBox.Text);

                    patientHistoryGridView.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
    }
}