using System;
using SplashKitSDK;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.IO;
using System.Text;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
//using Newtonsoft.Json.Bson.BsonReader;

namespace smsmarketing{


//pseudocode:
/*  prompt for database connection string
    load remote database via webhook
    connect to database

    query the database for all mobile phone numbers and create a list

    Prompt for entering the marketing text copy (include URL for lead capture page)

    connect to Twilio

    Authorise Twilio

    Load marketing campaign

    Send marketing copy and list of phone numbers to Twilio

    Send SMS

    Log client response
        => unsubscribe
        => null
        => followup

    if unsubscribe => update data in remote database (POST REQUEST)
    if null => log entry
    if followup => new list => voice call followup
    
 */

class Program 
{
   
        List<int> phonenumbers = new List<int>();
        
        public static void Main(string[] args)
    {
       Console.WriteLine("Welcome to the Bulk SMS Marketing Sofware App");
       Console.WriteLine("Ensure you generate your SMS Marketing List First");
       Console.WriteLine("Please enter your marketing text including the URL for your lead capture page ");
       string copytext = Console.ReadLine();
       MenuOption userSelection;

        do
        {

            userSelection = ReadUserOption();

            switch (userSelection)
            {
                case MenuOption.ConnectToMongo:
                Console.WriteLine("You have chosen to connect to your database ...");
                MainAsync().Wait();
                break;
                //case MenuOption.MarketingCopy:
                //Console.WriteLine("You have chosen to enter your marketing message ...");
                //MarketingCopy();
                //break;
                //case MenuOption.DeploySms:
                //Console.WriteLine("You would like to deploy your marketing message to your marketing list ... ");
                //DeployMarketing.DeploySms(copytext, List<int> phonenumbers);
                //break;
                case MenuOption.Quit:
                Console.WriteLine("Quit ...");
                break;
                } 
        } while (userSelection != MenuOption.Quit);
        Console.WriteLine(userSelection); 
        
    }


    public static MenuOption ReadUserOption()
        {
        
                Console.WriteLine("Please choose from the the following:");
                Console.WriteLine("** 1 = Connect to Mongo **\n** 2 = Enter Your Marketing Copy **");
                Console.WriteLine("** 3 = Deploy Your Bulk SMS Message");
                Console.WriteLine("** 4 = Quit **");
                int option = Convert.ToInt32(Console.ReadLine());
                return (MenuOption)(option - 1);
               
        }

        //Bazinga Mongo Cluster Connection String: "mongodb+srv://XXXXX/smsmarketing?retryWrites=true";
        //prompt for webhook for database
        //Console.WriteLine("Welcome to the sms marketing program");
        //Console.WriteLine("Which database would you like to connect to? Please enter your connection string");
        //string enterwebhook = Console.ReadLine();
        //load datafile
        
        //MainAsync().Wait();
        
        //MarketingCopy marketing = new MarketingCopy();

        /*const string accountSid = "XXXXX";
        const string authToken = "XXXXX";

        TwilioClient.Init(accountSid, authToken);

        var message = MessageResource.Create(
            body: "Hi Joe, how would you like to be able to now purchase the Aston Martin you have always wanted? https://luxecarimports.com/ ",
            from: new Twilio.Types.PhoneNumber("XXXXX"),
            to: new Twilio.Types.PhoneNumber("XXXXX")
        );

        Console.WriteLine(message.Sid);

    }
}

        
        

        
        //TwiHelper connect = new TwiHelper(marketingcopy, phonenumbers);

        
        

        //TwilioHelper twiliohelper = new TwilioHelper(string marketingcopy, List<string> phonenumbers);
        
        //Console.ReadLine();

        
       

        
        
        

        //prompt for marketing text
        //Console.WriteLine("Please enter the marketing text you would like to send to your client list");
        //string marketingtext = Console.ReadLine(); 
        
   /*  }

    private static void TwiHelper()
    {
        throw new NotImplementedException();
    }
*/
    public static async Task MainAsync()
            {
                //var url = enterwebhook;


                //var client = new MongoClient("mongodb+srv://XXXXXX/smsmarketing?retryWrites=true");
                //var database = client.GetDatabase("test");

                
                var client = new MongoClient("mongodb+srv://XXXXXXX/smsmarketing?retryWrites=true");
                var database = client.GetDatabase("smsmarketing");
                var collection = database.GetCollection<ClientList>("twentydollarbargain");

                int count = 1;
                await collection.Find(FilterDefinition<ClientList>.Empty)
                      .Project(x => new {x.MOBILE1})
                      .ForEachAsync(
                          mobile =>
                              { 
                                  List<int> phonenumbers = new List<int>();
                                  phonenumbers.Add(Convert.ToInt32(mobile.MOBILE1));
                                  Console.WriteLine($"MOBILE1: {mobile.MOBILE1}");
                                  //string json = MongoDB.Bson.IO.JsonConvert(
                                    //(JsonConvert.SerializeObject.(phonenumbers));
                                  System.IO.File.WriteAllText("Phonenumbers.Json", json);
                                      count++;
                                  using (var stream = new MemoryStream())
                                  {
                                      using (var writer = new BsonBinaryWriter(stream))
                                      {
                                          BsonSerializer.Serialize(writer, typeof(BsonDocument), bson);
                                      }
                                  }
                             });
                             
                Console.WriteLine();

                //await collection.Find(mobilequery = mOBILE1.Where(mobile => )
        
                //using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
                //{
                   
                //    while (await cursor.MoveNextAsync())
                //    {
                        // Specify the data source.
                        // BSON Document, As Queryable ... 
                        
                        // var 
                        // await collection.Find(filter).ForEachAsync(document => Console.WriteLine(filter));

                        // Define the query expression.
                        // ??Inumerable<string> mobileQuery = from mobile in mobilenumbers where mobile contains "04" select mobile;
                        // or
                        // IQueryable<Mobile> mobileQuery =
                        //      from mobile in database.MOBILE1
                        //      where mobile contains "04"
                        //      select mobile;

                        // To immediately build the query to a list (immediate execution)
                        // List<ClientListBuild> mobileQuery = 
                        //      (from mobile in database.MOBILE1
                        //       where mobile contains "04"
                        //       select mobile).ToList();


                        // mOBILE1: ({$exists: true})
                        // Execute the query expression. 
                        // In this case, the query results that for each document read, the result is added to a list. 
                        // for each (int i in mobileQuery)
                        //{
                        //      ListName.Add(mobile);
                        //      i ++;
                        //}

                    
                        //var mobFilter = builder.Eq(u => i.mOBILE1);

                        //await collection.Find(mobFilter).ForEachAsync(document => Console.WriteL);
                // {
                        //Console.WriteLine(document);
                //        Console.WriteLine();
                //    }
            }
            
            
                

            
            

            /*  private static void DeploySms(string copytext, List<string> phonenumbers)
                {
                    // Find your Account Sid and Token at twilio.com/console
                    // DANGER! This is insecure. See http://twil.io/secure
                    const string accountSid = "XXXXX";
                    const string authToken = "XXXXX";
                    

                    TwilioClient.Init(accountSid, authToken);

                    for (int i = 0; i < phonenumbers.Count; i ++) {

                    var message = MessageResource.Create(
                    body: (copytext),
                    from: new Twilio.Types.PhoneNumber("XXXXX"),
                    to: new Twilio.Types.PhoneNumber(phonenumbers[i])
                    );
                    
                    Console.WriteLine(message.Sid);
                    } */
                }

        
 }

public enum MenuOption
{
    ConnectToMongo,

    MarketingCopy,

    DeploySms,

    Quit

}
            
