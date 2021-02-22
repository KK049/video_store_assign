using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace video_store_assign
{
    public partial class Form1 : Form
    {
        int ProductID = 0;
        int ClientID = 0;
        int RentID = 0;
        String option = "";
        Product product = new Product();
        Client client = new Client();

        Bookings bookings = new Bookings();

        public Form1()
        {
            InitializeComponent();
        }



        private void btn_video_add__rental_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.ToString().Equals("") || txtRatting.Text.ToString().Equals("") || txtYear.Text.ToString().Equals("") || txtCost.Text.ToString().Equals("") || txtCopies.Text.ToString().Equals("") || txtGenre.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please fill all values ");
            }
            else
            {
                //pass the data to the class to insert 
                Product product = new Product(txtTitle.Text, txtRatting.Text, Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtCost.Text), Convert.ToInt32(txtCopies.Text), txtGenre.Text);
                product.InsertProduct();
                MessageBox.Show("Product is Saved ");

                txtTitle.Text = "";
                txtRatting.Text = "";
                txtYear.Text = "";
                txtCost.Text = "";
                txtCopies.Text = "";
                txtGenre.Text = "";

            }
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //dislay the cost of the price of the video after adding the year of the video
                DateTime dateNow = DateTime.Now;

                int Currentyear = dateNow.Year;

                int diffYear = Currentyear - Convert.ToInt32(txtYear.Text.ToString());
                int cost = 0;
                // MessageBox.Show(diff.ToString());
                if (diffYear >= 5)
                {
                    cost = 2;
                }
                if (diffYear >= 0 && diffYear < 5)
                {
                    cost = 5;

                }
                txtCost.Text = "" + cost;


            }
            catch (Exception ex)
            {

            }

        }

        private void btnVideoDelete_rental_Click(object sender, EventArgs e)
        {
            if (ProductID > 0)
            {
                product.DeleteProduct(ProductID);

                txtTitle.Text = "";
                txtRatting.Text = "";
                txtYear.Text = "";
                txtCost.Text = "";
                txtCopies.Text = "";
                txtGenre.Text = "";
                ProductID = 0;
            }
            else {
                MessageBox.Show("Select the Product to Delete ");
            }
        }

        private void btnVideoUpdate_rental_Click(object sender, EventArgs e)
        {
            if ( ProductID==0 || txtTitle.Text.ToString().Equals("") || txtRatting.Text.ToString().Equals("") || txtYear.Text.ToString().Equals("") || txtCost.Text.ToString().Equals("") || txtCopies.Text.ToString().Equals("") || txtGenre.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please fill all values ");
            }
            else
            {
                //pass the data to the class to insert 
                Product product = new Product(ProductID,txtTitle.Text, txtRatting.Text, Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtCost.Text), Convert.ToInt32(txtCopies.Text), txtGenre.Text);
                product.UpdateProduct();
                MessageBox.Show("Product is Updated ");

                txtTitle.Text = "";
                txtRatting.Text = "";
                txtYear.Text = "";
                txtCost.Text = "";
                txtCopies.Text = "";
                txtGenre.Text = "";

            }

        }

        private void btnCustomerAdd_rental_Click(object sender, EventArgs e)
        {
            if (txtName.Text.ToString().Equals("") || txtAddress.Text.ToString().Equals("") || txtContact.Text.ToString().Equals("") || cus_email.Text.ToString().Equals(""))
            {
                MessageBox.Show("Fill All details to register ");
            }
            else {

                Client client = new Client(txtName.Text,txtAddress.Text,txtContact.Text,cus_email.Text);
                client.InsertClient();
                MessageBox.Show("Client is registered"); 
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                cus_email.Text = "";

            }
        }

        private void btnCustomerDelete__rental_Click(object sender, EventArgs e)
        {
            if (ClientID == 0)
            {
                MessageBox.Show("Select the CLient ID to delete ");
            }
            else {

                client.deleteClient(ClientID);
                ClientID = 0;
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                cus_email.Text = "";
                MessageBox.Show("Client is deleted ");

            }
        }

        private void btnCustomerUpdate__rental_Click(object sender, EventArgs e)
        {
            if (ClientID==0 || txtName.Text.ToString().Equals("") || txtAddress.Text.ToString().Equals("") || txtContact.Text.ToString().Equals("") || cus_email.Text.ToString().Equals(""))
            {
                MessageBox.Show("Fill All details to register ");
            }
            else
            {

                Client client = new Client(ClientID,txtName.Text, txtAddress.Text, txtContact.Text, cus_email.Text);
                client.UpdateClient();
                MessageBox.Show("Client Record is updated ");

                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                cus_email.Text = "";

            }


        }

        private void show_custmer_Click(object sender, EventArgs e)
        {
            //get  the data 
            DataTable tbl = new DataTable();
            tbl = client.tblClient();
            DatabaseTable.DataSource = tbl;
            option = "client";
        }

        private void rental_video_show_Click(object sender, EventArgs e)
        {
            //get  the data 
            DataTable tbl = new DataTable();
            tbl = product.tblProduct();
            DatabaseTable.DataSource = tbl;
            option = "product";
        }

        private void btnVideoReturn_rental_Click(object sender, EventArgs e)
        {
            if (txtClientID.Text.ToString().Equals("") || txtVideoID.Text.ToString().Equals(""))
            {
                MessageBox.Show("Must select the video or client");
            }
            else
            {
                Bookings bookings = new Bookings(RentID,Convert.ToInt32(txtClientID.Text.ToString()), Convert.ToInt32(txtVideoID.Text.ToString()), BookingDate.Text.ToString(),ReturnDate.Text.ToString());
                bookings.returnProduct();
                RentID = 0;
                txtVideoID.Text = "";
                txtClientID.Text = "";

            }

        }

        private void btnVideoIssue_rental_Click(object sender, EventArgs e)
        {
            if (txtClientID.Text.ToString().Equals("") || txtVideoID.Text.ToString().Equals("")) {
                MessageBox.Show("Must select the video or client");
            }
            else {
                Bookings bookings = new Bookings(Convert.ToInt32(txtClientID.Text.ToString()),Convert.ToInt32(txtVideoID.Text.ToString()),BookingDate.Text.ToString());
                bookings.bookProduct();
                txtVideoID.Text = "";
                txtClientID.Text = "";

            }
        }

        private void show_videos_Click(object sender, EventArgs e)
        {
            //get  the data 
            DataTable tbl = new DataTable();
            tbl = bookings.tblBookedProduct();
            DatabaseTable.DataSource = tbl;
            option = "booking";
        }

        private void DatabaseTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (option.Equals("product")) {
                ProductID = Convert.ToInt32(DatabaseTable.CurrentRow.Cells[0].Value.ToString());
                txtVideoID.Text = DatabaseTable.CurrentRow.Cells[0].Value.ToString();
                txtTitle.Text = DatabaseTable.CurrentRow.Cells[1].Value.ToString();
                txtRatting.Text = DatabaseTable.CurrentRow.Cells[2].Value.ToString();
                txtYear.Text = DatabaseTable.CurrentRow.Cells[3].Value.ToString();
                txtCost.Text = DatabaseTable.CurrentRow.Cells[4].Value.ToString();
                txtCopies.Text = DatabaseTable.CurrentRow.Cells[5].Value.ToString();
                txtGenre.Text = DatabaseTable.CurrentRow.Cells[6].Value.ToString();
                option = "";
            }
            if (option.Equals("client")) {
                ClientID = Convert.ToInt32(DatabaseTable.CurrentRow.Cells[0].Value.ToString());
                txtClientID.Text = DatabaseTable.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = DatabaseTable.CurrentRow.Cells[1].Value.ToString();
                txtAddress.Text = DatabaseTable.CurrentRow.Cells[2].Value.ToString();
                txtContact.Text = DatabaseTable.CurrentRow.Cells[3].Value.ToString();
                cus_email.Text = DatabaseTable.CurrentRow.Cells[4].Value.ToString();
                option = "";
            }
            if (option.Equals("booking")) {
                RentID = Convert.ToInt32(DatabaseTable.CurrentRow.Cells[0].Value.ToString());
                txtVideoID.Text = DatabaseTable.CurrentRow.Cells[1].Value.ToString();
                txtClientID.Text = DatabaseTable.CurrentRow.Cells[2].Value.ToString();
                BookingDate.Text = DatabaseTable.CurrentRow.Cells[3].Value.ToString();
                option = "";
            }
        }

        private void popul_custmer_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.BestClient();

        }

        private void popular_mov_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.BestProduct();
        }

        private void btnVideoIssue_rental_Click_1(object sender, EventArgs e)
        {
            if (!txtClientID.Text.Equals("") && !txtVideoID.Text.Equals(""))
            {


                Bookings booking = new Bookings(Convert.ToInt32(txtClientID.Text), Convert.ToInt32(txtVideoID.Text), BookingDate.Text);
                booking.bookProduct();
            }
        }

        private void btnVideoReturn_rental_Click_1(object sender, EventArgs e)
        {
            if (RentID > 0)
            {

                Bookings booking = new Bookings(RentID, Convert.ToInt32(txtClientID.Text), Convert.ToInt32(txtVideoID.Text), BookingDate.Text, ReturnDate.Text);
                booking.returnProduct();
            }
        }

        private void rental_del_Click(object sender, EventArgs e)
        {
            Bookings booking = new Bookings(RentID, Convert.ToInt32(txtClientID.Text), Convert.ToInt32(txtVideoID.Text), BookingDate.Text, ReturnDate.Text);
            booking.deleteBookng(RentID);
            MessageBox.Show("Record IS deleted ");
            txtClientID.Text = "";
            txtVideoID.Text = "";
            RentID = 0;
        }
    }
}
