using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CuoiKi.All_User_Control
{
    public partial class UC_Phong : UserControl
    {
        public UC_Phong()
        {
            InitializeComponent();
            uC_Danhsachmay1.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            uC_Danhsachmay1.Visible = true;
            uC_Danhsachmay1.BringToFront();
        }
    }
}
