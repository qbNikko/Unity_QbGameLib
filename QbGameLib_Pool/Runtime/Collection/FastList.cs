using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QbGameLib.Pool.Collection
{
    
    public class FastList<T> : IList<T>, ICollection<T>, IFastReadOnlyList<T>
    {
        private T[] _data;
        private int _len;
        
        public int Count => _len;
        public bool IsReadOnly => false;
        
        public FastList () {
            _data = new T[16];
            _len = 0;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public FastList (int cap = 16, bool filled = false) {
            _data = new T[cap];
            _len = filled ? cap : 0;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ref T Get (int idx) {
            return ref _data[idx];
        }
        
        public T this[int index]
        {
            get => _data[index];
            set => Insert(index, value);
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            if (_data.Length == _len) {
                Array.Resize (ref _data, _len << 1);
            }
            _data[_len++] = item;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Add(ref T item)
        {
            if (_data.Length == _len) {
                Array.Resize (ref _data, _len << 1);
            }
            _data[_len++] = item;
        }
        
        public void Insert(int index, T item)
        {
            if(index < 0 || index >= _len) return;
            _data[index] = item;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            return IndexOf(item)!=-1;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item)
        {
            if(item == null) return -1;
            for (var i = 0; i < _len; i++) {
                if(item.Equals(_data[i])) return i;
            }
            return -1;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Remove(T item)
        {
            int indexOf = IndexOf(item);
            if (indexOf < 0) return false;
            RemoveAt(indexOf);
            return true;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Clear(true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Clear(bool setDefaults = true)
        {
            if (setDefaults) {
                for (var i = 0; i < _len; i++) {
                    _data[i] = default;
                }
            }
            _len = 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int idx)
        {
            if (idx >= 0 && idx < _len) {
                _data[idx] = default;
                _len--;
                if (idx < _len) {
                    (_data[idx], _data[_len]) = (_data[_len], _data[idx]);
                }
            }
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool RemoveAtAndCheckMove(int idx)
        {
            if (idx >= 0 && idx < _len) {
                _data[idx] = default;
                _len--;
                if (idx < _len) {
                    (_data[idx], _data[_len]) = (_data[_len], _data[idx]);
                    return true;
                }
            }
            return false;
        }

        private int i;
        public IEnumerator<T> GetEnumerator()
        {
            i = 0;
            while (i < _len)
            {
                yield return _data[i++];  
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void CopyTo(T[] array, int arrayIndex)
        {
            for (i = arrayIndex; i < _len; i++)
            {
                array[i] = _data[i];
            }
        }
        
    }
}