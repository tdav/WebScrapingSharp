using wss.Service;

namespace WebScrapingSharp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Scraper.Run();
        }
    }
}