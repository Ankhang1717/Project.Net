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
using Excel = Microsoft.Office.Interop.Excel;

namespace Project_CuoiKi.All_User_Control
{
    public partial class UC_HOADON : UserControl
    {
        public UC_HOADON()
        {
            InitializeComponent();
        }
        private void UC_HOADON_Load_1(object sender, EventArgs e)
        {
            txtmahoadonnhap.Enabled = false;
            btnluu2.Enabled = false;
            btnboqua2.Enabled = false;
            functions.fillcombo("SELECT MaNCC, MaNCC FROM NhaCungCap", cbonhacungcap, "MaNCC", "MaNCC");
            cbonhacungcap.SelectedIndex = -1;
            Load_datagridview();
        }
        System.Data.DataTable tbllhoadonnhap;
        public void Load_datagridview()
        {
            string sql = "SELECT * FROM HoaDonNhap";
            tbllhoadonnhap = functions.GetDataToTable(sql);
            dgridhoadonnhap.DataSource = tbllhoadonnhap;
            dgridhoadonnhap.Columns[0].HeaderText = "Mã Hóa Đơn Nhập";
            dgridhoadonnhap.Columns[1].HeaderText = "Mã Nhà Cung Cấp";
            dgridhoadonnhap.Columns[2].HeaderText = "Ngày Nhập";
            dgridhoadonnhap.Columns[3].HeaderText = "Tổng Tiền";

            foreach (DataGridViewColumn col in dgridhoadonnhap.Columns)
            {
                col.Width = 100;
            }
            dgridhoadonnhap.AllowUserToAddRows = false;
            dgridhoadonnhap.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgridhoadonnhap.AllowUserToAddRows = false;
            dgridhoadonnhap.EditMode = DataGridViewEditMode.EditProgrammatically;
            foreach (DataGridViewRow row in dgridhoadonnhap.Rows)
            {
                if (row.Cells["NgayNhap"].Value != null && row.Cells["NgayNhap"].Value.ToString() != "")
                {
                    DateTime ngaynhap;
                    if (DateTime.TryParse(row.Cells["NgayNhap"].Value.ToString(), out ngaynhap))
                    {
                        row.Cells["NgayNhap"].Value = ngaynhap.ToString("MM-dd-yyyy");
                    }
                }
            }
        }
        private void btnthem2_Click(object sender, EventArgs e)
        {
            btnsua2.Enabled = false;
            btnxoa2.Enabled = false;
            btnboqua2.Enabled = true;
            btnluu2.Enabled = true;
            btnthem2.Enabled = false;
            ResetValues();
            txtmahoadonnhap.Enabled = true;
            txtmahoadonnhap.Focus();
        }
        private void ResetValues()
        {
            txtmahoadonnhap.Text = "";
            cbonhacungcap.Text = "";
            mskngaynhap.Text = "";
            txttongtien.Text = "0";
        }

        private void dgridhoadonnhap_Click(object sender, EventArgs e)
        {
            functions.fillcombo("SELECT MaNCC,MaNCC FROM NhaCungCap", cbonhacungcap, "MaNCC", "MaNCC");
            cbonhacungcap.SelectedIndex = -1;

            if (btnthem2.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmahoadonnhap.Focus();
                return;
            }

            if (tbllhoadonnhap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtmahoadonnhap.Text = dgridhoadonnhap.CurrentRow.Cells["MaHDN"].Value.ToString();
            cbonhacungcap.SelectedValue = dgridhoadonnhap.CurrentRow.Cells["MaNCC"].Value.ToString();
            mskngaynhap.Text = dgridhoadonnhap.CurrentRow.Cells["NgayNhap"].Value.ToString();
            txttongtien.Text = dgridhoadonnhap.CurrentRow.Cells["TongTien"].Value.ToString();
            string ngaynhapstr = dgridhoadonnhap.CurrentRow.Cells["NgayNhap"].Value.ToString();
            DateTime ngaynhap;
            if (DateTime.TryParse(ngaynhapstr, out ngaynhap))
            {
                mskngaynhap.Text = ngaynhap.ToString("dd-MM-yyyy");
            }
            else
            {
                mskngaynhap.Text = ""; // Hoặc gán giá trị mặc định nếu không chuyển đổi được
            }
            btnsua2.Enabled = true;
            btnxoa2.Enabled = true;
            btnboqua2.Enabled = true;
        }

        private void btnsua2_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbllhoadonnhap.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtmahoadonnhap.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbonhacungcap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbonhacungcap.Focus();
                return;
            }

