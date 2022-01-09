using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace HelperLibrary.Types
{
    public readonly struct Fraction : IComparable, IConvertible, IComparable<Fraction>, IEquatable<Fraction>
    {
        private readonly uint _numerator;
        private readonly uint _denominator;

        //int or long
        // numerator
        // denominator
        // signed

        public Fraction(uint numerator, uint denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
            }
            _numerator = numerator;
            _denominator = denominator;
        }

        public int CompareTo(object? value)
        {
            if (value == null)
            {
                return 1;
            }

            if (value is Fraction i)
            {
                if (this < i) return -1;
                if (this > i) return 1;
                return 0;

            }

            throw new ArgumentException("Object must be of type Fraction.");
        }

        public int CompareTo(Fraction value)
        {
            if (this < value) return -1;
            if (this > value) return 1;
            return 0;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (!(obj is Fraction))
            {
                return false;
            }
            return this == ((Fraction)obj);
        }

        public bool Equals(Fraction other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_numerator, _denominator);
        }

        public override string ToString()
        {
            return $"{_numerator}/{_denominator}";
        }

        public string ToString(IFormatProvider? provider)
        {
            return ToString();
        }

        public TypeCode GetTypeCode()
        {
            return Type.GetTypeCode(GetType());
        }

        bool IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return Convert.ToBoolean((float)_numerator / _denominator);
        }

        char IConvertible.ToChar(IFormatProvider? provider)
        {
            return Convert.ToChar((float)_numerator / _denominator);
        }

        sbyte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return Convert.ToSByte(_numerator / _denominator);
        }

        byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return Convert.ToByte(_numerator / _denominator);
        }

        short IConvertible.ToInt16(IFormatProvider? provider)
        {
            return Convert.ToInt16(_numerator / _denominator);
        }

        ushort IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return Convert.ToUInt16(_numerator / _denominator);
        }

        int IConvertible.ToInt32(IFormatProvider? provider)
        {
            return Convert.ToInt32(_numerator / _denominator);
        }

        uint IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return Convert.ToUInt32(_numerator / _denominator);
        }

        long IConvertible.ToInt64(IFormatProvider? provider)
        {
            return Convert.ToInt64(_numerator / _denominator);
        }

        ulong IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return Convert.ToUInt64(_numerator / _denominator);
        }

        float IConvertible.ToSingle(IFormatProvider? provider)
        {
            return (float)_numerator / _denominator;
        }

        double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return (double)_numerator / _denominator;
        }

        decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return (decimal)_numerator / _denominator;
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            throw new InvalidCastException("Cannot cast from Fraction to DateTime");
        }

        object IConvertible.ToType(Type type, IFormatProvider? provider)
        {
            throw new NotImplementedException("How do I implement this?");
        }

        public static explicit operator float(Fraction value) => Convert.ToSingle(value);
        public static explicit operator double(Fraction value) => Convert.ToDouble(value);
        public static explicit operator decimal(Fraction value) => Convert.ToDecimal(value);
        public static explicit operator sbyte(Fraction value) => Convert.ToSByte(value);
        public static explicit operator short(Fraction value) => Convert.ToInt16(value);
        public static explicit operator int(Fraction value) => Convert.ToInt32(value);
        public static explicit operator byte(Fraction value) => Convert.ToByte(value);
        public static explicit operator ushort(Fraction value) => Convert.ToUInt16(value);
        public static explicit operator uint(Fraction value) => Convert.ToUInt32(value);
        public static explicit operator ulong(Fraction value) => Convert.ToUInt64(value);

        public static Fraction operator +(Fraction value) => value;

        public static Fraction operator +(Fraction left, Fraction right)
        {
            if (left._denominator == right._denominator)
            {
                return new Fraction(left._numerator + right._numerator, left._denominator);
            }
            return new Fraction(left._numerator * right._denominator + right._numerator * left._denominator, left._denominator * right._denominator);
        }

        public static Fraction operator -(Fraction left, Fraction right)
        {
            return new Fraction(left._numerator * right._denominator - right._numerator * left._denominator, left._denominator * right._denominator);
        }

        public static Fraction operator *(Fraction left, Fraction right)
        {
            return new Fraction(left._numerator * right._numerator, left._denominator * right._denominator);
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            if (right._numerator == 0)
            {
                throw new DivideByZeroException();
            }

            return new Fraction(left._numerator * right._denominator, right._numerator * right._denominator);
        }

        public static bool operator ==(Fraction left, Fraction right)
        {
            return left._numerator * right._denominator == right._numerator * left._denominator;
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return left._numerator * right._denominator != right._numerator * left._denominator;
        }

        public static bool operator >(Fraction left, Fraction right)
        {
            return left._numerator * right._denominator > right._numerator * left._denominator;
        }

        public static bool operator <(Fraction left, Fraction right)
        {
            return left._numerator * right._denominator < right._numerator * left._denominator;
        }

        public static bool operator >=(Fraction left, Fraction right)
        {
            return left._numerator * right._denominator >= right._numerator * left._denominator;
        }

        public static bool operator <=(Fraction left, Fraction right)
        {
            return left._numerator * right._denominator <= right._numerator * left._denominator;
        }

    }
}
