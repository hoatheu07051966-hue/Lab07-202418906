/****************
Mã sinh viên: 202418906
Họ tên: Lê Gia Huân
****************/

using System;
using System.Collections.Generic;

namespace Lab07
{
    /// <summary>
    /// Kho trong bộ nhớ. Dictionary cho tra cứu O(1); List giữ thứ tự thêm vào
    /// để kết quả duyệt (và FirstAvailableStrategy) ổn định.
    /// </summary>
    public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private readonly Dictionary<string, T> _byId = new Dictionary<string, T>();
        private readonly List<T> _order = new List<T>();

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (_byId.ContainsKey(item.Id))
                throw new DuplicateIdException(item.Id);

            _byId.Add(item.Id, item);
            _order.Add(item);
        }

        public bool Remove(string id)
        {
            if (id == null || !_byId.TryGetValue(id, out T? item))
                return false;

            _byId.Remove(id);
            _order.Remove(item);
            return true;
        }

        public T? FindById(string id)
        {
            if (id == null)
                return default;
            return _byId.TryGetValue(id, out T? item) ? item : default;
        }

        public IReadOnlyList<T> GetAll() => _order.ToArray();

        public IReadOnlyList<T> FindAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            return _order.FindAll(match);
        }
    }
}
