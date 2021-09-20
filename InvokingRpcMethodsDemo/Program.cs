using System;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace InvokingRpcMethodsDemo
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using AdsClient client = new AdsClient(); // New instance of the TwinCAT ADS client.
            client.Connect("192.168.250.2.1.1", 851);
            if (!client.IsConnected)
            {
                Console.WriteLine("Cannot connect to the PLC");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Succesfully connected to the PLC");
            Console.WriteLine("Summation demo, enter first value:");
            float value1 = float.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Summation demo, enter second value:");
            float value2 = float.Parse(Console.ReadLine() ?? string.Empty);
            ResultRpcMethod result =
                await client.InvokeRpcMethodAsync("Main._fbCalculator",
                                                  "Sum",
                                                  new object[] { value1, value2 },
                                                  CancellationToken.None);
            Console.WriteLine(result.Succeeded
                                  ? $"Succesfully calculated, result is: {result.ReturnValue}"
                                  : $"Failed calling RPC method, error code: {result.ErrorCode}");
            Console.ReadLine();
        }
    }
}