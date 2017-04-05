using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public class EventDay{
        public String EventID { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String EventDesc { get; set; }

        public EventDay(String pEventID, String pStartDate, String pEndDate, String pDesc)
        {
            EventID = pEventID;
            StartDate = pStartDate;
            EndDate = pEndDate;
            EventDesc = pDesc;
        }
        public EventDay(EventDay pEvent)
        {
            EventID = pEvent.EventID;
            StartDate = pEvent.StartDate;
            EndDate = pEvent.EndDate;
            EventDesc = pEvent.EventDesc;
        }

    }

    public partial class ExtraCalendar : UserControl
    {
        ExtraPanel _exPanel;
        // DateTime _CurrentDate;
        public DateTime CURRENTDATE;
        List<EventDay> _Events;

        //[Description("Cellclick"), Category("속성")]
        //public delegate void UserCellClick(object sender, MouseEventArgs e);
        //public event UserCellClick CellClick ;

        public ExtraCalendar()
        {
            InitializeComponent();
            CURRENTDATE = DateTime.Now;
            _exPanel = new ExtraPanel();
            _exPanel.Dock = DockStyle.Fill;
            _exPanel.setDay(CURRENTDATE);
            tableLayoutPanel1.Controls.Add(_exPanel);

        }
        public void NextMonth(List<EventDay> pEvents)
        {
            CURRENTDATE = CURRENTDATE.AddMonths(1);
            _exPanel.EventDays = pEvents;
            _exPanel.setDay(CURRENTDATE);
        
         }

        public void Today(List<EventDay> pEvents)
        {
            CURRENTDATE = DateTime.Now;
            _exPanel.EventDays = pEvents;
            _exPanel.setDay(CURRENTDATE);
         
        }

        public void PreMonth(List<EventDay> pEvents)
        {
            CURRENTDATE = CURRENTDATE.AddMonths(-1);
            _exPanel.EventDays = pEvents;
            _exPanel.setDay(CURRENTDATE);
           
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            int row = 0;
            int verticalOffset = 0;
            foreach (int h in tableLayoutPanel1.GetRowHeights())
            {
                int column = 0;
                int horizontalOffset = 0;
                foreach (int w in tableLayoutPanel1.GetColumnWidths())
                {
                    Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                    if (rectangle.Contains(e.Location))
                    {
                        MessageBox.Show("눌림" + String.Format("row {0}, column {1} was clicked", row, column));
                        return;
                    }
                    horizontalOffset += w;
                    column++;
                }
                verticalOffset += h;
                row++;
            }
        }

    }
}
