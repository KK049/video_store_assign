using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace video_store_assign
{
    public class SqlConn
    {

        //create the instance of the SQL Connection class
        public SqlConnection conn;

        //write the connection sstring to assthe data form one for to the database 
       public String conStr = "Data Source=DESKTOP-G2UGPMF\\SQLEXPRESS;Initial Catalog=Video_Store;Integrated Security=True";

        //command are use to excute the command of isnert , delete , update
        public SqlCommand cmd;

        //data reader is used to read thedata from the database table 
        SqlDataReader DReader;


        //method used to execute query which doent return any thing only modifit the database
        public void CmdQuery(String query)
        {
            conn = new SqlConnection(conStr);
            conn.Open();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // this method is used to search the record from the data base and then pass the whole record to the query using where clause of the sql
        public DataTable CmdRecord(String qry)
        {
            DataTable tbl = new DataTable();

            conn = new SqlConnection(conStr);

            conn.Open();

            cmd = new SqlCommand(qry, conn);

            DReader = cmd.ExecuteReader();

            tbl.Load(DReader);

            conn.Close();

            return tbl;
        }


    }
}