            if (mskngaynhap.Text == " / /")
            {
                MessageBox.Show("Bạn phải nhập ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskngaynhap.Focus();
                return;
            }
            if (!functions.isdate(mskngaynhap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskngaynhap.Text = "";
                mskngaynhap.Focus();
                return;
            }
            sql = "UPDATE HoaDonNhap SET  MaNCC = N'" + cbonhacungcap.SelectedValue.ToString() + "',NgayNhap='" + functions.convertdatetime(mskngaynhap.Text) +
            "',TongTien = N'" + txttongtien.Text.Trim() + "'  WHERE MaHDN=N'" + txtmahoadonnhap.Text + "'";
            functions.runsql(sql);
            Load_datagridview();
            ResetValues();
            btnboqua2.Enabled = false;
        }

        private void btnluu2_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtmahoadonnhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmahoadonnhap.Focus();
                return;
            }

            if (cbonhacungcap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbonhacungcap.Focus();
                return;
            }
            if (mskngaynhap.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskngaynhap.Focus();
                return;
            }
            if (txttongtien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tổng tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttongtien.Focus();
                return;
            }
            if (!functions.isdate(mskngaynhap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskngaynhap.Text = "";
                mskngaynhap.Focus();
                return;
            }
            sql = "SELECT MaHDN FROM HoaDonNhap WHERE MaNCC = N'" + txtmahoadonnhap.Text.Trim() + "'";
            if (functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hóa đơn này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmahoadonnhap.Focus();
                txtmahoadonnhap.Text = "";
                return;
            }
            sql = "INSERT INTO HoaDonNhap(MaHDN, MaNCC, NgayNhap, TongTien) VALUES(N'" + txtmahoadonnhap.Text.Trim() + "', N'" + cbonhacungcap.SelectedValue.ToString() + "', '" + functions.convertdatetime(mskngaynhap.Text).ToString() + "',N'" + txttongtien.Text.Trim() + "')";
            functions.runsql(sql);
            Load_datagridview();
            ResetValues();
            btnxoa2.Enabled = true;
            btnthem2.Enabled = true;
            btnsua2.Enabled = true;
            btnboqua2.Enabled = false;
            btnluu2.Enabled = false;
            txtmahoadonnhap.Enabled = false;

        }

        private void btnxoa2_Click(object sender, EventArgs e)
        {

            if (tbllhoadonnhap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmahoadonnhap.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = "DELETE FROM HoaDonNhap WHERE MaHDN = '" + txtmahoadonnhap.Text + "'";
                functions.runsql(sql);
                Load_datagridview();
                ResetValues();
            }
        }

        private void btnboqua2_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnboqua2.Enabled = false;
            btnthem2.Enabled = true;
            btnxoa2.Enabled = true;
            btnsua2.Enabled = true;
            btnluu2.Enabled = false;
            txtmahoadonnhap.Enabled = false;
        }

