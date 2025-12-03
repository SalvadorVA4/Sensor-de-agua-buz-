using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLuvia
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private bool isBlinking;
        private SoundPlayer soundPlayer;
    
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort("COM3", 9600);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            botonComenza.Click += botonComenza_Click;
            timer1.Interval = 500;// Intervalo de parpadeo en milisegundos
            timer1.Tick += timer1_Tick;
            soundPlayer = new SoundPlayer("C:\\Users\\miria\\OneDrive\\Escritorio\\2024 Semestre 2\\MECANICA\\gifs\\Emergency Siren Police Wail 02.wav");
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            {
                throw new NotImplementedException();
            }

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void botonComenza_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e )
            {
                string data = serialPort.ReadLine();
                this.Invoke(new Action(() => {
                    label1.Text = "Sensor Status: " + (data.Trim() == "1" ? "Water Detected" : "No Water Detected");
                    if (data.Trim() == "1")
                    {
                        if (!isBlinking)
                        {
                            timer1.Start();
                            soundPlayer.PlayLooping();
                            pictureBox1.BackColor = System.Drawing.Color.Red;
                            isBlinking = true;
                        }
                    }
                    else
                    {
                        if (isBlinking)
                        {
                            soundPlayer.Stop();
                            pictureBox1.BackColor = System.Drawing.Color.Gray;
                            isBlinking = false;
                        }
                    }
                }));
            }
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            {
                serialPort.Close();
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = pictureBox1.BackColor == System.Drawing.Color.Gray ? System.Drawing.Color.Yellow : System.Drawing.Color.Gray;
        }
    }
}
