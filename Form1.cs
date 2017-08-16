using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduardoXML
{
    public partial class Form1 : Form
    {

        TranslationClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {

            string credential_path = @"D:\TRABAJO\GOOGLE\Pilo-b05a0f62bd0c.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            this.client = TranslationClient.Create();

            string[] files = { @"D:\TRABAJO\Eduardo\XML\en-ALD-backend.xml", @"D:\TRABAJO\Eduardo\XML\en-ALD-frontend.xml" };

            string[] filesout = { @"D:\TRABAJO\Eduardo\XML\out_en-ALD-backend.xml", @"D:\TRABAJO\Eduardo\XML\out_en-ALD-frontend.xml" };

            string chunk = "";
            string full = "";

            for (int i = 0; i <= 1; i += 1)
            {
                string file = files[i];

                string[] readText  = File.ReadAllLines(file);

                chunk = "";
                full = "";

                foreach (string linea in readText)
                {

                    if ((chunk + linea).Length < 4000)
                    {
                        chunk += linea + Environment.NewLine;
                    }
                    else
                    {
                        full += translate(chunk);
                        chunk = linea + Environment.NewLine;

                        Random rnd = new Random();
                        int azar = rnd.Next(5000, 10000);  // month: >= 1 and < 13
                        System.Threading.Thread.Sleep(azar);
                    }


                }

                if (chunk != "")
                {
                    full += translate(chunk);
                }

                System.IO.File.WriteAllText(filesout[i], full);

                full = "";
            }


        }

        private string translate(string text)
        {


            var response = this.client.TranslateText(text, LanguageCodes.Spanish, LanguageCodes.English);
            return response.TranslatedText;

        }
    }
}
