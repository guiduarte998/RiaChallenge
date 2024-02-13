using System.Text;
using Newtonsoft.Json;

class Program
{
    static readonly HttpClient client = new HttpClient();
    static readonly string[] firstNames = { 
        "Leia",
        "Sadie",
        "Jose",
        "Sara",
        "Frank",
        "Dewey",
        "Tomas",
        "Joel",
        "Lukas",
        "Carlos"  
    };
    static readonly string[] lastNames = { 
        "Liberty",
        "Ray",
        "Harrison",
        "Ronan",
        "Drew",
        "Powell",
        "Larsen",
        "Chan",
        "Anderson",
        "Lane"
    };
    static int idCounter = 1;

    static async Task Main(string[] args)
    {
        List<Task> tasks = new List<Task>();

        for (int i = 0; i < 10; i++) // Adjust the number of requests as needed
        {
            tasks.Add(SendPostRequest());
        }

        await Task.WhenAll(tasks);
    }

    static async Task SendPostRequest()
    {
        var customers = new List<Customer>();
        var random = new Random();

        // Generate 2 customers with randomized data
        for (int i = 0; i < 2; i++)
        {
            customers.Add(new Customer
            {
                FirstName = firstNames[random.Next(firstNames.Length)],
                LastName = lastNames[random.Next(lastNames.Length)],
                Age = random.Next(10, 91),
                Id = idCounter++
            });
        }

        var json = JsonConvert.SerializeObject(customers);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://localhost:5000/api/customers", content);
        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
    }
}

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int Id { get; set; }
}
