using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Dominion
{
    /// <summary>
    /// Simple string->object value store with generic Get/Set methods.
    /// </summary>
    [Serializable]
    public class KeyValueStore : ISerializable
    {
        private Dictionary<string, object> data;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public KeyValueStore()
        {
            data = new Dictionary<string, object>();
        }

        /// <summary>
        /// Return if the collection contains a certain key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return data.ContainsKey(key);
        }

        /// <summary>
        /// Get Object of type T with key from the StateCache
        /// </summary>
        /// <typeparam name="T">type to get</typeparam>
        /// <param name="key">key</param>
        /// <returns>object</returns>
        public T Get<T>(string key)
        {
            return Get<T>(key, default(T));
        }

        /// <summary>
        /// Get with default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public T Get<T>(string key, T defaultVal)
        {
            object retval;
            if (data.TryGetValue(key, out retval))
                return (T)retval;
            return defaultVal;
        }

        /// <summary>
        /// Attempt to retrieve a value for a key, returning false if the
        /// value was not available.
        /// </summary>
        public bool TryGetValue<T>(string key, out T retval)
        {
            object untypedObject;
            if (data.TryGetValue(key, out untypedObject))
            {
                retval = (T)untypedObject;
                return true;
            }
            else
            {
                retval = default(T);
                return false;
            }
        }

        /// <summary>
        /// Set object of Type T with key into StateCache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public virtual void Set<T>(string key, T val)
        {
            if (!data.ContainsKey(key))
                data.Add(key, val);
            else
                data[key] = val;
        }

        /// <summary>
        /// string keys array accessor
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return Get<object>(key); }
            set { Set(key, value);}
        }

        /// <summary>
        /// Remove an entry
        /// </summary>
        /// <param name="key">The entry to remove</param>
        public void Remove(string key)
        {
            data.Remove(key);
        }

        /// <summary>
        /// Clear all entries
        /// </summary>
        public void Clear()
        {
            data.Clear();
        }
        
        /// <summary>
        /// Copy items from the specified source KeyValueStore
        /// </summary>
        /// <param name="source">The source KeyValueStore</param>
        /// <param name="overwrite">Whether to overwrite items when the source and destination have items with identical keys</param>
        public void CopyFrom(KeyValueStore source, bool overwrite)
        {
            if (source == null)
                return;
            foreach (KeyValuePair<string, object> item in source.data)
                if (overwrite || !this.ContainsKey(item.Key))
                    this[item.Key] = item.Value;
        }

        #region Serialization

        /// <summary>
        /// Deserialization Constructor
        /// </summary>
        protected KeyValueStore(SerializationInfo info, StreamingContext context)
        {
            this.data = (Dictionary<string, object>)info.GetValue("Cache", typeof(Dictionary<string, object>));
            this.data.OnDeserialization(this);
        }

        /// <summary>
        /// Virtual implementation of ISerializable.GetObjectData
        /// </summary>
        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Cache", this.data);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
        }

        #endregion
    }
}
