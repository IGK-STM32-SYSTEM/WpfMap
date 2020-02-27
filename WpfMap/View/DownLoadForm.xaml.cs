using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Modbus.Device;

namespace WpfMap.View
{
    /// <summary>
    /// DownLoadForm.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoadForm : Window
    {
        public abstract class BindableObject : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
            {
                if (!EqualityComparer<T>.Default.Equals(item, value))
                {
                    item = value;
                    OnPropertyChanged(propertyName);
                }
            }
        }
        public class ProcessValue : BindableObject
        {
            private double _Value = 0;

            /// <summary>
            /// 视图缩放比例
            /// </summary>
            public double Value
            {
                get { return _Value; }
                set { SetProperty(ref _Value, value); }
            }
        }
        ProcessValue processValue = new ProcessValue();
        SerialPort serialPort = new SerialPort();
        ModbusSerialMaster master;
        public DownLoadForm()
        {
            InitializeComponent();
            this.progressBar.DataContext = processValue;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //波特率
            serialPort.BaudRate = 115200;
            master = ModbusSerialMaster.CreateRtu(serialPort);
            //重试次数
            master.Transport.Retries = 1;
            //读超时时间
            master.Transport.ReadTimeout = 1000;
            //写超时时间
            master.Transport.WriteTimeout = 1000;

            string[] vs = SerialPort.GetPortNames();
            serialList.ItemsSource = vs;
            if (vs != null)
            {
                serialList.SelectedIndex = 0;
            }
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    lbConnect.Content = "未连接";
                    lbConnect.Foreground = Brushes.Red;
                    btnConnect.Content = "连接";
                }
                else
                {
                    serialPort.PortName = serialList.SelectedItem.ToString();
                    serialPort.Open();
                    lbConnect.Content = "已连接";
                    lbConnect.Foreground = Brushes.Green;
                    btnConnect.Content = "断开";
                }
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(ex.Message.ToString());
            }

        }
        Thread thread;
        private void BtnDownLoad_Click(object sender, RoutedEventArgs e)
        {
            thread = new Thread(new ThreadStart(function));
            thread.IsBackground = true;
            thread.Start();
        }

        private void function()
        {
            try
            {
                if (Route.Helper.AGVNeighbourList.Count == 0)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("还没有生成地图，请生成后再试！");
                    });
                    return;
                }
                processValue.Value = 0;
                //连续写入
                for (int i = 0; i < Route.Helper.AGVNeighbourList.Count; i++)
                {
                    processValue.Value = i / (Route.Helper.AGVNeighbourList.Count * 0.01);
                    //double dd = progressBar.Value;
                    //写入当前位置
                    master.WriteSingleRegister(1, 30, (ushort)MapElement.MapObject.RFIDS[i].Num);
                    //写入可到达的位置
                    ushort[] vs = new ushort[16];
                    int data = 0;
                    //前进
                    data = Route.Helper.AGVNeighbourList[i].go.TurnLeft; vs[0] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].go.LeftFork; vs[1] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].go.Straight; vs[2] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].go.RightFork; vs[3] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].go.TurnRight; vs[4] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    //平移
                    data = -1; vs[5] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = -1; vs[6] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    //后退
                    data = Route.Helper.AGVNeighbourList[i].back.TurnLeft; vs[7] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].back.LeftFork; vs[8] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].back.Straight; vs[9] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].back.RightFork; vs[10] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    data = Route.Helper.AGVNeighbourList[i].back.TurnRight; vs[11] = data == -1 ? (ushort)0 : (ushort)MapElement.MapObject.RFIDS[data].Num;
                    //前进角度
                    data = Route.Helper.AGVNeighbourList[i].go.AngleLeft; vs[12] = data == -1 ? (ushort)0 : (ushort)data;
                    data = Route.Helper.AGVNeighbourList[i].go.AngleRight; vs[13] = data == -1 ? (ushort)0 : (ushort)data;
                    //后退角度
                    data = Route.Helper.AGVNeighbourList[i].back.AngleLeft; vs[14] = data == -1 ? (ushort)0 : (ushort)data;
                    data = Route.Helper.AGVNeighbourList[i].back.AngleRight; vs[15] = data == -1 ? (ushort)0 : (ushort)data;

                    //更寄存器
                    master.WriteMultipleRegisters(1, 1, vs);
                    Thread.Sleep(50);
                    //执行写入Flash
                    master.WriteSingleRegister(1, 31, 1);
                    Thread.Sleep(100);
                }
                processValue.Value = 100;
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("下载完成，可以测试了！");
                });
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
            catch
            {

            }
        }
    }
}
