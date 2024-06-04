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

        //TÌM KIẾM
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if ((txtTen.Text == "") && (cboCaLam.Text == "") && (cboGioiTinh.Text == "") && (txtDiaChi.Text == "") && (mskDienThoai.Text == "(   )    -") && (mskNgaySinh.Text == "  /  /"))
            {
                MessageBox.Show("Bạn phải nhập 1 điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string sql;
            sql = "select * from nhanvien where 1=1";
            if (txtTen.Text != "")
            {
                sql = sql + "AND tennv like N'%" + txtTen.Text + "%'";
                tblnv = functions.GetDataToTable(sql);
                dgridNhanVien.DataSource = tblnv;
                if (tblnv.Rows.Count == 0)
                {
                    MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (txtDiaChi.Text != "")
            {
                sql = sql + "AND diachi like N'%" + txtDiaChi.Text + "%'";
                tblnv = functions.GetDataToTable(sql);
                dgridNhanVien.DataSource = tblnv;
                if (tblnv.Rows.Count == 0)
                {
                    MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (mskNgaySinh.Text != "  /  /")
            {
                string input = mskNgaySinh.Text;

                string[] parts = input.Split('/');
                string ngay = parts[0].Trim();
                string thang = parts[1].Trim();
                string nam = parts[2].Trim();

                string condition = "";
                if (!string.IsNullOrEmpty(ngay))
                {
                    condition += "DAY(namsinh) = " + ngay;
                }
                if (!string.IsNullOrEmpty(thang))
                {
                    if (condition != "") condition += " AND ";
                    condition += "MONTH(namsinh) = " + thang;
                }
                if (!string.IsNullOrEmpty(nam))
                {
                    if (condition != "") condition += " AND ";
                    condition += "YEAR(namsinh) = " + nam;
                }

                if (condition != "")
                {
                    sql = sql + "and " + condition;
                    tblnv = functions.GetDataToTable(sql);
                    dgridNhanVien.DataSource = tblnv;
                    if (tblnv.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            if (mskDienThoai.Text != "(   )    -")
            {
                string input1 = mskDienThoai.Text;

                // Loại bỏ các ký tự không phải là số
                string sdt = new string(input1.Where(char.IsDigit).ToArray());

                if (sdt.Length != 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.");
                    return;
                }
                else
                {
                    sql = sql + "and dienthoai = " + sdt + "";
                    tblnv = functions.GetDataToTable(sql);
                    dgridNhanVien.DataSource = tblnv;
                    if (tblnv.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (cboCaLam.SelectedIndex != -1)
            {
                string tenCa = cboCaLam.SelectedItem.ToString();
                string sqlmaca = "select maca from calam where tenca = N'" + tenCa + "'";
                DataTable dtMaCa = functions.GetDataToTable(sqlmaca);
                if(dtMaCa.Rows.Count > 0)
                {
                    string maca = dtMaCa.Rows[0]["maca"].ToString();
                    sql = sql + "AND maca = N'" + maca + "'";
                    tblnv = functions.GetDataToTable(sql);
                    dgridNhanVien.DataSource = tblnv;
                    if (tblnv.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }               
            }

            if (cboGioiTinh.SelectedIndex != -1)
            {
                string gt = cboGioiTinh.SelectedItem.ToString();
                sql = sql + "and gioitinh = N'" + gt + "'";
                tblnv = functions.GetDataToTable(sql);
                dgridNhanVien.DataSource = tblnv;
                if (tblnv.Rows.Count == 0)
                {
                    MessageBox.Show("Không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnHienthitatca_Click(object sender, EventArgs e)
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
            txtTen.Text = "";
            cboCaLam.SelectedIndex = -1;
            cboGioiTinh.SelectedIndex = -1;
            mskDienThoai.Text = "(   )    -";
            mskNgaySinh.Text = "  /  /";
            txtDiaChi.Text = "";
        }
    }
}
