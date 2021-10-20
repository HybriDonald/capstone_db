using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace dbLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new();
            Student student;

            var lecture = db.getLecture("TEST1255");

            //lecture.Print();

            //db.insert("stu", "'test0', 'test0', 'badman', '201507000'");
            //db.update("stu", "id = 'test5'", "stu_no = '201507000'");
            //db.delete("stu", "id = 'test5'");
        }
    }
}
