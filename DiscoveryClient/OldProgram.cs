using System;
using System.ServiceModel.Discovery;
using System.Xml;

namespace DiscoveryClient
{
    public class OldProgram
    {
        public static void OldMain(string[] args)
        {
            var endPoint = new UdpDiscoveryEndpoint(DiscoveryVersion.WSDiscoveryApril2005);

            //not sure what this does - if anything.
            //endPoint.Binding.CreateBindingElements().Insert(0, new MulticastCapabilitiesBindingElement(true));

            var discoveryClient = new System.ServiceModel.Discovery.DiscoveryClient(endPoint);

            discoveryClient.FindProgressChanged += DiscoveryClient_FindProgressChanged;

            var findCriteria = new FindCriteria
            {
                Duration = TimeSpan.FromSeconds(10), // Onvif device manager finds cameras almost instantly - so 10s should be plenty
                MaxResults = int.MaxValue
            };

            //Taking cue from sniffing Onvif DM UDP packets - only add one contract type filter
            findCriteria.ContractTypeNames.Add(new XmlQualifiedName("NetworkVideoTransmitter", @"http://www.onvif.org/ver10/network/wsdl"));
            //findCriteria.ContractTypeNames.Add(new XmlQualifiedName("Device", @"http://www.onvif.org/ver10/device/wsdl"));

            Console.WriteLine("Initiating find operation.");

            //discoveryClient.FindAsync(findCriteria);
            //Console.WriteLine("Returned from Async find operation");

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