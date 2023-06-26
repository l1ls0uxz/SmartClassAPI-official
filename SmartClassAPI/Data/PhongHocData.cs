using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SmartClassAPI.Data
{
    [Table("PhongHoc")]
    public class PhongHocData
    {
        [Key]
        public int IdPhongHoc { get; set; }
        [Required]
        public string MaPhongHoc { get; set; }
        public string TenPhongHoc { set; get; }
        public string MoTa { get; set; }
        public int TinhTrang { get; set; }
        [Required, NotNull]
        //public int? IdMonHoc { get; set; }
        public List<MonHoc> MonHocs { get; set; }
        //public MonHoc MonHoc { get; set; }

        // tạo ra các kết nối
        public ICollection<TkbData> TkbDatas { get; set; }

        public PhongHocData()
        {
            TkbDatas = new HashSet<TkbData>(); // khi khởi tạo truyền về 1 list rỗng trước
        }
    }
}