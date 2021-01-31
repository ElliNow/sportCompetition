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
    public partial class CostForm : Form
    {
        private readonly int _competitionId;
        public CostForm(int competition)
        {
            _competitionId = competition;
            InitializeComponent();
        }

        private void CostForm_Load(object sender, EventArgs e)
        {
            
            using (DataContext context = new DataContext())
            {
                var competition = context.Competitions.Find(_competitionId);
                var amountCompetitors = competition.Competitors.Count;
                decimal transportTotal = competition.Transport.CostPerMan * competition.Competitors.Count;
                decimal costDiet = 0;
                decimal housingtotal = competition.Housing.CostPerDay * competition.AmountDays * competition.Competitors.Count;

                label1.Text = $"Название соревнования: {competition.Name}";

                rtb.Text = String.Empty;

                rtb.Text += $"Призовой фонд: {competition.PrizeFund} \n\n";

                rtb.Text += "Транспорт \n";
                rtb.Text += $"Вид транспорта: {competition.Transport.Name} \n";
                rtb.Text += $"Кол-во спортсменов: {amountCompetitors} \n";
                rtb.Text += $"Стоимость на 1 спортсмена: {competition.Transport.CostPerMan} \n";
                rtb.Text += $"_________________________________________\n";
                rtb.Text += $"Итого: {transportTotal} \n\n";

                rtb.Text += "Питание \n";
                rtb.Text += $"Название рациона: {competition.Diet.Name} \n";
                rtb.Text += $"Кол-во спортсменов: {amountCompetitors} \n";
                rtb.Text += "Позиции меню (стоимость на 1 спортсмена): \n";
                foreach (Food food in competition.Diet.DietFoods.Select(df => df.Food))
                {
                    costDiet += food.Cost;
                    rtb.Text += $"    {food.Name} - {food.Cost}\n";
                }
                rtb.Text += $"_________________________________________\n";
                rtb.Text += $"Итого на 1 спортсмена: {costDiet} \n";
                rtb.Text += $"Итого: {costDiet = costDiet * amountCompetitors} \n\n";

                rtb.Text += "Проживание \n";
                rtb.Text += $"Вид жилья: {competition.Housing.Name} \n";
                rtb.Text += $"Стоимость в день на 1 спортсмена: {competition.Housing.CostPerDay} \n";
                rtb.Text += $"Кол-во спортсменов: {amountCompetitors} \n";
                rtb.Text += $"Кол-во дней: {competition.AmountDays} \n";
                rtb.Text += $"_________________________________________\n";
                rtb.Text += $"Итого: {housingtotal} \n";

                dateTimePicker1.Value = competition.BeginDate.Date;
                dateTimePicker2.Value = competition.EndDate.Value;
                label7.Text = $"(Кол-во дней: {competition.AmountDays})";

                
                label3.Text = $"Итого: {Math.Round(competition.PrizeFund + costDiet + transportTotal + housingtotal , 2)}";

            }
        }
    }
}
