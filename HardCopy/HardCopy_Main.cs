

namespace HardCopy
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Printing;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    public partial class HardCopy_Main : Form
    {
        public HardCopy_Main()
        {
            InitializeComponent();
        }

        private BulkUSB usb_conn;//USB 통신용 Class
        private C_CurrentTime _current_Time;//현재시간 Display
        private List<byte> ImageBuff = new List<byte>();//이미지 저장용


        private bool is_request_timeoutCheck = false;//요청요부용 변수
        private DateTime requestTime;//요청 타임아웃체크용 변수
        private int retry_count = 0;//요청 제한용 변수 

        
        private DateTime OK_Check_Time;// 데이터 수신 상태 확인


        private Thread tr_Loop; // 주기적인 체크용 쓰레드 500ms
        private bool is_run = true; //쓰레드 중지용 변수

        private int HORIZONTAL = 0, VERTICAL = 0; // 이미지 사이즈
        private int Current_Line = 0; //현재 요청 라인 
        private bool is_conn_value = false; //현재 연결 상태
        private DateTime _lastRecvTime = DateTime.Now;// 마지막 수신시간

        private void HardCopy_Main_Load(object sender, EventArgs e)
        {
            StatusMsg = "대기중";
            ProcessGauge(0, 100);
            usbstatus_img.Image = CompanyDll_1_1.LampBall.mini_Black_Ball;
            _current_Time = new C_CurrentTime(this, toolStripStatusLabel4);
            u_Margin1.value_changed += new EventHandler(u_Margin1_value_changed);

            usb_conn = new BulkUSB();
            usb_conn.connected += new EventHandler(usb_connected);
            usb_conn.recv_packet += new EventHandler(usbPacketReceived);

            tr_Loop = new Thread(new ThreadStart(T_Loop));
            tr_Loop.IsBackground = true;
            tr_Loop.Start();
        }

      
        private void T_Loop()
        {
            while (is_run)
            {
                //Todo : 01.데이터 수신 상태 체크
                if (is_request_timeoutCheck)
                {
                    if (retry_count < 2)
                    {
                        if ((DateTime.Now - requestTime).TotalSeconds > 3)
                        {
                            //Todo: 재요청
                            requestTime = DateTime.Now;
                            Request_Data();
                            retry_count++;
                        }
                    }
                    else
                    {
                        is_request_timeoutCheck = false;
                        // 이미지 받기 포기
                        Log_ADD("데이터수신 실패");
                    }

                }

                //Todo : 02.연결체크용 프로토콜 추가 할 것  
                /*   
                 * 마지막 수신 시간 기준으로
                 * 1분동안 데이터 수신이 없을 경우 전송 후 마지막 수신시간 변경
                 * 상태 프로토콜 전달
                 */

                if((DateTime.Now- _lastRecvTime).TotalSeconds>30)
                {
                    _lastRecvTime = DateTime.Now;
                    if(is_conn_value)// USB 접속상태 체크
                    {
                        Check_Data();
                    }
                }

                //Todo : 03.연상상태 확인 결과
                /*   
                 * 마지막 수신 시간 기준으로
                 * 2분동안 데이터 수신이 없을 경우 
                 * 에러상태로 표시 
                 */
                if ((DateTime.Now - OK_Check_Time).TotalSeconds > 120)
                {
                    OK_Check_Time = DateTime.Now.AddSeconds(-2);

                }

                Thread.Sleep(500);
            }
        }
        
        #region CheckSum

        private string CheckSum(string org)
        {
            Byte[] bt = Encoding.ASCII.GetBytes(org);
            Byte bi = 0;
            foreach (Byte b in bt)
            {
                bi += b;
            }
            string result = (string.Format("{0:x}", bi)).ToUpper();
            return result.Length == 1 ? "0" + result : result;
        }
        private byte[] CheckSum(byte[] org)
        {
            Byte bi = 0;
            foreach (Byte b in org)
            {
                switch (b)
                {
                    case 0x02:
                    case 0x03:
                        break;
                    default:
                        bi += b;
                        break;
                }
            }
            string result = (string.Format("{0:x}", bi)).ToUpper();
            result = result.Length == 1 ? "0" + result : result;

            return Encoding.ASCII.GetBytes(result);
        }
        private bool DeCheckSum(byte[] org)
        {
            Byte bi = 0;
            for (int i = 1; i < org.Length - 3; i++)
            {
                switch (org[i])
                {
                    case 0x02:
                    case 0x03:
                        break;
                    default:
                        bi += org[i];
                        break;
                }
            }
            string result = (string.Format("{0:x}", bi)).ToUpper();
            result = result.Length == 1 ? "0" + result : result;
            byte[] ck = Encoding.ASCII.GetBytes(result);

            return org[org.Length - 3] == ck[0] && org[org.Length - 2] == ck[1];
        }
        #endregion

        #region StatusBar
        /// <summary>
        /// Sutaus Bar에서 ProgressBar의 현재 상태를 표시하는 부분
        /// </summary>
        private string StatusMsg
        {
            set
            {
                Action act = new Action(delegate ()
                {
                    if (process_status.IsDisposed == false)
                    {
                        process_status.Text = value;
                    }

                });
                try
                {
                    if (this.IsDisposed == false)
                    {
                        this.Invoke(act);
                    }
                }
                catch (System.ObjectDisposedException)
                {
                }
                act = null;
            }
        }
        /// <summary>
        /// Sutaus Bar에서 ProgressBar값을 설정하는 부분
        /// </summary>
        private void ProcessGauge(int sub, int max)
        {
            if(sub>max)
            {
                // 현재 값이 max값보다 크면 오류생김
                return;
            }
            Action act = new Action(delegate ()
            {
                if (process_gague.IsDisposed == false)
                {
                    if (process_gague.Maximum < sub)
                    {
                        process_gague.Maximum = max;
                        process_gague.Value = sub;
                    }
                    else
                    {
                        process_gague.Value = sub;
                        process_gague.Maximum = max;
                    }

                }

            });
            try
            {

                if (this.IsDisposed == false)
                {
                    this.Invoke(act);
                }
            }
            catch (System.ObjectDisposedException)
            {
            }

            act = null;
        }
        #endregion
        #region CustomEvent
        private void u_Margin1_value_changed(object sender, EventArgs e)
        {//Todo: 여백 변경시 발생
            u_HV1.Margin_TOP = ((MyMargin)sender).top;
            u_HV1.Margin_BOTTOM = ((MyMargin)sender).bottom;
            u_HV1.Margin_Left = ((MyMargin)sender).left;
            u_HV1.Margin_Right = ((MyMargin)sender).right;
        }
        private void usb_connected(object sender, EventArgs e)
        { //Todo: 연결상태 표시
            Action act = new Action(delegate ()
            {
                if (usbstatus_img.IsDisposed == false)
                {
                    usbstatus_img.Image = ((bool)sender) ? CompanyDll_1_1.LampBall.mini_Green_Ball : CompanyDll_1_1.LampBall.mini_Black_Ball;
                }

            });
            try
            {

                if (this.IsDisposed == false)
                {
                    this.Invoke(act);
                }
            }
            catch (System.ObjectDisposedException)
            {

            }
            act = null;
            Log_ADD(((bool)sender) ? "연결" : "끊김");

            is_conn_value = ((bool)sender);

            if (((bool)sender) == false)
            {
                is_request_timeoutCheck = false;
            }
        }

        private void usbPacketReceived(object sender, EventArgs e)
        {
            //패킷단위로 전달됨 <STX>DATA<ETX>

            //Todo : 프로토콜 타입 분석
            /*
             * 1.화면캡쳐 응답
             *   STX H 9999 V 999 CK ETX
             * 2.데이터요청 응답
             *   STX L 999 D DATA CK ETX 
             *   DATA의 길이는 H의 길이가 나와야 한다. 
             */

            //Todo: 데이터 프로토콜의 경우 
            /*
             * 1. 데이터 수신시 Line 이 최대 라인을 넘지 않는지 확인
             * 2. Line+1 요청
             * 3. 마지막 라인 완성 후 인쇄
             */

            //Todo: 일정시간 이후 데이터 미수신시
            /* 
             * 1. USB 끊김 확인
             * 2. LastLine+1 요청
             * 3. 3회 시도 후 응답 없으면 Fail 
             * 4. 인쇄 안함 
             */

            byte[] buff = (byte[])sender;
            _lastRecvTime = DateTime.Now;
            //Todo : CheckSum 계산
            if (DeCheckSum(buff) == false)
            {
                //체크섬 에러
                Log_ADD("<체크섬에러> " + Encoding.ASCII.GetString(buff));
                return;
            }
            OK_Check_Time = DateTime.Now;
            switch (buff[1])
            {
                case ((byte)'H')://STX H 9999 V 999 CK ETX
                    // 여백 셋팅 
                    if (buff.Length == 13)
                    {
                        ProcessGauge(0, 100);
                        Log_ADD(Encoding.ASCII.GetString(buff));
                        Log_ADD("세팅완료");
                        HORIZONTAL = int.Parse(Encoding.ASCII.GetString(buff, 2, 4)) + 1;//H
                        VERTICAL = int.Parse(Encoding.ASCII.GetString(buff, 7, 3)) + 1;//V
                    }
                    break;
                case ((byte)'O')://STX OK CK ETX
                    if(buff[2]=='K')
                    {
                        //체크완료
                        Log_ADD("체크");
                    }
                    break;
                case ((byte)'L')://STX L 999 D DATA CK ETX 

                    int _line = int.Parse(Encoding.ASCII.GetString(buff, 2, 3));
                    Log_ADD(string.Format("{0}라인 수신", _line));
                    if (_line == Current_Line)
                    {
                        byte[] tempbuff = new byte[buff.Length - 9];
                        if ((buff.Length - 9) > 0)
                        {
                            Array.Copy(buff, 6, tempbuff, 0, buff.Length - 9);
                            ImageBuff.AddRange(tempbuff);
                            if (_line == VERTICAL - 1)
                            {
                                is_request_timeoutCheck = false;

                                StatusMsg = "대기중";
                                ProcessGauge(VERTICAL, VERTICAL);
                                if(HORIZONTAL*VERTICAL==ImageBuff.Count)
                                {
                                    Bitmap bitmap = C_ImageSet.ImageCreate(HORIZONTAL, VERTICAL, ImageBuff.ToArray());
                                    Action act = new Action(delegate ()
                                    {
                                        pb_printImg.Image = bitmap;
                                    });
                                    try
                                    {
                                        if (this.IsDisposed == false)
                                        {
                                            this.Invoke(act);
                                        }
                                    }
                                    catch (System.ObjectDisposedException)
                                    {
                                    }
                                    act = null;
                                }
                                
                            }
                            else
                            {
                                Current_Line = _line;
                                //다음 라인 요청
                                Current_Line++;
                                requestTime = DateTime.Now;
                                retry_count = 0;
                                Request_Data();
                            }
                        }

                    }
                    else
                    {
                        //현재 라인 재요청
                        requestTime = DateTime.Now;
                        retry_count = 0;
                        Request_Data();
                    }
                    break;

            }
            buff = null;
            sender = null;
        }
        #endregion
        #region Protocal_Packet

        private void Request_Setting()
        {
            StatusMsg = "캡쳐요청";
            StringBuilder stb = new StringBuilder();
            stb.Append("S");
            stb.AppendFormat("T{0}", u_Margin1.Margin_TOP.ToString("00"));
            stb.AppendFormat("B{0}", u_Margin1.Margin_BOTTOM.ToString("00"));
            stb.AppendFormat("L{0}", u_Margin1.Margin_LEFT.ToString("00"));
            stb.AppendFormat("R{0}", u_Margin1.Margin_RIGHT.ToString("00"));
            stb.AppendFormat("C{0}", u_ClockCount1.CLOCK.ToString("000000000"));
            stb.AppendFormat(CheckSum(stb.ToString()));
            stb.Insert(0, (char)0x02);
            stb.Append((char)0x03);
            Log_ADD("<요청>" + stb.ToString());
            usb_conn.USB_Data_Send(stb.ToString());
            stb.Remove(0, stb.Length);
            stb = null;
        }
        private void Request_Setting_Init()
        {
            StatusMsg = "캡쳐요청";
            StringBuilder stb = new StringBuilder();
            stb.Append("ST00B00L00R00");
            stb.AppendFormat("C{0}", u_ClockCount1.CLOCK.ToString("000000000"));
            stb.AppendFormat(CheckSum(stb.ToString()));
            stb.Insert(0, (char)0x02);
            stb.Append((char)0x03);
            Log_ADD("<요청>" + stb.ToString());
            usb_conn.USB_Data_Send(stb.ToString());
            stb.Remove(0, stb.Length);
            stb = null;
        }
        private void Request_Data()
        {
            StatusMsg = string.Format("데이터요청 ({0}/{1})", Current_Line+1, VERTICAL);
            Log_ADD(string.Format("데이터요청 ({0}/{1})", Current_Line+1, VERTICAL));
            ProcessGauge(Current_Line+1, VERTICAL);
            StringBuilder stb = new StringBuilder();
            stb.Append("L");
            stb.AppendFormat("{0}", Current_Line.ToString("000"));
            stb.AppendFormat(CheckSum(stb.ToString()));
            stb.Insert(0, (char)0x02);
            stb.Append((char)0x03);
            usb_conn.USB_Data_Send(stb.ToString());
            stb.Remove(0, stb.Length);
            stb = null;
        }
        private void Check_Data()
        {
            Log_ADD("Check Data");
            StringBuilder stb = new StringBuilder();
            stb.Append("OK");
            stb.AppendFormat(CheckSum(stb.ToString()));
            stb.Insert(0, (char)0x02);
            stb.Append((char)0x03);
            usb_conn.USB_Data_Send(stb.ToString());
            stb.Remove(0, stb.Length);
            stb = null;
        }
        #endregion
        public void Log_ADD(string msg)
        {
            Action act = new Action(delegate ()
            {
                if (list_log.IsDisposed == false)
                {
                    if (list_log.Items.Count > 200)
                    {
                        list_log.Items.RemoveAt(list_log.Items.Count - 1);
                    }
                    list_log.Items.Insert(0, string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
            });
            try
            {
                if (this.IsDisposed == false)
                {
                    this.Invoke(act);
                }
            }
            catch (System.ObjectDisposedException)
            {
            }

            act = null;
        }

        private void bt_Crop_Click(object sender, EventArgs e)
        {
            List<byte> Buff = new List<byte>();
            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    Buff.Add(0x01);
                }
            }
            this.pb_printImg.Image = C_ImageSet.ImageCreate(640, 480, 20, 100, 10, 50, Buff.ToArray());
            this.pb_printImg.Size = new Size(pb_printImg.Image.Width, pb_printImg.Image.Height);
        }

        private void bt_Create_Image_Click(object sender, EventArgs e)
        {
            List<byte> Buff = new List<byte>();
            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    Buff.Add(0x01);
                }
            }
            this.pb_printImg.Image = C_ImageSet.ImageCreate(640, 480, Buff.ToArray());
            this.pb_printImg.Size = new Size(pb_printImg.Image.Width, pb_printImg.Image.Height);
        }

        private void bt_Capture_Click(object sender, EventArgs e)
        {
            //요청
            Current_Line = 0;
            retry_count = 0;
            is_request_timeoutCheck = true;
            requestTime = DateTime.Now;
            Request_Data();
        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            // 여백 설정
            pd.DefaultPageSettings.Margins.Left = 10;
            pd.DefaultPageSettings.Margins.Right = 10;
            pd.DefaultPageSettings.Margins.Top = 10;
            pd.DefaultPageSettings.Margins.Bottom = 10;

            //용지방향
            pd.DefaultPageSettings.Landscape = true;//true:가로, false:세로

            //용지크기
            pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 210 * 4, 297 * 4);
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            //pd.Print();
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(pb_printImg.Image, new Point(0, 0));
        }

        private void bt_Close_Click(object sender, EventArgs e)
        {
            usb_conn.USB_Close();
        }

        private void bt_Set_Click(object sender, EventArgs e)
        {
            Request_Setting();
        }

        private void HardCopy_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            is_run = false;
            _current_Time.Close();
            usb_conn.Form_Closeing_USB();
        }
    }

}
