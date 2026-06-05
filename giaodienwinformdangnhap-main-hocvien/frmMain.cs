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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void btnQuanLyHocVien_Click(object sender, EventArgs e)
        {
            frmHocVien hv = new frmHocVien();
            hv.Show();
        }

        private void btnQuanLyGiangVien_Click(object sender, EventArgs e)
        {
            frmGiangVien gv = new frmGiangVien();
            gv.Show();
        }

        private void btnQuanLyKhoaHoc_Click(object sender, EventArgs e)
        {
            frmKhoaHoc kh = new frmKhoaHoc();
            kh.Show();
        }

        private void btnDangKyHoc_Click(object sender, EventArgs e)
        {
            frmDangKyHoc dk = new frmDangKyHoc();
            dk.Show();
        }
    }
}