using Project_CuoiKi.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CuoiKi.All_User_Control
{
    public partial class UC_Danhsachmay : UserControl
    {
        private UC_Phong callingForm = null;

        public string maPhongSelected;

        public UC_Danhsachmay()
        {
            InitializeComponent();
            Class.functions.ketnoi();
           // Load_DataGridView(maPhongSelected);
            txtmaphong.Enabled = false;
        }

        private void UC_Danhsachmay_Load(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            
        }

        DataTable tbldsm;
        public void Load_DataGridView(string maPhong)
        {
            string sql;
            sql = $"SELECT * FROM May where trangthai = 0 and maphong = '{maPhong}'";
            tbldsm = Class.functions.GetDataToTable(sql);
            dgridmay.DataSource = tbldsm;
            dgridmay.Columns[0].HeaderText = "Mã Máy";
            dgridmay.Columns[1].HeaderText = "Mã Phòng";
            dgridmay.Columns[2].HeaderText = "Trạng thái";
            dgridmay.AllowUserToAddRows = false;
            dgridmay.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        public void GetCallingForm(UC_Phong callingForm)
        {
            this.callingForm = callingForm;
        }

        private void dgridmay_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmaphong.Focus();
                return;
            }
            if (tbldsm.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trong csdl", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtmaphong.Text = dgridmay.CurrentRow.Cells["Maphong"].Value.ToString();
            txtmamay.Text = dgridmay.CurrentRow.Cells["Mamay"].Value.ToString();
            txtTrangthai.Text = dgridmay.CurrentRow.Cells["TrangThai"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            txtmamay.Enabled = false;

            selectedMaPhong = txtmaphong.Text;
            selectedMaMay = txtmamay.Text;

        }
        private void resetvalues()
        {
            txtmamay.Text = "";
            txtmaphong.Text = "";
            txtTrangthai.Text = "";
            txtmamay.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled= false;
            btnXoa.Enabled= false;
            btnBoqua.Enabled= true;
            btnLuu.Enabled= true;
            resetvalues();
            txtmaphong.Text = "P01";

            
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tbldsm.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }    
            if(txtmaphong.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE May WHERE Mamay = N'" + txtmamay.Text + " '";
                Class.functions.runsql(sql);
                Load_DataGridView(maPhongSelected);
                callingForm.LoadTotalMachines();
                resetvalues();
             
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            resetvalues();
            btnThem.Enabled = true;
            btnSua.Enabled= true;
            btnXoa.Enabled= true;
            btnBoqua.Enabled= false;
            btnLuu.Enabled= false;
            txtmaphong.Enabled= false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(tbldsm.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }    
            if(txtmaphong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtTrangthai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập trạng thái", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTrangthai.Focus();
                return;
            }
            string sql;
            sql = "UPDATE May set trangthai = N'" + txtTrangthai.Text + "' where mamay = N'" + txtmamay.Text + "' ";
            Class.functions.runsql(sql);
            Load_DataGridView(maPhongSelected);
            resetvalues();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtmamay.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmamay.Focus();
                return;
            }
            if (txtTrangthai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập trạng thái", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTrangthai.Focus();
                return;
            }
            string sql;
            sql = "select mamay from May where mamay = N'" + txtmamay.Text + "'  and maphong = N'" + txtmaphong.Text + "'";
            if (Class.functions.CheckKey(sql))
            {
                MessageBox.Show("Bị trùng mã máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmamay.Focus();
                txtmamay.Text = "";
                return;
            }

            sql = "insert into May(MaMay, MaPhong, TrangThai) values(N'" + txtmamay.Text + "', N'" + txtmaphong.Text + "', N'" + txtTrangthai.Text + "')";
            Class.functions.runsql(sql);
            Load_DataGridView(maPhongSelected);
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            txtmaphong.Enabled = false;
            resetvalues();


            callingForm.LoadTotalMachines();
        }
        private string selectedMaPhong;
        private string selectedMaMay;
        private void btnThuemay_Click(object sender, EventArgs e)
        {
            if (txtmaphong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Forms.ThueMay frmThueMay = new Forms.ThueMay(selectedMaPhong, selectedMaMay);
            frmThueMay.frmDSMay = this;
            frmThueMay.Show();


        }
    }
}
