using Project_CuoiKi.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

            LoadTotalMachines();
            txtTSMP1.Enabled = false;
            txtTSMP2.Enabled = false;
            txtTSMP3.Enabled = false;

        }

        public void LoadTotalMachines()
        {
            // Gọi phương thức GetTotalMachines từ lớp functions
            int totalMachines1 = Class.functions.GetTotalMachines1();
            int totalMachines2 = Class.functions.GetTotalMachines2();
            int totalMachines3 = Class.functions.GetTotalMachines3();

            // Gán kết quả vào textbox txtTSMP1
            txtTSMP1.Text = totalMachines1.ToString();
            txtTSMP2.Text = totalMachines2.ToString();
            txtTSMP3.Text = totalMachines3.ToString();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Controls.Add(this.uC_Danhsachmay1);
            uC_Danhsachmay1.Visible = true;
            uC_Danhsachmay1.maPhongSelected = guna2Button1.Tag.ToString();
            uC_Danhsachmay1.Load_DataGridView(guna2Button1.Tag.ToString());
            uC_Danhsachmay1.BringToFront();
            uC_Danhsachmay1.GetCallingForm(this);


        }

        private void uC_Danhsachmay1_Load(object sender, EventArgs e)
        {

        }

        private void UC_Phong_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Controls.Add(this.uC_Danhsachmay1);
            uC_Danhsachmay1.Visible = true;
            uC_Danhsachmay1.maPhongSelected = guna2Button3.Tag.ToString();
            uC_Danhsachmay1.Load_DataGridView(guna2Button3.Tag.ToString());
            uC_Danhsachmay1.BringToFront();
            uC_Danhsachmay1.GetCallingForm(this);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Controls.Add(this.uC_Danhsachmay1);
            uC_Danhsachmay1.Visible = true;
            uC_Danhsachmay1.maPhongSelected = guna2Button4.Tag.ToString();
            uC_Danhsachmay1.Load_DataGridView(guna2Button4.Tag.ToString());
            uC_Danhsachmay1.BringToFront();
            uC_Danhsachmay1.GetCallingForm(this);
        }
    }
}
