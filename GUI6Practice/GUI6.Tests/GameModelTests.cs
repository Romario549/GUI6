namespace GUI6.Tests
{
	[TestClass]
	public class GameModelTests
	{
		private GameModel game;
		[TestMethod]
		public void RightStartWord()
		{
			game = new GameModel("БАЛДА");
			char[,] board = game.GetBoard();

			Assert.AreEqual('Б', board[2, 0]);
			Assert.AreEqual('А', board[2, 1]);
			Assert.AreEqual('Л', board[2, 2]);
			Assert.AreEqual('Д', board[2, 3]);
			Assert.AreEqual('А', board[2, 4]);
		}

		[TestMethod]
		public void AddToEmptyCell()	
		{
			game = new GameModel("МОРЕ");
			bool result = game.AddLetterToCell(0, 0, 'А');

			Assert.IsTrue(result);
			Assert.AreEqual('А', game.GetBoard()[0, 0]);
		}

		[TestMethod]
		public void AddToNotEmptyCell()
		{
			game = new GameModel("МОРЕ");
			game.AddLetterToCell(1, 3, 'X');
			bool result = game.AddLetterToCell(1, 3, 'T');

			Assert.IsFalse(result);
			Assert.AreEqual('X', game.GetBoard()[1, 3]);
		}

		[TestMethod]
		public void AddCellToSelection()
		{
			game = new GameModel("МОРЕ");
			bool result = game.SelectCell(2, 0); 

			Assert.IsTrue(result);
			Assert.AreEqual("М", game.GetSelectedWord());
		}

		[TestMethod]
		public void CellSelected()
		{
			game = new GameModel("МОРЕ");
			game.SelectCell(2, 0); 
			bool result = game.SelectCell(2, 0); 

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void AddWord()
		{
			game = new GameModel("МОРЕ");
			game.SelectCell(2, 0); 
			game.SelectCell(2, 1); 
			game.SelectCell(2, 2); 
			game.SelectCell(2, 3); 

			bool result = game.SubmitWord();

			Assert.IsTrue(result);
			Assert.IsTrue(game.UsedWords.ToList().Contains("МОРЕ"));
		}

		[TestMethod]
		public void WordAlreadyAdded()
		{
			game = new GameModel("МОРЕ");
			game.SelectCell(2, 0); 
			game.SelectCell(2, 1); 
			game.SelectCell(2, 2); 
			game.SelectCell(2, 3); 
			game.SubmitWord(); 

			game.SelectCell(2, 0); 
			game.SelectCell(2, 1); 
			game.SelectCell(2, 2); 
			game.SelectCell(2, 3); 
			bool result = game.SubmitWord(); 

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ClearSelectedCells()
		{
			game = new GameModel("МОРЕ");
			game.SelectCell(2, 0); 
			game.SelectCell(2, 1); 
			game.SelectCell(2, 2); 
			game.SelectCell(2, 3); 
			game.SubmitWord(); 

			game.SelectCell(2, 0); 
			game.SelectCell(2, 1); 
			game.SelectCell(2, 2); 
			game.SelectCell(2, 3); 
			bool result = game.SubmitWord(); 

			Assert.IsFalse(result);
			Assert.AreEqual("", game.GetSelectedWord());
		}

		[TestMethod]
		public void AddUpperCaseToCell()
		{
			game = new GameModel("МОРЕ");
			game.AddLetterToCell(0, 0, 'а');

			Assert.AreEqual('А', game.GetBoard()[0, 0]);
		}

		[TestMethod]
		public void SelectEmptyCell()
		{
			game = new GameModel("МОРЕ");
			bool result = game.SelectCell(0, 0); 

			Assert.IsFalse(result);
			Assert.AreEqual("", game.GetSelectedWord());
		}
	}
}
