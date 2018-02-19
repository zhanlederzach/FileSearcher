using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace Searcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilesSearcher fs = new FilesSearcher();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = String.Empty;
            Image beat = Image.FromFile(@"D:\beat3.gif");

            pictureBox1.Image = beat;
            groupBox1.Hide();
            closeButton.Image = Image.FromFile(@"D:\close.png");

            Width = 480;

            stylize(closeButton);
            stylize(search);
            stylize(dirPicker);

            advus.TabStop = false;
            advus.FlatStyle = FlatStyle.Flat;
            advus.FlatAppearance.BorderSize = 0;
            advus.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            groupBox1.Text = "Advanced options";
            groupBox1.ForeColor = Color.Red;

            textBox1.BackColor = Color.FromArgb(255,32,32,32);
            textBox1.ForeColor = Color.White;

            listBox1.BackColor = Color.FromArgb(255, 32, 32, 32);
            listBox1.ForeColor = Color.White;

            path.Text = @"C:\work\test";
        }


        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            // p.Graphics.Clear(SystemColors.Control);
            p.Graphics.DrawString(box.Text, box.Font, Brushes.Black, 0, 0);
        }

        private void stylize(Button button)
        {
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void advus_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (groupBox1.Visible)
            {
                // Usual
                groupBox1.Hide();
                btn.Text = "Advanced";
                Width = 480;
                closeButton.Location = new Point(415, 0);
            }
            else
            {
                // Advanced
                Width = 810;
                groupBox1.Show();
                btn.Text = "Usual";
                closeButton.Location = new Point(745, 0);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void search_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string name = textBox1.Text;
            Options option = new Options();
            if (name.Length>0)
            {
                Hearts h = new Hearts();
                h.Add(pictureBox2);
                h.Add(pictureBox3);
                h.Add(pictureBox5);
                Thread t = new Thread(h.Go);
                t.Start();
                option.setExpression(name);
                if (groupBox1.Visible)
                {
                    option.setPath(path.Text)
                        .setRegExp(usingRegexp.Checked)
                        .setSearchContents(byContents.Checked)
                        .setStart(start.Text)
                        .setEnd(end.Text);
                }

                foreach (string s in fs.Search(option))
                {
                    listBox1.Items.Add(s);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dirPicker_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    path.Text = fbd.SelectedPath;
                }
            }
        }
        
    }
}
