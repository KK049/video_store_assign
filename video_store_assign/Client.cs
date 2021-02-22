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
    public class Client
    {

        SqlConn obj = new SqlConn();

        public int clientID;
        public String Name;
        public String Address;
        public String Contact;
        public String Email;

        //default constructor
        public Client() {

        }
        //parameterized constrcture 
        public Client(String name,String address,String contact,String email) {
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Contact = contact;
        }

        //parameterized constrcture 
        public Client(int ID,String name, String address, String contact, String email)
        {
            this.clientID = ID;
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Contact = contact;
        }

        public void InsertClient() {

            //insert the record 
            String query = "insert into Client(Name,Address,Contact,Email)values(@Name,@Address,@Contact,@Email)";
            obj.conn = new SqlConnection(obj.conStr);
            obj.conn.Open();
            obj.cmd = new SqlCommand(query, obj.conn);
            obj.cmd.Parameters.AddWithValue("@Name", Name);
            obj.cmd.Parameters.AddWithValue("@Address",Address);
            obj.cmd.Parameters.AddWithValue("@Contact", Contact);
            obj.cmd.Parameters.AddWithValue("@Email", Email);
            obj.cmd.ExecuteNonQuery();
            obj.conn.Close();
        }

        //delete the data 
        public void deleteClient(int clientID) {
            String Query = "delete from Client where ID="+clientID+"";
            obj.CmdQuery(Query);
        }

        public void UpdateClient()
        {

            //insert the record 
            String query = "update Client set Name=@Name,Address=@Address,Contact=@Contact,Email=@Email where ID=@ClientID";
            obj.conn = new SqlConnection(obj.conStr);
            obj.conn.Open();
            obj.cmd = new SqlCommand(query, obj.conn);
            obj.cmd.Parameters.AddWithValue("@ClientID", clientID);
            obj.cmd.Parameters.AddWithValue("@Name", Name);
            obj.cmd.Parameters.AddWithValue("@Address", Address);
            obj.cmd.Parameters.AddWithValue("@Contact", Contact);
            obj.cmd.Parameters.AddWithValue("@Email", Email);
            obj.cmd.ExecuteNonQuery();
            obj.conn.Close();
        }

        public DataTable tblClient() {
            //get the data from the datanase 
            String query = "select * from Client";
            DataTable tbl = new DataTable();
            tbl = obj.CmdRecord(query);
            return tbl;
        }

        public void BestClient() {

            DataTable tblData = new DataTable();
            tblData = obj.CmdRecord("select * from Client");
            int x = 0, y = 0, cunt = 0;
            String Title = "";
            for (x = 0; x < tblData.Rows.Count; x++)
            {
                DataTable tblData1 = new DataTable();
                tblData1 = obj.CmdRecord("select * from bookings where ClientID=" + Convert.ToInt32(tblData.Rows[x]["ID"].ToString()) + "");

                if (tblData1.Rows.Count > cunt)
                {
                    Title = tblData.Rows[x]["Name"].ToString();
                    cunt = tblData1.Rows.Count;
                }

            }
            MessageBox.Show("Best Client of the Store is= :" + Title);

        }

    }
}
