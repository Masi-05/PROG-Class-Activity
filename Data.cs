using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunWordle
{
    public class Data
    {

        public class WordResponse
        {
            public string Word { get; set; }
            public int AttemptsLeft { get; set; }
        }

        public class GuessResponse
        {
            public string Message { get; set; }
            public string Feedback { get; set; }
            public int AttemptsLeft { get; set; }
        }

        public class AttemptsResponse 
        {
            public int AttemptsLeft { get; set; }
        }

        public class NewUserResponse
        {
            public string UserId { get; set; }
        }



    }
}
