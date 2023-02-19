using System;
using UnityEngine;

namespace Mud
{
    [Serializable]
    public struct Identifier: ISerializationCallbackReceiver, IFormattable, IEquatable<Identifier>, IComparable, IComparable<Identifier>
    {
        [SerializeField] private string value;
        private Guid guid;

        public static Identifier Empty() =>
            new Identifier
            {
                guid = Guid.Empty,
            };

        public static Identifier Generate() =>
            new Identifier
            {
                guid = Guid.NewGuid()
            };

        public static Identifier Parse(string input) =>
            new Identifier
            {
                guid = Guid.Parse(input)
            };

        public static Identifier Parse(Guid input) =>
            new Identifier
            {
                guid = input
            };

        public static bool operator ==(Identifier first, Identifier second) => first.Equals(second);

        public static bool operator !=(Identifier first, Identifier second) => !first.Equals(second);
        
        public static bool operator ==(Identifier first, Guid second) => first.Equals(second);
        
        public static bool operator !=(Identifier first, Guid second) => !first.Equals(second);
        
        public static bool operator ==(Identifier first, string second) => first.Equals(second);
        
        public static bool operator !=(Identifier first, string second) => !first.Equals(second);

        public override int GetHashCode() => guid.GetHashCode();
        
        public override string ToString()=> guid.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => guid.ToString(format, formatProvider);

        public override bool Equals(object obj) => guid.Equals(obj);

        public bool Equals(Identifier other) => guid.Equals(other.guid);

        public bool Equals(Guid other) => guid.Equals(other);

        public bool Equals(string other) => guid.Equals(Guid.Parse(other));

        public int CompareTo(Identifier other) => guid.CompareTo(other.guid);

        public int CompareTo(object obj) => guid.CompareTo(obj);

        void ISerializationCallbackReceiver.OnBeforeSerialize() => value = guid.ToString();

        void ISerializationCallbackReceiver.OnAfterDeserialize() => guid = Guid.Parse(value);
    }
}