using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace video_store_assign
{
 public class Bookings
    {
        SqlConn obj = new SqlConn();

        public int RentID;
        public int ClientID;
        public int ProductID;
        public String BookingDate;
        public String ReturnDate;

        //default Constrcutor
        public Bookings() {

        }
        
        //parameterized constric
        public Bookings(int clientid,int productid,String bookingdate) {
            this.ClientID = clientid;
            this.ProductID = productid;
            this.BookingDate = bookingdate;
        }

        //parameterized constric
        public Bookings(int rentid,int clientid, int productid, String bookingdate,string returndate)
        {
            this.RentID = rentid;
            this.ClientID = clientid;
            this.ProductID = productid;
            this.BookingDate = bookingdate;
            this.ReturnDate = returndate;
        }

        public void bookProduct() {

            //get the details of tje booking 
            DataTable tbl = new DataTable();
            tbl = obj.CmdRecord("select * from Bookings where ClientID=" + ClientID + " and ReturnDate='Booked'");
            if (tbl.Rows.Count < 2)
            {
                //check the boooking sample 
                DataTable tblBooking = new DataTable();
                tblBooking = obj.CmdRecord("select * from Bookings where ProductID = " + ProductID + " and ReturnDate = 'Booked'");
                //get the copies
                DataTable tblCopies = new DataTable();
                tblCopies = obj.CmdRecord("select * from Product where ID=" + ProductID + "");
                int copies = Convert.ToInt32(tblCopies.Rows[0]["Copies"].ToString());

                    if (tblBooking.Rows.Count < copies)
                    {
            
                        //insert the record 
                        String query = "insert into Bookings(ClientID,ProductID,BookingDate,ReturnDate)values(@ClientID,@ProductID,@BookingDate,@ReturnDate)";
                obj.conn = new SqlConnection(obj.conStr);
                obj.conn.Open();
                obj.cmd = new SqlCommand(query, obj.conn);
                obj.cmd.Parameters.AddWithValue("@ClientID", ClientID);
                obj.cmd.Parameters.AddWithValue("@ProductID", ProductID);
                obj.cmd.Parameters.AddWithValue("@BookingDate", BookingDate);
                obj.cmd.Parameters.AddWithValue("@ReturnDate","Booked");
                obj.cmd.ExecuteNonQuery();
                    MessageBox.Show("Product is Booked ");
                obj.conn.Close();
                }
                else
                {
                    MessageBox.Show("No more sample available");
                }

            }
            else
            {
                MessageBox.Show("You already booked Video first return ");
            }
        }
        
        public void returnProduct() {

            //GET THE COST OF THE PRODUCT
            DataTable tblCopies = new DataTable();
            tblCopies = obj.CmdRecord("select * from Product where ID=" + ProductID + "");
            int cost = Convert.ToInt32(tblCopies.Rows[0]["Cost"].ToString());


            DateTime new_date = DateTime.Now;

            //convert the old date from string to Date fromat
            DateTime prev_date = Convert.ToDateTime(BookingDate);

           //// tblBookedProduct
            //get the difference in the days fromat
            String Daysdiff = (new_date - prev_date).TotalDays.ToString();


            // calculate the round off value 
            Double DaysInterval = Math.Round(Convert.ToDouble(Daysdiff));




            int Charges = Convert.ToInt32(DaysInterval) * cost;



            //insert the record 
            String query = "update Bookings set ClientID=@ClientID,ProductID=@ProductID,BookingDate=@BookingDate,ReturnDate=@ReturnDate where ID=@RentID";
                    obj.conn = new SqlConnection(obj.conStr);
                    obj.conn.Open();
                    obj.cmd = new SqlCommand(query, obj.conn);
                    obj.cmd.Parameters.AddWithValue("@RentID", RentID);
                    obj.cmd.Parameters.AddWithValue("@ClientID", ClientID);
                    obj.cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    obj.cmd.Parameters.AddWithValue("@BookingDate", BookingDate);
                    obj.cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate);
                    obj.cmd.ExecuteNonQuery();
                    obj.conn.Close();
            MessageBox.Show("Product is returned and the Charges is ==$" + Charges);
        }



        public DataTable tblBookedProduct()
        {
            //get the data from the datanase 
            String query = "select * from Bookings where ReturnDate='Booked'";
            DataTable tbl = new DataTable();
            tbl = obj.CmdRecord(query);
            return tbl;
        }
        public void deleteBookng(int rentID)
        {
            String Query = "delete from Bookings where ID=" + rentID + "";
            obj.CmdQuery(Query);
        }


    }
}
