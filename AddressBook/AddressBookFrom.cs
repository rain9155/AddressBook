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
using System.Xml;

namespace AddressBook
{
    public partial class AddressBookFrom : Form
    {

        private const string Path = "J:/VisualStudio/AddressBook/AddressBook.xml";
        private bool isEmpty = true;//通讯录是否为空
        private List<User> list;
        public static int powerUser = 0;//全权用户
        public static int onlyWriteUser = 1;//只写用户
        public static int onlyReadUser = 2;//只写用户
        public static int otherUser = 3;//非法用户
        public static int user = powerUser;

        public AddressBookFrom()
        {
            InitializeComponent();
            list = new List<User>();
        }


        private void AddressBookFrom_Load(object sender, EventArgs e)
        {
            this.panel3.Visible = false;
            this.panel4.Visible = false;

            if (!File.Exists(Path) || isEmptyNode())
            {
                isEmpty = true;
            }
            else {
                isEmpty = false;
            }

            if (isEmpty)
            {
                button3.Visible = true;
                listView1.Visible = false;
        
            }
            else
            {
                button3.Visible = false;
                listView1.Visible = true;
                listView1.Update();
                FillAddressBook();  
                listView1.EndUpdate();
            }

            this.listView1.Columns.Add("姓名", 150, HorizontalAlignment.Left); //一步添加
            this.listView1.Columns.Add("电话", 150, HorizontalAlignment.Left); //一步添加
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = 0; i < listView1.SelectedItems.Count; i++) {
                index = listView1.SelectedItems[i].Index;
            }
            int t = 0;
            Config.index = index;
            foreach (User u in list) {
                if (t == index) {
                    Config.user = u;
                    break;
                }
                t++;
            }

            Details details = new Details();
            if (Details.UPDATA == details.ShowDialog2())
            { //更新一项联系人

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Path);
                // DocumentElement 获取xml文档对象的根XmlElement.
                XmlElement rootNode = xmlDocument.DocumentElement;
                //构建xPath语句
                index++;
                string strPath = "/AddressBook/user[" + index + "]";
                //selectSingleNode 根据XPath表达式,获得符合条件的第一个节点.
                XmlElement selectedNode = (XmlElement)rootNode.SelectSingleNode(strPath);
                //更新
                selectedNode.GetElementsByTagName("name").Item(0).InnerText = Config.user.Name;
                selectedNode.GetElementsByTagName("birthday").Item(0).InnerText = Config.user.Birthday;
                selectedNode.GetElementsByTagName("sex").Item(0).InnerText = Config.user.Sex;
                selectedNode.GetElementsByTagName("address").Item(0).InnerText = Config.user.Address;
                selectedNode.GetElementsByTagName("workAddress").Item(0).InnerText = Config.user.WorkAddress;
                selectedNode.GetElementsByTagName("phone").Item(0).InnerText = Config.user.Phone;
                selectedNode.GetElementsByTagName("assess").Item(0).InnerText = Config.user.Address;
                xmlDocument.Save(Path);

