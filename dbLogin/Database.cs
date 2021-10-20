using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections;

namespace dbLogin
{
    class Database : IDatabase
    {
        /*
            readme.txt 를 꼭 읽어주세요!
        */
        private const int STUDENT = 0;
        private const int PROFESSOR = 1;
        private const int SCHEDULE = 2;

        private static string dbIp = "192.168.55.85";
        private static string dbName = "deskDB";
        private static string dbId = "C##capstone_admin";
        private static string dbPw = "yuhanunivcapstone1212";
        private OracleConnection conn;
        private OracleCommand command;
        private OracleDataAdapter adapter;
        private DataSet data;


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
        }

        ~Database()
        {
            conn.Close();
        }

        public void Execute(string query)
        {
            int row;

            if (IsOpen())
            {
                using (command = new OracleCommand(query, conn))
                {
                    row = command.ExecuteNonQuery();
                }
                Console.WriteLine($"{row}");
            }
            else
            {
                Console.WriteLine("ERROR : 데이터 베이스 연결에 실패했습니다.");
            }
        }

        public List<IInformation> ExecuteList(int flag, string student_Id = "")
        {
            List<IInformation> result = new List<IInformation>();
            string id, pw, name, studentId, professorId, lecture_code, lecture_name;

            switch (flag)
            {
                case STUDENT:
                    Student student;

                    using (data = Select("*", "student"))
                    {
                        foreach (DataRow r in data.Tables[0].Rows)
                        {
                            id = r["id"].ToString();
                            pw = r["pw"].ToString();
                            name = r["name"].ToString();
                            studentId = r["student_id"].ToString();
                            student = new Student(id, pw, studentId, name);
                            result.Add(student);
                        }
                    }
                    break;
                case PROFESSOR:
                    Professor professor;

                    using (data = Select("*", "Professor"))
                    {
                        foreach (DataRow r in data.Tables[0].Rows)
                        {
                            id = r["id"].ToString();
                            pw = r["pw"].ToString();
                            name = r["name"].ToString();
                            professorId = r["Professor_id"].ToString();
                            professor = new Professor(id, pw, professorId, name);
                            result.Add(professor);
                        }
                    }
                    break;
                case SCHEDULE:
                    Schedule schedule;

                    using (data = Select("Lecture_Code, Lecture_Name", "student_lecture", $"student_id = {student_Id}"))
                    {
                        foreach (DataRow r in data.Tables[0].Rows)
                        {
                            lecture_code = r["lecture_code"].ToString();
                            lecture_name = r["lecture_name"].ToString();
                            schedule = new Schedule(lecture_code, lecture_name);
                            result.Add(schedule);
                        }
                    }
                    break;
            }
            return result;
        }

        public bool IsOpen()
        {
            return (!Equals(conn.State, ConnectionState.Closed));
            
        }

        /// <summary>
        /// 데이터를 조회하는 구문입니다. 
        /// 컬럼명, 테이블명
        /// </summary>
        /// <param name="col"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataSet Select(string col, string table)
        {
            string query = $"select {col} from {table}";
            DataSet ds;

            using (ds = new DataSet())
            {
                if (IsOpen())
                {
                    using (adapter = new OracleDataAdapter(query, conn))
                    {
                        adapter.Fill(ds);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR : 데이터 베이스 연결이 실패했습니다.");
                }
                return ds;
            }
        }

        /// <summary>
        /// 데이터를 조회하는 함수입니다. 
        /// 컬럼명, 테이블명, 조건
        /// </summary>
        /// <param name="col"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet Select(string col, string table, string where)
        {
            DataSet ds = new DataSet();
            string query = $"select {col} from {table} where {where}";

            if (IsOpen())
            {
                using (adapter = new OracleDataAdapter(query, conn))
                {
                    adapter.Fill(ds);
                }
            }
            else
            {
                Console.WriteLine("ERROR : 데이터 베이스 연결에 실패했습니다.");
            }
            return ds;
        }

        /// <summary>
        /// 데이터를 삽입하는 함수입니다. 테이블명, 값 <br/>
        /// 값은 괄호로 묶이며 Comma(,)로 구분합니다. 문자열의 경우 Quotation(' ')로 감싸입니다.<br/>
        /// 예 ) table = "student", values = "('Yuhan', 1234)"
        /// </summary>
        /// <param name="table"></param>
        /// <param name="values"></param>
        public void Insert(string table, string values)
        {
            string query = @$"
                insert into {table} 
                values({values})
            ";

            Execute(query);
        }

        /// <summary>
        /// 데이터를 수정하는 함수입니다. 테이블명, 값, 조건 <br/>
        /// 조건은 " id = 'Yuhan' " 형식 입니다.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="value"></param>
        /// <param name="where"></param>
        public void Update(string table, string value, string where)
        {
            string query = @$"
                update {table} 
                set {value}
                where {where}
            ";

            Execute(query);
        }

        /// <summary>
        /// 데이터를 삭제하는 함수입니다. 테이블명, 조건
        /// 조건은 " id = 'Yuhan' " 형식 입니다.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        public void Delete(string table, string where)
        {
            string query = @$"
                delete from {table}
                where {where}
            ";

            Execute(query);
        }

        /// <summary>
        /// 학생들의 모든 정보를 가져오는 함수입니다.
        /// </summary>
        /// <returns></returns>
        public List<IInformation> GetStudentList()
        {
            return ExecuteList(STUDENT);
        }

        /// <summary>
        /// 교수의 모든 정보를 가져오는 함수입니다.
        /// </summary>
        /// <returns></returns>
        public List<IInformation> GetProfessorList()
        {
            return ExecuteList(PROFESSOR);
        }

        /// <summary>
        /// 학생의 시간표의 정보를 가져옵니다.
        /// </summary>
        /// <param name="student_Id"></param>
        /// <returns></returns>
        public List<IInformation> GetScheduleList(string student_Id)
        {
            return ExecuteList(SCHEDULE, student_Id);
        }

        public Lecture getLecture(string Lecture_code)
        {
            Lecture lecture = null;
            string code, pro_id, name, week_day, start_time, end_time;
            int credit;

            using (data = Select("*", "lecture", $"lecture_code = '{Lecture_code}'"))
            {
                DataRow[] row = data.Tables[0].Select();

                row[0][0];


                //lecture = new Lecture(code, pro_id, name, credit, week_day, start_time, end_time);
            }
            return lecture;
        }
    }
}
