namespace Hangman;
class Words
{
    private static List<string> _words = new List<string> {"koffert", "förundran", "gran", "kulvert", "anteckningsblock", 
        "kaskelottval", "fåtölj", "vattenfall", "änglaspel", "kaffeböna", "citronskal", "tjära"};
    
    
    // Get random word from list and then remove that word to avoid repetition.
    public string GetWord()
    {
        if (_words.Count < 1)
        {
            Console.Clear();
            Console.WriteLine("Kul att du gillar spelet, men nu har du gissat alla ord.");
            return default;
        }
        
        var randomIndex = new Random().Next(_words.Count);
        var word = _words[randomIndex];
        _words.RemoveAt(randomIndex);
        return word;
    }
}