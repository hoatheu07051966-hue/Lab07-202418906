/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

namespace Lab07
{
    /// <summary>Micro không dây: sẵn sàng khi Available, pin ≥ 30% và tín hiệu ≥ 40%.</summary>
    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; }
        public int SignalQuality { get; }

        public WirelessMicrophone(string id, string name, string storageLocation,
                                  int batteryLevel, int signalQuality)
            : base(id, name, storageLocation)
        {
            BatteryLevel = RequirePercent(batteryLevel, nameof(batteryLevel));
            SignalQuality = RequirePercent(signalQuality, nameof(signalQuality));
        }

        public override bool IsReadyForUse() =>
            Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40;

        public override double GetDefaultPriorityScore() => (BatteryLevel + SignalQuality) / 2.0;
    }
}
