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
using System.Data.Entity;

namespace SportGames.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }
        #region Page1
        public void UpdateCompetitions(string word = null)
        {
            listBox1.Items.Clear();

            using (DataContext context = new DataContext())
            {
                List<Competition> competitions;
                if (word != null)
                {
                    competitions = context.Competitions.Where(n => n.Name.ToLower().Contains(word.ToLower())).ToList();          
                }
                else
                {
                    competitions = context.Competitions.ToList();
                }

                
                foreach (var c in competitions)
                {
                    listBox1.Items.Add(c);
                }

                context.SaveChanges();
            }
        }

        //  Просмотр
        private void button20_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            var competition = (Competition)listBox1.SelectedItem;
            this.Hide();
            CompetitionInfo info = new CompetitionInfo(competition.Id);
            info.ShowDialog();
            this.Show();
        }

        //  Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCompetition add = new AddCompetition();
            add.ShowDialog();
            this.Show();
        }
        #endregion

        #region Page2
        public void UpdateSportsmans()
        {
            listBox2.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Sportsmans.ToList();

                foreach (var s in ls)
                {
                    listBox2.Items.Add(s);
                }

                context.SaveChanges();
            }
        }

        public void SportsmanDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var sportsman = (Sportsman)listBox2.SelectedItem;
                if (listBox2.SelectedIndex == -1) return;
                context.Sportsmans.Remove(context.Sportsmans.Find(sportsman.Id));
                context.SaveChanges();
            }
            UpdateSportsmans();
        }

        public void SportsmanAdd(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text)
                    && !string.IsNullOrEmpty(textBox3.Text))
                {
                    using (DataContext context = new DataContext())
                    {
                        Sportsman sportsman = new Sportsman();
                        sportsman.Name = textBox2.Text;
                        sportsman.Phone = textBox3.Text;
                        sportsman.TeamId = ((Team)comboBox1.SelectedItem).Id;
                        context.Sportsmans.Add(sportsman);
                        context.SaveChanges();
                    }
                    UpdateSportsmans();
                    textBox2.Text = null;
                    textBox3.Text = null;
                    comboBox1.SelectedItem = null;
                }
                else MessageBox.Show("Не все поля заполнены");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " + ex.Message);
            }

        }

        //Вывод выбранного спортсмена
        public void SportsmanOutput(object sender, EventArgs e)
        {
            var sportsman = (Sportsman)listBox2.SelectedItem;

            textBox2.Text = sportsman.Name;
            textBox3.Text = sportsman.Phone;
            comboBox1.SelectedIndex = sportsman.TeamId;
        }

        public void UpdateTeams()
        {
            listBox3.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Teams.ToList();

                foreach (var t in ls)
                {
                    listBox3.Items.Add(t);
                }

                comboBox1.Items.Clear();
                foreach (var item in context.Teams)
                {
                    comboBox1.Items.Add(item);
                }

                context.SaveChanges();
            }
        }

        public void TeamAdd(object sender, EventArgs e)
        {
            Team team = new Team();
            if (!string.IsNullOrEmpty(textBox4.Text)
                && !string.IsNullOrEmpty(textBox5.Text))
            {
                using (DataContext context = new DataContext())
                {
                    team.Name = textBox4.Text;
                    team.Country = textBox5.Text;
                    context.Teams.Add(team);
                    context.SaveChanges();
                    UpdateTeams();
                }
                textBox4.Text = null;
                textBox5.Text = null;
            }
            else MessageBox.Show("Не все поля заполнены");
        }

        //Вывод выбранной команды
        public void TeamOutput(object sender, EventArgs e)
        {
            var team = (Team)listBox3.SelectedItem;
            textBox4.Text = team.Name;
            textBox5.Text = team.Country;
        }

        public void TeamDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var team = (Team)listBox3.SelectedItem;

                if (listBox3.SelectedIndex == -1) return;
                context.Teams.Remove(context.Teams.Find(team.Id));
                context.SaveChanges();
            }
            UpdateTeams();
        }
        #endregion

        #region Page3
        public void UpdateDiets()
        {
            listBox4.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Diets.ToList();

                foreach (var d in ls)
                {
                    listBox4.Items.Add(d);
                }

                context.SaveChanges();
            }
        }

        public void DietAdd(object sender, EventArgs e)
        {
            Diet feed = new Diet();
            if (!string.IsNullOrEmpty(textBox9.Text)
                && !string.IsNullOrEmpty(richTextBox1.Text))
            {
                using (DataContext context = new DataContext())
                {
                    feed.Name = textBox9.Text;
                    feed.Description = richTextBox1.Text;
                    context.Diets.Add(feed);
                    foreach (Food i in listBox6.Items)
                    {
                        context.DietFoods.Add(new DietFood { DietId = feed.Id, FoodId = i.Id });
                    }
                    context.SaveChanges();
                }
                UpdateDiets();
                listBox6.Items.Clear();
                textBox9.Text = null;
                richTextBox1.Text = null;
            }
            else MessageBox.Show("Не все поля заполнены");
        }

        public void FeedOutput(object sender, EventArgs e)
        {
            listBox6.Items.Clear();

            using (DataContext db = new DataContext())
            {
                var diet = (Diet)listBox4.SelectedItem;
                var dietFoods = db.DietFoods
                    .Include(f => f.Diet)
                    .Include(f => f.Food)
                    .Where(f => f.DietId == diet.Id)
                    .ToList();
                
                textBox9.Text = diet.Name;
                richTextBox1.Text = diet.Description;

                foreach (DietFood df in dietFoods)
                {
                    listBox6.Items.Add(df);
                }   
            }
        }

        public void DietDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var feed = (Diet)listBox4.SelectedItem;

                if (listBox4.SelectedIndex == -1) return;
                context.Diets.Remove(context.Diets.Find(feed.Id));
                context.SaveChanges();
            }
            UpdateDiets();
        }

        public void UpdateFoods()
        {
            listBox5.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Foods.ToList();

                foreach (var f in ls)
                {
                    listBox5.Items.Add(f);
                }

                context.SaveChanges();
            }
        }
        public void FoodAdd(object sender, EventArgs e)
        {
            try
            {
                Food food = new Food();
                if (!string.IsNullOrEmpty(textBox6.Text)
                    && !string.IsNullOrEmpty(textBox7.Text)
                    && !string.IsNullOrEmpty(textBox8.Text))
                {
                    using (DataContext context = new DataContext())
                    {
                        food.Name = textBox6.Text;
                        food.Cost = Convert.ToDecimal(textBox7.Text);
                        food.Size = Convert.ToDecimal(textBox8.Text);
                        context.Foods.Add(food);
                        context.SaveChanges();
                        UpdateFoods();
                        textBox6.Text = null;
                        textBox7.Text = null;
                        textBox8.Text = null;
                    }
                }
                else MessageBox.Show("Не все поля заполнены");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FoodOutput(object sender, EventArgs e)
        {
            var food = listBox5.SelectedItem as Food;
            if (food == null) return; 

            textBox6.Text = food.Name;
            textBox7.Text = food.Cost.ToString();
            textBox8.Text = food.Size.ToString();
        }
        public void FoodDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var food = (Food)listBox5.SelectedItem;

                if (listBox5.SelectedIndex == -1) return;
                context.Foods.Remove(context.Foods.Find(food.Id));
                context.SaveChanges();
            }
            UpdateFoods();
        }

        //влево(Блюда рациона)
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (Food)listBox5.SelectedItem;
                listBox6.Items.Add(item);
            }
            catch
            {
                MessageBox.Show("Выберите элемент");
            }
        }
        //вправо(все блюда)
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (Food)listBox6.SelectedItem;
                listBox5.Items.Add(item);
                listBox6.Items.Remove(item);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Page4
        public void UpdateTransports()
        {
            listBox8.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Transports.ToList();

                foreach (var t in ls)
                {
                    listBox8.Items.Add(t);
                }

                context.SaveChanges();
            }
        }

        public void TransportAdd(object sender, EventArgs e)
        {
            Transport transport = new Transport();
            try
            {
                if (string.IsNullOrEmpty(textBox12.Text)
                || string.IsNullOrEmpty(textBox13.Text)
                || string.IsNullOrEmpty(richTextBox3.Text))
                {
                    MessageBox.Show("Не все поля заполнены");
                    return;
                }

                using (DataContext context = new DataContext())
                {
                    transport.Name = textBox13.Text;
                    transport.CostPerMan = Convert.ToDecimal(textBox12.Text);
                    transport.Description = richTextBox3.Text;
                    context.Transports.Add(transport);
                    context.SaveChanges();
                    UpdateTransports();
                }

                textBox12.Text = null;
                textBox13.Text = null;
                richTextBox3.Text = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        public void TransportOutput(object sender, EventArgs e)
        {
            var transport = (Transport)listBox8.SelectedItem;
            textBox13.Text = transport.Name;
            textBox12.Text = transport.CostPerMan.ToString();
            richTextBox3.Text = transport.Description;
        }

        public void TransportDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var transport = (Transport)listBox8.SelectedItem;

                if (listBox8.SelectedIndex == -1) return;
                context.Transports.Remove(context.Transports.Find(transport.Id));
                context.SaveChanges();
            }
            UpdateTransports();
        }

        public void UpdateHousings()
        {
            listBox9.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Housings.ToList();

                foreach (var h in ls)
                {
                    listBox9.Items.Add(h);
                }

                context.SaveChanges();
            }
        }

        public void HousingAdd(object sender, EventArgs e)
        {
            Housing housing = new Housing();
            try
            {
                if (!string.IsNullOrEmpty(textBox10.Text)
                && !string.IsNullOrEmpty(textBox11.Text)
                && !string.IsNullOrEmpty(richTextBox2.Text))
                {
                    using (DataContext context = new DataContext())
                    {
                        housing.Name = textBox10.Text;
                        housing.CostPerDay = Convert.ToDecimal(textBox11.Text);
                        housing.Description = richTextBox2.Text;
                        context.Housings.Add(housing);
                        context.SaveChanges();
                        UpdateHousings();
                    }

                    textBox10.Text = null;
                    textBox11.Text = null;
                    richTextBox2.Text = null;
                }
                else MessageBox.Show("Не все поля заполнены");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        public void HousingOutput(object sender, EventArgs e)
        {
            var housing = (Housing)listBox9.SelectedItem;
            textBox10.Text = housing.Name;
            textBox11.Text = housing.CostPerDay.ToString();
            richTextBox2.Text = housing.Description;
        }

        public void HousingDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var housing = (Housing)listBox9.SelectedItem;

                if (listBox9.SelectedIndex == -1) return;
                context.Housings.Remove(context.Housings.Find(housing.Id));
                context.SaveChanges();
            }
            UpdateHousings();
        }
        #endregion

        #region Page5
        public void UpdateReferees()
        {
            listBox7.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Referees.ToList();

                foreach (var r in ls)
                {
                    listBox7.Items.Add(r);
                }

                context.SaveChanges();
            }
        }

        public void RefereeAdd(object sender, EventArgs e)
        {
            Referee referee = new Referee();
            if (!string.IsNullOrEmpty(textBox14.Text))
            {
                using (DataContext context = new DataContext())
                {
                    referee.Name = textBox14.Text;
                    context.Referees.Add(referee);
                    context.SaveChanges();
                }
                UpdateReferees();
                textBox14.Text = null;
            }
            else MessageBox.Show("Не все поля были заполнены");
        }

        public void RefereeOutput(object sender, EventArgs e)
        {
            var referee = (Referee)listBox7.SelectedItem;
            textBox14.Text = referee.Name;
        }

        public void RefereeDelete(object sender, EventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var referee = (Referee)listBox7.SelectedItem;

                if (listBox7.SelectedIndex == -1) return;
                context.Referees.Remove(context.Referees.Find(referee.Id));
                context.SaveChanges();
            }
            UpdateReferees();
        }
        #endregion

        #region Page6
        public void UpdateDisciplines()
        {
            listBox10.Items.Clear();

            using (DataContext context = new DataContext())
            {
                var ls = context.Disciplines.ToList();

                foreach (var d in ls)
                {
                    listBox10.Items.Add(d);
                }

                context.SaveChanges();
            }
        }

        public void DisciplineAdd(object sender, EventArgs e)
        {

            if(!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrEmpty(richTextBox4.Text))
            {
                using (DataContext context = new DataContext())
                {
                    Discipline discipline = new Discipline();
                    discipline.Name = textBox15.Text;
                    discipline.Description = richTextBox4.Text;
                    context.Disciplines.Add(discipline);
                    context.SaveChanges();
                }
                UpdateDisciplines();
                textBox15.Text = null;
                richTextBox4.Text = null;
            }
        }

        public void DisciplineDelete(object sender, EventArgs e)
        {
            using(DataContext context = new DataContext())
            {
                var ls = (Discipline)listBox10.SelectedItem;
                if (listBox10.SelectedIndex == -1) return;
                context.Disciplines.Remove(context.Disciplines.Find(ls.Id));
                context.SaveChanges();
            }
            UpdateDisciplines();
        }

        public void OutputDiscipline(object sender, EventArgs e)
        {
            var discipline = listBox10.SelectedItem as Discipline;
            if (discipline == null) return;
            textBox15.Text = discipline.Name;
            richTextBox4.Text = discipline.Description;
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Page1
            UpdateCompetitions();
            //Page2
            UpdateSportsmans();
            UpdateTeams();
            //Page3
            UpdateDiets();
            //UpdateDietFood();
            UpdateFoods();
            //Page4
            UpdateTransports();
            UpdateHousings();
            //Page5
            UpdateReferees();
            //Page6
            UpdateDisciplines();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateCompetitions(textBox1.Text);
        }
    }
}
