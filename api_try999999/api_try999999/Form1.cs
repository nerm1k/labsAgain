using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace api_try999999
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "ara", "bul", "chs", "cht", "hrv", "cze", "dan", "dut", "eng", "fin", "fre", "ger", "gre", "hun", "kor", "ita", "jpn", "pol", "por", "rus", "slv", "spa", "swe", "tur" });
            openFileDialog1.Filter = "gif png jpg tif bmp|*.GIF;*PNG;*.JPG;*.TIF;*.BMP|All files (*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("Выбран другой формат файла");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string token = "c3e9a266e188957";
            string path = openFileDialog1.FileName;
            string link = $"https://api.ocr.space/parse/image/";
            var client = new RestClient(link);
            var request = new RestRequest(Method.POST);
            request.AddHeader("apikey", token);
            request.AddFile("url", path);
            if (comboBox1.Text == null)
            {
                MessageBox.Show("Не выбран язык");
            }
            else
            {
                request.AddParameter("language", $"{comboBox1.Text}");
            }
            IRestResponse response = client.Execute(request);
            JObject j = JObject.Parse(response.Content);
            JToken text = j.SelectToken("$.ParsedResults")[0].SelectToken(".ParsedText");
            textBox1.Text = text.ToString();
        }
    }
}
