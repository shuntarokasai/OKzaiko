using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;

namespace WebApplication3
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {

        int? flag;
        object p;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            

            //ログイン時に仕様したクッキーを保持していなかった場合，ログインページに飛ばす
            if (Request.Cookies["UserName"] == null)
            {

                Response.Redirect("~/login.aspx");
            }
            else //下記コードだと商品コードをクリックした時に商品コードリンクがハッセしない よう修正
            if (Request.QueryString.Get("S") == null && Request.Cookies["UserName"].Value != "fujii") /*Request.Cookies["UserName"].Value != "fujii" )*/
            {
                if(flag == null)
                {
                    Response.Redirect(string.Format("/WebForm1.aspx?S={0}", Request.Cookies["UserName"].Value));
                }
                else
                {
                    Response.Redirect(string.Format("~/WebForm1.aspx?P={0}", p));
                }
                   
                //Response.Redirect(string.Format("/WebForm1.aspx?S={0}", Request.Cookies["UserName"].Value));
            }



            //ページロード時にurlパラメータの先頭文字列に応じてボタン及びテキストボックスの表示非表示を行う
            var item = Request.QueryString.ToString();
            

                if (item.StartsWith("P"))
                {
                    Literal1.Visible = false;
                    Psearch.Visible = false;
                    resetbutton.Visible = true;
                }
                else
                if(item.StartsWith("S"))
                {
                    Literal2.Visible = false;
                    Ssearch.Visible = false;
                    resetbutton.Visible = true;
                }
                else
                {
                    resetbutton.Visible = false;
                }


            //テキストボックスから入力した値が存在していたらTOPへ戻るボタンを表示する
                if (Ssearch.SelectedIndex!=0 || Psearch.Text.Length != 0 || Isearch.Text.Length != 0)
            {
                resetbutton.Visible = true;
            }


            //ドロップダウンリストに格納する動作・初回はリストの個数が0のため，linqにてリストに代入する動作を実装・次からは，indexに格納する
            if(Ssearch.Items.Count == 0)
            {
                var dt = new Model1();
                var lists = from x in dt.SHOPs
                            where x.店舗区分 == 0 || x.店舗コード == 10 || x.店舗コード == 14
                            select new
                            {
                                x.店舗名
                            };

                this.Ssearch.DataSource = lists.ToList();
                this.Ssearch.DataValueField = "店舗名";
                this.Ssearch.DataBind();
                this.Ssearch.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                int idx = 0;
                foreach (ListItem list in Ssearch.Items)
                {
                    if (list.Value == Ssearch.SelectedItem.Value)
                    {
                        Ssearch.SelectedIndex = idx;
                        break;
                    }
                    idx++;
                }
            }


            //コンテキストクラスにて定義した各クラスをテキスト形式にて格納し，結合する部分
            //string content = "";

            //using (var context = new Model1())
            //{
            //    var stocks = context.zaikoes;

            //    foreach (var stock in stocks)
            //    {
            //        content += string.Format("{0},{1},{2},{3},{4},{5},{6}<br/>",stock.JAN,stock.商品コード,stock.good.商品名,stock.good.標準上代,stock.在庫数,stock.店舗コード, stock.SHOP.店舗名);
            //    }

            //    Literal1.Text = content;
            //}




        }





        // 戻り値の型は IEnumerable に変更できますが、// のページングと
        //並べ替えをサポートするには、次のパラメーターを追加する必要があります:
        //int maximumRows;
        //int startRowIndex;
        //int totalRowCount;
        //string sortByExpression;

        //IQueryable形式の戻り値については，linq形式を用いる，またzaiko.csにて定義したクラスに各プロパティを格納するcontrolはテキストボックス等・querystringはURLの値を取得する
        public static IQueryable<WebApplication3.tablejoin> GridView1_GetData([Control("Psearch")] string productcode, [Control("Ssearch")] string shopname, [Control("Isearch")] string importcode,[QueryString("P")] string productclick, [QueryString("S")] int? shopclick)
        {
            
            if (string.IsNullOrEmpty(productclick) && shopclick == null)
            {
                var context = new Model1();

                var querys = from x in context.zaikoes
                             join y in context.SHOPs on x.店舗コード equals y.店舗コード
                             join z in context.goods on x.商品コード equals z.商品コード
                             //where z.仕入先コード != "105"
                             orderby x.商品コード
                             select new tablejoin()
                             {
                                 id = x.id,
                                 shop = y,
                                 good = z,
                                 JAN = x.JAN,
                                 商品コード = x.商品コード,
                                 商品名 = z.商品名,
                                 標準上代 = z.標準上代,
                                 在庫数 = x.在庫数,
                                 店舗コード = x.店舗コード,
                                 店舗名 = y.店舗名,
                                 仕入先コード = z.仕入先コード

                             };

                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys;
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode));
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode));
                }
                else
                if (string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(productcode))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.商品コード.Contains(productcode));
                }
                else
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname) && x.仕入先コード.Contains(importcode));
                }

            }


            else



            if (string.IsNullOrEmpty(productclick))
            {
                var context = new Model1();

                var querys = from x in context.zaikoes
                             join y in context.SHOPs on x.店舗コード equals y.店舗コード
                             join z in context.goods on x.商品コード equals z.商品コード
                             where x.店舗コード == shopclick
                             orderby x.商品コード
                             select new tablejoin()
                             {
                                 shop = y,
                                 good = z,
                                 JAN = x.JAN,
                                 商品コード = x.商品コード,
                                 商品名 = z.商品名,
                                 標準上代 = z.標準上代,
                                 在庫数 = x.在庫数,
                                 店舗コード = x.店舗コード,
                                 店舗名 = y.店舗名,
                                 仕入先コード = z.仕入先コード

                             };


                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys;
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode));
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode));
                }
                else
                if (string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(productcode))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.商品コード.Contains(productcode));
                }
                else
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname) && x.仕入先コード.Contains(importcode));
                }
            }


            else



            if (shopclick == null)
            {
                var context = new Model1();

                var querys = from x in context.zaikoes
                             join y in context.SHOPs on x.店舗コード equals y.店舗コード
                             join z in context.goods on x.商品コード equals z.商品コード
                             where x.商品コード == productclick
                             orderby x.商品コード
                             select new tablejoin()
                             {
                                 shop = y,
                                 good = z,
                                 JAN = x.JAN,
                                 商品コード = x.商品コード,
                                 商品名 = z.商品名,
                                 標準上代 = z.標準上代,
                                 在庫数 = x.在庫数,
                                 店舗コード = x.店舗コード,
                                 店舗名 = y.店舗名,
                                 仕入先コード = z.仕入先コード

                             };

                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys;
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode));
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode));
                }
                else
                if (string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(productcode))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.商品コード.Contains(productcode));
                }
                else
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname) && x.仕入先コード.Contains(importcode));
                }
            }


            else

            {
                var context = new Model1();

                var querys = from x in context.zaikoes
                             join y in context.SHOPs on x.店舗コード equals y.店舗コード
                             join z in context.goods on x.商品コード equals z.商品コード
                             where x.商品コード == productclick && x.店舗コード == shopclick
                             orderby x.商品コード
                             select new tablejoin()
                             {
                                 shop = y,
                                 good = z,
                                 JAN = x.JAN,
                                 商品コード = x.商品コード,
                                 商品名 = z.商品名,
                                 標準上代 = z.標準上代,
                                 在庫数 = x.在庫数,
                                 店舗コード = x.店舗コード,
                                 店舗名 = y.店舗名,
                                 仕入先コード = z.仕入先コード

                             };
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys;
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname) && string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode));
                }
                else
                if (string.IsNullOrEmpty(productcode) && string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode));
                }
                else
                if (string.IsNullOrEmpty(importcode))
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(productcode))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.店舗名.Contains(shopname));
                }
                else
                if (string.IsNullOrEmpty(shopname))
                {
                    return querys.Where(x => x.仕入先コード.Contains(importcode) && x.商品コード.Contains(productcode));
                }
                else
                {
                    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname) && x.仕入先コード.Contains(importcode));
                }

            }




            //var context = new Model1();

            //var querys =  from x in context.zaikoes
            //              join y in context.SHOPs on x.店舗コード equals y.店舗コード
            //              join z in context.goods on x.商品コード equals z.商品コード
            //              where x.商品コード == productclick
            //              orderby x.店舗コード
            //              select new tablejoin()
            //              {
            //                  shop = y,
            //                  good = z,
            //                  JAN = x.JAN,
            //                  商品コード = x.商品コード,
            //                  商品名 = z.商品名,
            //                  標準上代 = z.標準上代,
            //                  在庫数 = x.在庫数,
            //                  店舗コード = x.店舗コード,
            //                  店舗名 = y.店舗名

            //              };

            //if (string.IsNullOrEmpty(productcode) & string.IsNullOrEmpty(shopname)) 
            //{

            //    return querys;
            //}
            //else
            //if (string.IsNullOrEmpty(productcode))
            //{
            //    return querys.Where(x => x.店舗名.Contains(shopname));
            //}
            //else
            //if (string.IsNullOrEmpty(shopname))
            //{
            //    return querys.Where(x => x.商品コード.Contains(productcode));
            //}
            //else
            //{
            //    return querys.Where(x => x.商品コード.Contains(productcode) && x.店舗名.Contains(shopname));
            //}


            //※FK及び，コンテキストを定義していればこれでできるはず？
            /*context.zaikoes.Select(zaiko => new { zaiko.id,zaiko.JAN });*/
        }

        //商品コードクリックボタン
        protected void PCode_Command(object sender, CommandEventArgs e)
        {
            flag = 0;
            p = e.CommandArgument;
            //Response.Redirect(string.Format("~/WebForm1.aspx?P={0}", e.CommandArgument));
        }

        //店舗コードクリックボタン
        protected void Scode_Command(object sender, CommandEventArgs e)
        {

            Response.Redirect(string.Format("~/WebForm1.aspx?S={0}", e.CommandArgument));
        }

        //TOPへ戻るボタン
        protected void resetbutton_Command(object sender, CommandEventArgs e)
        {
                Response.Redirect("~/WebForm1.aspx");
        }

        //ログアウト機能--未実装
        protected void logout_Click(object sender, EventArgs e)
        {
            if(Request.Cookies["UserName"]!=null)
            {
                //クッキーを過去の時間にして強制的に破棄する
                HttpCookie delcookie = new HttpCookie("UserName");
                delcookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(delcookie);
            }

            Response.Redirect("~/login.aspx");

        }







        //CSVダウンロード
        protected void CSVDownload_Click(object sender, EventArgs e)
        {
            var dt = DateTime.Now;


            Response.Clear();
            Response.Buffer = true;
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("Shift-jis");
            Response.AddHeader("content-disposition", "attachment;filename=OK在庫データ" + dt.ToString("yyyyMMddHHmmss") + ".csv");
            Response.ContentType = "application/text";
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            StringBuilder sbuilder = new System.Text.StringBuilder();
            for (int index = 0; index < GridView1.Columns.Count; index++)
            {
                sbuilder.Append(GridView1.Columns[index].HeaderText + ",");
            }
            sbuilder.Append("\r\n");

            foreach (GridViewRow row in GridView1.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    string text = "";
                    //boundfield以外はコントロール属性を判定してテキストに格納する※linkbuttonがある
                    if (cell.Controls.Count > 0)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "LinkButton":
                                    text = (control as LinkButton).Text;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        text = cell.Text;
                    }

                    sbuilder.Append(text + ",");
                }
                sbuilder.Append("\r\n");
            }
            Response.Output.Write(sbuilder.ToString());
            Response.Flush();
            Response.End();


            //for (int r = 0; r < GridView1.Rows.Count; r++)
            //{
            //    for (int c = 0; c < GridView1.Columns.Count; c++)
            //    {
            //        sbuilder.Append(GridView1.Rows[r].Cells[c].Text + ",");
            //    }
            //    sbuilder.Append("\r\n");
            //}



        }

    }


}






