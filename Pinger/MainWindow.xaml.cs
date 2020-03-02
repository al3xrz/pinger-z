using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Threading;
using System.Configuration;


namespace Pinger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)


        {
            //MessageBox.Show(textBox_z_server.Text.ToString());
            Properties.Settings.Default.z_server = textBox_z_server.Text.ToString();
            Properties.Settings.Default.kr_245 = textBox_kr_245.Text.ToString();
            Properties.Settings.Default.kr_246 = textBox_kr_246.Text.ToString();
            Properties.Settings.Default.interval = textBox_interval.Text.ToString();
            Properties.Settings.Default.vsc_name = textBox_vsc_name.Text.ToString();
            Properties.Settings.Default.z_server = textBox_z_server.Text.ToString();


            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

        }


       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            textBox_z_server.Text = Properties.Settings.Default.z_server;
            textBox_kr_245.Text = Properties.Settings.Default.kr_245;
            textBox_kr_246.Text = Properties.Settings.Default.kr_246;
            textBox_interval.Text = Properties.Settings.Default.interval;
            textBox_vsc_name.Text = Properties.Settings.Default.vsc_name;


            KR kr_245 = new KR("kr_245",
                            "www.ya.ru",
                //                Properties.Settings.Default.kr_245,
                                Convert.ToInt32(Properties.Settings.Default.interval),
                                Properties.Settings.Default.vsc_name,
                                Properties.Settings.Default.z_server);

            KR kr_246 = new KR("kr_246",
                  "127.0.0.1",
                //              Properties.Settings.Default.kr_246,
                                Convert.ToInt32(Properties.Settings.Default.interval),
                                Properties.Settings.Default.vsc_name,
                                Properties.Settings.Default.z_server);



            new Thread(PingThread).Start(kr_246);
            new Thread(PingThread).Start(kr_245);




        }

        private void PingThread(Object kr)
        {

            //throw new NotImplementedException();
            string address = ((KR)kr).address;
            string name = ((KR)kr).name;
            Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pingReply = null;
            try
            {
                while (true)
                {
                    pingReply = ping.Send(address);
                    string result_text = "";
                    if (pingReply.Status != IPStatus.TimedOut)
                    {
                        result_text = $"{name} {address}  {pingReply.Address.ToString()}  {pingReply.Options.Ttl.ToString()}ms \n";
                        Dispatcher.Invoke((Action)(() => {
                            ListBoxItem item = new ListBoxItem();
                            item.Content = result_text;
                            item.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                            listBox.Items.Add(item);
                            listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);

                            }));

                    }
                    else
                    {
                        result_text = $"{name} {address} {pingReply.Status.ToString()} \n";
                        Dispatcher.Invoke((Action)(() => {
                            ListBoxItem item = new ListBoxItem();
                            item.Content = result_text;
                            item.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            listBox.Items.Add(item);
                            listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);

                        }));

                    }

                    Thread.Sleep(5000);
                }
            }
            catch(TaskCanceledException)
            {

            }


        }
    }
}
