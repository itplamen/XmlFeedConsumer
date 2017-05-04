namespace XmlFeedConsumer.Web.App_Start.NinjectModules
{
    using System.Data.Entity;

    using Ninject.Extensions.Conventions;
    using Ninject.Modules;

    using Common;
    using Data;
    using Data.Common;

    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(DbContext)).To(typeof(XmlFeedConsumerDbContext));
            this.Bind(typeof(IDbRepository<>)).To(typeof(DbRepository<>));

            this.Bind(b => b.From(Assemblies.DataServices)
                            .SelectAllClasses()
                            .BindDefaultInterface());

            this.Bind(b => b.From(Assemblies.UtilsServices)
                            .SelectAllClasses()
                            .BindDefaultInterface());
        }
    }
}