﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChinhChien.Database;
using ChinhChien.DAL_DAO;

namespace ChinhChien.Forms_QuanLy
{
    public partial class KhoHang : Form
    {
        static string sqlVatLieu;
        KhoDAO khoDAO = new KhoDAO();

        public string MaQuan
        {
            get;
            set;
        }

        public string MaKho
        {
            get;
            set;
        }

        public string TenKho
        {
            get;
            set;
        }

        public KhoHang()
        {
            InitializeComponent();
            
        }
        
        private void KhoHang_Load(object sender, EventArgs e)
        {
            RefreshKho();
            RefreshVatLieu(cbbKho.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ChinhChien.GUI.formThem.ThemCapnhatVatLieu themCapnhatVatLieu = new ChinhChien.GUI.formThem.ThemCapnhatVatLieu();
            themCapnhatVatLieu.Owner = this;
            themCapnhatVatLieu.Show();
        }

        public void RefreshKho()
        {
            var fMainUI = (ChinhChien.UI.frmMainUI)this.Owner;
            this.MaQuan = fMainUI.MaQuan;
            this.TenKho = cbbKho.Text;

            cbbKho.DataSource = khoDAO.LayDanhSachKho(this.MaQuan);
            
            cbbKho.DisplayMember = "TenKho";
            cbbKho.ValueMember = "MaKho";
            this.MaKho = (string)cbbKho.SelectedValue;
        }

        public void RefreshVatLieu(string TenKho)
        {
            var fMainUI = (ChinhChien.UI.frmMainUI)this.Owner;
            this.MaQuan = fMainUI.MaQuan;
            this.TenKho = cbbKho.Text;

            sqlVatLieu = "SELECT vl.MaVatLieu, vl.TenVatLieu, vl.NhaCungCap, vl.SoLuong, vl.Gia, vl.DonViTinh FROM VatLieu vl, Kho k where vl.MaQuan = '"
                + fMainUI.MaQuan + "' and k.TenKho = '" + TenKho + "' and vl.MaKho = k.MaKho";

            dgvVatLieu.DataSource = DataProvider.ReturnDataTable(sqlVatLieu);

            dgvVatLieu.Columns["MaVatLieu"].HeaderText = "Mã vật liệu";
            dgvVatLieu.Columns["MaVatLieu"].Width = 75;

            dgvVatLieu.Columns["TenVatLieu"].HeaderText = "Tên vật liệu";
            dgvVatLieu.Columns["TenVatLieu"].Width = 200;

            dgvVatLieu.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
            dgvVatLieu.Columns["NhaCungCap"].Width = 200;

            dgvVatLieu.Columns["SoLuong"].HeaderText = "Số lượng";
            
            dgvVatLieu.Columns["Gia"].HeaderText = "Đơn giá";
            
            dgvVatLieu.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            
            // Mình hạn chế số cột trong Query nên các dòng ở dưới không tác dụng nữa
            //dgvVatLieu.Columns["MaQuan"].Visible = false;
            //dgvVatLieu.Columns["MaKho"].Visible = false;
        }

        public void RefreshVatLieu()
        {
            var fMainUI = (ChinhChien.UI.frmMainUI)this.Owner;
            sqlVatLieu = "SELECT * FROM VatLieu where MaQuan = '" + fMainUI.MaQuan + "'";

            dgvVatLieu.DataSource = DataProvider.ReturnDataTable(sqlVatLieu);

            dgvVatLieu.Columns["MaVatLieu"].HeaderText = "Mã vật liệu";
            dgvVatLieu.Columns["TenVatLieu"].HeaderText = "Tên vật liệu";
            dgvVatLieu.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
            dgvVatLieu.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvVatLieu.Columns["Gia"].HeaderText = "Đơn giá";
            dgvVatLieu.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            dgvVatLieu.Columns["MaQuan"].Visible = false;
            dgvVatLieu.Columns["MaKho"].Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //int i = dgvVatLieu.CurrentCell.RowIndex;
            int i = dgvVatLieu.SelectedRows[0].Index;
            string MaVatLieu = dgvVatLieu.SelectedRows[0].Cells[0].Value.ToString();
            string TenVatLieu = dgvVatLieu.SelectedRows[0].Cells[1].Value.ToString();
            string NhaCungCap = dgvVatLieu.SelectedRows[0].Cells[2].Value.ToString();
            int SoLuong = int.Parse(dgvVatLieu.SelectedRows[0].Cells[3].Value.ToString());
            float Gia = float.Parse(dgvVatLieu.SelectedRows[0].Cells[4].Value.ToString());
            string DonViTinh = dgvVatLieu.SelectedRows[0].Cells[5].Value.ToString();
            string MaQuan = this.MaQuan;
            string MaKho = this.MaKho;

            ChinhChien.GUI.formThem.ThemCapnhatVatLieu ThemVatLieu
                = new ChinhChien.GUI.formThem.ThemCapnhatVatLieu(
                    MaVatLieu, TenVatLieu, NhaCungCap, SoLuong, Gia, DonViTinh, MaQuan, MaKho);
            ThemVatLieu.Owner = this;
            ThemVatLieu.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string MaVatLieu = dgvVatLieu.SelectedRows[0].Cells[0].Value.ToString();
            XoaDAL.VatLieu(MaVatLieu);
        }

        private void ipbThemKho_Click(object sender, EventArgs e)
        {

        }

        private void cbbKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshVatLieu(cbbKho.Text);
        }
    }
}
