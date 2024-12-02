using static Microsoft.VisualBasic.Interaction;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI6
{
	public partial class MainWindow : Window
	{
		private GameModel _model;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void StartNewGameButton_Click(object sender, RoutedEventArgs e)
		{
			string startWord = StartWordInput.Text.Trim();
			if (string.IsNullOrWhiteSpace(startWord) || startWord.Length < 3)
			{
				MessageDisplay.Text = "Начальное слово должно быть не менее 3 символов.";
				return;
			}

			MessageDisplay.Text = "";
			InitializeGame(startWord.ToUpper());
		}

		private void SubmitWordButton_Click(object sender, RoutedEventArgs e)
		{
			if (_model.SubmitWord())
			{
				MessageDisplay.Text = "Слово успешно добавлено!";
				UpdateUsedWordsList();
			}
			else
			{
				MessageDisplay.Text = "Слово недопустимо или уже использовалось.";
			}

			UpdateSelectedWordDisplay();
		}

		private void InitializeGame(string startWord)
		{
			_model = new GameModel(startWord);
			UpdateGameGrid();
		}

		private void UpdateGameGrid()
		{
			GameGrid.Children.Clear();
			GameGrid.RowDefinitions.Clear();
			GameGrid.ColumnDefinitions.Clear();

			char[,] board = _model.GetBoard();
			int size = board.GetLength(0);

			for (int i = 0; i < size; i++)
			{
				GameGrid.RowDefinitions.Add(new RowDefinition());
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}

			for (int row = 0; row < size; row++)
			{
				for (int col = 0; col < size; col++)
				{
					Button button = new Button
					{
						Content = board[row, col] == '\0' ? "" : board[row, col].ToString(),
						FontSize = 16,
						Tag = new Tuple<int, int>(row, col)
					};
					button.Click += CellButton_Click;
					GameGrid.Children.Add(button);
					Grid.SetRow(button, row);
					Grid.SetColumn(button, col);
				}
			}
			UpdateUsedWordsList();
		}
		private void CellButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.Tag is Tuple<int, int> position)
			{
				int row = position.Item1;
				int col = position.Item2;

				char[,] board = _model.GetBoard();

				if (board[row, col] == '\0') 
				{
					string input = InputBox("Введите букву (только одну):", "Добавить букву", "");
					if (!string.IsNullOrWhiteSpace(input) && input.Length == 1 && char.IsLetter(input[0]))
					{
						char letter = char.ToUpper(input[0]);
						if (_model.AddLetterToCell(row, col, letter))
						{
							button.Content = letter.ToString();
						}
						else
						{
							MessageDisplay.Text = "Невозможно добавить букву в эту ячейку.";
						}
					}
					else
					{
						MessageDisplay.Text = "Некорректный ввод. Введите одну букву.";
					}
				}
				else 
				{
					if (_model.SelectCell(row, col))
					{
						button.BorderBrush = new SolidColorBrush(Colors.Green);
					}
					else
					{
						button.BorderBrush = new SolidColorBrush(Colors.Red);
					}
					UpdateSelectedWordDisplay();
				}
			}
		}

		private void UpdateSelectedWordDisplay()
		{
			SelectedWordDisplay.Text = "Выбранное слово: " + _model.GetSelectedWord();
		}
		private void UpdateUsedWordsList()
		{
			UsedWordsList.Items.Clear();
			foreach (var word in _model.UsedWords)
			{
				UsedWordsList.Items.Add(word);
			}
		}
	}
}
