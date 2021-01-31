using SportGames.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportGames.Forms
{
    public partial class AddCompetition : Form
    {

        public AddCompetition()
        {
            InitializeComponent();
        }

        #region Page1
        public void CompetitionAdd(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 3)
            {
                tabControl1.SelectedIndex++;
                return;
            }
            using (DataContext context = new DataContext())
            {
                Competition competition = new Competition();
                if (!string.IsNullOrEmpty(textBox1.Text)
                    && !string.IsNullOrEmpty(textBox2.Text)
                    && !string.IsNullOrEmpty(richTextBox1.Text)
                    && dateTimePicker1.Checked)
                {
                    competition.Name = textBox1.Text;
                    competition.Location = textBox2.Text;
                    competition.PrizeFund = Convert.ToDecimal(textBox3.Text);
                    competition.Description = richTextBox1.Text;
                    competition.BeginDate = dateTimePicker1.Value;

                    if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null
                        || comboBox3.SelectedItem == null) return;
                    var dietFeed = (Diet)comboBox3.SelectedItem;
                    competition.DietId = dietFeed.Id;

                    var transport = (Transport)comboBox2.SelectedItem;
                    competition.TransportId = transport.Id;

                    var housing = (Housing)comboBox1.SelectedItem;
                    competition.HousingId = housing.Id;

                    foreach (Sportsman i in listBox2.Items)
                    {
                        Competitor competitor = new Competitor();
                        competitor.SportsmanId = i.Id;
                        competitor.CompetitionId = competition.Id;
                        context.Competitors.Add(competitor);
                    }

                    foreach (Discipline i in listBox6.Items)
                    {
                        CompetitionDiscipline cd = new CompetitionDiscipline();
                        cd.DisciplineId = i.Id;
                        cd.CompetitionId = competition.Id;
                        context.CompetitionDisciplines.Add(cd);
                    }

                    foreach (Referee r in listBox4.Items)
                    {
                        RefereeCompetition rc = new RefereeCompetition();
                        rc.RefereeId = r.Id;
                        rc.CompetitionId = competition.Id;
                        context.RefereeCompetitions.Add(rc);
                    }

                    context.Competitions.Add(competition);
                    context.SaveChanges();
                    this.Close();
                    AddCompetition1 formAdd2 = new AddCompetition1(competition);
                    formAdd2.ShowDialog();
                }
            }

        }
        #endregion

        #region Page2
        public void UpdateSportsmans()
        {
            listBox1.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Sportsmans.ToList();

                foreach (var s in ls)
                {
                    listBox1.Items.Add(s);
                }

            }
        }

        public void UpdateReferees()
        {
            listBox3.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Referees.ToList();

                foreach (var r in ls)
                {
                    listBox3.Items.Add(r);
                }

                context.SaveChanges();
            }
        }

        //  вправо спортсменов
        private void button2_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem as Sportsman;
            if (item == null) return;
            listBox2.Items.Add(item);
            listBox1.Items.Remove(item);
        }

        //  влево спортсменов
        private void button3_Click(object sender, EventArgs e)
        {
            var item = listBox2.SelectedItem as Sportsman;
            if (item == null) return;
            listBox1.Items.Add(item);
            listBox2.Items.Remove(item);
        }

        //  вправо судей
        private void button4_Click(object sender, EventArgs e)
        {
            var item = listBox3.SelectedItem as Referee;
            if (item == null) return;
            listBox4.Items.Add(item);
            listBox3.Items.Remove(item);
        }

        //  влево судей
        private void button5_Click(object sender, EventArgs e)
        {
            var item = listBox4.SelectedItem as Referee;
            if (item == null) return;
            listBox3.Items.Add(item);
            listBox4.Items.Remove(item);
        }
        #endregion

        #region Page3

        public void Output()
        {
            using (DataContext context = new DataContext())
            {
                foreach (var item in context.Housings)
                {
                    comboBox1.Items.Add(item);
                }

                foreach (var item in context.Transports)
                {
                    comboBox2.Items.Add(item);
                }

                foreach (var item in context.Diets)
                {
                    comboBox3.Items.Add(item);

                }
            }
        }
        #endregion

        #region Page4
        public void UpdateDisciplines()
        {
            listBox5.Items.Clear();
            using (DataContext context = new DataContext())
            {
                var ls = context.Disciplines.ToList();

                foreach (var d in ls)
                {
                    listBox5.Items.Add(d);
                }

                context.SaveChanges();
            }
        }
        //Вправо дисциплины
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (Discipline)listBox5.SelectedItem;
                listBox6.Items.Add(item);
                listBox5.Items.Remove(item);
            }
            catch
            {
                MessageBox.Show("Выберите элемент");
            }
        }
        //Влево дисциплины
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (Discipline)listBox6.SelectedItem;
                listBox5.Items.Add(item);
                listBox6.Items.Remove(item);
            }
            catch
            {
                MessageBox.Show("Выберите элемент");
            }
        }
        #endregion

        private void AddCompetition_Load(object sender, EventArgs e)
        {
            UpdateSportsmans();
            UpdateReferees();
            Output();
            UpdateDisciplines();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
            {
                tabControl1.SelectedIndex--;
            }
            else
            {
                this.Close();
            }
        }
    }
}
