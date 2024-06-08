using Project_CuoiKi.All_User_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CuoiKi.Forms
{
    public partial class ThueMay : Form
    {
        public UC_Danhsachmay frmDSMay = null;
        public ThueMay(string maPhong, string maMay)
        {
            InitializeComponent();
            txtMaPhong.Text = maPhong;
            txtMaMay.Text = maMay;
            mskGioVao.Text = DateTime.Now.TimeOfDay.ToString();
            mskGioVao.ReadOnly = true;
            mskGioRa.ReadOnly = true;

            txtTongTien.ReadOnly = true;
        }

        private void ThueMay_Load(object sender, EventArgs e)
        {
            Class.functions.ketnoi();
            txtMaHDB.Enabled = false;
            txtMaPhong.Enabled = false;
            txtMaMay.Enabled = false;
            txtTongTien.Enabled = false;
            mskNgayThue.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtMaHDB.Text = Class.functions.GenerateMaHDB();
            Class.functions.fillcombo("Select MaNV, TenNV from Nhanvien ", cbMaNV, "MaNV", "MaNV");
        }

        private void mskGioRa_MaskChanged(object sender, EventArgs e)
        {
            
        }

        private void mskGioRa_TextChanged(object sender, EventArgs e)
        {
            //string strTime = mskGioRa.Text;
            //TimeSpan time;

            //if (TimeSpan.TryParse(strTime, out time) == true)
            //{
            //    float rentTime = (float)(Convert.ToDecimal(time.TotalHours) - Convert.ToDecimal(TimeSpan.Parse(mskGioVao.Text).TotalHours));

            //    float total = rentTime * 10000;
            //    txtTongTien.Text = total.ToString("N0") + " VND";
            //}
        }

        private void txtSoGioThue_TextChanged(object sender, EventArgs e)
        {
            string strRentTime = txtSoGioThue.Text;
            float rentTime;
            if (float.TryParse(strRentTime, out rentTime) == true)
            {
                TimeSpan rentTimeTimeSpan = TimeSpan.FromHours((double)(new decimal(rentTime)));

                TimeSpan endTime = TimeSpan.Parse(mskGioVao.Text).Add(rentTimeTimeSpan);

                mskGioRa.Text = new DateTime(endTime.Ticks).ToString("HH:mm");

                float total = rentTime * 10000;

                txtTongTien.Text = total.ToString("N0");
            }
        }

        private void txtSoGioThue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            this.frmDSMay.Load_DataGridView(frmDSMay.maPhongSelected);
            if (txtSoGioThue.Text == "")
            {
                MessageBox.Show("Chưa nhập số giờ chơi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ThemDuLieu();

            string maPhong = txtMaPhong.Text;
            string maMay = txtMaMay.Text;
            Forms.DangXuat frmDangXuat = new Forms.DangXuat();
            frmDangXuat.Text = $"{maPhong} - {maMay}";
            frmDangXuat.SetRentInfo(maPhong, maMay);
            frmDangXuat.Show();

            frmDangXuat.frmDSMay = this.frmDSMay;
  

                this.Close();
        }

        private void ThemDuLieu()
        {
            string gioVao = new DateTime(TimeSpan.Parse(mskGioVao.Text).Ticks).ToString("HH:mm");
            string gioRa = new DateTime(TimeSpan.Parse(mskGioRa.Text).Ticks).ToString("HH:mm");
            float tongTien = float.Parse(txtTongTien.Text, CultureInfo.InvariantCulture.NumberFormat);

            string sql;
            sql = "INSERT INTO Hoadonban (MaHDB, MaMay, MaPhong, NgayThue, GioVao, GioRa, MaNV, TongTien, GhiChu)" +
                " values(N'" + txtMaHDB.Text + "', N'" + txtMaMay.Text + "', N'" + txtMaPhong.Text + "', N'" + mskNgayThue.Text + "', N'" + gioVao + "', N'" + gioRa + "', N'" + cbMaNV.SelectedValue.ToString() + "', N'" + tongTien + "', N'" + txtGhiChu.Text + "')";
            sql += " UPDATE May set trangthai = '1' where mamay = N'" + txtMaMay.Text + "' ";
            Class.functions.runsql(sql);
        }
    }
}
