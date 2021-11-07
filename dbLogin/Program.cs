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
            int _Login, _Login2, _Login3;

            _Login = db.LoginReturn("tjdals0231", "alfl02@!", 0);
            _Login2 = db.LoginReturn("tjdals0231", "alfl0231", 1);
            _Login3 = db.LoginReturn("tjdals0231", "alfl02@!", 1);

            if(_Login == 0)
            {
                System.Console.WriteLine("로그인 성공");
            }
            else if (_Login == 1)
            {
                System.Console.WriteLine("로그인 실패 (비밀번호 불일치)");
            }
            else if (_Login == 2)
            {
                System.Console.WriteLine("로그인 실패 (아이디가 존재하지 않음)");
            }

            if(_Login2 == 0)
            {
                System.Console.WriteLine("로그인 성공");
            }
            else if (_Login2 == 1)
            {
                System.Console.WriteLine("로그인 실패 (비밀번호 불일치)");
            }
            else if (_Login2 == 2)
            {
                System.Console.WriteLine("로그인 실패 (아이디가 존재하지 않음)");
            }

            if(_Login3 == 0)
            {
                System.Console.WriteLine("로그인 성공");
            }
            else if (_Login3 == 1)
            {
                System.Console.WriteLine("로그인 실패 (비밀번호 불일치)");
            }
            else if (_Login3 == 2)
            {
                System.Console.WriteLine("로그인 실패 (아이디가 존재하지 않음)");
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
