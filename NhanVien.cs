using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap4
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
            isEdit = false;

        }

        public NhanVien NhanVien { get; private set; }
        private bool isEdit = false;

        public frmNhanVien(NhanVien nv)
        {
            InitializeComponent();
            if (nv != null)
            {
                isEdit = true;
                txtMSNV.Text = nv.MSNV;
                txtHoTen.Text = nv.TenNV;
                txtLuongCB.Text = nv.LuongCB.ToString();
                txtMSNV.Enabled = false;  // Không cho sửa MSNV
            }
        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                decimal luongCB;
                if (!decimal.TryParse(txtLuongCB.Text, out luongCB))
                {
                    MessageBox.Show("Lương cơ bản không hợp lệ!");
                    return;
                }

                NhanVien = new NhanVien(
                    txtMSNV.Text,
                    txtHoTen.Text,
                    luongCB
                );

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMSNV.Text))
            {
                MessageBox.Show("Vui lòng nhập MSNV!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLuongCB.Text))
            {
                MessageBox.Show("Vui lòng nhập lương cơ bản!");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtLuongCB_TextChanged(object sender, EventArgs e)
        {
            decimal luongCB;
            if (!decimal.TryParse(txtLuongCB.Text, out luongCB))
            {
                txtLuongCB.BackColor = Color.LightPink;
            }
            else
            {
                txtLuongCB.BackColor = SystemColors.Window;
            }
        }
    }
}
