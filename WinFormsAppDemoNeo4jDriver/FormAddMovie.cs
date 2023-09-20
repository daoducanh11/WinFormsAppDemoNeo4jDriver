using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsAppDemoNeo4jDriver.DAO;
using WinFormsAppDemoNeo4jDriver.Entities;

namespace WinFormsAppDemoNeo4jDriver
{
    public partial class FormAddMovie : Form
    {
        public FormAddMovie()
        {
            InitializeComponent();
        }

        private async void FormAddMovie_Load(object sender, EventArgs e)
        {
            List<Person> lst = await DataProvider.Instance.GetPersons();
            PersonCol.Items.AddRange(lst.Select(x => x.Name).ToArray());
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<Person> lst = new List<Person>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                    lst.Add(new Person(row.Cells[0].Value.ToString(), "", row.Cells[1].Value?.ToString()));
            }
            Movie movie = new Movie(textBoxTitle.Text.Trim(), textBoxTagline.Text.Trim(), long.Parse(textBoxReleased.Text), 0);
            movie.Cast = lst;

            await DataProvider.Instance.AddMovie(movie);
            this.Close();
        }
    }
}
