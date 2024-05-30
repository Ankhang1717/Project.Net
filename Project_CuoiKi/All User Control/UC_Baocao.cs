using Project_CuoiKi.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Project_CuoiKi.All_User_Control
{
    public partial class UC_Baocao : UserControl
    {
        public UC_Baocao()
        {
            InitializeComponent();
            functions.ketnoi();
        }
        DataTable dt;
        private string ngay1, ngay2, ngay3;
        private void UC_Baocao_Load(object sender, EventArgs e)
        {
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
            functions.fillcombo("SELECT MaHDB FROM HoaDonBan", cboMahoadon, "MaHDB", "MaHDB");
            cboMahoadon.SelectedIndex = -1;
            functions.fillcombo("SELECT MaNV  FROM NhanVien", cboManhanvien, "MaNV", "MaNV");
            cboManhanvien.SelectedIndex = -1;
        }
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT * FROM HoaDonBan  ";
            dt = functions.GetDataToTable(sql);
            datagridview.DataSource = dt;
            datagridview.Columns[0].HeaderText = "Mã Hoá đơn";
            datagridview.Columns[1].HeaderText = "Mã máy";
            datagridview.Columns[2].HeaderText = "Mã phòng";
            datagridview.Columns[3].HeaderText = "Ngày thuê";
            datagridview.Columns[4].HeaderText = "Giờ vào";
            datagridview.Columns[5].HeaderText = "Giờ ra";
            datagridview.Columns[6].HeaderText = "Mã nhân viên";
            datagridview.Columns[7].HeaderText = "Tổng tiền";
            datagridview.Columns[8].HeaderText = "Ghi chú";

            foreach (DataGridViewColumn col in datagridview.Columns)
            {
                col.Width = 100;
            }
            datagridview.AllowUserToAddRows = false;
            datagridview.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            rbtn1.Enabled = true;
            rbtn2.Enabled = true;
            date1.Enabled = false;
            date2.Enabled = false;
            date3.Enabled = false;

        }

        private void rbtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                date1.Enabled = true;
                date2.Enabled = true;
                date3.Enabled = false;
            }
        }

        private void rbtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn2.Checked)
            {
                date1.Enabled = false;
                date2.Enabled = false;
                date3.Enabled = true;
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (rbtn2.Checked)
            {

            }
        }

        private void btnDT_Click(object sender, EventArgs e)
        {

        }

        private void datagridview_Click(object sender, EventArgs e)
        {
          
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cboMahoadon.SelectedValue = datagridview.CurrentRow.Cells["MaHDB"].Value.ToString();
            cboManhanvien.SelectedValue = datagridview.CurrentRow.Cells["MaNV"].Value.ToString();
            rbtn2.Checked = true;
            date3.Value = (DateTime)datagridview.CurrentRow.Cells["NgayThue"].Value;
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void date2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void date3_ValueChanged(object sender, EventArgs e)
        {
            ngay3 = date3.Value.ToString();
        }
    }
}
