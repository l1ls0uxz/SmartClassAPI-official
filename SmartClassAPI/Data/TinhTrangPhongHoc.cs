using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClassAPI.Data
{
    [Table("TinhTrangPhongHoc")]
    public class TinhTrangPhongHoc
    {
        [Key]
        public int IdTinhTrang { get; set; }
        public string TenTinhTrang { get; set; }
    }
}
