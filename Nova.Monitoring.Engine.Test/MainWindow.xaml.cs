using GalaSoft.MvvmLight.Threading;
using Nova.Monitoring.Common;
using Nova.Monitoring.DAL;
using Nova.Monitoring.DataDispatcher;
using Nova.Monitoring.DataSource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace Nova.Monitoring.Engine.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.AppendPrivatePath(AppDomain.CurrentDomain.BaseDirectory + "/DataSource");
            DispatcherHelper.Initialize();
            DataEngine.Dispatcher = DispatcherHelper.UIDispatcher;
            
            this.DataContext = new MainViewModel();        
        }

       // public SimulationAcquisition DataAcquisition { get; set; }






        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //bool bSuccess;
        //    //var _dataAccessor = new MonitorDataAccessor(AppDataConfig.DataBaseFilePath, out bSuccess);
        //    //var hwConfig = _dataAccessor.GetHardWareCfg("ffffffffffffffff-00");
        //    //ClientDispatcher.Instance.UpdateLedMonitoringConfig("ffffffffffffffff-00", hwConfig["ffffffffffffffff-00"]);


        //    Strategy strategy = new Strategy();
        //    strategy.SN = "ffffffffffffffff-00";
        //    strategy.Type = StrategyType.TemperatureStrategy;
        //    strategy.RuleTable = new List<Rule>();
        //    var ruleImpr = new Rule();
        //    ruleImpr.RuleCondition.ConditionCollection.Add(new Common.Condition(OperatorType.LessThan, 90, ConditionAlgorithm.MaxValueAlgorithm));
        //    var actionImpr = new ActionCommand()
        //    {
        //        ActionType = ActionType.Set,
        //        ActionTarget = new ActionTarget()
        //        {
        //            ParameterTarget = new StateObject() { Type = StateQuantityType.Brightness, Value = 50 },
        //            TargetType = ActionTargetType.Parameter
        //        }
        //    };
        //    ruleImpr.RuleAction.ActionCommandCollection.Add(actionImpr);

        //    strategy.RuleTable.Add(ruleImpr);

        //    ClientDispatcher.Instance.UpdateStrategy(new StrategyTable() { StrategyList = new List<Strategy>() { strategy } });
        //}


    }
}
