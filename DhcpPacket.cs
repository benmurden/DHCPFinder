using System;
using PacketDotNet;
using System.Net.NetworkInformation;
using System.Net;

namespace DHCPScan
{
    class DhcpPacket
    {

        public EthernetPacket packet { get; set; }
        private IPv4Packet ipPacket;
        private UdpPacket udpPacket;
        private byte[] payload;
        public byte op { get; set; }
        public byte htype { get; set; }
        public byte hlen { get; set; }
        public byte hops { get; set; }
        public byte[] xid { get; set; }
        public byte[] secs { get; set; }
        public byte[] flags { get; set; }
        public IPAddress ciaddr { get; set; }
        public IPAddress yiaddr { get; set; }
        public IPAddress siaddr { get; set; }
        public IPAddress giaddr { get; set; }
        public PhysicalAddress chaddr { get; set; }
        public byte[] rawOptions { get; set; }
        public string HostName { get; set; }

        public DhcpPacket()
        {
            xid = new byte[4];
            secs = new byte[2];
            flags = new byte[2];
        }

        public void AssembleDiscover(byte[] deviceMAC)
        {
            try
            {
                if (deviceMAC.Length != 6) 
                {
                    throw new ArgumentException("Device MAC must be 6 bytes");
                }

                PhysicalAddress dstMAC = PhysicalAddress.Parse("FFFFFFFFFFFF");
                PhysicalAddress srcMAC = PhysicalAddress.Parse(BitConverter.ToString(deviceMAC));
                packet = new EthernetPacket(srcMAC, dstMAC, EthernetPacketType.IpV4);

                IPAddress srcIP = IPAddress.Parse("0.0.0.0");
                IPAddress dstIP = IPAddress.Parse("255.255.255.255");
                ipPacket = new IPv4Packet(srcIP, dstIP);

                udpPacket = new UdpPacket(68, 67);

                payload = new byte[244];
                byte[] xid = new byte[4];
                Random rnd = new Random();
                // OP
                payload[0] = 0x01;
                // HTYPE
                payload[1] = 0x01;
                // HLEN
                payload[2] = 0x06;
                // HOPS [3]
                // XID [4]
                rnd.NextBytes(xid);
                xid.CopyTo(payload, 4);
                // SECS [8]
                // FLAGS [10]
                // CIADDR [12]
                // YIADDR [16]
                // SIADDR [20]
                // GIADDR [24]
                // CHADDR [28]
                deviceMAC.CopyTo(payload, 28);
                // BOOTP Legacy [44]
                // Magic Cookie
                payload[236] = 0x63;
                payload[237] = 0x82;
                payload[238] = 0x53;
                payload[239] = 0x63;
                // Options
                // Message Type DHCPDISCOVER
                payload[240] = 53;
                payload[241] = 0x01;
                payload[242] = 0x01;

                // Host Name
                //payload[243] = 12;
                //payload[244] = 0x03;
                //payload[245] = 0x53;
                //payload[246] = 0x54;
                //payload[247] = 0x55;

                //End Option
                payload[243] = 0xFF;

                udpPacket.PayloadData = payload;
                ipPacket.PayloadPacket = udpPacket;
                packet.PayloadPacket = ipPacket;

                udpPacket.UpdateUDPChecksum();
                ipPacket.UpdateIPChecksum();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /**
         * Generates new transaction ID for the DHCP packet
         **/
        public void GenerateNewXid()
        {
            byte[] xid = new byte[4];
            Random rnd = new Random();
            rnd.NextBytes(xid);
            xid.CopyTo(payload, 4);

            udpPacket.UpdateUDPChecksum();
            ipPacket.UpdateIPChecksum();
        }

        internal static DhcpPacket Parse(byte[] data)
        {
            try
            {
                DhcpPacket dhcpPacket = new DhcpPacket();
                byte[] IPBuffer = new byte[4];

                dhcpPacket.op = data[0];
                dhcpPacket.htype = data[1];
                dhcpPacket.hlen = data[2];
                dhcpPacket.hops = data[3];
                Buffer.BlockCopy(data, 4, dhcpPacket.xid, 0, 4);
                Buffer.BlockCopy(data, 8, dhcpPacket.secs, 0, 2);
                Buffer.BlockCopy(data, 10, dhcpPacket.flags, 0, 2);

                Buffer.BlockCopy(data, 12, IPBuffer, 0, 4);
                dhcpPacket.ciaddr = new IPAddress(IPBuffer);

                Buffer.BlockCopy(data, 16, IPBuffer, 0, 4);
                dhcpPacket.yiaddr = new IPAddress(IPBuffer);

                Buffer.BlockCopy(data, 20, IPBuffer, 0, 4);
                dhcpPacket.siaddr = new IPAddress(IPBuffer);

                Buffer.BlockCopy(data, 24, IPBuffer, 0, 4);
                dhcpPacket.giaddr = new IPAddress(IPBuffer);

                int hlen = Convert.ToInt16(dhcpPacket.hlen);
                byte[] MACBuffer = new byte[hlen];
                Buffer.BlockCopy(data, 28, MACBuffer, 0, hlen);
                dhcpPacket.chaddr = new PhysicalAddress(MACBuffer);

                int optionsLen = data.Length - 240;
                dhcpPacket.rawOptions = new byte[optionsLen];
                Buffer.BlockCopy(data, 240, dhcpPacket.rawOptions, 0, optionsLen);

                dhcpPacket.HostName = ParseOption(dhcpPacket.rawOptions, 12);

                return dhcpPacket;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private static string ParseOption(byte[] opts, int optionNo)
        {
            try
            {
                int optsLen = opts.Length;
                int i = 0;
                int code;
                int len;
                byte[] optionValue;

                do
                {
                    code = opts[i++];
                    len = opts[i++];

                    if (code == optionNo)
                    {
                        optionValue = new byte[len];
                        Buffer.BlockCopy(opts, i++, optionValue, 0, len);
                        return System.Text.ASCIIEncoding.ASCII.GetString(optionValue);
                    }
                    else
                    {
                        i += len;
                    }
                }
                while (i <= optsLen);

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
