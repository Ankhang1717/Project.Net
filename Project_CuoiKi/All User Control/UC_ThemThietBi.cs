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
    public partial class UC_ThemThietBi : UserControl
    {
        public UC_ThemThietBi()
        {
            InitializeComponent();

            Class.functions.ketnoi();
        }
        private void UC_ThemThietBi_Load(object sender, EventArgs e)
        {
            Class.functions.fillcombo("SELECT MaLoaiTB,TenLoaiTB  FROM LoaiThietBi", cboloaithietbi,"MaLoaiTB", "TenLoaiTB");
            cboloaithietbi.SelectedIndex = -1;
            Class.functions.fillcombo("SELECT MaNhomTB,TenNhomTB  FROM NhomThietBi", cbonhomthietbi, "MaNhomTB", "TenNhomTB");
            cbonhomthietbi.SelectedIndex = -1;
            Load_DataGridView();
        }
        DataTable tblcl;
        private void Load_DataGridView()
        {
            string sql = "SELECT * FROM ThietBi";
            tblcl = functions.GetDataToTable(sql);
            dgridthietbi.DataSource = tblcl;
            dgridthietbi.Columns[0].HeaderText = "Mã Thiết Bị";
            dgridthietbi.Columns[1].HeaderText = "Tên Thiết Bị";
            dgridthietbi.Columns[2].HeaderText = "Mã Loại Thiết Bị";
            dgridthietbi.Columns[3].HeaderText = "Mã Nhóm Thiết Bị";
            dgridthietbi.Columns[4].HeaderText = "Mã Nhà Cung Cấp";
            dgridthietbi.Columns[5].HeaderText = "Giá";
            dgridthietbi.Columns[6].HeaderText = "Bảo Hành";
            dgridthietbi.Columns[7].HeaderText = "Số Lượng";

            foreach (DataGridViewColumn col in dgridthietbi.Columns)
            {
                col.Width = 100;
            }
            dgridthietbi.AllowUserToAddRows = false;
            dgridthietbi.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgridthietbi.AllowUserToAddRows = false;
            dgridthietbi.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValues()
        {
            txtmathietbi.Text = "";
            txttenthietbi.Text = "";
            cboloaithietbi.Text = "";
            cbonhomthietbi.Text = "";
            txtmanhacungcap.Text = "";
            txtgia.Text = "";
            txtbaohanh.Text = "";
            txtsoluong  .Text = "";

        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnboqua.Enabled = true;
            btnluu.Enabled = true;
            btnthem.Enabled = false;
            ResetValues();
            txtmathietbi.Enabled = true;
            txttenthietbi.Focus();
        }

        private void dgridthietbi_Click(object sender, EventArgs e)
        {
            if (btnthem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmathietbi.Focus();
                return;
            }

            if (tblcl.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtmathietbi.Text = dgridthietbi.CurrentRow.Cells["MaTB"].Value.ToString();
            txttenthietbi.Text = dgridthietbi.CurrentRow.Cells["TenTB"].Value.ToString();
            cboloaithietbi.SelectedValue = dgridthietbi.CurrentRow.Cells["MaLoaiTB"].Value.ToString();
            cbonhomthietbi.SelectedValue = dgridthietbi.CurrentRow.Cells["MaNhomTB"].Value.ToString();
            txtmanhacungcap.Text = dgridthietbi.CurrentRow.Cells["MaNCC"].Value.ToString();
            txtgia.Text = dgridthietbi.CurrentRow.Cells["Gia"].Value.ToString();
            txtbaohanh.Text = dgridthietbi.CurrentRow.Cells["BaoHanh"].Value.ToString();
            txtsoluong.Text = dgridthietbi.CurrentRow.Cells["SoLuong"].Value.ToString();

            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnboqua.Enabled = true;
        }
        private void btnluu_Click(object sender, EventArgs e)
        {
            {
                if (txtmathietbi.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmathietbi.Focus();
                    return;
                }
                if (txttenthietbi.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txttenthietbi.Focus();
                    return;
                }
                if (cboloaithietbi.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn phải chọn loại thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboloaithietbi.Focus();
                    return;
                }
                if (cbonhomthietbi.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn phải chọn nhóm thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbonhomthietbi.Focus();
                    return;
                }
                if (txtmanhacungcap.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmanhacungcap.Focus();
                    return;
                }
                if (txtgia.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập giá thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtgia.Focus();
                    return;
                }
                if (txtbaohanh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập thời gian bảo hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtbaohanh.Focus();
                    return;
                }
                if (txtsoluong.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập số lượng thiết bị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtsoluong.Focus();
                    return;
                }
                string sql = "INSERT INTO ThietBi (MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong) VALUES ('" + txtmathietbi.Text.Trim() + "', '" + txttenthietbi.Text.Trim() + "', '" + cboloaithietbi.SelectedValue + "', '" + cbonhomthietbi.SelectedValue + "', '" + txtmanhacungcap.Text.Trim() + "', '" + txtgia.Text.Trim() + "', '" + txtbaohanh.Text.Trim() + "', '" + txtsoluong.Text.Trim() + "')";
                functions.runsql(sql);
                Load_DataGridView();
                ResetValues();
                btnxoa.Enabled = true;
                btnthem.Enabled = true;
                btnsua.Enabled = true;
                btnboqua.Enabled = false;
                btnluu.Enabled = false;
            }
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Parent.Controls.Remove(this);
            }
        }

    }
}
