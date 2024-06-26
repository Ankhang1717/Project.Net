﻿using Project_CuoiKi.All_User_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CuoiKi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeUserControls();
        }
        private void InitializeUserControls()
        {

        }

            private void Form1_Load(object sender, EventArgs e)
        {
            Class.functions.ketnoi();
            uC_Phong1.Visible = false;
            btnPhong.PerformClick();
            uC_Nhanvien1.Visible = false;
            uC_ThemThietBi1.Visible = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBaotri_Click(object sender, EventArgs e)
        {

        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            PanelMoving.Top = btnPhong.Top + 30;
            uC_Phong1.Visible = true;
            uC_Phong1.BringToFront();
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            PanelMoving.Top = btnNhanvien.Top + 298;
            uC_Nhanvien1.Visible = true;
            uC_Nhanvien1.BringToFront();

        }

        private void btnThietbi_Click(object sender, EventArgs e)
        {
            PanelMoving.Top = btnThietbi.Top + 367;
            uC_ThemThietBi1.Visible = true;
            uC_ThemThietBi1.BringToFront();
        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            PanelMoving.Top = btnBaocao.Top + 161 ;
            uC_Baocao1.Load_DataGridView();
            uC_Baocao1.Visible = true;
            uC_Baocao1.BringToFront();
        }

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            PanelMoving.Top = btnHoadon.Top + 244;
            uC_HOADON1.Visible = true;
            uC_HOADON1.BringToFront() ;
        }
    }
}
