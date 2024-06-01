﻿using System;
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
    public partial class uC_Nhanvien : UserControl
    {
        public uC_Nhanvien()
        {
            InitializeComponent();
        }
        DataTable tblcalam;
        DataTable tblnv;

        private void uC_Nhanvien_Load(object sender, EventArgs e)
        {
            txtMaCa.Enabled = false;
            txtMaNV.Enabled = false;
            btnBoqua.Enabled = false;
            btnBoqua2.Enabled = false;
            btnLuu.Enabled = false;
            btnLuu2.Enabled = false;
            Load_DataGrid_CaLam();
            Load_DataGrid_NhanVien();
        }

        private void Load_DataGrid_CaLam()
        {
            string sql;
            sql = "select * from CaLam";
            tblcalam = Class.functions.GetDataToTable(sql);
            dgridCaLam.DataSource = tblcalam;
            dgridCaLam.Columns[0].HeaderText = "Mã ca làm";
            dgridCaLam.Columns[1].HeaderText = "Tên ca làm";
            dgridCaLam.Columns[2].HeaderText = "Thời gian";
            dgridCaLam.AllowUserToAddRows = false;
            dgridCaLam.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

       

        private void btnThem2_Click(object sender, EventArgs e)
        {
            btnThem2.Enabled = false;
            btnSua2.Enabled = false;
            btnLuu2.Enabled = true;
            btnBoqua2.Enabled = true;
            txtMaCa.Enabled = true;
            resetvalue_CaLam();

        }

        private void resetvalue_CaLam()
        {
            txtMaCa.Text = "";
            txtTenCa.Text = "";
            txtThoiGIan.Text = "";
        }
       

        private void dgridCaLam_Click(object sender, EventArgs e)
        {
            if (btnThem2.Enabled == false)
            {
                MessageBox.Show("Dang o che do them moi", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tblcalam.Rows.Count == 0)
            {
                MessageBox.Show("Khong co du lieu trong csdl", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtMaCa.Text = dgridCaLam.CurrentRow.Cells["MaCa"].Value.ToString();
            txtTenCa.Text = dgridCaLam.CurrentRow.Cells["TenCa"].Value.ToString();
            txtThoiGIan.Text = dgridCaLam.CurrentRow.Cells["ThoiGian"].Value.ToString();
            btnSua2.Enabled = true;
            btnXoa2.Enabled = true;
            btnBoqua2.Enabled = true;
        }

        private void btnLuu2_Click(object sender, EventArgs e)
        {
            if (txtMaCa.Text == "")
            {
                MessageBox.Show("Ban phai nhap ma ca", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaCa.Focus();
                return;
            }
            if (txtTenCa.Text == "")
            {
                MessageBox.Show("Ban phai nhap ten ca", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenCa.Focus();
                return;
            }
            if (txtThoiGIan.Text == "")
            {
                MessageBox.Show("Ban phai nhap thoi gian", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtThoiGIan.Focus();
                return;
            }
            string sql;
            sql = "select MaCa from CaLam where MaCa=N'" + txtMaCa.Text.Trim().ToLower() + "'";
            if (Class.functions.CheckKey(sql))
            {
                MessageBox.Show("Bi trung ma chat lieu", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaCa.Focus();
                txtMaCa.Text = "";
                return;
            }
            sql = "insert into CaLam(MaCa,TenCa,ThoiGian) values(N'" + txtMaCa.Text.Trim() + "',N'" + txtTenCa.Text.Trim() + "',N'" + txtThoiGIan.Text.Trim() + "')";
            Class.functions.runsql(sql);
            Load_DataGrid_CaLam();
            btnThem2.Enabled = true;
            btnXoa2.Enabled = true;
            btnSua2.Enabled = true;
            btnLuu2.Enabled = false;
            btnBoqua2.Enabled = false;
            txtMaCa.Enabled = false;
            resetvalue_CaLam();
        }

        private void btnBoqua2_Click(object sender, EventArgs e)
        {
            resetvalue_CaLam();
            btnThem2.Enabled = true;
            btnSua2.Enabled = true;
            btnXoa2.Enabled = true;
            btnBoqua2.Enabled = true;
            btnLuu2.Enabled = false;
            txtMaCa.Enabled = true;
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblcalam.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaCa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE CaLam WHERE MaCa =N'" + txtMaCa.Text + "'";
                Class.functions.runsql(sql);
                Load_DataGrid_CaLam();
                resetvalue_CaLam();

            }
        }

        private void btnSua2_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblcalam.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaCa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên ca", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCa.Focus();
                return;
            }
            if (txtThoiGIan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập thời gian", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThoiGIan.Focus();
                return;
            }
            sql = "UPDATE CaLam SET TenCa=N'" + txtTenCa.Text.ToString() + "', ThoiGian=N'"+txtThoiGIan.Text.ToString() + "' WHERE MaCa=N'" + txtMaCa.Text + "'";
            Class.functions.runsql(sql);
            Load_DataGrid_CaLam();
            resetvalue_CaLam();
            btnBoqua2.Enabled = false;
        }










        //NHÂN VIÊN

        private void Load_DataGrid_NhanVien()
        {
            string sql;
            sql = "select * from NhanVien";
            tblnv = Class.functions.GetDataToTable(sql);
            dgridNhanVien.DataSource = tblnv;
            dgridNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgridNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgridNhanVien.Columns[2].HeaderText = "Mã ca";
            dgridNhanVien.Columns[3].HeaderText = "Năm sinh";
            dgridNhanVien.Columns[4].HeaderText = "Giới tính";
            dgridNhanVien.Columns[5].HeaderText = "Địa chỉ";
            dgridNhanVien.Columns[6].HeaderText = "Điện thoại";
            dgridNhanVien.AllowUserToAddRows = false;
            dgridNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void resetvalue_NhanVien()
        {
            txtMaNV.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            cboCaLam.Text = "";
            cboGioiTinh.Text = "";
            mskDienThoai.Text = "";
            mskNgaySinh.Text = "";

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

    }
}
