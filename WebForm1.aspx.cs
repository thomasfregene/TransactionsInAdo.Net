using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransactionsInAdo.Net
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        //method to retrieve data from DB
        private void GetData()
        {
            string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * From Accounts", con);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr["AccountNumber"].ToString() == "A1")
                    {
                        lblAccountNumber1.Text = "A1";
                        lblBalance1.Text = rdr["Balance"].ToString();
                        lblName1.Text = rdr["CustomerName"].ToString();
                    }
                    else
                    {
                        lblAccountNumber2.Text = "A2";
                        lblBalance2.Text = rdr["Balance"].ToString();
                        lblName2.Text = rdr["CustomerName"].ToString();
                    }
                }
            }
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update Accounts Set Balance = Balance - 10 Where AccountNumber = 'A1'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update Accounts1 Set Balance = Balance + 10 Where AccountNumber = 'A2'", con);
                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Transaction Successful";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception)
                {
                    lblMessage.Text = "Transaction Failed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                GetData();
            }
        }
    }
}