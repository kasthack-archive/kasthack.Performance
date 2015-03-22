using System.Runtime.CompilerServices;
/*
    Bit twiddling hacks for dotnet
    theory: http://graphics.stanford.edu/~seander/bithacks.html
*/
namespace kasthack.Performance.Math {
    public static class BitTwiddling {
        private const int BitsInByte = 8;
        private const int ByteSize = sizeof(sbyte) * BitsInByte - 1;    //7
        private const int ShortSize = sizeof(short) * BitsInByte - 1;   //15
        private const int IntSize = sizeof(int) * BitsInByte - 1;       //31
        private const int LongSize = sizeof(long) * BitsInByte - 1;     //63
        /*
            DBS(X) = sizeof(X) * 8 - 1
        */
        #region Constants for fighting with 'optimizing' compiler
        private const int iz = 0, io = 1;
        private const uint uiz = 0u, uio = 1u;
        private const long lz = 0L, lo = 1L;
        private const ulong ulz = 0UL, ulo = 1UL;
        private const short sz = 0, so = 1;
        private const ushort usz = 0, uso = 1;
        private const byte bz = 0, bo = 1;
        private const sbyte sbz = 0, sbo = 1;
        #endregion

        #region Abs: ( ( x >> DBS(x_type) ) ^ x ) - ( x >> DBS(x_type) )
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs( int value ) {
            var a = value >> IntSize;
            return ( value ^ a ) - a;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Abs( long value ) {
            var a = value >> LongSize;
            return ( value ^ a ) - a;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Abs( sbyte value ) {
            var a = ( value >> ByteSize );
            return (sbyte)( ( value ^ a ) - a );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Abs( short value ) {
            //var a = (short)( value >> ShortSize );
            //return (short)( (short)( value ^ a ) - a );
            var a = ( value >> ShortSize );
            return (short)( ( value ^ a ) - a );
        }
        #endregion

        #region QMax: a - ( ( a - b ) & ( ( a - b ) >> DBS(x_type) ) )
        //faster than Math.Max but works only with T_MIN <= ( x - y ) <= T_MAX
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int QMax( int a, int b ) => a - ( ( a - b ) & ( ( a - b ) >> IntSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint QMax( uint a, uint b ) => a - ( ( a - b ) & ( ( a - b ) >> IntSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long QMax( long a, long b ) => a - ( ( a - b ) & ( ( a - b ) >> LongSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong QMax( ulong a, ulong b ) => a - ( ( a - b ) & ( ( a - b ) >> LongSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte QMax( sbyte a, sbyte b ) => (sbyte)( a - ( ( a - b ) & ( ( a - b ) >> ByteSize ) ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte QMax( byte a, byte b ) => (byte)( a - ( ( a - b ) & ( ( a - b ) >> ByteSize ) ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short QMax( short a, short b ) => (short)( a - ( ( a - b ) & ( ( a - b ) >> ShortSize ) ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort QMax( ushort a, ushort b ) => (ushort)( a - ( ( a - b ) & ( ( a - b ) >> ShortSize ) ) );
        #endregion

        #region QMin: b + ( ( a - b ) & ( ( a - b ) >> DBS(x_type) ) )
        //faster than Math.Min but works only with T_MIN <= ( x - y ) <= T_MAX
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int QMin( int a, int b ) => b + ( ( a - b ) & ( ( a - b ) >> IntSize ) ); //extracting (a-b) to variable only drops performance

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint QMin( uint a, uint b ) => b + ( ( a - b ) & ( ( a - b ) >> IntSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long QMin( long a, long b ) => b + ( ( a - b ) & ( ( a - b ) >> LongSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong QMin( ulong a, ulong b ) => b + ( ( a - b ) & ( ( a - b ) >> LongSize ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte QMin( byte a, byte b ) => (byte)( b + ( ( a - b ) & ( ( a - b ) >> ByteSize ) ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte QMin( sbyte a, sbyte b ) => (sbyte)( b + ( ( a - b ) & ( ( a - b ) >> ByteSize ) ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short QMin( short a, short b ) => (short)( b + ( ( a - b ) & ( ( a - b ) >> ShortSize ) ) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort QMin( ushort a, ushort b ) => (ushort)( b + ( ( a - b ) & ( ( a - b ) >> ShortSize ) ) );
        #endregion
        
        #region Sign: ( value != 0) | ( value >> DBS(x_type) )
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Sign( int value ) {
            var input = value != 0;
            return *( (byte*)&input ) | ( value >> IntSize );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Sign( long value ) {
            var input = value != 0;
            return (int)( *( (byte*)&input ) | ( value >> LongSize ) );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Sign( sbyte value ) {
            var input = value != 0;
            return *( (byte*)&input ) | ( value >> ByteSize );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Sign( short value ) {
            var input = value != 0;
            return *( (byte*)&input ) | ( value >> ShortSize );
        }
        #endregion
        
        #region OppositeSigns: ( a ^ b ) < 0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OppositeSigns( int a, int b ) => ( a ^ b ) < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OppositeSigns( long a, long b ) => ( a ^ b ) < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OppositeSigns( short a, short b ) => ( a ^ b ) < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OppositeSigns( sbyte a, sbyte b ) => ( a ^ b ) < 0;
        #endregion
        
        #region IsPowerOfTwo: value && !( value & ( value - 1) )
        //unsigned != 0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( byte value ) => ( value & ( value - bo ) ) == iz && value != bz;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( ushort value ) => ( value & ( value - uso ) ) == iz && value != usz;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( uint value ) => ( value & ( value - uio ) ) == uiz && value != uiz;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( ulong value ) => ( value & ( value - ulo ) ) == uiz && value != uiz;

        //signed > 0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( sbyte value ) => ( value & ( value - sbo ) ) == iz && value > sbz;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( int value ) => ( value & ( value - io ) ) == iz && value > iz;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( long value ) => ( value & ( value - lo ) ) == lz && value > lz;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo( short value ) => ( value & ( value - so ) ) == iz && value > sz;
        #endregion

        #region Exponentiation by squaring
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Pow( long x, long y ) {
            var result = 1L;
            while ( y != 0 ) {
                if ( ( y & 1L ) == 1L ) result *= x;
                y >>= 1;
                x *= x;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Pow( ulong x, ulong y ) {
            var result = 1UL;
            while ( y != 0 ) {
                if ( ( y & 1UL ) == 1UL ) result *= x;
                y >>= 1;
                x *= x;
            }
            return result;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Pow( int x, int y ) {
            var result = 1;
            while ( y != 0 ) {
                if ( ( y & 1 ) == 1 ) result *= x;
                y >>= 1;
                x *= x;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Pow( uint x, uint y ) {
            var result = 1U;
            while ( y != 0 ) {
                if ( ( y & 1U ) == 1U ) result *= x;
                y >>= 1;
                x *= x;
            }
            return result;
        }
        #endregion
    }
}