

namespace HardCopy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using LibUsbDotNet;
    using LibUsbDotNet.Main;
    using LibUsbDotNet.DeviceNotify;
    using System.Threading;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    class BulkUSB
    {
        private bool USB_isOpen = false;
        private UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x0adc, 0x0018);//2780,24
        private UsbDevice MyUsbDevice = null;
        private UsbEndpointReader reader = null;
        private UsbEndpointWriter writer = null;

        private IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        private Thread T_USB_Conn_Check;
        private List<byte> buff = new List<byte>();
        public event EventHandler recv_packet = null;
        public event EventHandler connected = null;

        private bool is_run = true;


        public BulkUSB()
        {
            UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;
            T_USB_Conn_Check = new Thread(new ThreadStart(T_Bulk_Connect_Check));
            T_USB_Conn_Check.IsBackground = true;
            T_USB_Conn_Check.Start();
        }


        
        private void T_Bulk_Connect_Check()
        {
            //Todo : 01.주기적으로 연결끊겼을경우 연결 시도

            
            while (is_run)
            {
                if (!USB_isOpen)
                {
                    USB_Connect();
                }
                Thread.Sleep(2000);
            }
        }

        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            try
            {
                if (e.Device != null)
                {
                    if (e.Device.IdProduct == 24 && e.Device.IdVendor == 2780)
                    {
                        if (!USB_isOpen)
                        {
                            USB_Connect();
                        }
                        else
                        {
                            USB_Close();
                        }
                    }
                }
            }
            catch (System.ObjectDisposedException)
            {
            }
            catch (System.Exception)
            {
            }
        }
        public void USB_Data_Send(string send)
        {
            if (USB_isOpen == false)
            {
                return;
            }
            try
            {
                ErrorCode ec = ErrorCode.None;
                int bytesWritten;
                ec = writer.Write(Encoding.ASCII.GetBytes(send), 1000, out bytesWritten);

                if (ec != ErrorCode.None)
                {
                    USB_Close();
                }
            }
            catch (System.ObjectDisposedException)
            {
                USB_Close();
            }
            catch (System.Exception)
            {
                USB_Close();
            }
        }
        public void USB_Connect()
        {
            try
            {
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                if (MyUsbDevice == null) return;

                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (reader != null)
                {
                    reader.DataReceived -= USB_ReadEvent;
                }
                // 벌크 통신은 8 16 32 64byte를 일반적으로 사용
                // 최소는 8이하로 가능

                reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep02, 64, EndpointType.Bulk);//수신은 ID를 EP02를 사용
                reader.Flush();
                writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);
                reader.ReadThreadPriority = ThreadPriority.Normal;

                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    wholeUsbDevice.SetConfiguration(1);
                    wholeUsbDevice.ClaimInterface(0);
                }
                USB_isOpen = true;
                USB_Data_Send("tt");
                buff.Clear();
                if (connected != null)
                {
                    connected(USB_isOpen, null);
                }
                reader.DataReceived += USB_ReadEvent;
                reader.DataReceivedEnabled = true;
                reader.ReadBufferSize = 64;
            }
            catch (System.ObjectDisposedException)
            {
                USB_Close();
            }
            catch (Exception)
            {
                USB_Close();
            }
        }

        private void USB_ReadEvent(object sender, EndpointDataEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.Count; i++)
                {
                    switch (e.Buffer[i])
                    {
                        case 0x02:
                            buff.Clear();
                            buff.Add(e.Buffer[i]);
                            break;
                        case 0x03:
                            buff.Add(e.Buffer[i]);

                            //전송
                            if (recv_packet != null)
                                recv_packet(buff.ToArray(), null);
                            buff.Clear();
                            break;
                        default:
                            buff.Add(e.Buffer[i]);
                            if (buff.Count > 3000)
                            {
                                buff.Clear();
                            }
                            break;
                    }
                }
            }
            catch (System.ObjectDisposedException)
            {
                USB_Close();
            }
            catch (System.Exception)
            {
                USB_Close();
            }

        }
        public void USB_Close()
        {

            try
            {
                if (reader != null)
                {
                    reader.DataReceived -= USB_ReadEvent;
                }
            }
            catch (System.ObjectDisposedException)
            {
            }
            catch (System.Exception)
            {
            }

            try
            {
                if (MyUsbDevice.IsOpen)
                {
                    if (MyUsbDevice != null)
                    {
                        if (MyUsbDevice.IsOpen)
                        {
                            IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                            if (!ReferenceEquals(wholeUsbDevice, null))
                            {
                                wholeUsbDevice.ReleaseInterface(0);
                            }
                            MyUsbDevice.Close();
                        }
                        MyUsbDevice = null;
                    }
                }
            }
            catch (System.ObjectDisposedException)
            {

            }
            catch (System.Exception)
            {

            }
            finally
            {
                UsbDevice.Exit();
            }
            USB_isOpen = false;
            if (connected != null)
            {
                connected(USB_isOpen, null);
            }


        }
        public void Form_Closeing_USB()
        {
            try
            {
                is_run = false;
                if (MyUsbDevice != null)
                {
                    if (MyUsbDevice.IsOpen)
                    {
                        IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                        if (!ReferenceEquals(wholeUsbDevice, null))
                        {
                            wholeUsbDevice.ReleaseInterface(0);
                        }
                        MyUsbDevice.Close();
                    }

                }
            }
            catch (System.ObjectDisposedException)
            {

            }
            catch (System.Exception)
            {

            }
            finally
            {
                MyUsbDevice = null;
                UsbDevice.Exit();
            }

        }


    }
}
