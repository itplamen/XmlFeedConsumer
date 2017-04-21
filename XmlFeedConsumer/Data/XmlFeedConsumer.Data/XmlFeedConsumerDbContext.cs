namespace XmlFeedConsumer.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Common.Models;
    using Models;

    public class XmlFeedConsumerDbContext : DbContext
    {
        public XmlFeedConsumerDbContext()
            : base("DefaultConnection")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public virtual IDbSet<Odd> Odds { get; set; }

        public virtual IDbSet<Bet> Bets { get; set; }

        public virtual IDbSet<Match> Matches { get; set; }
         
        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Sport> Sports { get; set; }

        public virtual IDbSet<XmlSport> XmlSport { get; set; }

        public static XmlFeedConsumerDbContext Create()
        {
            return new XmlFeedConsumerDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
