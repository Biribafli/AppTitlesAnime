using AppTitlesAnime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppContext = AppTitlesAnime.Models.AppContext;

namespace AppTitlesAnime
{
    public partial class FormListGenres : Form
    {
        private AppContext db;
        public FormListGenres()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);
            this.db = new AppContext();
            //Загружает все сущности 'Type' из базы данных в локальный кэш DbContext.
            //Это важно для эффективной работы привязки данных.
            this.db.Genres.Load();
            this.dataGridViewGenres.DataSource = this.db.Types.Local.OrderBy(o => o.TypeName).ToList();

            //скрытие столбцов
            dataGridViewGenres.Columns["Id"].Visible = false;
            dataGridViewGenres.Columns["AnimeGenres"].Visible = false;

            // изменение названий заголовков столбца 
            dataGridViewGenres.Columns["GenreName"].HeaderText = "Жанр аниме";
        }

        //private void btnAddGenre_Click(object sender, EventArgs e)
        //{
        //    FormAddGenre formAddGenre = new();
        //    DialogResult result = formAddGenre.ShowDialog(this);

        //    if (result == DialogResult.Cancel)
        //        return;

        //    Genre genre = new Genre();
        //    // Назначает текст из элемента управления «textBoxTypeName»
        //    //// формы «FormAddType» свойству «TypeName» сущности «Type».
        //    //genre.GenreName = formAddGenre.textBoxGenreName.Text;

        //    db.Genres.Add(genre);
        //    db.SaveChanges();
        //}

        private void btnDeleteGenre_Click(object sender, EventArgs e)
        {
            if (dataGridViewGenres.SelectedRows.Count == 0)
                return;

            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить объект? \nВсе связанные данные будут удалены.",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

            if (result == DialogResult.No)
                return;

            int index = dataGridViewGenres.SelectedRows[0].Index;
            short id = 0;
            bool converted = Int16.TryParse(dataGridViewGenres[0, index].Value.ToString(), out id);
            if (!converted)
                return;
            Genre genre = new Genre();


            db.Genres.Remove(genre);
            db.SaveChanges();

            MessageBox.Show("Объект удалён");

            this.dataGridViewGenres.DataSource = this.db.Types.Local.OrderBy(o => o.TypeName).ToList();
        }
    }
}
