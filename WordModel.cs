namespace ConsoleWordle
{
    public class WordModel
    {
        private static WordModel instance;
        private WordModel() { }

        // Makes only one class word run, frees memory. If the user wants to run class they must call instance method. 
        // If there is already one running, will not run.

        // Make use in projects, makes classes not go to waste
        public static WordModel Instance()
        {
            if (instance == null)
            {
                instance = new WordModel();
            }
            return instance;
        
        }

        public int? Id { get; set; }
        public string Word { get; set; }

    }
}
