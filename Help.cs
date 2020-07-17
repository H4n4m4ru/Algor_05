using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algor_05
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }
        
        private void Help_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath _path = new System.Drawing.Drawing2D.GraphicsPath();
            _path.AddEllipse(new Rectangle(0, 0, 75, 75));
            button1.Region = new Region(_path);//改變形狀
        }

    }
}
        
