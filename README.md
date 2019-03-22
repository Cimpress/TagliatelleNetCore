# TagliatelleNetCore

This is a client package making use of Tagliatelle tagging service easier from C# code base.


## Usage
        
Install package

    dotnet add package Cimpress.TagliatelleNetCore
    
Instantiate fluent client
            
    IClient client = new Client(accessToken);
            
The high level client follows fluent API design.            
In order to get tags matching criteria:
            
    var response = client.Tag().WithKey("urn:rafals-namespace:demo").Fetch();
    foreach (var tag in response.Results)
    {
        Console.WriteLine(tag.ResourceUri);
        Console.WriteLine(tag.Value);
        Console.WriteLine(tag.Key);
    }
    
In order to tag a resource
            
    client.Tag().WithKey("urn:rafals-namespace:demo").WithResource("http://some-site").Apply();
            
In order to remove tags based on certain criteria.
            

    client.Tag().WithKey("urn:rafals-namespace:demo").WithResource("http://some-site").Remove();
            
            
will remove the tag created in the previous step. However

            
    client.Tag().WithKey("urn:rafals-namespace:demo").Remove();
    
will remove tall tags tagged with `urn:rafals-namespace:demo`