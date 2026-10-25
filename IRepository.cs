/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;
using System.Collections.Generic;

namespace Lab07
{
    /// <summary>Hợp đồng kho lưu trữ theo mã.</summary>
    public interface IRepository<T> where T : IIdentifiable
    {
        /// <summary>Thêm phần tử; ném ArgumentNullException nếu null, DuplicateIdException nếu trùng Id.</summary>
        void Add(T item);

        /// <summary>Xóa theo mã; false nếu không thấy.</summary>
        bool Remove(string id);

        /// <summary>Tìm theo mã; null nếu không thấy.</summary>
        T? FindById(string id);

        /// <summary>Bản sao chỉ đọc của toàn bộ phần tử.</summary>
        IReadOnlyList<T> GetAll();

        /// <summary>Lọc theo điều kiện, giữ thứ tự thêm vào.</summary>
        IReadOnlyList<T> FindAll(Predicate<T> match);
    }
}
