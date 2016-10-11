using System;
using System.Data.SqlClient;
using System.Collections;
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
        const String URL = "http://localhost:64087/MainView.aspx";
        #endregion

        //追加する情報
        #region
        int id;       //ID
        String name;　　 //名前
        int point;       //点数
        Boolean result;  //合否
        #endregion

        //テーブル
        Hashtable select_hash = new Hashtable();

        //SQL
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if(IsPostBack)
            {
                return;
            }
            */
            //ラベル表示
            #region
            //追加
            ID_Label.Text = STUDENT_ID;
            Name_Label.Text = STUDENT_NAME;
            Point_Label.Text = STUDENT_POINT;

            //更新
            UP_ID.Text = STUDENT_ID;
            UP_Name.Text = STUDENT_NAME;
            UP_Point.Text = STUDENT_POINT;

            //削除
            Del_ID.Text = STUDENT_ID;
            #endregion

            //SQL読み込み
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT REPORT_ID,NAME,POINT , iif(RESULT=1,'合格','不合格')AS RESULT FROM Reportmst;", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DB_Grid.DataSource = dr;
            DB_Grid.DataBind();
            con.Close();
        }

        //追加ボタンクリック
        protected void Add_Button_Click(object sender, EventArgs e)
        {
            id = int.Parse(ID_Text.Text);
            name = Name_Text.Text;
            point = int.Parse(Point_Text.Text);

            //成績判定
            if(point >= 60)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //データベースを開く
            con.Open();

            //SQL分作成
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Reportmst(NUMBER,NAME,POINT,RESULT) VALUES(@NUMBER,@NAME,@POINT,@RESULT)", con);
            cmd.Parameters.Add(new SqlParameter("@NUMBER", id));
            cmd.Parameters.Add(new SqlParameter("@NAME", name));
            cmd.Parameters.Add(new SqlParameter("@POINT", point));
            cmd.Parameters.Add(new SqlParameter("@RESULT", result));

            SqlDataReader dr = cmd.ExecuteReader();

            con.Close();

            Response.Redirect(URL);
        }

        //削除ボタンクリック
        protected void Delete_Button_Click(object sender, EventArgs e)
        {
            select_hash = (Hashtable)ViewState["select_hash"];

            if (select_hash != null)
            {
                id = (int)select_hash["ID"];
            }

            //データベースを開く
            con.Open();

            //SQL分作成
            SqlCommand cmd = new SqlCommand("DELETE FROM Reportmst WHERE REPORT_ID = @REPORT_ID", con);
            cmd.Parameters.Add(new SqlParameter("@REPORT_ID", id));
        
            SqlDataReader dr = cmd.ExecuteReader();

            con.Close();

            Response.Redirect(URL);
        }

        //選択ボタンクリック
        protected void DB_Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_ID_Label.Text = DB_Grid.SelectedRow.Cells[1].Text;
            DEL_ID_Label.Text = DB_Grid.SelectedRow.Cells[1].Text;

            //選択された列情報を追加
            select_hash.Add("ID", int.Parse(DB_Grid.SelectedRow.Cells[1].Text));
            select_hash.Add("NAME", DB_Grid.SelectedRow.Cells[2].Text);
            select_hash.Add("POINT", DB_Grid.SelectedRow.Cells[3].Text);

            //ViewState格納
            ViewState["select_hash"] = select_hash;
        }

        //更新ボタンクリック
        protected void Update_Button_Click(object sender, EventArgs e)
        {
            id = int.Parse(UP_ID_Label.Text);
            name = UP_Name_Text.Text;
            point = int.Parse(UP_Point_Text.Text);

            //成績判定
            if (point >= 60)
            {
                result = true;
            }
            else
            {
                result = false;
            }


            //SQL読み込み
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Reportmst SET NAME = @NAME,POINT = @POINT, RESULT = @RESULT WHERE REPORT_ID = @REPORT_ID;", con);
            cmd.Parameters.Add(new SqlParameter("@REPORT_ID", id));
            cmd.Parameters.Add(new SqlParameter("@NAME", name));
            cmd.Parameters.Add(new SqlParameter("@POINT", point));
            cmd.Parameters.Add(new SqlParameter("@RESULT", result));

            SqlDataReader dr = cmd.ExecuteReader();
            DB_Grid.DataSource = dr;
            DB_Grid.DataBind();
            con.Close();

            Response.Redirect(URL);
        }
    }
}