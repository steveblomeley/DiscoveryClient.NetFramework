using System;
using System.ServiceModel.Discovery;
using System.Xml;

namespace DiscoveryClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var endPoint = new UdpDiscoveryEndpoint(DiscoveryVersion.WSDiscoveryApril2005);

            var discoveryClient = new System.ServiceModel.Discovery.DiscoveryClient(endPoint);

            discoveryClient.FindProgressChanged += DiscoveryClient_FindProgressChanged;

            var findCriteria = new FindCriteria
            {
                Duration = TimeSpan.FromSeconds(10),
                MaxResults = int.MaxValue
            };

            findCriteria.ContractTypeNames.Add(new XmlQualifiedName("NetworkVideoTransmitter", @"http://www.onvif.org/ver10/network/wsdl"));

            Console.WriteLine("Initiating find operation.");
            var response = discoveryClient.Find(findCriteria);
            Console.WriteLine($"Operation returned - Found {response.Endpoints.Count} endpoints.");

            Console.ReadKey();
        }

        private static void DiscoveryClient_FindProgressChanged(object sender, FindProgressChangedEventArgs e)
        {
            Console.WriteLine($"Found this: {e}");
        }
    }
}