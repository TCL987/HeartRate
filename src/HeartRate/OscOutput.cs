using System;
using System.Net;
using Rug.Osc;

namespace HeartRate;

internal class OscOutput : IDisposable
{
    private readonly string _path;
    private readonly OscSender _sender;

    public OscOutput(IPAddress address, int? port, string path)
    {
        _path = path;

        if(address == null || port == null)
        {
            return;
        }

        if(port is < 1 or > 65535)
        {
            return;
        }

        _sender = new OscSender(address, 0, port.Value);
        _sender.Connect();
    }


    public void Reading(HeartRateReading reading)
    {
        if(string.IsNullOrWhiteSpace(_path))
        {
            return;
        }

        _sender?.Send(new OscMessage(_path, reading.BeatsPerMinute));
    }

    public void Dispose()
    {
        _sender?.Dispose();
    }
}
