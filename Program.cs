using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RunWordle
{
    public class Program
    {
        static async Task Main()
        {
            await new Run().PlayGameAsync();
        }
    }
}
