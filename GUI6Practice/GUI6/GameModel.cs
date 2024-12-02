namespace GUI6
{
	public class GameModel
	{
		private char[,] _board;
		private string _selectedWord = "";
		private HashSet<string> _usedWords = new HashSet<string>();
		private List<(int, int)> _selectedCells = new List<(int, int)>();

		public ICollection<string> UsedWords => _usedWords;
		public char[,] GetBoard() => _board;
		public string GetSelectedWord() => _selectedWord;


		public GameModel(string startWord)
		{
			int size = startWord.Length;
			_board = new char[size, size];

			int startRow = size / 2;
			int startCol = (size - startWord.Length) / 2;

			for(int i = 0; i < startWord.Length; i++)
			{
				_board[startRow, startCol + i] = startWord[i];
			}
		}

		public bool SelectCell(int row, int col)
		{
			if (row < 0 || row >= _board.GetLength(0) || col < 0 || col >= _board.GetLength(1))
				return false;

			char cellValue = _board[row, col];
			if (cellValue == '\0')
			{
				return false;
			}

			if (_selectedCells.Contains((row, col)))
			{
				return false;
			}

			_selectedCells.Add((row, col));
			_selectedWord += cellValue;
			return true;
		}

		public bool SubmitWord()
		{
			if (_selectedWord.Length < 2 || _usedWords.Contains(_selectedWord))
			{
				_selectedCells.Clear();
				_selectedWord = "";
				return false;
			}

			_usedWords.Add(_selectedWord);
			_selectedCells.Clear();
			_selectedWord = "";
			return true;
		}

		public bool AddLetterToCell(int row, int col, char letter)
		{
			if (row < 0 || row >= _board.GetLength(0) || col < 0 || col >= _board.GetLength(1) || _board[row, col] != '\0')
				return false;

			_board[row, col] = char.ToUpper(letter);
			return true;
		}

	}
}