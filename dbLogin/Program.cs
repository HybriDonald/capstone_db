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

            foreach (DataRow r in db.select("name", "student").Tables[0].Rows)
            {
                Console.WriteLine(r["name"]);
            }
            //db.insert("stu", "'test0', 'test0', 'badman', '201507000'");
            //db.update("stu", "id = 'test5'", "stu_no = '201507000'");
            //db.delete("stu", "id = 'test5'");
        }
    }
}
