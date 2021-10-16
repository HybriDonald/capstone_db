using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static string dbId = "C##capstone_admin";
        private static string dbPw = "yuhanunivcapstone1212";
        private OracleConnection conn;
        private OracleCommand command;
        private OracleDataAdapter adapter;


        public Database()
        {
            string strConn = string.Format($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
            conn = new OracleConnection(strConn);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            Console.WriteLine(isOpen());
        }

        ~Database()
        {
            conn.Close();
        }

        public bool isOpen()
        {
            return (!Equals(conn.State, ConnectionState.Closed));
            
        }

        public DataSet select(string col, string table)
        {
            DataSet ds = new DataSet();
            string query = $"select {col} from {table}";

            if (!isOpen())
                return ds;
            adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(ds);
            return ds;
        }

        public DataSet select(string col, string table, string where)
        {
            DataSet ds = new DataSet();
            string query = $"select {col} from {table} where {where};";

            if (!isOpen())
                return ds;
            adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(ds);
            adapter.Dispose();
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

            if (!isOpen())
                return;
            command.Connection = conn;
            command.CommandText = query;
            row = command.ExecuteNonQuery();

            Console.WriteLine($"총 {row}개 삽입됨");
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

            if (!isOpen())
                return;
            command.Connection = conn;
            command.CommandText = query;
            Console.WriteLine(query);
            row = command.ExecuteNonQuery();

            Console.WriteLine($"총 {row}개 수정됨");
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

            if (!isOpen())
                return;
            command.Connection = conn;
            command.CommandText = query;
            row = command.ExecuteNonQuery();

            Console.WriteLine($"총 {row}개 삭제됨");
            command.Dispose();
        }
    }
}
