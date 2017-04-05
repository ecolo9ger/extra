using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    class ExtraPanel : TableLayoutPanel
    {

        public delegate void CellClickEventHandler(object sender, EventArgs e);

        public event CellClickEventHandler OnCellClick;


        public DateTime CURRENTDATE { get; set; }

        internal List<EventDay> EventDays
        {
            get
            {

                return eventDays;
            }

            set
            {
              //
                eventDays = value;
            }
        }

        string SettingDay;
        private List<EventDay> eventDays;

        public ExtraLabel[] exLabel = new ExtraLabel[42];
        public ExtraLabel[] exSolar24 = new ExtraLabel[42];
        public ExtraLabel[] exEvent1 = new ExtraLabel[42];
        public ExtraLabel[] exEvent2 = new ExtraLabel[42];
        public ExtraLabel[] exEvent3 = new ExtraLabel[42];

        ExtraLabel lblDay = null; //날짜 표시
        ExtraLabel lblSolar24 = null; //24절기
        ExtraLabel lblEvent1 = null;
        ExtraLabel lblEvent2 = null;
        ExtraLabel lblEvent3 = null;

        private int _FlagYear;
        HolidayList hlist;
        SolarTerma solar24;

        public ExtraPanel()
        {
            DoubleBuffered = true;
            EventDays = new List<EventDay>();
            CURRENTDATE = DateTime.Now;
            SetRowColumn(7, 7);
            hlist = new HolidayList();

            hlist.CreateHoliday(CURRENTDATE);

            solar24 = new SolarTerma(CURRENTDATE);
           


        }

        private void lblDay_Click(object sender, EventArgs e)
        {
            if (((Control)sender).Text != null && ((Control)sender).Text != "")
            {
                DateTime dt;
                try
                {
                    dt = new DateTime(CURRENTDATE.Year, CURRENTDATE.Month, int.Parse(((Control)sender).Text), 0, 0, 0);
                    MessageBox.Show(dt.ToString());
                }
                catch
                {
                    return;
                }
                //setDay(CURRENTDATE);
            }
        }

        //칸, 줄을 그려 놓는다.
        public void SetRowColumn(int rows, int columns)
        {
            EventDays = null;
            ColumnCount = 0;
            RowCount = 0;
            ColumnCount = columns;
            RowCount = rows;
            Controls.Clear();
            int dummyDate = 1;


            DateTime startNum = new DateTime(CURRENTDATE.Year, CURRENTDATE.Month, 1);
            int endNum = DateTime.DaysInMonth(CURRENTDATE.Year, CURRENTDATE.Month);
            int startWeek = (int)startNum.DayOfWeek;

            int index =0;//  = i + startWeek - 1; //인덱스로 날짜요일 저장
                      // exLabel[index].Text = string.Format("{0:0}", i);

            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            this.BackColor = Color.White;
            for (int i = 0; i < RowCount; i++)
            {
                if (i == 0)
                {
                    RowStyles.Add(new RowStyle(SizeType.Absolute, 21));
                }
                else
                {
                    RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }

            }
            for (int i = 0; i < ColumnCount; i++) ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));


            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {



                    DisplayPanel floorTop = new DisplayPanel();
                    DisplayPanel floorBottm = new DisplayPanel();
                    DisplayPanel floorContainer = new DisplayPanel();

                    lblDay = new ExtraLabel();
                    lblSolar24 = new ExtraLabel();
                    lblEvent1 = new ExtraLabel();
                    lblEvent2 = new ExtraLabel();
                    lblEvent3 = new ExtraLabel();

                    lblDay.AutoSize = true;

                    // ColumnStyles.Add(new ColumnStyle(, Color.Brown));
                    if (i == 0) //첫번째 라인은  요일
                    {
                        lblDay.Text = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[j][0].ToString();
                        lblDay.ForeColor = System.Drawing.Color.Black;
                        //  lblDay.BackColor = System.Drawing.Color.Azure;



                        //   lblDay.Visible = false;
                        //    lblSolar24.Visible = false;
                        lblEvent1.Height = 0;
                        //   lblEvent2Height = 0;
                        // lblEvent3.Height = 0;
                    }
                    else
                    {

                        ////날짜 만들기
                        //if (dummyDate >= startWeek)
                        //{
                        //    index = dummyDate - startWeek; //인덱스로 날짜요일 저장
                        //    SettingDay2 = CURRENTDATE.ToString("MM") + index.ToString("00");
                        //}
                        //else SettingDay2 = "0000";

                        //dummyDate++;



                       
                        lblDay.ForeColor = System.Drawing.Color.Black;
                        lblDay.BackColor = System.Drawing.Color.White;
                       // lblDay.Text = SettingDay2.Substring(3,1);

                        lblSolar24.ForeColor = System.Drawing.Color.Black;
                        //   lblSolar24.BackColor = System.Drawing.Color.Aqua;
                        //  lblSolar24.Text = "sfd";
                        //
                       // lblEvent3.Text = "event3";
                       // lblEvent2.Text = "event2";
                        //  lblEvent1.Text = "event1";

                        //이벤트 표시
                       // SetEvent();


                        lblDay.MouseClick += new MouseEventHandler(lblDay_Click); //이벤트 등록

                        exLabel[j + ((i - 1) * 7)] = lblDay; //인덱스로 만들기 위해 첫줄을 빼고 저장 해 놓는다.
                        exSolar24[j + (i - 1) * 7] = lblSolar24;
                        exEvent1[j + ((i - 1) * 7)] = lblEvent1;
                        exEvent2[j + ((i - 1) * 7)] = lblEvent2;
                        exEvent3[j + ((i - 1) * 7)] = lblEvent3;
                    }

                    lblEvent3.Dock = DockStyle.Top;
                    // lblEvent3.Dock = DockStyle.Left;
                    lblEvent3.Padding = new Padding(0);
                    // lblEvent3.Margin = new Padding(1);
                    //   lblEvent3.BackColor = Color.Yellow;
                    lblEvent3.TextAlign = System.Drawing.ContentAlignment.TopLeft;

                    lblEvent2.Dock = DockStyle.Top;
                    lblEvent2.Padding = new Padding(0);
                    // lblEvent2.Margin = new Padding(1);
                    //   lblEvent2.BackColor = Color.Violet;
                    lblEvent2.TextAlign = System.Drawing.ContentAlignment.TopLeft;

                    lblEvent1.Dock = DockStyle.Top;
                    lblEvent1.Padding = new Padding(0);
                    //  lblEvent1.Margin = new Padding(1);
                    //    lblEvent1.BackColor = Color.Tomato;
                    //   lblEvent1.AutoSize = true;
                    lblEvent1.TextAlign = System.Drawing.ContentAlignment.TopLeft;



                    lblSolar24.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblSolar24.Font = new System.Drawing.Font("맑은고딕", 9);
                    lblSolar24.BorderStyle = BorderStyle.None;
                    lblSolar24.Margin = new Padding(1);
                    lblSolar24.Padding = new Padding(0);
                    lblDay.Anchor = AnchorStyles.Right;
                    lblSolar24.Dock = DockStyle.Right;
                    lblSolar24.Dock = DockStyle.Top;
                    lblSolar24.AutoSize = true;

                    lblDay.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblDay.Font = new System.Drawing.Font("맑은고딕", 9, FontStyle.Bold);
                    lblDay.BorderStyle = BorderStyle.None;
                    lblDay.Margin = new Padding(1);
                    lblDay.Padding = new Padding(0);
                    lblDay.Anchor = AnchorStyles.Left;
                    lblDay.Dock = DockStyle.Left;


                    floorTop.Dock = DockStyle.Top;
                    floorTop.Padding = new Padding(0);
                    floorTop.Margin = new Padding(0);
                    //floorTop.BorderStyle = BorderStyle.FixedSingle;
                    floorTop.BackColor = Color.White;
                    floorTop.AutoSize = true;


                    floorBottm.Dock = DockStyle.Left;
                    floorBottm.BackColor = Color.White;

                    floorBottm.Controls.Add(lblEvent3);
                    floorBottm.Controls.Add(lblEvent2);
                    floorBottm.Controls.Add(lblEvent1);

                    floorTop.Controls.Add(lblSolar24);
                    floorTop.Controls.Add(lblDay);


                    floorContainer.Controls.Add(floorBottm);
                    floorContainer.Controls.Add(floorTop);
                    Controls.Add(floorContainer, j, i);
                }

            }
        }

        //날짜를 그린다.
        public void setDay(DateTime pDateTime)
        {
            this.CURRENTDATE = pDateTime;
            ClearDay();

            DateTime startNum = new DateTime(CURRENTDATE.Year, CURRENTDATE.Month, 1);
            int endNum = DateTime.DaysInMonth(CURRENTDATE.Year, CURRENTDATE.Month);
            int startWeek = (int)startNum.DayOfWeek;

            for (int i = 1; i <= endNum; i++)
            {

                SettingDay = CURRENTDATE.ToString("MM") + i.ToString("00");

                int index = i + startWeek - 1; //인덱스로 날짜요일 저장
                exLabel[index].Text = string.Format("{0:0}", i);

                //요일별 날짜색깔 지정
                if (index % 7 == 0)
                {
                    exLabel[index].ForeColor = Color.Red;
                }

                //공휴일 날짜색깔 지정
                if (_FlagYear != CURRENTDATE.Year)//년도가 바뀌었으면 휴일을 업데이트 한다.
                {
                    hlist.CreateHoliday(CURRENTDATE); //공휴일, 명절정보 가져온다.
                    solar24 = new SolarTerma(CURRENTDATE);
                }


                //휴일 표시
                if (hlist.GetHoliday(SettingDay) != null)
                {
                    exLabel[index].ForeColor = Color.Red;
                    exLabel[index].Text += " " + hlist.GetHoliday(SettingDay);
                }

                //24절기 표시
                if (solar24.getSolarTermaDate(SettingDay) != null)
                {
                    exLabel[index].Text += " " + solar24.getSolarTermaDate(SettingDay);
                }

                //이벤트 표시
                //  exEvent1[index].Text = "dfsdf";
                SetEvent(index);


            }

            _FlagYear = CURRENTDATE.Year; //1년에 한번만 공휴일 24절기 내용을 가져온다.
        }

        public void SetEvent(int pIndex)
        {
            if (EventDays == null) return;
            //EventDays;
            for(int i =0; i<EventDays.Count; i++)
            {
                String temp = EventDays[i].StartDate.Substring(4, 4);
                if (temp == SettingDay)
                {
                    if (exEvent1[pIndex].Text == "") exEvent1[pIndex].Text = EventDays[i].EventDesc;
                    else if(exEvent1[pIndex].Text != "") exEvent2[pIndex].Text = EventDays[i].EventDesc;
                    else if(exEvent2[pIndex].Text != "") exEvent3[pIndex].Text = EventDays[i].EventDesc;

                    Console.WriteLine(EventDays[0].EventDesc);
                }
            }
            

            //EventDays;
            //temp = EventDays[0].EndDate.Substring(4, 4);
            //if (temp == SettingDay2)
            //{
            //    lblEvent1.Text = EventDays[0].EventDesc;
            //    Console.WriteLine(EventDays[0].EventDesc);
            //}

        }

        //날짜를 지운다.
        public void ClearDay()
        {
            for (int i = 0; i < 42; i++)
            {
                exLabel[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                exLabel[i].BackColor = Color.White;
                exLabel[i].Cursor = Cursors.Default;
                exLabel[i].ForeColor = Color.Black;
                exLabel[i].Text = string.Empty;


                exEvent1[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                exEvent1[i].BackColor = Color.White;
                exEvent1[i].Cursor = Cursors.Default;
                exEvent1[i].ForeColor = Color.Black;
                exEvent1[i].Text = string.Empty;

                exEvent2[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                exEvent2[i].BackColor = Color.White;
                exEvent2[i].Cursor = Cursors.Default;
                exEvent2[i].ForeColor = Color.Black;
                exEvent2[i].Text = string.Empty;

                exEvent3[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                exEvent3[i].BackColor = Color.White;
                exEvent3[i].Cursor = Cursors.Default;
                exEvent3[i].ForeColor = Color.Black;
                exEvent3[i].Text = string.Empty;
            }
        }

    }
}