        private void btndong2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng cửa sổ hóa đơn nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void btninhoadon_Click(object sender, EventArgs e)
        {
            System.Data.DataTable hdn;
            string sql = "SELECT MaHDN, MaNCC, NgayNhap, TongTien " +
                         "FROM HoaDonNhap WHERE MaHDN = N'" + txtmahoadonnhap.Text + "'";
            hdn = functions.GetDataToTable(sql);
            if (dgridhoadonnhap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmahoadonnhap.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    Excel.Application exApp = new Excel.Application();
                    Excel.Workbook exBook;
                    Excel.Worksheet exSheet;
                    Excel.Range exRange;

                    // Thêm một Workbook mới
                    exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    // Định dạng chung cho trang tính
                    exRange = (Excel.Range)exSheet.Cells[1, 1];
                    exRange.Range["A1:Z300"].Font.Name = "Times New Roman";
                    exRange.Range["A1:B3"].Font.Size = 11;
                    exRange.Range["A1:B3"].Font.Bold = true;
                    exRange.Range["A1:B3"].Font.ColorIndex = 5;
                    exRange.Range["A1:A1"].ColumnWidth = 10;
                    exRange.Range["B1:B1"].ColumnWidth = 16;
                    exRange.Range["A1:C1"].MergeCells = true;
                    exRange.Range["A1:C1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["A1:C1"].Value = "Tiny Cyber";
                    exRange.Range["A2:C2"].MergeCells = true;
                    exRange.Range["A2:C2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["A2:C2"].Value = "Hà Nội";
                    exRange.Range["A3:C3"].MergeCells = true;
                    exRange.Range["A3:C3"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["A3:C3"].Value = "Điện thoại: 0123456789";
                    exRange.Range["D2:F2"].Font.Size = 16;
                    exRange.Range["D2:F2"].Font.Bold = true;
                    exRange.Range["D2:F2"].Font.ColorIndex = 3;
                    exRange.Range["D2:F2"].MergeCells = true;
                    exRange.Range["D2:F2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["D2:F2"].Value = "HÓA ĐƠN NHẬP";
                    exRange.Range["D3:F3"].Font.Size = 14;
                    exRange.Range["D3:F3"].Font.Bold = true;
                    exRange.Range["D3:F3"].Font.ColorIndex = 3;
                    exRange.Range["D3:F3"].MergeCells = true;
                    exRange.Range["D3:F3"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                    // Định dạng tiêu đề cột
                    exRange.Range["B5:F5"].Font.Bold = true;
                    exRange.Range["B5:F5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["B5:B100"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["B5:B5"].ColumnWidth = 8;
                    exRange.Range["C5:C5"].ColumnWidth = 22;
                    exRange.Range["D5:D5"].ColumnWidth = 22;
                    exRange.Range["E5:E5"].ColumnWidth = 22;
                    exRange.Range["F5:F5"].ColumnWidth = 22;
                    exRange.Range["E5:E5"].Font.Bold = true;
                    exRange.Range["E5:E5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // Tiêu đề cột
                    exRange.Range["B5:B5"].Value = "STT";
                    exRange.Range["C5:C5"].Value = "Mã Hóa Đơn Nhập";
                    exRange.Range["D5:D5"].Value = "Mã Nhà Cung Cấp";
                    exRange.Range["E5:E5"].Value = "Ngày Nhập";
                    exRange.Range["F5:F5"].Value = "Tổng Tiền";
                    int hang = 0, cot = 0;

                    for (hang = 0; hang <= hdn.Rows.Count - 1; hang++)
                    {
                        exSheet.Cells[2][hang + 6] = hang + 1; // Assuming column B for STT
                        for (cot = 0; cot <= hdn.Columns.Count - 1; cot++)
                        {
                            if (cot == hdn.Columns["NgayNhap"].Ordinal) // Check if it's the NgayNhap column
                            {
                                exSheet.Cells[cot + 3][hang + 6] = hdn.Rows[hang][cot].ToString();
                                ((Excel.Range)exSheet.Cells[cot + 3][hang + 6]).NumberFormat = "dd/mm/yyyy"; // Set the format to "dd/mm/yyyy" (or your preferred format)
                            }
                            else
                            {
                                exSheet.Cells[cot + 3][hang + 6] = hdn.Rows[hang][cot].ToString();
                            }
                        }
                    }
                    exRange = (Excel.Range)exSheet.Cells[hdn.Rows.Count + 8, 4];
                    exRange.Range["A1:C1"].MergeCells = true;
                    exRange.Range["A1:C1"].Font.Italic = true;
                    exRange.Range["A1:C1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    exRange.Range["A1:C1"].Value = "Hà Nội, Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

                    // Hiển thị Excel
                    exSheet.Name = "Hóa Đơn Nhập";
                    exApp.Visible = true;
                    MessageBox.Show("Đã in báo cáo thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
    }
}
