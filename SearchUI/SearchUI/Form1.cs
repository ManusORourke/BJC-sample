namespace SearchUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            toolStripButtonSearch.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetJobs();
            DisplayJobs(jobs);
        }

        List<Job> jobs = new List<Job>();

        private void GetJobs()
        {
            jobs = new List<Job>();
            jobs.Add(new Job() { ID = 1, Title = "Garage Attendant", Company = "Fred's Car park" });
            jobs.Add(new Job() { ID = 2, Title = "Chef", Company = "Joe's Burger House" });
            jobs.Add(new Job() { ID = 3, Title = "Electromechanical Engineer", Company = "Joe's Aviation" });
            jobs.Add(new Job() { ID = 4, Title = "Mechanic", Company = "Joe's Garage" });
            jobs.Add(new Job() { ID = 5, Title = "Chief Mechanic", Company = "We Fix Cars" });
            jobs.Add(new Job() { ID = 6, Title = "Chef", Company = "Meccano" });
            jobs.Add(new Job() { ID = 6, Title = "Chief Financial Officer East Asia Region", Company = "The Really Long Company Name of Kilarney" });
        }

        private void DisplayJobs(List<Job> jobs)
        {
            listView1.Items.Clear();
            foreach (Job job in jobs)
            {
                ListViewItem l = new ListViewItem(job.ID.ToString());
                l.SubItems.Add(job.Title);
                l.SubItems.Add(job.Company);
                listView1.Items.Add(l);
            }
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void toolStripTextBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                Search();
            }
        }

        private void toolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(toolStripTextBoxSearch.Text))
            {
                toolStripButtonSearch.Enabled = true;
            }
            else
            {
                toolStripButtonSearch.Enabled = false;
            }
        }

        private void Search()
        {
            string tofind = toolStripTextBoxSearch.Text;
            if (!string.IsNullOrEmpty(toolStripTextBoxSearch.Text))
            {
                List<Job> tmpJobs = jobs.Where(a => a.Title.Contains(tofind, StringComparison.CurrentCultureIgnoreCase)).ToList();
                tmpJobs.AddRange(jobs.Where(a => a.Company.Contains(tofind, StringComparison.CurrentCultureIgnoreCase)));
                DisplayJobs(tmpJobs);
            }
        }
    }
}