/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

namespace Lab07
{
    /// <summary>Điều khiển máy chiếu: sẵn sàng khi Available và pin ≥ 20%.</summary>
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; }

        public ProjectorRemote(string id, string name, string storageLocation, int batteryLevel)
            : base(id, name, storageLocation)
        {
            BatteryLevel = RequirePercent(batteryLevel, nameof(batteryLevel));
        }

        public override bool IsReadyForUse() =>
            Status == DeviceStatus.Available && BatteryLevel >= 20;

        public override double GetDefaultPriorityScore() => BatteryLevel;
    }
}
