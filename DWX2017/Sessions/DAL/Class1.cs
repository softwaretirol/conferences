using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using PRISMDWX;

namespace DAL
{
    public class DALModule : IModule
    {
        private readonly ILoggerFacade logger;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public DALModule(ILoggerFacade logger, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.logger = logger;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(DALView));

            logger.Log("Hello DAL Module", Category.Exception, Priority.None);

            eventAggregator.GetEvent<PubSubEvent<ISplashScreenProgressMessage>>()
                .Publish(new DALMessage("DAL wird initialisiert..."));
            Thread.Sleep(10000);
        }
    }

    public class DALMessage : ISplashScreenProgressMessage
    {
        public DALMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    internal class Speaker : ISpeaker
    {
        public string Name { get; set; }
    }

    internal class SpeakerRepository : ISpeakerRepository
    {
        public IEnumerable<ISpeaker> Get()
        {
            yield return new Speaker() {Name = "Christian"};
            yield return new Speaker() {Name = "Christian"};
            yield return new Speaker() {Name = "Christian"};
            yield return new Speaker() {Name = "Christian"};
            yield return new Speaker() {Name = "Christian"};
        }
    }
}
