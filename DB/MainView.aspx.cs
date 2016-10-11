using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DB
{
    public partial class MainView : System.Web.UI.Page
    {
        //表示項目で使う定数
        #region
        const String STUDENT_ID = "出席番号：";
        const String STUDENT_NAME = "名前：";
        const String STUDENT_POINT = "点数：";
        const String STUDENT_RESULT = "合否：";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                return;
            }
            //ラベル表示
            ID_Label.Text = STUDENT_ID;
            Name_Label.Text = STUDENT_NAME;
            Point_Label.Text = STUDENT_POINT;

            //SQL読み込み
            //SQL
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ID,NAME,POINT , iif(RESULT=1,'合格','不合格')AS RESULT FROM ReportTable;", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DB_Grid.DataSource = dr;
            DB_Grid.DataBind();
            con.Close();

        }


    }
}