﻿using CPC2020_2_Lab4.Models.Entities;
using CPC2020_2_Lab4.Repositories;
using System;
using System.Windows.Forms;

namespace CPC2020_2_Lab4.Forms
{
    /// <summary>
    /// Klasa okna głównego aplikacji
    /// </summary>
    public partial class FormMain : Form
    {
        private readonly BooksRepository booksRepository = new BooksRepository();
        /// <summary>
        /// Konstruktor okna głownego aplikacji
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //Ustawienie okna, żeby pojawiało się na środku ekranu
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Aktualizacja danych w DataGridViewBooks przy ładowaniu okna
            RefreshDataGridViewBooks();
        }

        /// <summary>
        /// Metoda wywoływana po naciśnięciu przycisku do dodawania nowej książki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddBook_Click(object sender, EventArgs e)
        {
            string title = textBoxBookTitle.Text;
            int yearOfPublish = int.Parse(textBoxYearOfPublication.Text);
            float price = float.Parse(textBoxPrice.Text);

            string genre = textBoxGenre.Text;
            string authorFirstName = textBoxFirstName.Text;
            string authorLastName = textBoxLastName.Text;

            bool isAdded = booksRepository.AddBook(title, yearOfPublish, price, genre, authorFirstName, authorLastName);

            if (isAdded)
            {
                RefreshDataGridViewBooks();
                ClearTextBoxes();
                labelLastAction.Text = "Dodano książkę";
            }
            else
            {
                MessageBox.Show("Ksiązka nie została dodana sprawdź poprawność paretremów");
            }
        }

        /// <summary>
        /// Metoda wywoływana po naciśnięciu przycisku do usuwania książki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteBook_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBoxId.Text);

            bool isDeleted = booksRepository.DeleteBook(id);

            if (isDeleted)
            {
                RefreshDataGridViewBooks();
                ClearTextBoxes();
                labelLastAction.Text = "Usunięto książkę";
            }
            else
            {
                MessageBox.Show("Ksiązka nie została usunięta sprawdź poprawność paretremów");
            }
        }

        /// <summary>
        /// Metoda wywoływana po naciśnięciu przycisku od czyszczenia TextBoxów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearTextBoxes_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            labelLastAction.Text = "Wyczyszczono pola";
        }

        /// <summary>
        /// Metoda wykonywana za każdym razem gdy użytkownik zmieni zaznaczenie wiersza w DataGridViewBook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            labelLastAction.Text = "Wybrano książkę";

            Book book = (Book)dataGridViewBooks.CurrentRow.DataBoundItem;
            if (book.Genre == null || book.Author == null)
            {
                return;
            } 
            //nadpisz dane zaznaczonym rekordem
            textBoxId.Text = book.Id.ToString();
            textBoxBookTitle.Text = book.Title;
            textBoxYearOfPublication.Text = book.YearOfPublish.ToString();
            textBoxPrice.Text = book.Price.ToString();

            textBoxGenre.Text = book.Genre.Name;
            textBoxFirstName.Text = book.Author.FirstName;
            textBoxLastName.Text = book.Author.LastName;

            labelLastAction.Text = "Wybrano książkę";
        }

        /// <summary>
        /// Metoda czyszcząca wszystkie TextBoxy w oknie głównym
        /// </summary>
        private void ClearTextBoxes()
        {
            textBoxId.Text = "";
            textBoxBookTitle.Text = "";
            textBoxYearOfPublication.Text = "";
            textBoxPrice.Text = "";
            textBoxGenre.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
        }

        /// <summary>
        /// Metoda odświeżająca dane w DataGridViewBooks
        /// </summary>
        private void RefreshDataGridViewBooks()
        {
            dataGridViewBooks.DataSource = booksRepository.GetBooks();
        }
    }
}
