using Project_CuoiKi.All_User_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CuoiKi.Forms
{
    public partial class DangXuat : Form
    {
        public UC_Danhsachmay frmDSMay = null;
        public string maPhongThue;
        public string maMayThue;

        private bool canClose = false;
        public DangXuat()
        {
            InitializeComponent();
        }

        private void DangXuat_Load(object sender, EventArgs e)
        {
            Class.functions.ketnoi();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            UpdateEndTime();

            canClose = true;
            this.Close();
        }

        private void UpdateEndTime()
        {
            string gioRa = DateTime.Now.ToString("HH:mm");

            string sql;
            sql = "UPDATE Hoadonban set GioRa = N'" + gioRa + "' where maphong = N'" + maPhongThue + "' and mamay = N'" + maMayThue + "' ";
            sql += " UPDATE May set trangthai = '0' where mamay = N'" + maMayThue + "' ";
            Class.functions.runsql(sql);

            this.frmDSMay.Load_DataGridView(frmDSMay.maPhongSelected);
        }

        public void SetRentInfo(string maPhong, string maMay)
        {
            maPhongThue = maPhong;
            maMayThue = maMay;
        }

        private void DangXuat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
                e.Cancel = true;
        }
    }
}
