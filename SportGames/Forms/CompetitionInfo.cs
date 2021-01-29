﻿using SportGames.Models;
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
    public partial class CompetitionInfo : Form
    {
        private int _competitionId;
        public CompetitionInfo(int competitionId)
        {
            InitializeComponent();
            _competitionId = competitionId;
        }

        public void OutputCompetition()
        {
            using (DataContext context = new DataContext())
            {
                var competition = context.Competitions
                    .FirstOrDefault(x => x.Id == _competitionId);

                if (competition == null)
                {
                    MessageBox.Show("Соревнование не найдено.");
                    var mainForm = new MainForm();
                    mainForm.Show();
                    this.Close();
                    return;
                }

                textBox2.Text = competition.Name;
                richTextBox1.Text = competition.Description;
                dateTimePicker1.Value = competition.BeginDate;

                textBox4.Text = competition.Housing.Name;
                textBox5.Text = competition.Housing.CostPerDay.ToString();
                richTextBox2.Text = competition.Housing.Description;

                textBox3.Text = competition.Diet.Name;
                richTextBox4.Text = competition.Diet.Description;

                foreach (var food in competition.Diet.DietFoods.Select(df => df.Food))
                {
                    listBox3.Items.Add(food);
                }

                textBox6.Text = competition.Transport.Name;
                textBox7.Text = competition.Transport.CostPerMan.ToString();
                richTextBox3.Text = competition.Transport.Description;

                foreach (var referee in competition.RefereeCompetitions.Select(rc => rc.Referee))
                {
                    listBox4.Items.Add(referee);
                }


                foreach(var discipline in competition.CompetitionDisciplines)
                {
                    listBox1.Items.Add(discipline);
                }
            }

            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            if (listBox1.SelectedIndex == -1) return;
            using(DataContext context = new DataContext())
            {
                var selectedDiscipline = (CompetitionDiscipline)listBox1.SelectedItem;
                var competition = context.Competitions.Find(_competitionId);

                selectedDiscipline = context.CompetitionDisciplines
                    .FirstOrDefault(cd => cd.Id == selectedDiscipline.Id);
                if(competition.EndDate != null)
                {
                    CompetitorDiscipline.OutputType = OutputType.Detailed;
                    foreach (var competitorDiscipline in selectedDiscipline.CompetitorDisciplines)
                    {
                        listBox2.Items.Add(competitorDiscipline);
                    }
                }
                else
                {
                    CompetitorDiscipline.OutputType = OutputType.DetailedWithPlaces;
                    var competitors = selectedDiscipline.CompetitorDisciplines.OrderBy(p => p.Place);
                    foreach (var competitorDiscipline in competitors)
                    {
                        listBox2.Items.Add(competitorDiscipline);
                    }

                }
                
            }
            
        } 

        private void CompetitionInfo_Load(object sender, EventArgs e)
        {
            OutputCompetition();
            dateTimePicker2.Enabled = false;
            using(DataContext context = new DataContext())
            {
                var competition = context.Competitions.Find(_competitionId);
                if (competition.EndDate != null)
                {
                    button1.Visible = false;
                    button2.Visible = false;
                    textBox1.Enabled = false;
                    label17.Visible = true;
                    label18.Visible = true;
                    dateTimePicker2.Enabled = true;
                    
                }
            }
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using(DataContext context = new DataContext())
            {
                var selectedCompetitor = (CompetitorDiscipline)listBox2.SelectedItem;
                selectedCompetitor = context.CompetitorDesciplines.Find(selectedCompetitor.Id);

                textBox1.Text = selectedCompetitor.Score.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var selectedCompetitor = (CompetitorDiscipline)listBox2.SelectedItem;
                selectedCompetitor = context.CompetitorDesciplines.Find(selectedCompetitor.Id);
                selectedCompetitor.Score = Convert.ToInt32(textBox1.Text);
                context.SaveChanges();
            }
        }

        //Завершение
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            CompetitionInfo info = new CompetitionInfo(_competitionId);

            info.Show();
            using(DataContext context = new DataContext())
            {
                var competition = context.Competitions.Find(_competitionId);
                dateTimePicker2.Value = competition.EndDate.Value;
                var competitors = context.CompetitorDesciplines;
                
                //прописать алгоритм вывода по местам.
                foreach(CompetitorDiscipline i in competitors.OrderBy(p => p.Place))
                {
                    listBox2.Items.Add(i);
                }
            }
        }

        public void UpdateCompletedCompetition()
        {

        }
    }
}