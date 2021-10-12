using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace dbLogin
{
    class Database
    {
        /*
        현재 공용 DB가 없는 관계로 개인 PC에 Oracle 설치 후 공용 DB처럼 사용 중 입니다.
        테스트 중에는 아래 DB로 접속 후 테스트 해주세요.
        작성 되어있는 dbIp 값을 내부 IP가 아닌 외부 IP로 변경 해주세요.
        외부 IP : 222.239.64.185
        내부 IP : 192.168.55.85
        
        테이블 목록 
        학생 : stu
        교수 : prf
        강좌 : lec
        (작업중)출석 테이블 :   check_table, 
                                test_attendance_02, 
                                test_attendance_02_firstclass, 
                                test_attendance_02_secondclass,
                                test_attendance_02_thirdclass
        
        출석 테이블은 미구현 상태이니 양해 부탁드립니다.
        모르시는 부분이나 이해 안가시는 부분 있으시면 개인 카카오톡 "홍성민"에게 보내주시면 답변 해드리겠습니다.
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
