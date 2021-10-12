using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace dbLogin
{
    class Database
    {
        /*
         readme.txt 를 꼭 읽어주세요!
         */
        private static string dbIp = "192.168.55.85";
        private static string dbName = "deskDB";
        private static string dbId = "C##capstone";
        private static string dbPw = "capstone1234";
        private OracleConnection conn;
        private OracleCommand command;
        private OracleDataAdapter adapter;


        public Database()
        {
            string strConn = string.Format($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
            conn = new OracleConnection(strConn);
        }

        public bool isOpen()
        {
            if (conn == null)
                return false;
            return true;
        }

        public DataSet select(string col, string table)
        {
            DataSet ds = new DataSet();
            string query = $"select {col} from {table}";

            conn.Open();
            adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(ds);
            conn.Close();
            return ds;
        }

        public DataSet select(string col, string table, string where)
        {
            DataSet ds = new DataSet();
            string query = $"select {col} from {table} where {where};";

            conn.Open();
            adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(ds);
            conn.Close();
            return ds;
        }

        public void insert(string table, string values)
        {
            string query = $"insert into {table} values({values});";

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();

            foreach (DataRow r in db.select("id, pw, stu_no", "stu").Tables[0].Rows)
            {
                Console.WriteLine(r["id"]);
                Console.WriteLine(r["pw"]);
                Console.WriteLine(r["stu_no"]);
            }
        }
    }
}
