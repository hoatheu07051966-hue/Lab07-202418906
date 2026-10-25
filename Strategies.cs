/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;
using System.Collections.Generic;

namespace Lab07
{
    /// <summary>Chọn phần tử có điểm cao nhất; hòa thì lấy phần tử đứng trước.</summary>
    public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
    {
        private readonly Func<T, double> _score;

        public HighestPriorityStrategy(Func<T, double> score)
        {
            _score = score ?? throw new ArgumentNullException(nameof(score));
        }

        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException();

            T best = candidates[0];
            double bestScore = _score(best);
            for (int i = 1; i < candidates.Count; i++)
            {
                double s = _score(candidates[i]);
                if (s > bestScore)
                {
                    best = candidates[i];
                    bestScore = s;
                }
            }
            return best;
        }
    }

    /// <summary>Chọn phần tử đầu tiên của danh sách ứng viên.</summary>
    public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
    {
        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException();
            return candidates[0];
        }
    }
}
