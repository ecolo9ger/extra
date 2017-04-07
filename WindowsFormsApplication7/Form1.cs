using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Json;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        DateTime currentDate;

        public Form1()
        {
            InitializeComponent();
            currentDate = extraCalendar1.CURRENTDATE;
           // label1.Text = currentDate.Year.ToString() + "," + currentDate.Month.ToString();
            label2_Click(null, null);
            SetData();
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            //String coll = "{\"eID\": \"0108\",\"Startdate\": \"전정하기\",\"endDate\": \"rlawnsgh\",\"desc\":\"탱자나무 식재\"}";
            //JsonTextParser parser = new JsonTextParser();
            //JsonObject obj = parser.Parse(coll);
            //JsonObjectCollection col = (JsonObjectCollection)obj;
            //String accno = (String)col["eDate"].GetValue();
            //String desc = (String)col["desc"].GetValue();

            List<EventDay> Events = new List<EventDay>();
            Events.Add(new EventDay("0001", "20170506", "20170506", "5제초하기"));
            Events.Add(new EventDay("0002", "20170511", "20170511", "5감귤나무 식재"));
            Events.Add(new EventDay("00011", "20170512", "20170512", "5벚나무 식재"));
            Events.Add(new EventDay("00019", "20170525", "20170525", "5한라봉 식재"));
            Events.Add(new EventDay("00019", "20170527", "20170528", "5궁천 식재"));

            //작업일지 보이기
            


            extraCalendar1.NextMonth(Events);
            currentDate = extraCalendar1.CURRENTDATE;
            label1.Text = currentDate.Year.ToString() + "," + currentDate.Month.ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            List<EventDay> Events = new List<EventDay>();
            Events.Add(new EventDay("0001", "20170405", "20170406", "4탱자나무 식재"));
            Events.Add(new EventDay("0002", "20170401", "20170406", "4감귤나무 식재"));
            Events.Add(new EventDay("00011", "20170407", "20170409", "4벚나무 식재"));
            Events.Add(new EventDay("00019", "20170405", "20170405", "4한라봉 식재"));
            Events.Add(new EventDay("00019", "20170415", "20170420", "4궁천 식재"));


            extraCalendar1.PreMonth(Events);
            currentDate = extraCalendar1.CURRENTDATE;
            label1.Text = currentDate.Year.ToString() + "," + currentDate.Month.ToString();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
         
            List<EventDay> Events = new List<EventDay>();
            Events.Add(new EventDay("0001", "20170405", "20170406", "4탱4자나무 식재"));
            Events.Add(new EventDay("0005", "20170405", "20170406", "4유류비발생"));
            Events.Add(new EventDay("0002", "20170401", "20170406", "4감귤나무 식재"));
            Events.Add(new EventDay("00011", "20170407", "20170409", "4벚나무 식재"));
            Events.Add(new EventDay("00019", "20170405", "20170405", "4한라봉 식재"));
            Events.Add(new EventDay("00019", "20170415", "20170420", "4궁천 식재"));

            WorkDiary wd = new WorkDiary();
            wd.WDID = "0001";
            wd.WDDate = new DateTime(2017, 04, 06);
            wd.ItemName = "감귤";
            wd.WorkDesc = "감귤재배하다";
            wd.SetWDPic("c:\\pic\aaa.jpg");
            wd.SetWDPic("c:\\pic\aaa.jpg");




            extraCalendar1.Today(Events);
            currentDate = extraCalendar1.CURRENTDATE;
            label1.Text = currentDate.Year.ToString() + "," + currentDate.Month.ToString();
           
        }

       public void SetData()
        {
            JsonObjectCollection collection = new JsonObjectCollection();

            collection.Add(new JsonStringValue("0107", "예약날짜1"));           
            collection.Add(new JsonStringValue("0215", "예약날짜2"));
            collection.Add(new JsonStringValue("0307", "예약날짜3"));
        }

        
        public void GetData()
        {
            String coll = "{\"eDate\": \"0108\",\"desc\": \"전정하기\",\"pass\": \"rlawnsgh\"}";
            JsonTextParser parser = new JsonTextParser();
            JsonObject obj = parser.Parse(coll);
            JsonObjectCollection col = (JsonObjectCollection)obj;
            String accno = (String)col["eDate"].GetValue();
            String desc = (String)col["desc"].GetValue();
        }
     
    }
}
