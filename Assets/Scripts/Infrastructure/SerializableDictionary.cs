using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    [Serializable]
    public class SerializableDictionary<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        [Serializable]
        public class Pair
        {
            [SerializeField] private K key;
            [SerializeField] private V value;

            public K Key { get => key; set => key = value; }

            public V Value { get => value; set => this.value = value; }

            public Pair(K key, V value)
            {
                Key = key;
                Value = value;
            }
        }

        [SerializeField]
        private List<Pair> items = new List<Pair>();

        public void Add(K key, V value)
        {
            if (ContainsKey(key))
                throw new ArgumentException($"Key {key} already exists.");
            items.Add(new Pair(key, value));
        }

        public bool ContainsKey(K key)
        {
            return items.Exists(p => EqualityComparer<K>.Default.Equals(p.Key, key));
        }

        public bool TryGetValue(K key, out V value)
        {
            foreach (var pair in items)
            {
                if (EqualityComparer<K>.Default.Equals(pair.Key, key))
                {
                    value = pair.Value;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public void Remove(K key)
        {
            items.RemoveAll(p => EqualityComparer<K>.Default.Equals(p.Key, key));
        }

        public void Clear()
        {
            items.Clear();
        }

        public List<K> Keys
        {
            get
            {
                var keys = new List<K>();
                foreach (var pair in items)
                    keys.Add(pair.Key);
                return keys;
            }
        }

        public List<V> Values
        {
            get
            {
                var values = new List<V>();
                foreach (var pair in items)
                    values.Add(pair.Value);
                return values;
            }
        }

        public int Count => items.Count;

        public V this[K key]
        {
            get
            {
                foreach (var pair in items)
                    if (EqualityComparer<K>.Default.Equals(pair.Key, key))
                        return pair.Value;
                throw new KeyNotFoundException($"Key {key} not found.");
            }
            set
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (EqualityComparer<K>.Default.Equals(items[i].Key, key))
                    {
                        items[i].Value = value;
                        return;
                    }
                }

                items.Add(new Pair(key, value));
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            foreach (var pair in items)
                yield return new KeyValuePair<K, V>(pair.Key, pair.Value);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}