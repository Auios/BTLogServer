using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

namespace BTLogServer
{
    class Program
    {
        static void echo(string text)
        {
            Console.WriteLine(text);
        }

        static void Main(string[] args)
        {
            bool runApp = true;

            TcpListener server = null;
            int port = 13500;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddress, port);

            server.Start();

            byte[] buffer;
            string data;

            while(runApp)
            {
                echo("Waiting for connection...");

                TcpClient cl = server.AcceptTcpClient();
                NetworkStream netStream = cl.GetStream();
                echo("Client connected!");

                buffer = null;
                data = null;

                if(cl.Available > 0)
                {
                    echo("Receiving data...");
                    buffer = new byte[cl.Available];
                    netStream.Read(buffer, 0, cl.Available);
                    data = Encoding.ASCII.GetString(buffer);
                    echo("Data: " + data);
                }

                echo("Sending data...");
                data = "Hello world!";
                buffer = Encoding.ASCII.GetBytes(data);
                netStream.Write(buffer, 0, buffer.Length);
                echo("Sent: " + data);

                cl.Close();
            }

            echo("Shutting down server...");
            server.Stop();
            echo("Complete!");
        }
    }
}
