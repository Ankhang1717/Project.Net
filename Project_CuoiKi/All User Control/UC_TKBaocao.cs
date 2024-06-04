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

namespace Project_CuoiKi.All_User_Control
{
    public partial class UC_TKBaocao : UserControl
    {
        public UC_TKBaocao()
        {
            InitializeComponent();
            functions.ketnoi();
        }

        DataTable dt;
        private void UC_TKBaocao_Load(object sender, EventArgs e)
        {
            Load_DataGridView();
            ResetValues();
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
        }
        private void btnHienthitatca_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from Hoadonban";
            dt = functions.GetDataToTable(sql);
            datagridview.DataSource = dt;
            txtNhap.Text = "";
            txtNhap.Focus();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from Hoadonban where 1=1";
            if (txtNhap.Text != "")
                sql = sql + "AND mahdb like N'%" + txtNhap.Text + "%'";
            dt = functions.GetDataToTable(sql);
            datagridview.DataSource = dt;
            //Thiếu code điều kiện nhập, điều kiện kiểm tra tồn tại, mới chỉ có điều kiện tìm kiếm cho mã hóa đơn
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
