using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool.UI
{
    delegate void handler(int num);

    public partial class ScanCalibration : Form
    {
        List<Component> componentList;
        List<string> profileNameList;
        public ScanCalibration(List<string> profileNameList)
        {
            this.profileNameList = profileNameList;
            InitializeComponent();
            componentList = new List<Component>();
            if (this.profileNameList.Count == 0)
            {
                add();
            }
        }

        private void ScanCalibration_Load(object sender, EventArgs e)
        {
            foreach(string profileName in profileNameList)
            {
                add(profileName);
            }
        }

        private void add(string profileName = null)
        {
            int startY = 0;
            if (componentList.Count != 0)
            {
                startY = componentList.Last().del.Location.Y;
            }
            Component one = new Component(startY, componentList.Count);
            one.deleteHandler += delete;
            componentList.Add(one);
            this.panel1.Controls.Add(one.del);
            this.panel1.Controls.Add(one.text);
            one.del.BringToFront();
            one.text.BringToFront();
            if(profileName != null)
            {
                one.text.Text = profileName;
            }
        }

        private void delete(int num)
        {
            this.panel1.Controls.Remove(componentList[num].del);
            this.panel1.Controls.Remove(componentList[num].text);
            componentList.RemoveAt(num);
            for (int i = num; i < componentList.Count; i++)
            {
                componentList[i].up();
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            profileNameList.Clear();
            foreach(Component oneComponent in componentList)
            {
                profileNameList.Add(oneComponent.text.Text);
            }
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            add();
        }
    }

    class Component
    {
        private int number;
        public Button del = new Button();
        public TextBox text = new TextBox();
        public event handler deleteHandler;
        private const int hight = 20;
        private const int blank = 5;

        public Component(int startY, int num)
        {
            this.number = num;
            del.Size = new System.Drawing.Size(30, hight);
            del.Location = new Point(blank, startY + hight + blank);
            del.Text = "-";
            del.Click += this.delClick;

            text.Multiline = true;
            text.Size = new System.Drawing.Size(200, hight);
            text.Location = new Point(blank + del.Size.Width + blank, startY + hight + blank);
            string initString = string.Format("Profile name{0}", number + 1);
            text.Text = initString;
        }

        private void delClick(object sender, EventArgs e)
        {
            deleteHandler(number);
        }

        public void up()
        {
            del.Location = new Point(del.Location.X, del.Location.Y - hight - blank);
            text.Location = new Point(text.Location.X, text.Location.Y - hight - blank);

            this.number--;
        }
    }
}
