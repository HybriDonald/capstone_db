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
            adapter.Dispose();
            conn.Close();
            return ds;
        }

        public void insert(string table, string values)
        {
            string query = @$"
                insert into {table} 
                values({values})
            ";
            int row;
            command = new OracleCommand();

            conn.Open();
            command.Connection = conn;
            command.CommandText = query;
            row = command.ExecuteNonQuery();

            Console.WriteLine($"총 {row}개 삽입됨");
            conn.Close();
            command.Dispose();
        }

        public void update(string table, string value, string where)
        {
            string query = @$"
                update {table} 
                set {value}
                where {where}
            ";
            int row;
            command = new OracleCommand();

            conn.Open();
            command.Connection = conn;
            command.CommandText = query;
            Console.WriteLine(query);
            row = command.ExecuteNonQuery();

            Console.WriteLine($"총 {row}개 수정됨");
            conn.Close();
            command.Dispose();
        }

        public void delete(string table, string where)
        {
            string query = @$"
                delete from {table}
                where {where}
            ";
            int row;
            command = new OracleCommand();

            conn.Open();
            command.Connection = conn;
            command.CommandText = query;
            row = command.ExecuteNonQuery();

            Console.WriteLine($"총 {row}개 삭제됨");
            conn.Close();
            command.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();

            foreach (DataRow r in db.select("name", "stu").Tables[0].Rows)
            {
                Console.WriteLine(r["name"]);
            }
            //db.insert("stu", "'test0', 'test0', 'badman', '201507000'");
            //db.update("stu", "id = 'test5'", "stu_no = '201507000'");
            //db.delete("stu", "id = 'test5'");
        }
    }
}
