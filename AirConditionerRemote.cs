/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;
using System.Collections.Generic;

namespace Lab07
{
    /// <summary>Điều khiển điều hòa: sẵn sàng khi Available; điểm phụ thuộc phòng tương thích.</summary>
    public class AirConditionerRemote : TeachingDevice
    {
        private readonly HashSet<string> _compatibleRooms;

        public IReadOnlyCollection<string> CompatibleRooms => _compatibleRooms;

        public AirConditionerRemote(string id, string name, string storageLocation,
                                    IEnumerable<string> compatibleRooms)
            : base(id, name, storageLocation)
        {
            if (compatibleRooms == null)
                throw new ArgumentNullException(nameof(compatibleRooms));
            _compatibleRooms = new HashSet<string>(compatibleRooms);
        }

        public bool IsCompatibleWith(string roomId) => _compatibleRooms.Contains(roomId);

        public override bool IsReadyForUse() => Status == DeviceStatus.Available;

        /// <summary>Điểm cho một phòng cụ thể: 100 nếu tương thích, ngược lại 0.</summary>
        public double GetPriorityScoreFor(string roomId) => IsCompatibleWith(roomId) ? 100 : 0;

        /// <summary>Không gắn với phòng nào nên mặc định 0; dùng GetPriorityScoreFor khi biết phòng.</summary>
        public override double GetDefaultPriorityScore() => 0;
    }
}
