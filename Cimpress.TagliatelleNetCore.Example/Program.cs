using System;
using Cimpress.TagliatelleNetCore.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cimpress.TagliatelleNetCore.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Pass token as first parameter");
                return;
            }
            IClient client = new Client();

            Console.WriteLine("Placing a tag....");

            client.Tag(args[0]).WithKey("urn:rafals-namespace:demo").WithResource("http://some-site").WithValue("custom meta data").Apply();

            Console.WriteLine("Trying and fetching....");

            var response = client.Tag(args[0]).WithKey("urn:rafals-namespace:demo").Fetch();
            foreach (var tag in response.Results)
            {
                System.Console.WriteLine($"{tag.ResourceUri} is tagged with {tag.Key} and custom meta value [{tag.Value}]");
            }
            
            Console.WriteLine("Removing all....");

            client.Tag(args[0]).WithKey("urn:rafals-namespace:demo").Remove();;
        }
    }
}
