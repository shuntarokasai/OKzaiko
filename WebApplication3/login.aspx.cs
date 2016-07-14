using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

            if (this.Login1.UserName == "fujii" && this.Login1.Password == "fujii")
            {
                //クッキーを新たに取得する
                HttpCookie cookie = new HttpCookie("UserName");
                cookie.Value = "fujii";
                cookie.Expires = DateTime.Now.AddMinutes(1); //クッキー所有時間を設定
                Response.Cookies.Add(cookie);
                Login1.DestinationPageUrl = "~/WebForm1.aspx";
                e.Authenticated = true;
            }
            else
            {
                var content = "";
                var context = new Model1();
                var loginshops = context.SHOPs;
                foreach (var loginshop in loginshops)
                {
                    content += string.Format("{0}<br/>", loginshop.店舗コード);
                }
                //コンテキストの店舗コードにユーザーが入力した値が入っているかどうかをチェック・及びパスワードはIDと同一
                if (0 <= content.IndexOf(this.Login1.UserName) && this.Login1.Password == this.Login1.UserName)
                {
                    //クッキーを新たに取得する
                    HttpCookie cookie = new HttpCookie("UserName");
                    cookie.Value = this.Login1.UserName;
                    cookie.Expires = DateTime.Now.AddMinutes(1);
                    Response.Cookies.Add(cookie);


                    this.Login1.DestinationPageUrl = string.Format("~/WebForm1.aspx?S={0}", this.Login1.UserName);
                    e.Authenticated = true;
                }
                else
                {
                    e.Authenticated = false;
                }

            }
            //if (this.Login1.UserName.Length >= 4 || (0<= this.Login1.UserName.Length && this.Login1.UserName.Length <=2))
            //{
            //    e.Authenticated = false;
            //}
            //else
            //{
            //    Login1.DestinationPageUrl = string.Format("~/WebForm1.aspx?S={0}", this.Login1.UserName);
            //    e.Authenticated = true;
            //}
        }
    }
}