/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab07
{
    class Program
    {
        static int _failed;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var projectors = new InMemoryRepository<ProjectorRemote>();
            var acs = new InMemoryRepository<AirConditionerRemote>();
            var mics = new InMemoryRepository<WirelessMicrophone>();

            projectors.Add(new ProjectorRemote("PJ01", "Máy chiếu 1", "Kho A", 85));
            projectors.Add(new ProjectorRemote("PJ02", "Máy chiếu 2", "Kho A", 15));
            projectors.Add(new ProjectorRemote("PJ03", "Máy chiếu 3", "Kho A", 60));
            acs.Add(new AirConditionerRemote("AC01", "Điều hòa 1", "Kho B", new[] { "A203", "A204" }));
            acs.Add(new AirConditionerRemote("AC02", "Điều hòa 2", "Kho B", new[] { "B101", "B102" }));
            mics.Add(new WirelessMicrophone("MC01", "Micro 1", "Kho C", 40, 45));
            mics.Add(new WirelessMicrophone("MC02", "Micro 2", "Kho C", 25, 80));
            mics.Add(new WirelessMicrophone("MC03", "Micro 3", "Kho C", 90, 80));

            // 1
            Check("1. GetAll().Count",
                $"{projectors.GetAll().Count}/{acs.GetAll().Count}/{mics.GetAll().Count}", "3/2/3");

            // 2
            try
            {
                projectors.Add(new ProjectorRemote("PJ01", "Trùng", "Kho A", 50));
                Check("2. Thêm trùng PJ01", "không ném ngoại lệ", "DuplicateIdException");
            }
            catch (DuplicateIdException ex)
            {
                Check("2. Thêm trùng PJ01", $"{ex.DuplicateId}, kho={projectors.GetAll().Count}", "PJ01, kho=3");
            }

            // 3
            Check("3. FindById/Remove",
                $"{projectors.FindById("PJ03")?.Id ?? "null"}; {projectors.FindById("PJ99")?.Id ?? "null"}; {projectors.Remove("PJ99")}",
                "PJ03; null; False");

            // 4
            Check("4. FindAll sẵn sàng",
                Ids(projectors.FindAll(d => d.IsReadyForUse())) + " | " + Ids(mics.FindAll(d => d.IsReadyForUse())),
                "PJ01,PJ03 | MC01,MC03");

            // 5
            var pjService = new AllocationService<ProjectorRemote>(
                projectors, new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore()));
            var pj = pjService.Allocate(d => true);
            Check("5. Highest priority máy chiếu", pj.Id, "PJ01");

            // 6
            var acService = new AllocationService<AirConditionerRemote>(
                acs, new HighestPriorityStrategy<AirConditionerRemote>(d => d.GetPriorityScoreFor("A203")));
            Check("6. Điều hòa phòng A203", acService.Allocate(d => d.IsCompatibleWith("A203")).Id, "AC01");

            // 7
            var micService = new AllocationService<WirelessMicrophone>(
                mics, new FirstAvailableStrategy<WirelessMicrophone>());
            var mc = micService.Allocate(d => true);
            Check("7. FirstAvailable micro", mc.Id, "MC01");

            // 8
            mc.Return();
            micService.ChangeStrategy(
                new HighestPriorityStrategy<WirelessMicrophone>(d => d.GetDefaultPriorityScore()));
            Check("8. Đổi chiến lược -> Highest", micService.Allocate(d => true).Id, "MC03");

            // 9
            try
            {
                acService.Allocate(d => d.IsCompatibleWith("Z999"));
                Check("9. Phòng Z999", "không ném ngoại lệ", "NoSuitableItemException");
            }
            catch (NoSuitableItemException)
            {
                Check("9. Phòng Z999",
                    $"NoSuitableItemException; AC02={acs.FindById("AC02")!.Status}",
                    "NoSuitableItemException; AC02=Available");
            }

            // 10
            try
            {
                pj.Borrow();
                Check("10. Borrow lần 2", "không ném ngoại lệ", "DeviceNotReadyException");
            }
            catch (DeviceNotReadyException ex)
            {
                Check("10. Borrow lần 2", $"{pj.Status}; {ex.DeviceId}; {ex.CurrentStatus}", "Borrowed; PJ01; Borrowed");
            }

            // 11: cùng một lớp generic cho cả ba loại; dòng dưới KHÔNG biên dịch được:
            // projectors.Add(new WirelessMicrophone("MC99", "x", "y", 50, 50));
            Check("11. Generic dùng chung",
                projectors.GetType().GetGenericTypeDefinition().Name,
                mics.GetType().GetGenericTypeDefinition().Name);

            Console.WriteLine();
            Console.WriteLine(_failed == 0 ? "TẤT CẢ CA KIỂM THỬ ĐẠT." : $"{_failed} ca KHÔNG đạt.");
        }

        static string Ids<T>(IReadOnlyList<T> list) where T : TeachingDevice =>
            string.Join(",", list.Select(d => d.Id));

        static void Check(string name, string actual, string expected)
        {
            bool ok = actual == expected;
            if (!ok) _failed++;
            Console.WriteLine($"[{(ok ? "ĐẠT" : "LỖI")}] {name}: {actual}" + (ok ? "" : $" (mong đợi: {expected})"));
        }
    }
}
