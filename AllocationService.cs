/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;

namespace Lab07
{
    /// <summary>
    /// Dịch vụ cấp phát. Chỉ phụ thuộc IRepository và ISelectionStrategy,
    /// không kiểm tra loại thiết bị cụ thể nên thêm loại mới không phải sửa lớp này.
    /// </summary>
    public class AllocationService<T> where T : TeachingDevice
    {
        private readonly IRepository<T> _repository;
        private ISelectionStrategy<T> _strategy;

        public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        /// <summary>Đổi chiến lược chọn lúc chạy.</summary>
        public void ChangeStrategy(ISelectionStrategy<T> strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        /// <summary>Lọc thiết bị sẵn sàng + điều kiện bổ sung, chọn theo chiến lược rồi Borrow().</summary>
        public T Allocate(Predicate<T> extraCondition)
        {
            if (extraCondition == null)
                throw new ArgumentNullException(nameof(extraCondition));

            var candidates = _repository.FindAll(d => d.IsReadyForUse() && extraCondition(d));
            T chosen = _strategy.Select(candidates);
            chosen.Borrow();
            return chosen;
        }
    }
}