                //更新listView
                this.listView1.Update();
                FillAddressBook();
                this.listView1.EndUpdate();
                MessageBox.Show("修改成功！");

            }
            else
            {  //删除一项联系人
                DeleteOne(ref index);

                //更新listView
                this.listView1.Update();
                FillAddressBook();
                this.listView1.EndUpdate();
                if (isEmptyNode())
                {
                    this.listView1.Visible = false;
                    this.button3.Visible = true;
                }

                MessageBox.Show("删除成功！");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            if (isEmpty)
            {
                button3.Visible = true;
                listView1.Visible = false;
            
            }
            else {
                button3.Visible = false;
                listView1.Visible = true;

                listView1.Update();
                FillAddressBook();
                listView1.EndUpdate();
              
            }
         
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = false;
            this.panel4.Visible = true;
            button3.Visible = false;
            listView1.Visible = false;
      
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = true;
            this.panel4.Visible = false;
            button3.Visible = false;
            listView1.Visible = false;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isTextEmpty(textBox1))
            {
                MessageBox.Show("姓名不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (isTextEmpty(textBox6) ) {
                MessageBox.Show("电话不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (textBox7.Text.Length > 300) {
                MessageBox.Show("评价字数不能超过300", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }


            if (!File.Exists(Path))
            {
                try
                {
                    //创建xml文件
                    XmlDocument xmlDocument = new XmlDocument();
                    //创建类型声明节点
                    XmlNode declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "");
                    xmlDocument.AppendChild(declaration);
                    //创建根节点
                    XmlNode rootNode = xmlDocument.CreateElement("AddressBook");
                    xmlDocument.AppendChild(rootNode);
                    //创建其他节点
                    XmlNode node1 = xmlDocument.CreateNode(XmlNodeType.Element, "user", "");
                    CreateNode(xmlDocument, node1, "name", textBox1.Text);
                    CreateNode(xmlDocument, node1, "birthday", textBox2.Text);
                    CreateNode(xmlDocument, node1, "sex", textBox3.Text);
                    CreateNode(xmlDocument, node1, "address", textBox4.Text);
                    CreateNode(xmlDocument, node1, "workAddress", textBox5.Text);
                    CreateNode(xmlDocument, node1, "phone", textBox6.Text);
                    CreateNode(xmlDocument, node1, "assess", textBox7.Text);
                    //把节点追加到root中
                    rootNode.AppendChild(node1);
                    //保存xml文件
                    xmlDocument.Save(Path);
                    if (MessageBox.Show("添加成功", "成功") == DialogResult.OK) isEmpty = false;

                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                    MessageBox.Show("添加失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }

            }
            else
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    //把已有的xml文件加载进来
                    xmlDocument.Load(Path);
                    //得到根节点
                    XmlNode rootNode = xmlDocument.SelectSingleNode("AddressBook");
                    //添加新节点
                    XmlNode node1 = xmlDocument.CreateNode(XmlNodeType.Element, "user", "");
                    CreateNode(xmlDocument, node1, "name", textBox1.Text);
                    CreateNode(xmlDocument, node1, "birthday", textBox2.Text);
                    CreateNode(xmlDocument, node1, "sex", textBox3.Text);
                    CreateNode(xmlDocument, node1, "address", textBox4.Text);
                    CreateNode(xmlDocument, node1, "workAddress", textBox5.Text);
                    CreateNode(xmlDocument, node1, "phone", textBox6.Text);
                    CreateNode(xmlDocument, node1, "assess", textBox7.Text);
                    //把新节点追加到root中
                    rootNode.AppendChild(node1);
                    //保存xml文件
                    xmlDocument.Save(Path);
                    if (MessageBox.Show("添加成功", "成功") == DialogResult.OK) isEmpty = false;
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                    MessageBox.Show("添加失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            user = powerUser;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            user = onlyReadUser;
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            user = onlyWriteUser;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            user = otherUser;
        }

        /**
         * 创建xml节点
         * @xmlDocument xml文档   
         * @rootNode 根节点
         * @name 节点名    
         * @value 节点值
         */
        private void CreateNode(XmlDocument xmlDocument, XmlNode rootNode, String name, String value) {
            XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, name, "");
            node.InnerText = value;
            rootNode.AppendChild(node);
        }

        /**
         * 判空函数
         */
        private bool isTextEmpty(TextBox textBox){
            return textBox.Text == null || textBox.Text.Length == 0;
        }

        /**
        * 填充联系人
        */
        private void FillAddressBook()
        {
         
            list.Clear();
            this.listView1.Items.Clear();  //只移除所有的项

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path);
            //得到根节点
            XmlNode rootNode = xmlDocument.SelectSingleNode("AddressBook");
            //得到根节点的所有节点
            XmlNodeList nodeList = rootNode.ChildNodes;

            //遍历
            foreach (XmlNode node in nodeList)
            {


                User user = new User();

                //得到User节点的所有子节点,并取出元素
                XmlNodeList nodeList2 = node.ChildNodes;
                user.Name = nodeList2.Item(0).InnerText;
                user.Birthday = nodeList2.Item(1).InnerText;
                user.Sex = nodeList2.Item(2).InnerText;
                user.Address = nodeList2.Item(3).InnerText;
                user.WorkAddress = nodeList2.Item(4).InnerText;
                user.Phone = nodeList2.Item(5).InnerText;
                user.Access = nodeList2.Item(6).InnerText;

                list.Add(user);
            }
            foreach (User user in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = user.Name;
                item.SubItems.Add(user.Phone);
                item.SubItems.Add(user.Phone);
                this.listView1.Items.Add(item);
            }
        }

       /**
        * 删除一项联系人
        */
        private void DeleteOne(ref int index)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path);
            // DocumentElement 获取xml文档对象的根XmlElement.
            XmlElement rootNode = xmlDocument.DocumentElement;
            //构建xPath语句
            index++;
            string strPath = "/AddressBook/user[" + index + "]";
            //selectSingleNode 根据XPath表达式,获得符合条件的第一个节点.
            XmlNode selectedNode = rootNode.SelectSingleNode(strPath);
            //删除
            rootNode.RemoveChild(selectedNode);
            xmlDocument.Save(Path);
           
        }

        /**
      * 判断根节点是否为空
      */
        private bool isEmptyNode()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path);
            XmlElement rootNode = xmlDocument.DocumentElement;
            string strPath2 = "/AddressBook/user[1]";
            XmlNode node = rootNode.SelectSingleNode(strPath2);
            return node == null;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    
    }
}
