using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Sportsman> Sportsmans { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<CompetitionDiscipline> CompetitionDisciplines { get; set; }
        public DbSet<CompetitorDiscipline> CompetitorDesciplines { get; set; }
        public DbSet<RefereeCompetition> RefereeCompetitions { get; set; }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<DietFood> DietFoods { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Food> Foods { get; set; }

        public DataContext()
            : base("sportgames") { }
    }
}
