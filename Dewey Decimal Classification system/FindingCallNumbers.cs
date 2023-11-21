using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Dewey_Decimal_Classification_system
{
    public partial class FindingCallNumbers : Form
    {
        public string toDisplay;
        public int score = 0;

        public FindingCallNumbers()
        {
            InitializeComponent();

            // loads the questionnair onto the form
            loadingData();

        }
        // this module creates the questions and the answers.
        public void loadingData()
        {
            List<string> options = new List<string>();
            List<string> questions = new List<string>();

            var callNumberFile = File.ReadAllLines("CallNumbers.txt");
            foreach (var l in callNumberFile)
            {
                if (l.Contains("000") || l.Contains("100") || l.Contains("200") || l.Contains("300")
                    || l.Contains("400") || l.Contains("500") || l.Contains("600") || l.Contains("700")
                    || l.Contains("800") || l.Contains("900"))
                {
                    options.Add(l);
                }
                else if (l != "" && l != " ")
                {
                    questions.Add(l);
                }
            }

            Random r = new Random();

            int ind = r.Next(questions.Count);

            toDisplay = questions[ind];
            string result = toDisplay.Remove(0, 4);

            questionLbl.Text = result;

            for (int i = 0; i < 3; i++)
            {
                int rr = r.Next(options.Count);
                comboBox1.Items.Add(options[rr]);
            }
            comboBox1.Items.Add(options[int.Parse(toDisplay.Substring(0, 1))]);
        }
        // submit button that processes the users answer.
        private void button1_Click(object sender, EventArgs e)
        {
            int q;
            int a;

            string qu = toDisplay.Substring(0, 3);

            string selected = comboBox1.SelectedItem.ToString();
            string an = selected.Substring(0,3);

            a = int.Parse(an);
            q = int.Parse(qu);

            int range = a + 99;
            //checks if the answer is correct and awards point or deduct points based on the result.
            if (a <= q && q <= range)
            {
                SubmitBtn.ForeColor = Color.Green;
                MessageLbl.Text = " Awesome! You have chosen the Correct Answer.";
                MessageLbl.ForeColor = Color.Green;

                score = score + 50;

                ScoreLbl.Text = score.ToString();
            }
            else
            {
                SubmitBtn.ForeColor = Color.Red;
                MessageLbl.Text = "Oops! You have chosen the inccorect answer, try again.";
                MessageLbl.ForeColor = Color.Red;

                score = score - 50;

                ScoreLbl.Text = score.ToString();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubmitBtn.ForeColor = Color.Black;
            MessageLbl.ForeColor = Color.Black;
            MessageLbl.Text = "";

            comboBox1.Text = "Select the correct answer below";
            comboBox1.Items.Clear(); 
            loadingData();
        }

        private void FindingCallNumbers_Load(object sender, EventArgs e)
        {

        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            TasksForm tf = new TasksForm();
            tf.Show();
            this.Close();
        }
    }
}
