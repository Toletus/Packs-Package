﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Toletus.Pack.Core.Utils;

public abstract class NetworkInterfaceUtils
{
    public static Dictionary<string, IPAddress> GetNetworkInterfaces()
    {
        var redes = new Dictionary<string, IPAddress>();

        foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        foreach (var ip in nic.GetIPProperties().UnicastAddresses)
            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                redes.TryAdd(nic.Name, ip.Address);

        return redes;
    }

    public static IPAddress? GetNetworkInterfaceIpAddressByName(string networkInterfaceName)
    {
        var adaptors = NetworkInterface.GetAllNetworkInterfaces().ToList();
        var adaptor = adaptors.FirstOrDefault(c => c.Name == networkInterfaceName);

        return adaptor?.GetIPProperties()
            .UnicastAddresses
            .FirstOrDefault(c => c.Address.AddressFamily == AddressFamily.InterNetwork)?.Address;
    }
}