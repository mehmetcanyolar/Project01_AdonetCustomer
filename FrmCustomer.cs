using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01_AdonetCustomer
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; initial catalog=DbCustomer; integrated security=true");
        private void btnList_Click(object sender, EventArgs e)
        {

          

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("Select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName From TblCustomer\r\nInner Join TblCity On TblCity.CityId= TblCustomer.CustomerCity", sqlConnection);



            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;


            sqlConnection.Close();

        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("Execute CustomerListWithCity", sqlConnection);



            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;


            sqlConnection.Close();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("Select * From TblCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            cmbCity.ValueMember = "CityId";
            cmbCity.DisplayMember = "CityName";
            cmbCity.DataSource=dataTable;

            sqlConnection.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("Insert Into TblCustomer(CustomerName,CustomerSurname,CustomerCity, CustomerBalance,CustomerStatus)  values (@customerName,@customerSurname,@customerCity,@customerBalance,@customerStatus)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@customerName",txtCustomerName.Text);
            sqlCommand.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            sqlCommand.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
           
            

            if (rdbActive.Checked) { sqlCommand.Parameters.AddWithValue("@customerStatus",true); }
            if (rdbPassive.Checked) { sqlCommand.Parameters.AddWithValue("@customerStatus", false); }
            
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Eklendi");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("Delete From TblCustomer Where CustomerId=@customerId", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@customerId",txtCustomerId.Text);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri başarıyla silindi", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(" Update TblCustomer Set CustomerName=@customerName,CustomerSurname=@customerSurname,CustomerCity=@customerCity, CustomerBalance=@customerBalance,CustomerStatus=@customerStatus where CustomerId=@customerId", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            sqlCommand.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            sqlCommand.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            sqlCommand.Parameters.AddWithValue("@customerId", txtCustomerId.Text);



            if (rdbActive.Checked) { sqlCommand.Parameters.AddWithValue("@customerStatus", true); }
            if (rdbPassive.Checked) { sqlCommand.Parameters.AddWithValue("@customerStatus", false); }

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Güncellendi");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("Select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName From TblCustomer\r\nInner Join TblCity On TblCity.CityId= TblCustomer.CustomerCity where CustomerName=@customerName", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@customerName",txtCustomerName.Text);
            


            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;


            sqlConnection.Close();
        }
    }
}
