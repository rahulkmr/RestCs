using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace RestCs
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task<List<Repo>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
            );
            client.DefaultRequestHeaders.Add("User-Agent", ".net");

            var serializer = new DataContractJsonSerializer(typeof(List<Repo>));
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            return serializer.ReadObject(await streamTask) as List<Repo>;
        }

        static void Main(string[] args)
        {
            foreach (var repo in ProcessRepositories().Result)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.LastPush);
                Console.WriteLine();
            }
        }
    }
}
