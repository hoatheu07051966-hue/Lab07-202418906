/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;

namespace Lab07
{
    /// <summary>
    /// Lớp cơ sở của thiết bị phòng dạy. Borrow/Return dùng chung,
    /// còn điều kiện sẵn sàng và điểm ưu tiên do lớp dẫn xuất quyết định (đa hình).
    /// </summary>
    public abstract class TeachingDevice : IIdentifiable
    {
        public string Id { get; }
        public string Name { get; }
        public string StorageLocation { get; }
        public DeviceStatus Status { get; private set; } = DeviceStatus.Available;

        protected TeachingDevice(string id, string name, string storageLocation)
        {
            Id = Require(id, nameof(id));
            Name = Require(name, nameof(name));
            StorageLocation = Require(storageLocation, nameof(storageLocation));
        }

        /// <summary>Thiết bị có đủ điều kiện để cấp phát không.</summary>
        public abstract bool IsReadyForUse();

        /// <summary>Điểm ưu tiên mặc định của thiết bị.</summary>
        public abstract double GetDefaultPriorityScore();

        /// <summary>Mượn thiết bị; chỉ thành công khi IsReadyForUse() = true.</summary>
        public void Borrow()
        {
            if (!IsReadyForUse())
                throw new DeviceNotReadyException(Id, Status);
            Status = DeviceStatus.Borrowed;
        }

        /// <summary>Trả thiết bị; chỉ thành công khi đang ở trạng thái Borrowed.</summary>
        public void Return()
        {
            if (Status != DeviceStatus.Borrowed)
                throw new InvalidOperationException($"Thiết bị {Id} không ở trạng thái Borrowed.");
            Status = DeviceStatus.Available;
        }

        protected static string Require(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Giá trị không được null hoặc rỗng.", paramName);
            return value;
        }

        protected static int RequirePercent(int value, string paramName)
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(paramName, "Giá trị phải trong khoảng 0–100.");
            return value;
        }

        public override string ToString() => $"{GetType().Name} {Id} ({Name}) - {Status}";
    }
}
