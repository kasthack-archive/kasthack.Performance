using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace kasthack.Performance.Strings {
    /*
        Fixed-length strings
        Pros:
            lower destLength compared dest System.String
            faster comparison
        Cons:
            ascii-only
            fixed length
            big-endian systems are not supported(comparison code relies on byte order)
        TODO:
            GetHashCode()
            Conversions 8<->16<->24<->32
    */
    public unsafe struct String8 : IComparable, IComparable<String8>, IEquatable<String8> {
        internal ulong _1;
        #region Comparators : IComparable, IComparable<String8>, IEquatable<String8>, ==, !=, >, <,>=,<=
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals( object obj ) => !ReferenceEquals( null, obj ) && ( obj is String8 && Equals( (String8)obj ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String8 a, String8 b ) => a._1 > b._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String8 a, String8 b ) => a._1 < b._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String8 a, String8 b ) => a._1 <= b._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String8 a, String8 b ) => a._1 >= b._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String8 a, String8 b ) => a._1 == b._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String8 a, String8 b ) => a._1 != b._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String8 other ) => _1 == other._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare( String8 a, String8 b ) => a._1.CompareTo( b._1 );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( object obj ) => _1.CompareTo( ( (String8)obj )._1 );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String8 other ) => _1.CompareTo( other._1 );
        #endregion
        #region String conversions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String8( string s ) => FromString( s );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String8 FromString( string s ) {
            fixed (char* rc = s)
            {
                String8 ret;
                StructStringHelper.FillAsciiBytes( rc, s.Length, (byte*)&ret, sizeof(String8) );
                return ret;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() { fixed (String8* r = &this) return StructStringHelper.ToString( (byte*)r, sizeof(String8) ); }
        #endregion
    }
    /// <summary>
    /// Compact ascii string storage(16 chars max)
    /// </summary>
    public unsafe struct String16 : IComparable, IComparable<String16>, IEquatable<String16>, IComparable<String8>, IEquatable<String8> {
        internal ulong _2;
        internal ulong _1;
        #region Comparators : IComparable, IComparable<String16>, IEquatable<String16>, ==, !=, >, <,>=,<=
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals( object obj ) => !ReferenceEquals( null, obj ) && ( obj is String16 && Equals( (String16)obj ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String16 a, String16 b ) => a._1 > b._1 || a._1 == b._1 && a._2 > b._2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String16 a, String16 b ) => a._1 < b._1 || a._1 == b._1 && a._2 < b._2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String16 a, String16 b ) => a._1 > b._1 || a._1 == b._1 && a._2 >= b._2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String16 a, String16 b ) => a._1 < b._1 || a._1 == b._1 && a._2 <= b._2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String16 a, String16 b ) => a._1 == b._1 && a._2 == b._2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String16 a, String16 b ) => a._1 != b._1 || a._2 != b._2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String16 a, String8 b ) => a._1 > b._1 || a._2 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String16 a, String8 b ) => a._1 < b._1 || a._2 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String16 a, String8 b ) => a._1 > b._1 || a._1 == b._1 && a._2 !=0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String16 a, String8 b ) => a._1 < b._1 || a._1 == b._1 && a._2 == 0UL;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String16 a, String8 b ) => a._1 == b._1 && a._2 == 0UL;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String16 a, String8 b ) => a._1 != b._1 || a._2 != 0UL;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String16 other ) => _2 == other._2 && _1 == other._1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String8 other ) => _1 == other._1 && _2 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( object obj ) {
            var b = (String16)obj;
            var cmp = _1.CompareTo( b._1 );
            return cmp != 0 ? cmp : _2.CompareTo( b._2 );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String16 b ) {
            var cmp = _1.CompareTo( b._1 );
            return cmp != 0 ? cmp : _2.CompareTo( b._2 );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String8 other ) {
            var cmp = _1.CompareTo( other._1 );
            return cmp != 0 ? cmp : _2.CompareTo( 0UL );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare( String16 a, String16 b ) {
            var cmp = a._1.CompareTo( b._1 );
            return cmp != 0 ? cmp : a._2.CompareTo( b._2 );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare( String16 a, String8 b ) {
            var cmp = a._1.CompareTo( b._1 );
            return cmp != 0 ? cmp : a._2.CompareTo( 0UL );
        }
        #endregion
        #region String conversions
        public static String16 FromString( string s ) {
            fixed (char* rc = s)
            {
                String16 ret;
                StructStringHelper.FillAsciiBytes( rc, s.Length, (byte*)&ret, sizeof(String16) );
                return ret;
            }
        }
        public override string ToString() { fixed (String16* r = &this) return StructStringHelper.ToString( (byte*)r, sizeof(String16) ); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String16( string s ) => FromString( s );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String8( String16 s ) => new String8 { _1 = s._1 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String16( String8 s ) => new String16 { _1 = s._1 };
        #endregion
    }
    /// <summary>
    /// Compact ascii string storage(24 chars max)
    /// </summary>
    public unsafe struct String24 : IComparable, IComparable<String24>, IComparable<String16>, IEquatable<String32>, IEquatable<String24>, IEquatable<String16>, IEquatable<String8> {
        internal ulong _3;
        internal ulong _2;
        internal ulong _1;

        #region Comparators : IComparable, IComparable<String24>, IEquatable<String24>, ==, !=, >, <,>=,<=
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals( object obj ) => !ReferenceEquals( null, obj ) && ( obj is String24 && Equals( (String24)obj ) );
        #region String8
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String24 a, String8 b ) => a._1 > b._1 || a._1 == b._1 && a._2 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String24 a, String8 b ) => a._1 >= b._1 || a._1 == b._1 && a._2 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String24 a, String8 b ) => a._1 < b._1 || a._1 == b._1 && a._2 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String24 a, String8 b ) => a._1 <= b._1 || a._1 == b._1 && a._2 == 0UL;
        #endregion
        #region String16
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String24 a, String16 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && a._3 != 0UL );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String24 a, String16 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && a._3 == 0UL );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String24 a, String16 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && a._3 != 0UL );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String24 a, String16 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && a._3 == 0UL );
        #endregion
        #region String24
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String24 a, String24 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && a._3 > b._3 );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String24 a, String24 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && a._3 < b._3 );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String24 a, String24 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && a._3 >= b._3 );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String24 a, String24 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && a._3 <= b._3 );
        #endregion
        #region Equality
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String24 a, String24 b ) => a._1 == b._1 && a._2 == b._2 && a._3 == b._3;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String24 a, String24 b ) => a._1 != b._1 || a._2 != b._2 || a._3 != b._3;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String24 a, String16 b ) => a._1 != b._1 || a._2 != b._2 || a._3 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String24 a, String16 b ) => a._1 == b._1 && a._2 == b._2 && a._3 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String24 a, String8 b ) => a._1 != b._1 || a._2 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String24 a, String8 b ) => a._1 == b._1 && a._2 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String32 other ) => this._1 == other._1 && this._2 == other._2 && this._3 == other._3;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String24 other ) => this._1 == other._1 && this._2 == other._2 && this._3 == other._3;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String16 other ) => this._1 == other._1 && this._2 == other._2 && this._3 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String8 other ) => this._1 == other._1 && this._2 == 0UL && this._3 == 0UL;
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( object obj ) {
            var b = (String24)obj;
            var cmp = _1.CompareTo( b._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( b._2 );
            return cmp != 0 ? cmp : _3.CompareTo( b._3 );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String24 b ) {
            var cmp = _1.CompareTo( b._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( b._2 );
            return cmp != 0 ? cmp : _3.CompareTo( b._3 );
        }
        public int CompareTo( String16 other ) {
            var cmp = _1.CompareTo( other._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( other._2 );
            return cmp != 0 ? cmp : _3.CompareTo( 0UL );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare( String24 a, String24 b ) {
            var cmp = a._1.CompareTo( b._1 );
            if ( cmp != 0 ) return cmp;
            cmp = a._2.CompareTo( b._2 );
            return cmp != 0 ? cmp : a._3.CompareTo( b._3 );
        }
        #endregion
        #region String conversions
        public static String24 FromString( string s ) {
            fixed (char* rc = s)
            {
                String24 ret;
                StructStringHelper.FillAsciiBytes( rc, s.Length, (byte*)&ret, sizeof(String24) );
                return ret;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String24( string s ) => FromString( s );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String24( String8 s ) => new String24 { _1 = s._1 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String24( String16 s ) => new String24 { _1 = s._1, _2 = s._2 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String8( String24 s ) => new String8 { _1 = s._1 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String16( String24 s ) => new String16 { _1 = s._1, _2 = s._2 };
        public override string ToString() { fixed (String24* r = &this) return StructStringHelper.ToString( (byte*)r, sizeof(String24) ); }
        #endregion
    }
    /// <summary>
    /// Compact ascii string storage(32 chars max)
    /// </summary>
    public unsafe struct String32 : IComparable,
            IComparable<String32>, IComparable<String24>, IComparable<String16>, IComparable<String8>,
            IEquatable<String32>,  IEquatable<String24>,  IEquatable<String16>,  IEquatable<String8> {
        internal ulong _4;
        internal ulong _3;
        internal ulong _2;
        internal ulong _1;

        #region Comparators : >, <,>=,<=
        #region String16
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String32 a, String16 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && a._3 != 0UL );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String32 a, String16 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && a._3 == 0UL );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String32 a, String16 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && a._3 != 0UL );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String32 a, String16 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && a._3 == 0UL );
        #endregion
        #region String24
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String32 a, String24 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && ( a._3 > b._3 || a._3 == b._3 && a._4 != 0UL ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String32 a, String24 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && ( a._3 < b._3  && a._4 == 0UL ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String32 a, String24 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && ( a._3 > b._3 || a._3 == b._3 && a._4 != 0UL ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String32 a, String24 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && ( a._3 < b._3 || a._3 == b._3 && a._4 == 0UL ) );
        #endregion
        #region String8
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String32 a, String8 b ) => a._1 > b._1 || a._1 == b._1 && a._2 !=0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String32 a, String8 b ) => a._1 >= b._1 || a._1 == b._1 && a._2 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String32 a, String8 b ) => a._1 < b._1 || a._1 == b._1 && a._2 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String32 a, String8 b ) => a._1 <= b._1 || a._1 == b._1 && a._2 == 0UL;
        #endregion
        #region String32
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >( String32 a, String32 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && ( a._3 > b._3 || a._3 == b._3 && a._4 > b._4 ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <( String32 a, String32 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && ( a._3 < b._3 || a._3 == b._3 && a._4 < b._4 ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=( String32 a, String32 b ) => a._1 > b._1 || a._1 == b._1 && ( a._2 > b._2 || a._2 == b._2 && ( a._3 > b._3 || a._3 == b._3 && a._4 >= b._4 ) );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=( String32 a, String32 b ) => a._1 < b._1 || a._1 == b._1 && ( a._2 < b._2 || a._2 == b._2 && ( a._3 < b._3 || a._3 == b._3 && a._4 <= b._4 ) );
        #endregion
        #endregion
        #region Equality
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals( object obj ) => !ReferenceEquals( null, obj ) && ( obj is String32 && Equals( (String32)obj ) );
        public static bool operator ==( String32 a, String32 b ) => a._1 == b._1 && a._2 == b._2 && a._3 == b._3 && a._4 == b._4;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String32 a, String32 b ) => a._1 != b._1 || a._2 != b._2 || a._3 != b._3 || a._4 != b._4;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String32 a, String24 b ) => a._1 != b._1 || a._2 != b._2 || a._3 != b._3 || a._4 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String32 a, String24 b ) => a._1 == b._1 && a._2 == b._2 && a._3 == b._3 && a._4 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String32 a, String16 b ) => a._1 != b._1 || a._2 != b._2 || a._3 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String32 a, String16 b ) => a._1 == b._1 && a._2 == b._2 && a._3 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=( String32 a, String8 b ) => a._1 != b._1 || a._2 != 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==( String32 a, String8 b ) => a._1 == b._1 && a._2 == 0UL;
        [ MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String32 other ) => this._1 == other._1 && this._2 == other._2 && this._3 == other._3 && this._4 == other._4;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String24 other ) => this._1 == other._1 && this._2 == other._2 && this._3 == other._3 && this._4 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String16 other ) => this._1 == other._1 && this._2 == other._2 && this._3 == 0UL && this._4 == 0UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals( String8 other ) => this._1 == other._1 && this._2 == 0UL && this._3 == 0UL && this._4 == 0UL;
        #endregion
        #region IComparable
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( object obj ) {
            var b = (String32)obj;
            var cmp = _1.CompareTo( b._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( b._2 );
            if ( cmp != 0 ) return cmp;
            cmp = _3.CompareTo( b._3 );
            return cmp != 0 ? cmp : _4.CompareTo( b._4 );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String32 other ) {
            var cmp = _1.CompareTo( other._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( other._2 );
            if ( cmp != 0 ) return cmp;
            cmp = _3.CompareTo( other._3 );
            return cmp != 0 ? cmp : _4.CompareTo( other._4 );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String24 other ) {
            var cmp = _1.CompareTo( other._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( other._2 );
            if ( cmp != 0 ) return cmp;
            cmp = _3.CompareTo( other._3 );
            return cmp != 0 ? cmp : _4.CompareTo( 0UL );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String16 other ) {
            var cmp = _1.CompareTo( other._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( other._2 );
            if ( cmp != 0 ) return cmp;
            cmp = _3.CompareTo( 0UL );
            return cmp != 0 ? cmp : _4.CompareTo( 0UL );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo( String8 other ) {
            var cmp = _1.CompareTo( other._1 );
            if ( cmp != 0 ) return cmp;
            cmp = _2.CompareTo( 0UL );
            if ( cmp != 0 ) return cmp;
            cmp = _3.CompareTo( 0UL );
            return cmp != 0 ? cmp : _4.CompareTo( 0UL );
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare( String32 a, String32 b ) {
            var cmp = a._1.CompareTo( b._1 );
            if ( cmp != 0 ) return cmp;
            cmp = a._2.CompareTo( b._2 );
            if ( cmp != 0 ) return cmp;
            cmp = a._3.CompareTo( b._3 );
            return cmp != 0 ? cmp : a._4.CompareTo( b._4 );
        }
        #endregion
        #region String conversions
        public static String32 FromString( string s ) {
            fixed (char* rc = s)
            {
                String32 ret;
                StructStringHelper.FillAsciiBytes( rc, s.Length, (byte*)&ret, sizeof(String32) );
                return ret;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String32( string s ) => FromString( s );
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String32( String8 s ) => new String32 { _1 = s._1 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String32( String16 s ) => new String32 { _1 = s._1, _2 = s._2};
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String32( String24 s ) => new String32 { _1 = s._1, _2 = s._2, _3 = s._3 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String8( String32 s ) => new String8 { _1 = s._1 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String16( String32 s ) => new String16 { _1 = s._1, _2 = s._2 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator String24( String32 s ) => new String24 { _1 = s._1, _2 = s._2, _3 = s._3 };
        public override string ToString() { fixed (String32* r = &this) return StructStringHelper.ToString( (byte*)r, sizeof(String32) ); }
        #endregion
    }
    internal static class StructStringHelper {
        
        internal static unsafe void FillAsciiBytes( char* source, int sourceLength, byte* dest, int destLength ) {
            var sourceStart = ( (byte*)source );                     //source source


            /*
                !!! writer goes from destEnd to destStart because strings are stored in _REVERSE_ order
            */
            var destEnd = dest + destLength - 1;                                //struct end
            var destStart = destEnd - System.Math.Min( destLength, sourceLength ); //write end
            /*
                copy first byte(ascii space) of every char to destination
            */
            do {
                *destEnd = *sourceStart;
                --destEnd;
                sourceStart += sizeof(char);
            } while ( destEnd > destStart );
            /* fill the rest part with zeros */
            const byte zero = 0;
            while ( destEnd > dest ) *destEnd-- = zero;
        }
        internal static unsafe string ToString( byte* source, int count ) {
            var output = stackalloc char[count];
            source += count;
            int i=0;
            byte b;
            while ( i < count ) {
                b = *--source;
                output[ i ] = (char) b;
                if ( b == 0 ) break;
                ++i;
            }
            return new string( output, 0, i );
        }
    }
}
