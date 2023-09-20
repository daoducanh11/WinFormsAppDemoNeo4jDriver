using WinFormsAppDemoNeo4jDriver.DAO;
using WinFormsAppDemoNeo4jDriver.Entities;

namespace WinFormsAppDemoNeo4jDriver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await DataProvider.Instance.Search("");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await DataProvider.Instance.Search(textBox1.Text.Trim());
            label1.Text = "Chưa chọn bộ phim nào";
            richTextBox1.Text = "";
            button2.Enabled = false;
        }

        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            label1.Text = row.Cells[0].Value.ToString();
            Movie movie = await DataProvider.Instance.FindByTitle(row.Cells[0].Value.ToString());
            string contentMovie = "";
            foreach (Person person in movie.Cast)
            {
                contentMovie += "- " + person.Name + " " + person.Job + "\n";
            }
            richTextBox1.Text = contentMovie;
            button2.Enabled = true;
            button4.Enabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DataProvider.Instance.VoteByTitle(label1.Text);
            dataGridView1.DataSource = await DataProvider.Instance.Search(textBox1.Text.Trim());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAddMovie frm = new FormAddMovie();
            frm.ShowDialog();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await DataProvider.Instance.DeleteMovie(label1.Text);
            dataGridView1.DataSource = await DataProvider.Instance.Search(textBox1.Text.Trim());
            button4.Enabled = false;
            label1.Text = "Chưa chọn bộ phim nào";
            richTextBox1.Text = "";
            button2.Enabled = false;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await DataProvider.Instance.FetchD3Graph(50);
        }
    }
}