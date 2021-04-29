using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ado
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                show(); //excuteReader
                showTable(); //dataTable
                showText(); //show一筆資料
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //按鈕事件 -做修改
            //按兩下button會跳出來
            // 1.連接資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DeliveryDocketConnectionString"].ConnectionString);
            // 2.SQL語法
            string sql = "UPDATE customer SET name = @customer WHERE(id = 1)"; //參數化
            //string sql = "UPDATE customer SET name = N'up' WHERE(id = 1)"; //未參數化
            // 3.創新的Command物件
            SqlCommand command = new SqlCommand(sql, connection);
            // 4.參數化 加上@ SQL注入
            command.Parameters.AddWithValue("@customer", TextBox1.Text); // (參數化的東西, 控制器)
            // 5.資料庫開啟
            connection.Open();

            // 6.執行SQL (新增刪除修改)
            command.ExecuteNonQuery(); //不含查詢 查詢ExecuteQuery();
            // 7.資料庫關閉
            connection.Close();
            //畫面即時渲染 render page
            showTable(); //再次查詢再次導入資料 就達成渲染
            // 也可以redirect 但比較耗能(因為要全部載入一次)
            // Response.Redirect("WebFrom1.aspx");
        }

        private void showText()
        {
            // 1.連接資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DeliveryDocketConnectionString"].ConnectionString);
            // 2.SQL語法
            string sql = "SELECT customer.* FROM customer WHERE (id = 1)";
            // 3.創新的Command物件
            SqlCommand command = new SqlCommand(sql, connection);
            // 4.資料庫開啟
            connection.Open(); //同在ssms按下連線的概念
            // 5.執行sql
            SqlDataReader reader = command.ExecuteReader();
            //reader.Read(); 讀全部
            //bof beginning of file 第一筆的上面
            //eof end of file 最後一筆的下面 無資料
            // 6.讀出一筆資料
            //如果要從第一行讀到最後一行用while迴圈

            if (reader.Read())
            {
                p1.InnerHtml = reader["name"].ToString();
                Label2.Text = reader["address"].ToString();
                HyperLink1.Text = reader["phone"].ToString();
                TextBox2.Text = reader["last5Digits"].ToString();
            }
        }

        private void showTable()
        {
            // 1.連接資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DeliveryDocketConnectionString"].ConnectionString);
            // 2.SQL語法
            string sql = "SELECT id, name, phone, last5Digits, address FROM customer";
            // 3.創新的Command物件
            SqlCommand command = new SqlCommand(sql, connection);
            // 4.創建DataAdaptor  //adaptor會取代open cloase
            SqlDataAdapter adaptor = new SqlDataAdapter(command);
            // 5.創建一個空表
            DataTable table = new DataTable();
            // 6.把數據填到表裡
            adaptor.Fill(table);
            GridView2.DataSource = table; //控制器資料來源
            GridView2.DataBind(); //控制器綁定
        }

        // function內步驟總攬
        // 1.連接資料庫
        // 2.SQL語法
        // 3.創新的Command物件
        // 4.資料庫開啟
        // 5.執行SQL
        // 6.資料庫關閉
        // Ado.net主要類別物件: connection, command, datareader(??效率比datatable高), dataset(下面是多個data table--不需要開關) //dataset類似資料庫 比較不常用, DataAdaptor(搭配dataReader使用 開關)

        private void show()
        {
            // 1.連接資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DeliveryDocketConnectionString"].ConnectionString);
            // 2.SQL語法
            string sql = "SELECT id, name, phone, last5Digits, address FROM customer";
            // 3.創新的Command物件
            SqlCommand command = new SqlCommand(sql, connection);
            // 4.資料庫開啟
            connection.Open(); //同在ssms按下連線的概念
            // 5.執行SQL
            SqlDataReader reader = command.ExecuteReader(); //同在ssms按下execute的概念 這邊會拿到東西(可以下斷點看)

            GridView1.DataSource = reader; //控制器資料來源
            GridView1.DataBind(); //控制器綁定
            // 6.關閉資料庫連線
            connection.Close();
        }
    }
}