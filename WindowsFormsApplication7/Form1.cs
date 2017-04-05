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
            Events.Add(new EventDay("0001", "20170506", "20170506", "제초하기"));
            Events.Add(new EventDay("0002", "201705011", "20170511", "감귤나무 식재"));
            Events.Add(new EventDay("00011", "201705012", "20170512", "벚나무 식재"));
            Events.Add(new EventDay("00019", "20170525", "20170525", "한라봉 식재"));
            Events.Add(new EventDay("00019", "20170527", "20170528", "궁천 식재"));

            extraCalendar1.NextMonth(Events);
            currentDate = extraCalendar1.CURRENTDATE;
            label1.Text = currentDate.Year.ToString() + "," + currentDate.Month.ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            List<EventDay> Events = new List<EventDay>();
            Events.Add(new EventDay("0001", "20170405", "20170406", "탱자나무 식재"));
            Events.Add(new EventDay("0002", "20170401", "20170406", "감귤나무 식재"));
            Events.Add(new EventDay("00011", "20170407", "20170409", "벚나무 식재"));
            Events.Add(new EventDay("00019", "20170405", "20170405", "한라봉 식재"));
            Events.Add(new EventDay("00019", "20170415", "20170420", "궁천 식재"));


            extraCalendar1.PreMonth(Events);
            currentDate = extraCalendar1.CURRENTDATE;
            label1.Text = currentDate.Year.ToString() + "," + currentDate.Month.ToString();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
         
            List<EventDay> Events = new List<EventDay>();
            Events.Add(new EventDay("0001", "20170405", "20170406", "탱자나무 식재"));
            Events.Add(new EventDay("0005", "20170405", "20170406", "유류비발생"));
            Events.Add(new EventDay("0002", "20170401", "20170406", "감귤나무 식재"));
            Events.Add(new EventDay("00011", "20170407", "20170409", "벚나무 식재"));
            Events.Add(new EventDay("00019", "20170405", "20170405", "한라봉 식재"));
            Events.Add(new EventDay("00019", "20170415", "20170420", "궁천 식재"));

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
