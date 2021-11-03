using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace dbLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database db = new();

           // var information = db.GetScheduleAboutTime("1200");

           // Lecture lec = db.GetLecture(information[0].LectureCode);



            Program Pg = new Program();
            System.Console.WriteLine(Pg.getDay(DateTime.Now));

          //  CryptoConfig config = new CryptoConfig();

           // information[0].Print();
           // lec.Print();

            //db.insert("stu", "'test0', 'test0', 'badman', '201507000'");
            //db.update("stu", "id = 'test5'", "stu_no = '201507000'");
            //db.delete("stu", "id = 'test5'");
        }

        public string getDay(DateTime now) 
        {
            string day;

            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    day = "월";
                    break;
                case DayOfWeek.Tuesday:
                    day = "화";
                    break;
                case DayOfWeek.Wednesday:
                    day = "수";
                    break;
                case DayOfWeek.Thursday:
                    day = "목";
                    break;
                case DayOfWeek.Friday:
                    day = "금";
                    break;
                case DayOfWeek.Saturday:
                    day = "토";
                    break;
                case DayOfWeek.Sunday:
                    day = "일";
                    break;
                default:
                    day = "일";
                    break;
            }

            return day;
        }
    }
}
