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
    public partial class AddCompetition1 : Form
    {
        Competition _competition;
        public AddCompetition1(Competition competition)
        {
            InitializeComponent();
            this._competition = competition;
        }

        public void UpdateCompetitors()
        {
            listBox1.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Competitors.ToList();
                

                foreach (var c in ls.Where(competitor => competitor.CompetitionId == _competition.Id))
                {
                    listBox1.Items.Add(c);
                }
            }
        }
        public void UpdateCompetitionDisciplines()
        {
            listBox2.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.CompetitionDisciplines.ToList();
                

                foreach (CompetitionDiscipline c in ls.Where(d => _competition.Id == d.CompetitionId && d.DisciplineId == d.Discipline.Id))
                {
                    listBox2.Items.Add(c);
                }
            }
        }
        public void UpdateCompetitorDisciplines()
        {
            listBox3.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var competition = context.Competitions
                    .FirstOrDefault(x => x.Id == _competition.Id);

                foreach (var discipline in competition.CompetitionDisciplines)
                    foreach(var competitor in discipline.CompetitorDisciplines)
                        listBox3.Items.Add(competitor);
              
            } 
        }
        public void CompetitorDisciplineDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var cd = (CompetitorDiscipline)listBox3.SelectedItem;

                context.CompetitorDesciplines.Remove(context.CompetitorDesciplines.Find(cd.Id));
                context.SaveChanges();
            }
            UpdateCompetitorDisciplines();
        }
        public void CompetitorDisciplineAdd(object sender, EventArgs e)
        {
           
            using (DataContext context = new DataContext())
            {
                CompetitorDiscipline competitorDiscipline = new CompetitorDiscipline();
                var competitor = (Competitor)listBox1.SelectedItem;
                var discipline = (CompetitionDiscipline)listBox2.SelectedItem;
                competitorDiscipline.CompetitorId = competitor.Id;
                competitorDiscipline.CompetitionDisciplineId = discipline.Id;

                if (context.CompetitorDesciplines
                    .FirstOrDefault(cd =>
                    cd.CompetitorId == competitorDiscipline.CompetitorId &&
                    cd.CompetitionDisciplineId == competitorDiscipline.CompetitionDisciplineId) != null)
                {
                    MessageBox.Show("Выбранный спортсмен уже является участником выбранной дисциплины.");
                    return;
                }

                context.CompetitorDesciplines.Add(competitorDiscipline);
                context.SaveChanges();
                UpdateCompetitorDisciplines();
            }   
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.ShowDialog();
            this.Show();
        }

        private void AddCompetition1_Load(object sender, EventArgs e)
        {
            UpdateCompetitors();
            UpdateCompetitionDisciplines();
            UpdateCompetitorDisciplines();
            CompetitorDiscipline.OutputType = OutputType.AddingForm;
        }
    }
}
