using System;

namespace kasthack.Performance.Math {
    /*
        Xorshift RNG for .net
        Algorithm: Marsaglia, George. (2003). Xorshift RNGs
        First version: Colin Green, January 2005
    */
    public class Random {
        // The +1 ensures NextDouble doesn't generate 1.0
        private const double RealUnitInt = 1.0 / ( int.MaxValue + 1.0 );
        private const double RealUnitUint = 1.0 / ( uint.MaxValue + 1.0 );
        private const uint Y = 842502087;
        private const uint Z = 3579807591;
        private const uint W = 273326509;
        private uint _x;
        private uint _y;
        private uint _z;
        private uint _w;
        
        #region Constructor
        public Random() {
            Reinitialize( Environment.TickCount );
        }
        /// <summary> 
        /// Initialises a new instance using an int value as seed.
        /// This constructor signature is provided to maintain compatibility with
        /// System.Random
        /// </summary>
        public Random( int seed ) {
            Reinitialize( seed );
        }
        #endregion
        #region Public Methods [Reinitialisation]
        public void Reinitialize( int seed ) {
            _x = (uint)seed;
            _y = Y;
            _z = Z;
            _w = W;
        }
        public int Next() {
            while ( true ) {
                uint t = _x ^ _x << 11;
                _x = _y;
                _y = _z;
                _z = _w;
                _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 );
                // Handle the special case where the value int.MaxValue is generated. This is outside of 
                // the range of permitted values, so we therefore call Next() to try again.
                uint rtn = _w & 0x7FFFFFFF;
                if ( rtn != 0x7FFFFFFF ) return (int)rtn;
            }
        }

        /// <summary>
        /// Generates a random int over the range 0 to upperBound-1, and not including upperBound.
        /// </summary>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        public int Next( int upperBound ) {
            uint t = _x ^ _x << 11;
            _x = _y; _y = _z; _z = _w;
            // The explicit int cast before the first multiplication gives better performance.
            // See comments in NextDouble.
            return (int)( RealUnitInt * (int)( 0x7FFFFFFF & ( _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 ) ) ) * upperBound );
        }
        public int Next( int lowerBound, int upperBound ) {
            uint t = _x ^ _x << 11;
            _x = _y; _y = _z; _z = _w;
            // The explicit int cast before the first multiplication gives better performance.
            // See comments in NextDouble.
            int range = upperBound - lowerBound;
            if ( range < 0 ) {
                // If range is <0 then an overflow has occured and must resort to using long integer arithmetic instead (slower).
                // We also must use all 32 bits of precision, instead of the normal 31, which again is slower.	
                return lowerBound + (int)( RealUnitUint * ( _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 ) ) * ( (long)upperBound - lowerBound ) );
            }
            // 31 bits of precision will suffice if range<=int.MaxValue. This allows us to cast to an int and gain
            // a little more performance.
            return lowerBound + (int)( RealUnitInt * (int)( 0x7FFFFFFF & ( _w = ( _w ^ _w >> 19 ) ^ ( t ^ t >> 8 ) ) ) * range );
        }
        public double NextDouble() {
            uint t = _x ^ _x << 11;
            _x = _y; _y = _z; _z = _w;
            // Here we can gain a 2x speed improvement by generating a value that can be cast to 
            // an int instead of the more easily available uint. If we then explicitly cast to an 
            // int the compiler will then cast the int to a double to perform the multiplication, 
            // this final cast is a lot faster than casting from a uint to a double. The extra cast
            // to an int is very fast (the allocated bits remain the same) and so the overall effect 
            // of the extra cast is a significant performance improvement.
            //
            // Also note that the loss of one bit of precision is equivalent to what occurs within 
            // System.Random.
            return RealUnitInt * (int)( 0x7FFFFFFF & ( _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 ) ) );
        }
        public unsafe void NextBytes( byte[] buffer ) {
            // Fill up the bulk of the buffer in chunks of 4 bytes at a time.
            uint x = _x, y = _y, z = _z, w = _w;
            int i = 0;
            uint t;
            if ( buffer.Length > 3 ) {
                fixed (byte* bptr = buffer)
                {
                    uint* iptr = (uint*)bptr;
                    uint* endptr = iptr + buffer.Length / 4;
                    do {
                        t = ( x ^ ( x << 11 ) );
                        x = y; y = z; z = w;
                        w = w ^ w >> 19 ^ ( t ^ t >> 8 );
                        *iptr = w;
                    }
                    while ( ++iptr < endptr );
                    i = buffer.Length - buffer.Length % 4;
                }
            }
            // Fill up any remaining bytes in the buffer.
            if ( i < buffer.Length ) {
                // Generate 4 bytes.
                t = ( x ^ ( x << 11 ) );
                x = y; y = z; z = w;
                w = w ^ w >> 19 ^ ( t ^ t >> 8 );
                do {
                    buffer[ i ] = (byte)( w >>= 8 );
                } while ( ++i < buffer.Length );
            }
            _x = x; _y = y; _z = z; _w = w;
        }
        #endregion
        #region Public Methods [Methods not present on System.Random]
        public uint NextUInt() {
            uint t = _x ^ _x << 11;
            _x = _y; _y = _z; _z = _w;
            return _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 );
        }
        public int NextInt() {
            uint t = _x ^ _x << 11;
            _x = _y; _y = _z; _z = _w;
            return (int)( 0x7FFFFFFF & ( _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 ) ) );
        }
        // Buffer 32 bits in bitBuffer, return 1 at a time, keep track of how many have been returned
        // with bitBufferIdx.
        uint _bitBuffer;
        uint _bitMask = 1;
        /// <summary>
        /// Generates a single random bit.
        /// This method's performance is improved by generating 32 bits in one operation and storing them
        /// ready for future calls.
        /// </summary>
        /// <returns></returns>
        public bool NextBool() {
            if ( _bitMask != 1 ) return ( _bitBuffer & ( _bitMask >>= 1 ) ) == 0;
            // Generate 32 more bits.
            uint t = ( _x ^ ( _x << 11 ) );
            _x = _y; _y = _z; _z = _w;
            _bitBuffer = _w = ( _w ^ ( _w >> 19 ) ) ^ ( t ^ ( t >> 8 ) );
            // Reset the bitMask that tells us which bit to read next.
            _bitMask = 0x80000000;
            return ( _bitBuffer & _bitMask ) == 0;
        }

        uint _byteBuffer;
        uint _byteMove = 0;
        public byte NextByte() {
            if ( _byteMove != 0 ) {
                --_byteMove;
                return (byte)( _byteBuffer >>= 8 );
            }
            uint t = _x ^ _x << 11;
            _x = _y; _y = _z; _z = _w;
            _byteBuffer = _w = _w ^ _w >> 19 ^ ( t ^ t >> 8 );
            _byteMove = 3;
            return (byte)_byteBuffer;
        }

        #endregion
    }
}