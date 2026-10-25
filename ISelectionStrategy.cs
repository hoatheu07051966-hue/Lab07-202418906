/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System.Collections.Generic;

namespace Lab07
{
    /// <summary>Chiến lược chọn một phần tử từ danh sách ứng viên.</summary>
    public interface ISelectionStrategy<T>
    {
        /// <summary>Ném NoSuitableItemException nếu danh sách rỗng; không sửa danh sách đầu vào.</summary>
        T Select(IReadOnlyList<T> candidates);
    }
}
