using Log4NetLibrary;
using Newtonsoft.Json;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nova.Monitoring.Engine
{
    public class RuleEngine
    {
        private static volatile Dictionary<string, List<Strategy>> _strategyTable = new Dictionary<string, List<Strategy>>();
        private static object _syncObj = new object();
        private static FileLogService _fLogService = new FileLogService(typeof(RuleEngine));

        public static void UpdateStrategy(Command updateStrategyCommand)
        {
            UpdateStrategy(updateStrategyCommand.CommandText);
            _fLogService.Info(string.Format("@CommandLog@Command Execute finished####Code={0},id={1},source={2},Target={3},updateStrategyCommandText={4},Description={5}", updateStrategyCommand.Code.ToString(), updateStrategyCommand.Id, updateStrategyCommand.Source, updateStrategyCommand.Target.ToString(), updateStrategyCommand.CommandText, updateStrategyCommand.Description));
        }

        public static void UpdateStrategy(string strategyTableString)
        {
            System.Diagnostics.Debug.WriteLine("-------------启动策略更新！-------------");
            StrategyTable strategyTable = JsonConvert.DeserializeObject<StrategyTable>(strategyTableString);
            if (strategyTable == null)
            {
                return;
            }

            foreach (var strategy in strategyTable.StrategyList)
            {
                if (_strategyTable.Keys.Contains(strategy.SN))
                {
                    var findResult = _strategyTable[strategy.SN].Find(s => s.Type == strategy.Type);
                    if (findResult == null)
                    {
                        _strategyTable[strategy.SN].Add(strategy);
                    }
                    else
                    {
                        _strategyTable[strategy.SN].Remove(findResult);
                        _strategyTable[strategy.SN].Add(strategy);
                    }
                }
                else
                {
                    _strategyTable.Add(strategy.SN, new List<Strategy> { strategy });
                }
            }
            System.Diagnostics.Debug.WriteLine("-------------完成策略更新！-------------");
        }

        public static void ReceiveData(string key, object value)
        {
            if (key.Equals("MonitoringData", StringComparison.OrdinalIgnoreCase))
            {
                var ledData = value as DataPoint;

                if (ledData == null)
                {
                    System.Diagnostics.Debug.WriteLine("监控数据格式错误！！");
                    return;
                }

                if (_strategyTable == null)
                {
                    return;
                }

                if (_strategyTable.Keys == null || !_strategyTable.Keys.Contains(ledData.Key))
                {
                    return;
                }

                List<DataPoint> temperatureDataList = new List<DataPoint>();
                List<DataPoint> brightnessDataList = new List<DataPoint>();
                List<DataPoint> smokeDataList = new List<DataPoint>();
                //if (key.Equals("MonitoringData", StringComparison.OrdinalIgnoreCase))
                //{



                List<DataPoint> dataPointCollection = new List<DataPoint>();
                if (typeof(List<DataPoint>) == ledData.Value.GetType())
                {
                    dataPointCollection = ledData.Value as List<DataPoint>;
                }
                else if (typeof(DataPoint) == ledData.Value.GetType())
                {
                    dataPointCollection = new List<DataPoint>() { ledData.Value as DataPoint };
                }

                temperatureDataList = dataPointCollection.FindAll(p => IsTemperatureToken(p.Key));
                brightnessDataList = dataPointCollection.FindAll(p => IsBrightnessToken(p.Key));
                smokeDataList = dataPointCollection.FindAll(p => IsSmokeToken(p.Key));

                var temperatureTaskArgObj = new TaskArgPackage { DataPoints = temperatureDataList, Key = ledData.Key };
                Task temperatureStrategyTask = Task.Factory.StartNew((obj) =>
                {
                    var taskArgObj = (TaskArgPackage)obj;
                    TemperatrueStrategyTask(taskArgObj.Key, taskArgObj.DataPoints);
                }, temperatureTaskArgObj);

                var smokeTaskArgObj = new TaskArgPackage { DataPoints = smokeDataList, Key = ledData.Key };
                Task smokeStrategyTask = Task.Factory.StartNew((obj) =>
                {
                    var taskArgObj = (TaskArgPackage)obj;
                    SmokeStrategyTask(taskArgObj.Key, taskArgObj.DataPoints);
                }, smokeTaskArgObj);


                //lock (_syncObj)
                //{
                //    temperatureDataList = dataPointCollection.FindAll(p => IsTemperatureToken(p.Key));
                //    brightnessDataList = dataPointCollection.FindAll(p => IsBrightnessToken(p.Key));
                //    smokeDataList = dataPointCollection.FindAll(p => IsSmokeToken(p.Key));


                //    TemperatrueStrategyTask(key, temperatureDataList);
                //    BrightnessStrategyTask(key, brightnessDataList);
                //    SmokeStrategyTask(key, smokeDataList);
                //}
            }
            //}

        }


        private static void TemperatrueStrategyTask(string ledSN, List<DataPoint> inputDatas)
        {
            if (!_strategyTable.Keys.Contains(ledSN))
                return;

            Strategy strategy = _strategyTable[ledSN].FirstOrDefault(s => s.Type == StrategyType.TemperatureStrategy);

            if (strategy == null)
                return;

            if (inputDatas == null || inputDatas.Count == 0)
                return;
            List<Rule> ruleExcuteTable = new List<Rule>();
            foreach (var ruleItem in strategy.RuleTable)
            {
                bool conditionResult = true;
                foreach (var conditionItem in ruleItem.RuleCondition.ConditionCollection)
                {
                    switch (conditionItem.Algorithm)
                    {
                        case ConditionAlgorithm.MaxValueAlgorithm:
                            Expression<Func<List<double>, double, bool>> maxExpression;
                            if (conditionItem.Operator == OperatorType.GreaterThan)
                            {
                                maxExpression = (a, b) => a.Max() > b;
                            }
                            else
                            {
                                maxExpression = (a, b) => a.Max() < b;
                            }
                            var temp = inputDatas.Select(t => double.Parse(t.Value.ToString())).ToList();
                            conditionResult &= maxExpression.Compile()(temp, conditionItem.RightExpression);
                            break;
                        case ConditionAlgorithm.AverageAlgorithm:
                            Expression<Func<List<double>, double, bool>> averageExpression;
                            if (conditionItem.Operator == OperatorType.GreaterThan)
                            {
                                averageExpression = (a, b) => (a.Sum() / a.Count) > b;
                            }
                            else
                            {
                                averageExpression = (a, b) => (a.Sum() / a.Count) < b;
                            }

                            conditionResult &= averageExpression.Compile()(inputDatas.Select(t => double.Parse(t.Value.ToString())).ToList(), conditionItem.RightExpression);
                            break;
                        default:
                            break;
                    }
                }
                if (conditionResult)
                {
                    ruleExcuteTable.Add(ruleItem);
                }
                //if (conditionResult)
                //{
                //    foreach (var actionItem in ruleItem.RuleAction.ActionCommandCollection)
                //    {
                //        if (actionItem.ActionTarget.TargetType == ActionTargetType.Parameter)
                //        {
                //            Command command = new Command()
                //            {
                //                Code = CommandCode.SetBrightness,
                //                Target = TargetType.ToDataSource,
                //                CommandText = strategy.SN + "|" + actionItem.ActionTarget.ParameterTarget.Value.ToString()
                //            };
                //            System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------",CommandCode.SetBrightness));
                //            DataEngine.ExecuteCommand(command);
                //            System.Threading.Thread.Sleep(100);
                //        }
                //        else if (actionItem.ActionTarget.TargetType == ActionTargetType.Device)
                //        {
                //            if (actionItem.ActionType == ActionType.Open)
                //            {
                //                foreach (var deviceInfo in actionItem.ActionTarget.DeviceTarget)
                //                {
                //                    Command command = new Command()
                //                    {
                //                        Code = CommandCode.OpenDevice,
                //                        Target = TargetType.ToDataSource,
                //                        CommandText = deviceInfo
                //                    };
                //                    System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.OpenDevice));

                //                    DataEngine.ExecuteCommand(command);
                //                    System.Threading.Thread.Sleep(100);
                //                }

                //            }
                //            else if (actionItem.ActionType == ActionType.Close)
                //            {
                //                foreach (var deviceInfo in actionItem.ActionTarget.DeviceTarget)
                //                {
                //                    Command command = new Command()
                //                    {
                //                        Code = CommandCode.CloseDevice,
                //                        Target = TargetType.ToDataSource,
                //                        CommandText = deviceInfo
                //                    };
                //                    System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.CloseDevice));

                //                    DataEngine.ExecuteCommand(command);
                //                    System.Threading.Thread.Sleep(100);
                //                }
                //            }
                //        }
                //        else if (actionItem.ActionTarget.TargetType == ActionTargetType.SmartFunction)
                //        {
                //            if (actionItem.ActionType == ActionType.Enable)
                //            {
                //                Command command = new Command()
                //                {
                //                    Code = CommandCode.StartSmartBrightness,
                //                    Target = TargetType.ToDataSource,
                //                    CommandText = strategy.SN + "|" + ActionType.Enable.ToString()
                //                };
                //                System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.StartSmartBrightness));

                //                DataEngine.ExecuteCommand(command);
                //                System.Threading.Thread.Sleep(100);
                //            }
                //            else if (actionItem.ActionType == ActionType.Disable)
                //            {
                //                Command command = new Command()
                //                {
                //                    Code = CommandCode.StopSmartBrightness,
                //                    Target = TargetType.ToDataSource,
                //                    CommandText = strategy.SN + "|" + ActionType.Disable.ToString()
                //                };
                //                System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.StopSmartBrightness));

                //                DataEngine.ExecuteCommand(command);
                //                System.Threading.Thread.Sleep(100);
                //            }
                //        }
                //    }
                //}
            }
            if (ruleExcuteTable.Count > 1 && IsExistSmartFunctionRule(ruleExcuteTable))
            {
                ruleExcuteTable.Remove(ruleExcuteTable.Find(rule => rule.RuleAction.ActionCommandCollection.Any(action => action.ActionTarget.TargetType == ActionTargetType.SmartFunction)));
            }


            foreach (var ruleItem in ruleExcuteTable)
            {
                foreach (var actionItem in ruleItem.RuleAction.ActionCommandCollection)
                {
                    if (actionItem.ActionTarget.TargetType == ActionTargetType.Parameter)
                    {
                        Command command = new Command()
                        {
                            Code = CommandCode.SetBrightness,
                            Target = TargetType.ToDataSource,
                            CommandText = strategy.SN + "|" + actionItem.ActionTarget.ParameterTarget.Value.ToString()
                        };
                        System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.SetBrightness));
                        DataEngine.AddCommand(command);
                        System.Threading.Thread.Sleep(100);
                    }
                    else if (actionItem.ActionTarget.TargetType == ActionTargetType.Device)
                    {
                        if (actionItem.ActionType == ActionType.Open)
                        {
                            foreach (var deviceInfo in actionItem.ActionTarget.DeviceTarget)
                            {
                                Command command = new Command()
                                {
                                    Code = CommandCode.OpenDevice,
                                    Target = TargetType.ToDataSource,
                                    CommandText = deviceInfo
                                };
                                System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.OpenDevice));

                                DataEngine.AddCommand(command);
                                System.Threading.Thread.Sleep(100);
                            }

                        }
                        else if (actionItem.ActionType == ActionType.Close)
                        {
                            foreach (var deviceInfo in actionItem.ActionTarget.DeviceTarget)
                            {
                                Command command = new Command()
                                {
                                    Code = CommandCode.CloseDevice,
                                    Target = TargetType.ToDataSource,
                                    CommandText = deviceInfo
                                };
                                System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.CloseDevice));

                                DataEngine.AddCommand(command);
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }
                    else if (actionItem.ActionTarget.TargetType == ActionTargetType.SmartFunction)
                    {
                        if (actionItem.ActionType == ActionType.Enable)
                        {
                            Command command = new Command()
                            {
                                Code = CommandCode.StartSmartBrightness,
                                Target = TargetType.ToDataSource,
                                CommandText = strategy.SN + "|" + ActionType.Enable.ToString()
                            };
                            System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.StartSmartBrightness));

                            DataEngine.AddCommand(command);
                            System.Threading.Thread.Sleep(100);
                        }
                        else if (actionItem.ActionType == ActionType.Disable)
                        {
                            Command command = new Command()
                            {
                                Code = CommandCode.StopSmartBrightness,
                                Target = TargetType.ToDataSource,
                                CommandText = strategy.SN + "|" + ActionType.Disable.ToString()
                            };
                            System.Diagnostics.Debug.WriteLine(string.Format("-------------策略命令{0}发送！-------------", CommandCode.StopSmartBrightness));

                            DataEngine.AddCommand(command);
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }





        }

        private static bool IsExistSmartFunctionRule(List<Rule> ruleExcuteTable)
        {
            return ruleExcuteTable.Any(rule => IsExitSmartFunctionAction(rule));
        }

        private static bool IsExitSmartFunctionAction(Rule rule)
        {
            if (rule == null)
            {
                return false;
            }
            return rule.RuleAction.ActionCommandCollection.Any(action => action.ActionTarget.TargetType == ActionTargetType.SmartFunction);
        }

        private static void BrightnessStrategyTask(string ledSN, List<DataPoint> inputDatas)
        {
            if (!_strategyTable.Keys.Contains(ledSN))
                return;

            Strategy strategy = _strategyTable[ledSN].FirstOrDefault(s => s.Type == StrategyType.LightStrategy);

            if (strategy == null)
                return;

            if (inputDatas == null || inputDatas.Count == 0)
                return;

            foreach (var ruleItem in strategy.RuleTable)
            {
                bool conditionResult = true;
                foreach (var conditionItem in ruleItem.RuleCondition.ConditionCollection)
                {
                    switch (conditionItem.Algorithm)
                    {
                        case ConditionAlgorithm.MaxValueAlgorithm:
                            Expression<Func<List<int>, int, bool>> maxExpression;
                            if (conditionItem.Operator == OperatorType.GreaterThan)
                            {
                                maxExpression = (a, b) => a.Max() > b;
                            }
                            else
                            {
                                maxExpression = (a, b) => a.Max() < b;
                            }

                            conditionResult &= maxExpression.Compile()(inputDatas.Select(t => int.Parse(t.Value as string)).ToList(), conditionItem.RightExpression);
                            break;
                        case ConditionAlgorithm.AverageAlgorithm:
                            Expression<Func<List<int>, int, bool>> averageExpression;
                            if (conditionItem.Operator == OperatorType.GreaterThan)
                            {
                                averageExpression = (a, b) => (a.Max() / a.Count) > b;
                            }
                            else
                            {
                                averageExpression = (a, b) => (a.Max() / a.Count) < b;
                            }

                            conditionResult &= averageExpression.Compile()(inputDatas.Select(t => int.Parse(t.Value as string)).ToList(), conditionItem.RightExpression);
                            break;
                        default:
                            break;
                    }
                }
                if (conditionResult)
                {
                    foreach (var actionItem in ruleItem.RuleAction.ActionCommandCollection)
                    {
                        if (actionItem.ActionTarget.TargetType == ActionTargetType.Parameter)
                        {
                            Command command = new Command()
                            {
                                Code = CommandCode.SetBrightness,
                                Target = TargetType.ToDataSource,
                                CommandText = string.Empty
                            };
                            DataEngine.AddCommand(command);
                        }
                        else if (actionItem.ActionTarget.TargetType == ActionTargetType.Device)
                        {
                            if (actionItem.ActionType == ActionType.Open)
                            {
                                Command command = new Command()
                                {
                                    Code = CommandCode.OpenDevice,
                                    Target = TargetType.ToDataSource,
                                    CommandText = string.Empty
                                };
                                DataEngine.AddCommand(command);
                            }
                            else if (actionItem.ActionType == ActionType.Close)
                            {
                                Command command = new Command()
                                {
                                    Code = CommandCode.CloseDevice,
                                    Target = TargetType.ToDataSource,
                                    CommandText = string.Empty
                                };
                                DataEngine.AddCommand(command);
                            }
                        }
                    }
                }
            }
        }

        private static void SmokeStrategyTask(string ledSN, List<DataPoint> inputDatas)
        {
            if (!_strategyTable.Keys.Contains(ledSN))
                return;

            Strategy strategy = _strategyTable[ledSN].FirstOrDefault(s => s.Type == StrategyType.SmokeStrategy);

            if (strategy == null)
                return;

            if (inputDatas == null || inputDatas.Count == 0)
                return;

            foreach (var ruleItem in strategy.RuleTable)
            {
                bool conditionResult = true;
                foreach (var conditionItem in ruleItem.RuleCondition.ConditionCollection)
                {
                    Expression<Func<List<bool>, int, bool>> expression;
                    expression = (a, b) => a.Count(t => t == false) > b;
                    conditionResult &= expression.Compile()(inputDatas.Select(t => bool.Parse(t.Value.ToString())).ToList(), conditionItem.RightExpression);
                    //switch (conditionItem.Algorithm)
                    //{
                    //    case ConditionAlgorithm.MaxValueAlgorithm:
                    //        Expression<Func<List<int>, int, bool>> maxExpression;
                    //        if (conditionItem.Operator == OperatorType.GreaterThan)
                    //        {
                    //            maxExpression = (a, b) => a.Max() > b;
                    //        }
                    //        else
                    //        {
                    //            maxExpression = (a, b) => a.Max() < b;
                    //        }

                    //        conditionResult &= maxExpression.Compile()(inputDatas.Select(t => int.Parse(t.Value as string)).ToList(), conditionItem.RightExpression);
                    //        break;
                    //    case ConditionAlgorithm.AverageAlgorithm:
                    //        Expression<Func<List<int>, int, bool>> averageExpression;
                    //        if (conditionItem.Operator == OperatorType.GreaterThan)
                    //        {
                    //            averageExpression = (a, b) => (a.Max() / a.Count) > b;
                    //        }
                    //        else
                    //        {
                    //            averageExpression = (a, b) => (a.Max() / a.Count) < b;
                    //        }

                    //        conditionResult &= averageExpression.Compile()(inputDatas.Select(t => int.Parse(t.Value as string)).ToList(), conditionItem.RightExpression);
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                if (conditionResult)
                {
                    foreach (var actionItem in ruleItem.RuleAction.ActionCommandCollection)
                    {
                        if (actionItem.ActionTarget.TargetType == ActionTargetType.Parameter)
                        {
                            Command command = new Command()
                            {
                                Code = CommandCode.SetBrightness,
                                Target = TargetType.ToDataSource,
                                CommandText = string.Empty
                            };
                            DataEngine.AddCommand(command);
                            System.Threading.Thread.Sleep(50);
                        }
                        else if (actionItem.ActionTarget.TargetType == ActionTargetType.Device)
                        {
                            if (actionItem.ActionType == ActionType.Open)
                            {
                                Command command = new Command()
                                {
                                    Code = CommandCode.OpenDevice,
                                    Target = TargetType.ToDataSource,
                                    CommandText = string.Empty
                                };
                                DataEngine.AddCommand(command);
                                System.Threading.Thread.Sleep(50);
                            }
                            else if (actionItem.ActionType == ActionType.Close)
                            {
                                Command command = new Command()
                                {
                                    Code = CommandCode.CloseDevice,
                                    Target = TargetType.ToDataSource,
                                    CommandText = string.Empty
                                };
                                DataEngine.AddCommand(command);
                                System.Threading.Thread.Sleep(50);
                            }
                        }
                    }
                }
            }
        }

        public static bool IsTemperatureToken(string token)
        {
            string keyToken = token.Substring(0, 3);
            return keyToken.Equals((int)DeviceType.ReceiverCard + "|" + (int)StateQuantityType.Temperature);
        }

        public static bool IsBrightnessToken(string token)
        {
            string keyToken = token.Substring(0, 3);
            return keyToken.Equals((int)DeviceType.MonitoringCard + "|" + (int)StateQuantityType.Brightness);
        }

        public static bool IsSmokeToken(string token)
        {
            string keyToken = token.Substring(0, 3);
            return keyToken.Equals((int)DeviceType.ReceiverCard + "|" + (int)StateQuantityType.Smoke);
        }
    }

    public class TaskArgPackage
    {
        public List<DataPoint> DataPoints { get; set; }
        public string Key { get; set; }
    }
}
