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
    public class Product
    {
        SqlConn obj = new SqlConn();


        //global variable to pass the data to the database 
        public int ProductID;
        public String Name;
        
        public String Ratting;
        public int Year;
        
        public int Cost;
        public int Copies;
        public String Genre;

        public Product() {
                
        }
        //parameterized construcutor of the class 
        public Product(String name,String ratting,int year,int cost,int copies,string genre) {
            this.Name = name;
            
            this.Ratting = ratting;
            this.Year = year;
            this.Cost = cost;
            this.Copies = copies;
            this.Genre = genre;
        }
        
        //parameterized construcutor of the class 
        public Product(int id,String name, String ratting, int year, int cost, int copies, string genre)
        {
            this.ProductID = id;
            this.Name = name;
            this.Ratting = ratting;
            this.Year = year;
            this.Cost = cost;
            this.Copies = copies;
            this.Genre = genre;
        }



        public void InsertProduct() {
            //insert the record 
            String query = "insert into Product(Name,Ratting,Year,Cost,Copies,Genre)values(@Name,@Ratting,@Year,@Cost,@Copies,@Genre)";
            obj.conn = new SqlConnection(obj.conStr);
            obj.conn.Open();
            obj.cmd = new SqlCommand(query, obj.conn);
            obj.cmd.Parameters.AddWithValue("@Name",Name);
            obj.cmd.Parameters.AddWithValue("@Ratting",Ratting);
            obj.cmd.Parameters.AddWithValue("@Year", Year);
            obj.cmd.Parameters.AddWithValue("@Cost", Cost);
            obj.cmd.Parameters.AddWithValue("@Copies", Copies);
            obj.cmd.Parameters.AddWithValue("@Genre", Genre);
            obj.cmd.ExecuteNonQuery();
            obj.conn.Close();

        }

        public void DeleteProduct(int ProductID) {
            //delete the product
            String query = "delete from Product where ID=" + ProductID + "";
            obj.CmdQuery(query);
        }

        public void UpdateProduct() {

            String query = "update Product set Name=@Name,Ratting=@Ratting,Year=@Year,Cost=@Cost,Copies=@Copies,Genre=@Genre where id=@ProductID";
            obj.conn = new SqlConnection(obj.conStr);
            obj.conn.Open();
            obj.cmd = new SqlCommand(query, obj.conn);
            obj.cmd.Parameters.AddWithValue("@ProductID", ProductID);
            obj.cmd.Parameters.AddWithValue("@Name", Name);
            obj.cmd.Parameters.AddWithValue("@Ratting", Ratting);
            obj.cmd.Parameters.AddWithValue("@Year", Year);
            obj.cmd.Parameters.AddWithValue("@Cost", Cost);
            obj.cmd.Parameters.AddWithValue("@Copies", Copies);
            obj.cmd.Parameters.AddWithValue("@Genre", Genre);
            obj.cmd.ExecuteNonQuery();
            obj.conn.Close();

        }

        public DataTable tblProduct()
        {
            //get the data from the datanase 
            String query = "select * from Product";
            DataTable tbl = new DataTable();
            tbl = obj.CmdRecord(query);
            return tbl;
        }


        public void BestProduct()
        {

            DataTable tblData = new DataTable();
            tblData = obj.CmdRecord("select * from Product");
            int x = 0, y = 0, cunt = 0;
            String Title = "";
            for (x = 0; x < tblData.Rows.Count; x++)
            {
                DataTable tblData1 = new DataTable();
                tblData1 = obj.CmdRecord("select * from bookings where ProductID=" + Convert.ToInt32(tblData.Rows[x]["ID"].ToString()) + "");

                if (tblData1.Rows.Count > cunt)
                {
                    Title = tblData.Rows[x]["Name"].ToString();
                    cunt = tblData1.Rows.Count;
                }

            }
            MessageBox.Show("Best Movie of the Store is= :" + Title);

        }


    }

}
