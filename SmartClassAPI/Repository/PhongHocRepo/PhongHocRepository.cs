using Microsoft.EntityFrameworkCore;
using SmartClassAPI.Data;
using SmartClassAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace SmartClassAPI.Repository.PhongHocRepo
{
    public class PhongHocRepository : IPhongHocRepository
    {
        private readonly MyDbContext _context;

        public PhongHocRepository(MyDbContext context)
        {
            _context = context;
        }

        public PhongHocVM Add(PhongHocModel phongHoc)
        {
            var _phonghoc = new PhongHocData
            {
                TenPhongHoc = phongHoc.TenPhongHoc,
                MaPhongHoc = phongHoc.MaPhongHoc,
                MoTa = phongHoc.MoTa,
                IdTinhTrang = phongHoc.IdTinhTrang,
            };
            _context.Add(_phonghoc);
            _context.SaveChanges();
            return new PhongHocVM
            {
                TenPhongHoc = _phonghoc.TenPhongHoc
            };
        }

        public void Delete(int id)
        {
            var phongHoc = _context.PhongHocDatas.SingleOrDefault(ph => ph.IdPhongHoc == id);
            if (phongHoc != null)
            {
                _context.Remove(phongHoc);
                _context.SaveChanges();
            }
        }

        public List<PhongHocVM> GetAll()
        {
            var phongHocs = _context.PhongHocDatas
                .Select(ph => new PhongHocVM
                {
                    IdPhongHoc = ph.IdPhongHoc,
                    MaPhongHoc = ph.MaPhongHoc,
                    TenPhongHoc = ph.TenPhongHoc,
                    MoTa = ph.MoTa,
                    IdTinhTrang = ph.IdTinhTrang,
                    TenTinhTrang = ph.TinhTrang.TenTinhTrang,
                    MonHocs = ph.MonHocs.ToList(),
                })
                .ToList();
            return phongHocs;
        }

        public PhongHocVM GetById(int id)
        {
            var _phongHoc = _context.PhongHocDatas.Include(ph => ph.TinhTrang).FirstOrDefault(ph => ph.IdPhongHoc == id);
            if (_phongHoc != null)
            {
                return new PhongHocVM
                {
                    IdPhongHoc = _phongHoc.IdPhongHoc,
                    MaPhongHoc = _phongHoc.MaPhongHoc,
                    TenPhongHoc = _phongHoc.TenPhongHoc,
                    MoTa = _phongHoc.MoTa,
                    IdTinhTrang = _phongHoc.IdTinhTrang,
                    TenTinhTrang = _phongHoc.TinhTrang.TenTinhTrang,
                    MonHocs = _phongHoc.MonHocs.ToList(),
                };
            }
            return null;
        }

        public void Update(int id, PhongHocVM phongHoc)
        {
            var _phongHoc = _context.PhongHocDatas.FirstOrDefault(ph => ph.IdPhongHoc == id);
            if (_phongHoc == null) return;

            _phongHoc.MaPhongHoc = phongHoc.MaPhongHoc ?? _phongHoc.MaPhongHoc;
            _phongHoc.TenPhongHoc = phongHoc.TenPhongHoc ?? _phongHoc.TenPhongHoc;
            _phongHoc.MoTa = phongHoc.MoTa ?? _phongHoc.MoTa;
            _phongHoc.IdTinhTrang = phongHoc.IdTinhTrang; // Cập nhật IdTinhTrang

            // Cập nhật lại tên tình trạng dựa trên IdTinhTrang mới
            var tinhTrang = _context.TinhTrangPhongHocs.FirstOrDefault(t => t.IdTinhTrang == phongHoc.IdTinhTrang);
            _phongHoc.TinhTrang = tinhTrang;
            _context.SaveChanges();
        }
    }
}
