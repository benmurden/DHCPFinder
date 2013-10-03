using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using SharpPcap;
using PacketDotNet;
using System.Runtime.InteropServices;
using DHCPScan;

namespace DHCPFinder
{

    public partial class DHCPFinderForm : Form
    {
        private delegate void setOutputTextCallback(String[] newRow);
        private Timer discoverTimer = new Timer();
        public bool bCapturing = false;
        public CaptureDeviceList devices { get; set; }
        public ICaptureDevice device { get; set; }
        DhcpPacket dhcpPacket = new DhcpPacket();

        public DHCPFinderForm()
        {
            InitializeComponent();
            discoverTimer.Tick += new EventHandler(discoverTimer_Tick);
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("That's not a MAC address");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbInterfaces.Text == "")
            {
                MessageBox.Show("Select an interface before probing the network. Don't want to probe without an interface, son!", "DHCP Finder", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                device = devices[cmbInterfaces.SelectedIndex];

                if (!bCapturing)
                {

                    btnStart.Text = "Stop";

                    bCapturing = true;

                    device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);

                    device.Open(DeviceMode.Promiscuous);

                    Console.WriteLine("Listening on {0}", device.Description);

                    dhcpPacket.AssembleDiscover(device.MacAddress.GetAddressBytes());

                    string strRealMac = txtRealMac.Text;
                    string strFilter = String.Format("udp dst port 68 && ether src not {0}", strRealMac);
                    //string strFilter = String.Format("udp dst port 68 or 67", strRealMac);
                    device.Filter = strFilter;

                    device.StartCapture();

                    discoverTimer.Interval = Convert.ToInt32(numTimerInterval.Value);
                    discoverTimer.Start();

                }
                else
                {
                    btnStart.Text = "Start";
                    bCapturing = false;

                    //Close the socket and stop capturing
                    device.StopCapture();
                    device.Close();

                    //Stop the timer
                    discoverTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DHCP Finder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void discoverTimer_Tick(object sender, EventArgs e)
        {
            dhcpPacket.GenerateNewXid();
            SendDiscover();
        }

        private void SendDiscover()
        {
            try
            {
                device.SendPacket(dhcpPacket.packet);
                Console.WriteLine("Sent DHCP Discover");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to send DHCP Discover");
            }
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            try
            {
                Packet p = Packet.ParsePacket(LinkLayers.Ethernet, e.Packet.Data);
                EthernetPacket eth = EthernetPacket.GetEncapsulated(p);
                if (eth != null)
                {
                    DateTime time = e.Packet.Timeval.Date;
                    int len = e.Packet.Data.Length;

                    string srcMac = eth.SourceHwAddress.ToString();

                    UdpPacket udp = UdpPacket.GetEncapsulated(eth);
                    byte[] payload = udp.PayloadData;
                    DhcpPacket response = DhcpPacket.Parse(payload);

                    String[] newRow = new String[7];

                    newRow[0] = String.Format("{0}.{1}",
                        time.ToString("yyyy-MM-dd HH:mm:ss"), time.Millisecond);

                    newRow[1] = srcMac;

                    newRow[2] = response.ciaddr.ToString();

                    newRow[3] = response.yiaddr.ToString();

                    newRow[4] = response.siaddr.ToString();

                    newRow[5] = response.giaddr.ToString();

                    newRow[6] = response.HostName;

                    setOutputText(newRow);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void setOutputText(String[] newRow)
        {
            // InvokeRequired compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.grdOutput.InvokeRequired)
            {
                setOutputTextCallback d = new setOutputTextCallback(setOutputText);
                this.Invoke(d, new object[] { newRow });
            }
            else
            {
                int n = this.grdOutput.Rows.Add();

                int len = newRow.Length;
                for (int i = 0; i < newRow.Length; i++)
                {
                    this.grdOutput.Rows[n].Cells[i].Value = newRow[i];
                    this.grdOutput.AutoResizeColumn(i);
                }
            }
        }

        private void DHCPFinder_Load(object sender, EventArgs e)
        {
            string ver = SharpPcap.Version.VersionString;
            Console.WriteLine("SharpPcap {0}, DHCP Finder", ver);

            devices = CaptureDeviceList.Instance;

            foreach (ICaptureDevice dev in devices)
                cmbInterfaces.Items.Add(dev.Description);

            lblVersion.Text = "0.0.3";
        }

        public static byte[] HexToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to lose all of them logs?", "DHCP Finder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer.Equals(DialogResult.Yes)) {
                grdOutput.Rows.Clear();
            }
        }
    }
}
