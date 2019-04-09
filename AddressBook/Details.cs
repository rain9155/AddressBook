using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class Details : Form
    {
        public static String UPDATA = "upData";
        public static String DELETE = "delete";
        private String answer = UPDATA;

        public Details()
        {
            InitializeComponent();
        }

        private void Details_Load(object sender, EventArgs e)
        { 

            this.textBox1.Text = Config.user.Name;
            this.textBox2.Text = Config.user.Birthday;
            this.textBox3.Text = Config.user.Sex;
            this.textBox4.Text = Config.user.Address;
            this.textBox5.Text = Config.user.WorkAddress;
            this.textBox6.Text = Config.user.Phone;
            this.textBox7.Text = Config.user.Access;

            //除评价外只读
            if (AddressBookFrom.user == AddressBookFrom.onlyWriteUser)
            {
                this.textBox1.ReadOnly = true;
                this.textBox2.ReadOnly = true; 
                this.textBox3.ReadOnly = true;
                this.textBox4.ReadOnly = true; 
                this.textBox5.ReadOnly = true;
                this.textBox6.ReadOnly = true;
                this.textBox7.ReadOnly = false;
                this.button2.Enabled = true;
            }

            //只读
            if (AddressBookFrom.user == AddressBookFrom.onlyReadUser)
            {
                this.textBox1.ReadOnly = true;
                this.textBox2.ReadOnly = true;
                this.textBox3.ReadOnly = true;
                this.textBox4.ReadOnly = true;
                this.textBox5.ReadOnly = true;
                this.textBox6.ReadOnly = true;
                this.textBox7.ReadOnly = true;
                this.button2.Enabled = false;
            }

            //非法用户
            if (AddressBookFrom.user == AddressBookFrom.otherUser)
            {
                this.textBox1.ReadOnly = true;
                this.textBox2.ReadOnly = true;
                this.textBox3.ReadOnly = true;
                this.textBox4.ReadOnly = true;
                this.textBox5.ReadOnly = true;
                this.textBox6.ReadOnly = true;
                this.textBox7.ReadOnly = true;
                this.button2.Enabled = false;
                this.button1.Enabled = false;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public String ShowDialog2() {
            base.ShowDialog();
            return answer;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Config.user.Name = textBox1.Text;
            Config.user.Birthday = textBox2.Text;
            Config.user.Sex = textBox3.Text;
            Config.user.Address = textBox4.Text;
            Config.user.WorkAddress = textBox5.Text;
            Config.user.Phone = textBox6.Text;
            Config.user.Access = textBox7.Text;
            answer = UPDATA;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            answer = DELETE;
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
