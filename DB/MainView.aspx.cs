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
        const String STUDENT_ID = "出席番号";
        const String STUDENT_NAME = "名前";
        const String STUDENT_POINT = "点数";
        const String STUDENT_RESULT = "合否";
        #endregion

        //変数
        #region
        //追加する情報
        #region
        int id;          //id
        String number;   //出席番号
        String name;　　 //名前
        int point;       //点数
        Boolean result;  //合否
        #endregion

        //テーブル
        Hashtable select_hash = new Hashtable();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //表示ラベル
            #region
            //追加
            Add_Number.Text = STUDENT_ID;
            Add_Name.Text = STUDENT_NAME;
            Add_Point.Text = STUDENT_POINT;

            //更新
            UP_Number.Text = STUDENT_ID;
            UP_Name.Text = STUDENT_NAME;
            UP_Point.Text = STUDENT_POINT;

            //削除
            Del_Number.Text = STUDENT_ID;

            #endregion

            //SQL読み込み
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT REPORT_ID,NUMBER,NAME,POINT , iif(RESULT=1,'合格','不合格')AS RESULT FROM Reportmst ORDER BY NUMBER;", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DB_Grid.DataSource = dr;
            DB_Grid.DataBind();
            con.Close();

            //ヘッダー文字
            DB_Grid.HeaderRow.Cells[2].Text = STUDENT_ID;
            DB_Grid.HeaderRow.Cells[3].Text = STUDENT_NAME;
            DB_Grid.HeaderRow.Cells[4].Text = STUDENT_POINT;
            DB_Grid.HeaderRow.Cells[5].Text = STUDENT_RESULT;

        }

        //追加ボタンクリック
        protected void Add_Button_Click(object sender, EventArgs e)
        {
            try
            {
                //追加する情報
                number = Add_Number_Text.Text;
                name = Add_Name_Text.Text;
                point = int.Parse(Add_Point_Text.Text);

                //成績判定
                if (point >= 60)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                //データベースを開く
                SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
                con.Open();

                //SQL分作成
                SqlCommand cmd = new SqlCommand("INSERT INTO Reportmst(NUMBER,NAME,POINT,RESULT) VALUES(@NUMBER,@NAME,@POINT,@RESULT)", con);
                cmd.Parameters.Add(new SqlParameter("@NUMBER", number));
                cmd.Parameters.Add(new SqlParameter("@NAME", name));
                cmd.Parameters.Add(new SqlParameter("@POINT", point));
                cmd.Parameters.Add(new SqlParameter("@RESULT", result));

                SqlDataReader dr = cmd.ExecuteReader();

                con.Close();

                Response.Redirect(Request.Url.OriginalString);
            }
            catch
            {
                return;
            }
            
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
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
            con.Open();

            //SQL分作成
            SqlCommand cmd = new SqlCommand("DELETE FROM Reportmst WHERE REPORT_ID = @REPORT_ID", con);
            cmd.Parameters.Add(new SqlParameter("@REPORT_ID", id));
        
            SqlDataReader dr = cmd.ExecuteReader();

            con.Close();

            Response.Redirect(Request.Url.OriginalString);
        }

        //選択ボタンクリック
        protected void DB_Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_Number_Text.Text = DB_Grid.SelectedRow.Cells[2].Text;
            Del_Number_Label.Text = DB_Grid.SelectedRow.Cells[2].Text;

            //選択された列情報を追加
            select_hash.Add("ID", int.Parse(DB_Grid.SelectedRow.Cells[1].Text));
            select_hash.Add("NAME", DB_Grid.SelectedRow.Cells[2].Text);
            select_hash.Add("POINT", DB_Grid.SelectedRow.Cells[3].Text);

            //ViewState格納
            ViewState["select_hash"] = select_hash;

            //各変更項目の活性化
            #region
            //更新
            UP_Number_Text.Enabled = true;
            UP_Name_Text.Enabled = true;
            UP_Point_Text.Enabled = true;
            Update_Button.Enabled = true;

            //削除
            Delete_Button.Enabled = true;
            #endregion

        }

        //更新ボタンクリック
        protected void Update_Button_Click(object sender, EventArgs e)
        {
            select_hash = (Hashtable)ViewState["select_hash"];

            try
            {
                id = (int)select_hash["ID"];
                number = UP_Number_Text.Text;
                name = UP_Name_Text.Text;

                //例外処理
                #region
                //出席番号が未入力の場合
                if (number == null || number == "")
                {
                    number = DB_Grid.SelectedRow.Cells[2].Text;
                }

                //名前が未入力の場合
                if (name == null || name == "")
                {
                    name = DB_Grid.SelectedRow.Cells[3].Text;
                }

                //成績のチェック
                if (UP_Point_Text.Text != null && UP_Point_Text.Text != "" && int.Parse(UP_Point_Text.Text) <= 100)
                {
                    point = int.Parse((UP_Point_Text.Text));
                }
                else
                {
                    point = int.Parse(DB_Grid.SelectedRow.Cells[4].Text);
                }
                #endregion

                //成績判定
                #region
                if (point >= 60)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                #endregion

                //SQL読み込み
                SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=ReportDB;Integrated Security=SSPI");
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Reportmst SET NUMBER = @NUMBER, NAME = @NAME, POINT = @POINT, RESULT = @RESULT WHERE REPORT_ID = @REPORT_ID;", con);
                cmd.Parameters.Add(new SqlParameter("@REPORT_ID", id));
                cmd.Parameters.Add(new SqlParameter("@NUMBER", number));
                cmd.Parameters.Add(new SqlParameter("@NAME", name));
                cmd.Parameters.Add(new SqlParameter("@POINT", point));
                cmd.Parameters.Add(new SqlParameter("@RESULT", result));

                SqlDataReader dr = cmd.ExecuteReader();
                DB_Grid.DataSource = dr;
                DB_Grid.DataBind();
                con.Close();

                Response.Redirect(Request.Url.OriginalString);
            }
            catch
            {
                return;
            }
            
        }

        //データソースがバインドされたとき
        protected void DB_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //主キーであるIDを非表示
            e.Row.Cells[1].Visible = false;
        }
    }
}