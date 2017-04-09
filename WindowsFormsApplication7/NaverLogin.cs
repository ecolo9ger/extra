using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Json;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication7
{
   
    public partial class NaverLogin : Form
    {
        string ClientID = "plNwcW3pVWEltuCEwIAU";
        string ClientSecret = "nrSuLV4eoy";
        String access_token;
        string State;

        [PermissionSet(SecurityAction.Demand,Name="FullTrust")]
       // [Com(true)]
        public NaverLogin()
        {
            InitializeComponent();
           // webBrowser2.Navigate(new Uri("http://localhost/smarteditor2-master/dist/SmartEditor2.html"));
          //  webBrowser2.ObjectForScripting = this;
        }

        public void CallForm(object msg)
        {
            System.Diagnostics.Debug.Write(msg.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string RedirectURL = "http://127.0.0.1:3000/naverlogin";
            State = (new Random()).Next().ToString();
                       
            StringBuilder AuthURL = new StringBuilder();

            AuthURL.Append("https://nid.naver.com/oauth2.0/authorize?");
            AuthURL.Append("response_type=code");
            AuthURL.Append("&client_id=" + ClientID);
            AuthURL.Append("&redirect_uri=" + RedirectURL);
            AuthURL.Append("&state=" + State);

            textBox1.Text = AuthURL.ToString();
            webBrowser1.Navigate(new Uri(AuthURL.ToString()));
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = webBrowser1.Url.Query;

           // ?code = zVYxbC9FeVgVMi62 & state = RAMDOM_STATE

            string loginOK = textBox2.Text;
            State = (new Random()).Next().ToString();

             loginOK = HttpUtility.ParseQueryString(webBrowser1.Url.Query).Get("code");

            WebClient webClient;

            string sAccessToken_Url = "https://nid.naver.com/oauth2.0/token";

            //쿼리 세팅
            webClient = new WebClient();
            webClient.QueryString.Add("grant_type", "authorization_code");
            webClient.QueryString.Add("client_id", ClientID);
            webClient.QueryString.Add("client_secret", ClientSecret);
            webClient.QueryString.Add("code", loginOK);
            webClient.QueryString.Add("state", State);

            //요청
            Stream stream = webClient.OpenRead(sAccessToken_Url);
            //결과 받기
            String sResultJson = new StreamReader(stream).ReadToEnd();

            JsonTextParser parser = new JsonTextParser();
            JsonObject obj = parser.Parse(sResultJson.ToString());
            JsonObjectCollection col = (JsonObjectCollection)obj;
           
            access_token = (String)col["access_token"].GetValue();

            //MessageBox.Show(access_token);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebClient webClient;

            string sListCategoryApiUri = "https://openapi.naver.com/blog/listCategory.json";
            string sBlogID = "ecologer";

            webClient = new WebClient();
            webClient.Headers.Add("Authorization", "Bearer " + access_token);
            webClient.QueryString.Add("blogId", sBlogID);


            //요청
            Stream stream = webClient.OpenRead(sListCategoryApiUri);
            //결과 받기
            string responseJSON2 = new StreamReader(stream).ReadToEnd();
            MessageBox.Show(responseJSON2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string token = access_token;// 네이버 로그인 접근 토큰;
            string header = "Bearer " + token; // Bearer 다음에 공백 추가
            String apiURL = "https://openapi.naver.com/blog/writePost.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Headers.Add("X-Naver-Client-Id", "YOUR-CLIENT-ID");
            request.Headers.Add("X-Naver-Client-Secret", "YOUR-CLIENT-SECRET");
            request.Headers.Add("Authorization", header);
            request.Method = "POST";
            string title = textBox3.Text;
            // string contents = richTextBox1.Text;
            string contents = htmlwysiwyg1.getHTML().Replace("\r\n", string.Empty);
            byte[] byteDataParams = Encoding.UTF8.GetBytes("title=" + title + "&contents=" + contents);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
            st.Close();
            response.Close();
           // MessageBox.Show(contents);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(webBrowser2.Document.InvokeScript("aaa()").ToString());
            MessageBox.Show(htmlwysiwyg1.getHTML());
        }
    }
}
