namespace Hangman;

public class Hangman
{
    public static int Attempts { get; set; }
    private static char[] _mask;
    private static List<char> _usedLetters = new List<char>();
    private static string _currentWord;
    private static bool _continueGame;
    private static bool _won;
    private static int _count;

    public Hangman()
    {
      
    }

    public void Play()
    {
      // Start a new game, continue if the user wants to continue and if there are still words left to guess.
      do
      {
        Game();
      } while (!string.IsNullOrEmpty(_currentWord) && PlayAgain());  
    }
    
  
  private void Game()
  {
    Words words = new Words();
    _currentWord = words.GetWord();
    if (string.IsNullOrEmpty(_currentWord))
      return;
    _currentWord = _currentWord.ToLower();
    
    _continueGame = true;
    Attempts = 6;
    _mask = new char[_currentWord.Length];
    Array.Fill(_mask, value:'_');
    _usedLetters.Clear();
    _won = false;
    _count = 0;
    
    while (_continueGame)
    {
      ShowUi(_currentWord, _mask, _usedLetters);
      if (Attempts > 0 && !_won)
        ReadInput(_currentWord);
    }
  }
  
  private void ShowUi(string currentWord, char[] mask, List<char> usedLetters)
  {
    Console.Clear();
    Console.WriteLine(string.Join(" ", mask));
    Console.WriteLine($"Använda bokstäver: {string.Join(" ", usedLetters)}");
    Console.WriteLine($"Försök kvar: {Attempts}\n");
    if (_won == true)
    {
      Console.WriteLine("GRATTIS, du gissade rätt ord!");
      _continueGame = false;
    }
    else if (Attempts > 0)
    {
      Console.WriteLine("Gissa en bokstav: ");
    }
    else
    {
      Console.WriteLine("Tyvärr, försöken är slut");
      Console.WriteLine($"Ordet var: {currentWord}");
      _continueGame = false;
    }
  }
  
  private void ReadInput(string currentWord)
  {
    var letter = char.ToLower(ReadLetter());

    // Check if the letter has already been guessed, otherwise add it to used letters.
    if (_usedLetters.Contains(letter))
      return;
    else
      _usedLetters.Add(letter);

    bool isInWord = false;
    for (int i = 0; i < currentWord.Length; i++)
    {
      if (letter == currentWord[i])
      {
        _mask[i] = letter;
        _count++;
        isInWord = true;
      }
    }

    if (_count == currentWord.Length)
      _won = true;

    if (isInWord == false)
      Attempts--;
  }
  
  // Read only one letter, discard on backspace and confirm on Enter.
  private char ReadLetter()
  {
    char letter = ' ';
    ConsoleKeyInfo c;
    while (true)
    {
      c = Console.ReadKey(true);
      if (char.IsLetter(c.KeyChar) && char.IsWhiteSpace(letter))
      {
        Console.Write(c.KeyChar);
        letter = c.KeyChar;
      }
      if (c.Key == ConsoleKey.Backspace)
      {
        letter = ' ';
        Console.CursorLeft = 0;
        Console.Write(letter);
        Console.CursorLeft = 0;
      }
      if (c.Key== ConsoleKey.Enter && !char.IsWhiteSpace(letter))
        break;
    }
    Console.WriteLine();
    return letter;
  }
  
  private bool PlayAgain()
  {
    while (true)
    {
      Console.WriteLine("\nVill du spela en gång till? j/n");
      var answer = Console.ReadLine().ToLower();
      if (answer == "j")
        return true;
      if(answer == "n")
        return false;
    }
  }
}