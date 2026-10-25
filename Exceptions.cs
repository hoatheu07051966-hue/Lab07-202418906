/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;

namespace Lab07
{
    /// <summary>Thêm phần tử có Id đã tồn tại trong kho.</summary>
    public class DuplicateIdException : Exception
    {
        public string DuplicateId { get; }

        public DuplicateIdException(string duplicateId)
            : base($"Mã {duplicateId} đã tồn tại trong kho.")
        {
            DuplicateId = duplicateId;
        }
    }

    /// <summary>Không có ứng viên phù hợp để chọn.</summary>
    public class NoSuitableItemException : Exception
    {
        public NoSuitableItemException()
            : base("Không có thiết bị phù hợp để cấp phát.") { }
    }

    /// <summary>Mượn thiết bị chưa sẵn sàng.</summary>
    public class DeviceNotReadyException : Exception
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceNotReadyException(string deviceId, DeviceStatus currentStatus)
            : base($"Thiết bị {deviceId} chưa sẵn sàng (trạng thái: {currentStatus}).")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }
}
