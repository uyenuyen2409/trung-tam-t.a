using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trung_tâm_tiếng_anh
{
    public partial class frmHocVien : Form
    {
        public frmHocVien()
        {
            InitializeComponent();
            dgvHocVien.Columns.Add("MaHV", "Mã HV");
            dgvHocVien.Columns.Add("HoTen", "Họ Tên");
            dgvHocVien.Columns.Add("SoDienThoai", "Số Điện Thoại"); 
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dgvHocVien);
            row.Cells[0].Value = txtMaHV.Text;
            row.Cells[1].Value = txtHoTen.Text;
            row.Cells[2].Value = txtSoDienThoai.Text;
            dgvHocVien.Rows.Add(row);

            txtMaHV.Text = "";
            txtHoTen.Text = "";
            txtSoDienThoai.Text = "";
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}