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
    public partial class frmListView : Form
    {
        private List<NhanVien> danhSachNhanVien = new List<NhanVien>();
        public frmListView()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvNhanvien.AutoGenerateColumns = false;

            // Thêm các cột cho DataGridView
            dgvNhanvien.Columns.Add("MSNV", "MSNV");
            dgvNhanvien.Columns.Add("TenNV", "Tên NV");
            dgvNhanvien.Columns.Add("LuongCB", "Lương cơ bản");

            // Định dạng cột lương hiển thị dạng tiền tệ
            DataGridViewColumn colLuong = dgvNhanvien.Columns["LuongCB"];
            colLuong.DefaultCellStyle.Format = "N0";
            colLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadDataGridView()
        {
            dgvNhanvien.Rows.Clear();
            foreach (var nv in danhSachNhanVien)
            {
                dgvNhanvien.Rows.Add(nv.MSNV, nv.TenNV, nv.LuongCB);
            }
        }

        private void dgvNhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien();
            frm.Text = "Thêm Nhân Viên";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                danhSachNhanVien.Add(frm.NhanVien);
                LoadDataGridView();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanvien.CurrentRow != null)
                {
                    string msnv = dgvNhanvien.CurrentRow.Cells["MSNV"].Value?.ToString();
                    if (string.IsNullOrEmpty(msnv))
                    {
                        MessageBox.Show("Không tìm thấy MSNV!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    NhanVien nv = danhSachNhanVien.Find(x => x.MSNV == msnv);
                    if (nv == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    frmNhanVien frm = new frmNhanVien(nv);
                    frm.Text = "Sửa Nhân Viên";
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        int index = danhSachNhanVien.FindIndex(x => x.MSNV == msnv);
                        danhSachNhanVien[index] = frm.NhanVien;
                        LoadDataGridView();
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanvien.CurrentRow != null && dgvNhanvien.CurrentRow.Cells["MSNV"].Value != null)
                {
                    string msnv = dgvNhanvien.CurrentRow.Cells["MSNV"].Value.ToString();

                    // Xác nhận trước khi xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc muốn xóa nhân viên có MSNV: {msnv}?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Tìm và xóa nhân viên khỏi danh sách
                        NhanVien nhanVienCanXoa = danhSachNhanVien.Find(x => x.MSNV == msnv);
                        if (nhanVienCanXoa != null)
                        {
                            danhSachNhanVien.Remove(nhanVienCanXoa);
                            LoadDataGridView(); // Cập nhật lại DataGridView
                            MessageBox.Show("Đã xóa nhân viên thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên cần xóa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi xóa: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đóng form?", "Xác nhận",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }

    public class NhanVien
    {
        public string MSNV { get; set; }
        public string TenNV { get; set; }
        public decimal LuongCB { get; set; }

        public NhanVien(string msnv, string tenNV, decimal luongCB)
        {
            MSNV = msnv;
            TenNV = tenNV;
            LuongCB = luongCB;
        }
    }
}
