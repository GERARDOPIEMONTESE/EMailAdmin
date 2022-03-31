using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Strategies.Clauses
{
    public class ClauseStrategyLocator
    {
        #region Singleton

        private static ClauseStrategyLocator _Instance;

        private IDictionary<string, IClauseStrategy> _Strategies = new Dictionary<string, IClauseStrategy>();

        private ClauseStrategyLocator()
        {
        }

        public static ClauseStrategyLocator GetIntance()
        {
            if (_Instance == null)
            {
                _Instance = new ClauseStrategyLocator();
            }
            return _Instance;
        }

        private void Init()
        {
            AddStrategy(new DummyClauseStrategy());
            AddStrategy(new UpgradeClauseStrategy());
        }

        private void AddStrategy(IClauseStrategy strategy)
        {
            _Strategies.Add(strategy.GetCode(), strategy);
        }

        #endregion

        public IClauseStrategy GetStrategy(string code)
        {
            if (_Strategies.Keys.Contains(code))
            {
                return _Strategies[code];
            }
            return _Strategies[DummyClauseStrategy.CODE];
        }

        public IList<string> GetClauses()
        {
            return (IList<string>) _Strategies.Keys;
        }
    }
}
