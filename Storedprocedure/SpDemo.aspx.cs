using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace Storedprocedure
{
    public partial class SpDemo : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=H:\Storedprocedure\Storedprocedure\App_Data\storepdemo.mdf;Integrated Security=True");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //save button
            try
            {
                cmd = new SqlCommand("SaveBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BId", TextBox1.Text);
                cmd.Parameters.AddWithValue("@BName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Price", TextBox3.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                TextBox1.Text = " ";
                TextBox2.Text = " ";
                TextBox3.Text = " ";
                Response.Write("Date Saved");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Search Butoon
            try
            {
                cmd = new SqlCommand("SearchBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BId", TextBox1.Text);
                conn.Open();
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    TextBox1.Text = dr["BId"].ToString();
                    TextBox2.Text = dr["BName"].ToString();
                    TextBox3.Text = dr["Price"].ToString();

                }
                else
                {
                    Response.Write("Data Not Found");
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //update button
            try
            {
                cmd = new SqlCommand("UpdateBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BId", TextBox1.Text);
                cmd.Parameters.AddWithValue("@BName",TextBox2.Text);
                cmd.Parameters.AddWithValue("@Price", TextBox3.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                TextBox1.Text = " ";
                TextBox2.Text = " ";
                TextBox3.Text = " ";
                Response.Write("Data Saved");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //Delte button
            try
            {
                cmd = new SqlCommand("DeleteBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BId", TextBox1.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                TextBox1.Text = " ";
                TextBox2.Text = " ";
                TextBox3.Text = " ";
                Response.Write("Data Deleted");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //Showall
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("ShowAllBooks", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
    }
}