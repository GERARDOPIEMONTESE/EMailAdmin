using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class EMailProcessStrategyContainer
    {
        #region Singleton

        private IDictionary<string, AbstractEMailProcess> _Strategies;

        private static EMailProcessStrategyContainer _Instance;

        private EMailProcessStrategyContainer()
        {
            Init();
        }

        public static EMailProcessStrategyContainer Instance()
        {
            if (_Instance == null)
            {
                _Instance = new EMailProcessStrategyContainer();
            }
            return _Instance;
        }

        private void Add(AbstractEMailProcess emailProcess)
        {
            _Strategies.Add(emailProcess.GetTypeCode(), emailProcess);
        }

        private void Init()
        {
            _Strategies = new Dictionary<string, AbstractEMailProcess>();
            Add(new AlertaXAMCasosProcess());
            //Add(new XAMCasesExtendedProcess());
            Add(new HappyBirthProcess());
            Add(new QuoteExchangeProcess());
            Add(new ACCOMQuoteProcess());
            Add(new AlertProcess());
            Add(new NiceTripProcess());
            Add(new WelcomeBackProcess());
            Add(new PrepurchaseProcess());
            Add(new PrepurchasePaxProcess());           
            Add(new PointsReportProcess());
            Add(new AlertaChatProcess());
            Add(new ACCOMAlertNotIssue());
            Add(new EMailEKitProcess());
            Add(new ContinuaTuCompraProcess());
        }

        #endregion

        public AbstractEMailProcess GetProcess(string key)
        {
            if (_Strategies.Keys.Contains(key))
            {
                return _Strategies[key];
            }
            return new DummyProcess();
        }

        public ICollection<string> GetProcessKeys()
        {
            return _Strategies != null ? _Strategies.Keys : new List<string>();
        }
    }
}
