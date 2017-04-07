using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
    public class WorkDiary
    {
        private String wDID;
        private DateTime wDDate;
        private String itemName;
        private String wDWheather;
        private String wDTemperHigh;
        private String wDTemperLow;
        private int rainFall;
        private int selfManCnt;
        private int selfManTime;
        private int selfWomanCnt;
        private int selfWomanTime;
        private int empyManCnt;
        private int empyManTime;
        private int empyWomanCnt;
        private int empyWomanTime;
        private String workDesc;
        private List<String> wDPic = new List<String>();

        public string WDID
        {
            get
            {
                return wDID;
            }

            set
            {
                wDID = value;
            }
        }

        public DateTime WDDate
        {
            get
            {
                return wDDate;
            }

            set
            {
                wDDate = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
            }
        }

        public string WDWheather
        {
            get
            {
                return wDWheather;
            }

            set
            {
                wDWheather = value;
            }
        }

        public string WDTemperHigh
        {
            get
            {
                return wDTemperHigh;
            }

            set
            {
                wDTemperHigh = value;
            }
        }

        public string WDTemperLow
        {
            get
            {
                return wDTemperLow;
            }

            set
            {
                wDTemperLow = value;
            }
        }

        public int RainFall
        {
            get
            {
                return rainFall;
            }

            set
            {
                rainFall = value;
            }
        }

        public int SelfManCnt
        {
            get
            {
                return selfManCnt;
            }

            set
            {
                selfManCnt = value;
            }
        }

        public int SelfManTime
        {
            get
            {
                return selfManTime;
            }

            set
            {
                selfManTime = value;
            }
        }

        public int SelfWomanCnt
        {
            get
            {
                return selfWomanCnt;
            }

            set
            {
                selfWomanCnt = value;
            }
        }

        public int SelfWomanTime
        {
            get
            {
                return selfWomanTime;
            }

            set
            {
                selfWomanTime = value;
            }
        }

        public int EmpyManCnt
        {
            get
            {
                return empyManCnt;
            }

            set
            {
                empyManCnt = value;
            }
        }

        public int EmpyManTime
        {
            get
            {
                return empyManTime;
            }

            set
            {
                empyManTime = value;
            }
        }

        public int EmpyWomanCnt
        {
            get
            {
                return empyWomanCnt;
            }

            set
            {
                empyWomanCnt = value;
            }
        }

        public int EmpyWomanTime
        {
            get
            {
                return empyWomanTime;
            }

            set
            {
                empyWomanTime = value;
            }
        }

        public string WorkDesc
        {
            get
            {
                return workDesc;
            }

            set
            {
                workDesc = value;
            }
        }

        public WorkDiary()
        {
           
        }

        public void SetWDPic(string pPICURI)
        {
            wDPic.Add(pPICURI);
        }
    }
}
