using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace BrightnessLibrary
{
    public class Communication
    {

        private SerialPort comport = new SerialPort();

        public string _Model = "";
        private string PortName = "";
        private const int STX = 2;
        private const int ACK = 0x06;
        private const int NAK = 0x15;

        private const int UART_GET_MODEL = 10;
        private const int UART_SET_BRIGHTNESS = 31;

        private const int UART_TIMEOUT = 20;            // Basetime is 10ms
        private const int UART_TRY_COUNT = 3;           // 通訊失敗連續次數

        string[] aryPortName = new string[128];
        string[] ports = SerialPort.GetPortNames();
        private int Length;
        private byte C_BrightnessCH1 = 0;
        private byte C_BrightnessCH2 = 0;
        private byte C_BrightnessCH3 = 0;
        private byte C_BrightnessCH4 = 0;
        private byte C_BrightnessCH5 = 0;
        private byte C_BrightnessCH6 = 0;
        private byte C_BrightnessCH7 = 0;
        private byte C_BrightnessCH8 = 0;
        private bool C_confirmation = false;

        public void OpenComPort(string COM)
        {
            bool Status = true;
            aryPortName = ports;
            Length = aryPortName.Length;

            comport.BaudRate = 115200;
            comport.DataBits = 8;
            comport.Parity = Parity.None;
            comport.StopBits = StopBits.One;
            comport.ReadTimeout = 500;


            for (int i = 0; i < Length + 1; i++)
            {
            //comport.PortName = aryPortName[i];
            comport.PortName = COM;
                comport.Open();
                if (GetModel() != Status)
                {
                    comport.Close();
                    if (i == Length + 1)
                    {
                        break;
                    }
                }
                else
                {
                    PortName = comport.PortName;
                    break;
                }
            }
        }

        public bool GetModel()
        {
            bool Status = true;
            int Count = 0;

            comport.BaudRate = 115200;
            comport.DataBits = 8;
            comport.Parity = Parity.None;
            comport.StopBits = StopBits.One;

            try
            {
                Count = 0;
                do
                {
                    Status = UartGetModel();
                    if (!Status)
                    {
                        Count++;
                    }
                } while (!Status && Count < (int)UART_TRY_COUNT);     // 連續三次設定失敗才算失敗

                if (Status)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            catch
            {
                return (false);
            }

        }
        public string PortCode()
        {
            return PortName;
        }

        public void ClosePort()
        {
            try
            {
                comport.Close();
            }
            catch
            {
            }
        }
        public bool Confirmation()
        {
            return C_confirmation;
        }

        public void CH1_SetBrightness(byte BrightnessCH1)
        {
            C_BrightnessCH1 = BrightnessCH1;
            SetBrightness();
        }
        public void CH2_SetBrightness(byte BrightnessCH2)
        {
            C_BrightnessCH2 = BrightnessCH2;
            SetBrightness();
        }
        public void CH3_SetBrightness(byte BrightnessCH3)
        {
            C_BrightnessCH3 = BrightnessCH3;
            SetBrightness();
        }
        public void CH4_SetBrightness(byte BrightnessCH4)
        {
            C_BrightnessCH4 = BrightnessCH4;
            SetBrightness();
        }
        public void CH5_SetBrightness(byte BrightnessCH5)
        {
            C_BrightnessCH5 = BrightnessCH5;
            SetBrightness();
        }
        public void CH6_SetBrightness(byte BrightnessCH6)
        {
            C_BrightnessCH6 = BrightnessCH6;
            SetBrightness();
        }
        public void CH7_SetBrightness(byte BrightnessCH7)
        {
            C_BrightnessCH7 = BrightnessCH7;
            SetBrightness();
        }
        public void CH8_SetBrightness(byte BrightnessCH8)
        {
            C_BrightnessCH8 = BrightnessCH8;
            SetBrightness();
        }

        public void SetUnit1CH(byte BrightnessCH1)
        {
            C_BrightnessCH1 = BrightnessCH1;
            SetBrightness();
        }

        public void SetUnit2CH(byte BrightnessCH1, byte BrightnessCH2)
        {
            C_BrightnessCH1 = BrightnessCH1;
            C_BrightnessCH2 = BrightnessCH2;
            SetBrightness();
        }
        public void SetUnit4CH(byte BrightnessCH1, byte BrightnessCH2, byte BrightnessCH3, byte BrightnessCH4)
        {
            C_BrightnessCH1 = BrightnessCH1;
            C_BrightnessCH2 = BrightnessCH2;
            C_BrightnessCH3 = BrightnessCH3;
            C_BrightnessCH4 = BrightnessCH4;
            SetBrightness();
        }
        public void SetUnit8CH(byte BrightnessCH1, byte BrightnessCH2, byte BrightnessCH3, byte BrightnessCH4, byte BrightnessCH5, byte BrightnessCH6, byte BrightnessCH7, byte BrightnessCH8)
        {
            C_BrightnessCH1 = BrightnessCH1;
            C_BrightnessCH2 = BrightnessCH2;
            C_BrightnessCH3 = BrightnessCH3;
            C_BrightnessCH4 = BrightnessCH4;
            C_BrightnessCH5 = BrightnessCH5;
            C_BrightnessCH6 = BrightnessCH6;
            C_BrightnessCH7 = BrightnessCH7;
            C_BrightnessCH8 = BrightnessCH8;
            SetBrightness();
        }


        private byte GetTotalChannel()
        {
            string TotalChannel = "";
            switch (_Model)
            {
                case "LE060WA0":
                case "LSP120B-1":
                case "LSP240B-1":
                    TotalChannel = "1";
                    break;
                case "PDX2S0":
                    TotalChannel = "1";
                    break;
                case "PDX2":
                    TotalChannel = "1";
                    break;
                case "PDX4S0":
                    TotalChannel = "1";
                    break;
                case "PDX4":
                    TotalChannel = "1";
                    break;
                case "PDX6S0":
                    TotalChannel = "1";
                    break;
                case "PDX6":
                    TotalChannel = "1";
                    break;
                case "PDX8S0":
                    TotalChannel = "1";
                    break;
                case "PDX8":
                    TotalChannel = "1";
                    break;
                case "SPD2":
                case "LSG045A-2":
                case "LSG075A-2":
                case "LSG150B-2":
                    TotalChannel = "2";
                    break;
                case "SPD3":
                    TotalChannel = "3";
                    break;
                case "SPD4":
                case "LSG045A-4":
                case "LSG075A-4":
                case "LSG120A-4":
                    TotalChannel = "4";
                    break;
                case "SPD5":
                    TotalChannel = "5";
                    break;
                case "SPD6":
                    TotalChannel = "6";
                    break;
                case "SPD7":
                    TotalChannel = "7";
                    break;
                case "SPD8":
                case "LSG075A-8":
                    TotalChannel = "8";
                    break;
                default:
                    TotalChannel = _Model.Substring(2, 1);    // 從第2個字元開始, 擷取1個字元, 內容為通道數
                    break;
            }
            return (Convert.ToByte(TotalChannel));
        }

        private void DiscardBuffer()
        {
            comport.DiscardInBuffer(); // 清除 serial port 接收緩衝區
            comport.DiscardOutBuffer(); // 清除 serial port 傳送緩衝區
        }

        private bool UartGetModel()
        {
            try
            {
                DiscardBuffer();

                int Checksum = 0;
                int Pos = 0;

                // 宣告傳送與接收緩衝區大小
                byte[] SendBuf = new byte[128];
                byte[] RecBuf = new byte[128];

                // 實際所接收到的封包位元組數大小
                int m_ReadLen = 0;
                int Length = 0;

                comport.ReadTimeout = 500;  // 500ms

                // Start
                SendBuf[0] = STX;

                // Length
                SendBuf[1] = 1 % 256;    // low byte
                SendBuf[2] = 1 / 256;    // high byte
                Pos = 3;

                // comm
                SendBuf[Pos] = UART_GET_MODEL;
                Checksum += SendBuf[Pos];
                Pos++;

                SendBuf[Pos] = (byte)(Checksum & 0x00ff);
                Pos++;
                SendBuf[Pos] = (byte)((Checksum & 0xff00) >> 8);
                Pos++;

                comport.Write(SendBuf, 0, Pos);

                Array.Clear(RecBuf, 0, RecBuf.Length);

                m_ReadLen = 0;
                Length = 12 + 5;    // 採用最長的MODEL長度, Ex. GD4075B0-12
                int Count = 0;
                do
                {
                    try
                    {
                        m_ReadLen = comport.BytesToRead;
                        Count++;
                        Thread.Sleep(10);
                    }
                    catch (TimeoutException tx)
                    {
                        //CommFinish = false;
                        //Debug.WriteLine(tx.Message);
                        return (false);
                    }
                } while (m_ReadLen < Length && Count < UART_TIMEOUT);

                if (m_ReadLen != 0)
                {
                    comport.Read(RecBuf, 0, m_ReadLen);
                }
                else
                {
                    // 失敗
                    return (false);
                }

                Length = RecBuf[1] + (RecBuf[2] << 8) + 5;
                if (m_ReadLen != Length)
                {
                    // 接收到的資料與實際資料長度不符
                    return (false);
                }

                if (RecBuf[3] != UART_GET_MODEL)
                {
                    // 回傳的命令錯
                    return (false);
                }

                // 計算與比對回傳檢查碼
                Checksum = 0;
                for (int i = 0; i < Length - 5; i++)
                {
                    Checksum += RecBuf[3 + i];
                }

                if (Checksum != (((int)RecBuf[m_ReadLen - 1] << 8) + (int)RecBuf[m_ReadLen - 2]) && (RecBuf[m_ReadLen - 1] != 0x66) && (RecBuf[m_ReadLen - 2] != 0x66))
                {
                    // 回傳的檢查碼錯誤
                    return (false);
                }

                // 比對各項資料正確無誤, 開始擷取資料
                byte[] Temp = new byte[Length - 6];

                for (int i = 0; i < Length - 6; i++)
                {
                    Temp[i] = RecBuf[i + 4];
                }
                _Model = Encoding.ASCII.GetString(Temp);
                return (true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (false);
            }
        }
        public string ModeCode()
        {
            return _Model;
        }
        public bool SetBrightness()
        {
            bool Status = true;
            int Count = 0;

            try
            {
                Count = 0;
                do
                {
                    Status = UartSetBrightness();
                    if (!Status)
                    {
                        Count++;
                    }
                } while (!Status && Count < (int)UART_TRY_COUNT);     // 連續三次設定失敗才算失敗

                if (Status)
                {
                    C_confirmation = true;
                    return (true);
                }
                else
                {
                    C_confirmation = false;
                    return (false);
                }
            }
            catch
            {
                return (false);
            }

        }
        private bool UartSetBrightness()
        {
            try
            {
                DiscardBuffer();

                int Checksum = 0;
                int Pos = 0;

                // 宣告傳送與接收緩衝區大小
                byte[] SendBuf = new byte[128];
                byte[] RecBuf = new byte[128];

                // 實際所接收到的封包位元組數大小
                int m_ReadLen = 0;
                int Length = 0;

                comport.ReadTimeout = 500;  // 500ms

                // Start
                SendBuf[0] = STX;

                // Length
                switch (GetTotalChannel())
                {
                    case 1:
                        Length = 2;
                        break;
                    case 2:
                        Length = 3;
                        break;
                    case 4:
                        Length = 5;
                        break;
                    case 8:
                        Length = 9;
                        break;
                    default:
                        Length = 0;
                        break;
                }
                SendBuf[1] = (byte)(Length % 256);    // low byte
                SendBuf[2] = (byte)(Length / 256);    // high byte
                Pos = 3;

                // comm
                SendBuf[Pos] = UART_SET_BRIGHTNESS;
                Checksum += SendBuf[Pos];
                Pos++;

                switch (GetTotalChannel())
                {
                    case 1:
                        // CH1 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH1;
                        Checksum += SendBuf[Pos];
                        Pos++;
                        break;
                    case 2:
                        // CH1 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH1;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH2 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH2;
                        Checksum += SendBuf[Pos];
                        Pos++;
                        break;
                    case 4:
                        // CH1 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH1;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH2 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH2;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH3 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH3;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH4 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH4;
                        Checksum += SendBuf[Pos];
                        Pos++;
                        break;
                    case 8:
                        // CH1 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH1;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH2 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH2;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH3 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH3;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH4 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH4;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH5 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH5;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH6 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH6;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH7 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH7;
                        Checksum += SendBuf[Pos];
                        Pos++;

                        // CH8 Brightness (0 ~ 100)
                        SendBuf[Pos] = C_BrightnessCH8;
                        Checksum += SendBuf[Pos];
                        Pos++;
                        break;
                    default:
                        Length = 0;
                        break;
                }

                SendBuf[Pos] = (byte)(Checksum & 0x00ff);
                Pos++;
                SendBuf[Pos] = (byte)((Checksum & 0xff00) >> 8);
                Pos++;

                comport.Write(SendBuf, 0, Pos);

                Array.Clear(RecBuf, 0, RecBuf.Length);

                m_ReadLen = 0;
                Length = 2 + 5;
                int Count = 0;

                do
                {
                    try
                    {
                        m_ReadLen = comport.BytesToRead;
                        Count++;
                        Thread.Sleep(10);
                    }
                    catch (TimeoutException tx)
                    {
                        Debug.WriteLine(tx.Message);
                        return false;
                    }
                } while (m_ReadLen < Length && Count < UART_TIMEOUT);

                if (m_ReadLen != 0)
                {
                    comport.Read(RecBuf, 0, m_ReadLen);
                }
                else
                {
                    // 失敗
                    return (false);
                }

                Length = RecBuf[1] + (RecBuf[2] << 8) + 5;
                if (m_ReadLen != Length)
                {
                    // 接收到的資料與實際資料長度不符
                    return (false);
                }

                if (RecBuf[3] != UART_SET_BRIGHTNESS)
                {
                    // 回傳的命令錯
                    return (false);
                }

                // 計算與比對回傳檢查碼
                Checksum = 0;
                for (int i = 0; i < Length - 5; i++)
                {
                    Checksum += RecBuf[3 + i];
                }

                if (Checksum != (((int)RecBuf[m_ReadLen - 1] << 8) + (int)RecBuf[m_ReadLen - 2]) && (RecBuf[m_ReadLen - 1] != 0x66) && (RecBuf[m_ReadLen - 2] != 0x66))
                {
                    // 回傳的檢查碼錯誤
                    return (false);
                }

                // 比對各項資料正確無誤, 開始擷取資料
                if (RecBuf[4] == (byte)ACK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (false);
            }
            
        }
    
    }
}
