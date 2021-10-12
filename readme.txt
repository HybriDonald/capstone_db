
        현재 공용 DB가 없는 관계로 개인 PC에 Oracle 설치 후 공용 DB처럼 사용 중 입니다.
        테스트 중에는 아래 DB로 접속 후 테스트 해주세요.
        작성 되어있는 dbIp 값을 내부 IP가 아닌 외부 IP로 변경 해주세요.
        외부 IP : 222.239.64.185
        내부 IP : 192.168.55.85
        
        테이블 목록 
        학생 :    stu 
                  { 
                    id varchar2(15) pk
                    pw varchar2(20)
                    name varchar2(10)
                    stu_no varchar2(15)
                  }
        교수 : prf ( 전체적인 테이블은 위와 동일합니다. stu_no가 아닌 prf_no 차이점이 있습니다.)
        강좌 : lec
                {
                    lec_code varchar2(10) pk
                    prf_id varchar2(15) fk
                    lec_name varchar2(30)
                    credit number                   // 학점
                    day varchar2(10)                // 요일
                    s_time date                     // 시작시간
                    e_time date                     // 종료시간
                }
        (작업중)출석 테이블 :   check_table, 
                                test_attendance_02, 
                                test_attendance_02_firstclass, 
                                test_attendance_02_secondclass,
                                test_attendance_02_thirdclass
        
        출석 테이블은 미구현 상태이니 양해 부탁드립니다.
        모르시는 부분이나 이해 안가시는 부분 있으시면 개인 카카오톡 "홍성민"에게 보내주시면 답변 해드리겠습니다.
