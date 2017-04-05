using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication8
{
    public class HolidayList
    {
        public string HolyNum;
        public string HoliName;
        public HolidayList(string holyNum,string holiName)
        {
            HolyNum = holyNum;
            HoliName = holiName;
        }
    }

    public class Holiday
    {
        List<HolidayList> htHoliday = new List<HolidayList>();

        DateTime _currentDate;
        public  Holiday(DateTime currentDate)
        {
            _currentDate = currentDate;
            SetHoliDay();
        }

        private void SetHoliDay()
        {
            

            htHoliday = new List<HolidayList>();
            htHoliday.Add(new HolidayList("0101", "신정"));
            htHoliday.Add(new HolidayList("0301", "삼일절"));
            htHoliday.Add(new HolidayList("0505", "어린이날"));
            htHoliday.Add(new HolidayList("0606", "현충일"));
            htHoliday.Add(new HolidayList("0815", "광복절"));
            htHoliday.Add(new HolidayList("1003", "개천절"));
            htHoliday.Add(new HolidayList("1225", "성탄절"));
            
            koreaHoliday(_currentDate);
        }

        public  string GetHoliday(string dt)
        {            
            for(int i = 0; i< htHoliday.Count; i++)
            {
                if(htHoliday[i].HolyNum ==dt)
                {
                    return htHoliday[i].HoliName;
                }
            }
           

            return null;
        }


       public void koreaHoliday(DateTime currentDate)
        {
            int year = currentDate.Year;
           

            DateTime dt = new DateTime(year - 1, 12, 30);
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//설날
            if(dt.Month == currentDate.Month)
            {
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "설날"));
            }
            

            dt = new DateTime(year, 1, 1);
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//설날
            if (dt.Month == currentDate.Month)
            {
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "설날"));
            }
            dt = new DateTime(year, 1, 2);
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//설날
            if (dt.Month == currentDate.Month)
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "설날"));

            dt = new DateTime(year, 4, 8);//석가탄신일
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//석가탄신일
            if (dt.Month == currentDate.Month)
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "석가탄신일"));

            dt = new DateTime(year, 8, 14);//추석
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//추석
            if (dt.Month == currentDate.Month)
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "추석"));
            dt = new DateTime(year, 8, 15);//추석
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//추석
            if (dt.Month == currentDate.Month)
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "추석"));
            dt = new DateTime(year, 8, 16);//추석
            dt = convertKoreanMonth(dt.Year, dt.Month, dt.Day);//추석
            if (dt.Month == currentDate.Month)
                htHoliday.Add(new HolidayList(currentDate.Month.ToString("00") + string.Format("{00:00}", dt.Day), "추석"));

            
        }


        private DateTime convertKoreanMonth2(DateTime dt)//양력을 음력 변환
        {
            int n윤월;
            int n음력년, n음력월, n음력일;
            bool b윤달 = false;
            System.Globalization.KoreanLunisolarCalendar 음력 =
            new System.Globalization.KoreanLunisolarCalendar();


            n음력년 = 음력.GetYear(dt);
            n음력월 = 음력.GetMonth(dt);
            n음력일 = 음력.GetDayOfMonth(dt);
            if (음력.GetMonthsInYear(n음력년) > 12)             //1년이 12이상이면 윤달이 있음..
            {
                b윤달 = 음력.IsLeapMonth(n음력년, n음력월);     //윤월인지
                n윤월 = 음력.GetLeapMonth(n음력년);             //년도의 윤달이 몇월인지?
                if (n음력월 >= n윤월)                           //달이 윤월보다 같거나 크면 -1을 함 즉 윤8은->9 이기때문
                    n음력월--;
            }
            return new DateTime(int.Parse(n음력년.ToString()), int.Parse(n음력월.ToString()), int.Parse(n음력일.ToString()));
        }
        private DateTime convertKoreanMonth(int n음력년, int n음력월, int n음력일)//음력을 양력 변환
        {
            System.Globalization.KoreanLunisolarCalendar 음력 =
            new System.Globalization.KoreanLunisolarCalendar();

            bool b달 = 음력.IsLeapMonth(n음력년, n음력월);
            int n윤월;

            if (음력.GetMonthsInYear(n음력년) > 12)
            {
                n윤월 = 음력.GetLeapMonth(n음력년);
                if (b달)
                    n음력월++;
                if (n음력월 > n윤월)
                    n음력월++;
            }
            try
            {
                음력.ToDateTime(n음력년, n음력월, n음력일, 0, 0, 0, 0);
            }
            catch
            {
                return 음력.ToDateTime(n음력년, n음력월, 음력.GetDaysInMonth(n음력년, n음력월), 0, 0, 0, 0);//음력은 마지막 날짜가 매달 다르기 때문에 예외 뜨면 그날 맨 마지막 날로 지정
            }

            return 음력.ToDateTime(n음력년, n음력월, n음력일, 0, 0, 0, 0);
        }


    }
}
