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
using System.Collections;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication7
{

    public partial class NaverLogin : Form
    {
        string ClientID = "plNwcW3pVWEltuCEwIAU";
        string ClientSecret = "nrSuLV4eoy";
        String access_token;
        string State;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
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
            string headertoken = "Bearer " + token; // Bearer 다음에 공백 추가
            String apiURL = "https://openapi.naver.com/blog/writePost.json";



            //내용넣기
            string title = textBox3.Text;
            // string contents = "테스트문자열";
            //  string contents = HttpUtility.UrlEncode(htmlwysiwyg1.getHTML().Replace("\r\n", string.Empty));
            string contents = editor1.Html;


            //// get base64 data from image
            //byte[] bytes = File.ReadAllBytes(@"D:\tmp\WpfApplication1\WpfApplication1\Images\Icon128.gif");
            //string encoded = Convert.ToBase64String(bytes);
            //postData += "fileupload=" + encoded;

            //byte[] reqData = Encoding.UTF8.GetBytes(postData);
            //using (Stream dataStream = req.GetRequestStream())
            //{
            //    dataStream.Write(reqData, 0, reqData.Length);
            //}
            // string image = editor1.ReplaceFileSystemImages2(editor1.Html);
            string naverContent = editor1.NaverBlogContentFormat();
            List<string> imgSrc = new List<string>();
            imgSrc = editor1.GetImageSrc();


            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(apiURL);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            wr.Headers.Add("X-Naver-Client-Id", ClientID);
            wr.Headers.Add("X-Naver-Client-Secret", ClientSecret);
            wr.Headers.Add("Authorization", headertoken);
            wr.Method = "POST";

            Stream rs = wr.GetRequestStream();
            Hashtable nvc = new Hashtable();
            nvc.Add("title", title);
            nvc.Add("contents",naverContent);
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string contentType = null;
            string imageFileName = null;
            FileStream fileStream = null;
            int dummy = 0;
            foreach (string v in imgSrc)
            {
                if (File.Exists(v))
                {

                    imageFileName = Path.GetFileName(v);
                    switch (Path.GetExtension(imageFileName))
                    {
                        case ".jpeg":
                            contentType = "image/jpeg"; break;
                        case ".jpg":
                            contentType = "image/jpeg"; break;
                        case ".png":
                            contentType = "image/png"; break;
                        case ".gif":
                            contentType = "image/gif"; break;
                    }
                    
 
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                    string header = string.Format(headerTemplate, "image", imageFileName, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                    rs.Write(headerbytes, 0, headerbytes.Length);

                    fileStream = new FileStream(v, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        Console.WriteLine(buffer);
                        rs.Write(buffer, 0, bytesRead);
                    }
                    fileStream.Close();
                }
                if(dummy < imgSrc.Count-1)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                }
                dummy++;
               
            }


            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                MessageBox.Show(reader2.ReadToEnd());
                // log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                // log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }




            //  //using (var reqStream = req.GetRequestStream())
            //  //{
            //  //    var reqWriter = new StreamWriter(reqStream);
            //  //    var tmp = string.Format(propFormat, "str1", "hello world");
            //  //    reqWriter.Write(tmp);
            //  //    tmp = string.Format(propFormat, "str2", "hello world 2");
            //  //    reqWriter.Write(tmp);
            //  //    reqWriter.Write("--" + boundary + "--");
            //  //    reqWriter.Flush();
            //  //}
            //  byte[] formitembytes1 = System.Text.Encoding.UTF8.GetBytes(title);
            //  byte[] formitembytes2 = System.Text.Encoding.UTF8.GetBytes(contents);

            //  var boundary = "------------------------" + DateTime.Now.Ticks;
            //  var newLine = Environment.NewLine;
            //  var propFormat = "--" + boundary + newLine +
            //                      "Content-Disposition: form-data; name=\"{0}\"" + newLine + newLine +
            //                      "{1}" + newLine;
            //  var fileHeaderFormat = "--" + boundary + newLine +
            //                          "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" + newLine;


            ////  byte[] byteDataParams = Encoding.UTF8.GetBytes("title=" +  title + "&contents=" + contents);

            //  HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            //  request.Headers.Add("X-Naver-Client-Id", ClientID);
            //  request.Headers.Add("X-Naver-Client-Secret", ClientSecret);
            //  request.Headers.Add("Authorization", header);
            //  request.Method = "POST";
            //  request.ContentType = "multipart/form-data; boundary=" + boundary;


            //    Stream st = request.GetRequestStream();
            //    StreamWriter reqWriter = new StreamWriter(st);
            //    reqWriter.Write(string.Format(propFormat, "title","fgdfgdf"));
            //  reqWriter.Write(string.Format(propFormat, "contents","gdfgdfgdfgdfgd"));
            //  reqWriter.Write(string.Format(fileHeaderFormat, "image", image));
            //    reqWriter.Write("--" + boundary + "--");
            // // request.ContentLength = st.Length;
            //  reqWriter.Flush();

            //  //  st.Write(byteDataParams, 0, byteDataParams.Length);
            //  //  st.Close();

            //  HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //  string status = response.StatusCode.ToString();
            //  if (status == "OK")
            //  {
            //      Stream stream = response.GetResponseStream();
            //      StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            //      string text = reader.ReadToEnd();
            //      Console.WriteLine(text);
            //  }
            //  else
            //  {
            //      Console.WriteLine("Error 발생=" + status);
            //  }
            //  st.Close();
            //  response.Close();
            // // MessageBox.Show(contents);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(webBrowser2.Document.InvokeScript("aaa()").ToString());
            //   MessageBox.Show(htmlwysiwyg1.getHTML());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //  webBrowser1.Document.ExecCommand("InsertImage", true, null);
            folderBrowserDialog1.ShowDialog();

        }


        public void HttpUploadFile(string url, string file, string paramName, string contentType, Hashtable nvc)
        {

        }

    }
}
