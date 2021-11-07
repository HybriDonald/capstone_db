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
            Database db = new();

            foreach(var temp in db.GetStudentsExistLecture("TEST1255"))
            {
                temp.Print();
            }

           // var information = db.GetScheduleAboutTime("1200");

           // Lecture lec = db.GetLecture(information[0].LectureCode);

          //  CryptoConfig config = new CryptoConfig();

           // information[0].Print();
           // lec.Print();

            //db.insert("stu", "'test0', 'test0', 'badman', '201507000'");
            //db.update("stu", "id = 'test5'", "stu_no = '201507000'");
            //db.delete("stu", "id = 'test5'");
        }
    }
}
