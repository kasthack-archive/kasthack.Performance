using Microsoft.VisualStudio.TestTools.UnitTesting;
using Def = System.Math;
using Fast = kasthack.Performance.Math.BitTwiddling;

namespace kasthack.Performance.Math.Tests {
    [TestClass]
    public class BitTwiddlingTests {
        [TestMethod]
        public void Abs() {
            // abs(int)
            const int ip1 = 1;
            const int in1 = -1;
            Cmpr( Fast.Abs( ip1 ), Def.Abs( ip1 ), "Abs(1)" );
            Cmpr( Fast.Abs( in1 ), Def.Abs( in1 ), "Abs(-1)" );
            Cmpr( Fast.Abs( int.MaxValue ), Def.Abs( int.MaxValue ), "Abs(int.max)" );
            Cmpr( Fast.Abs( int.MaxValue - 1 ), Def.Abs( int.MaxValue - 1 ), "Abs(int.max-1)" );
            Cmpr( Fast.Abs( int.MinValue+1 ), Def.Abs( int.MinValue+1 ), "Abs(int.min+1)" );

            //abs(long)
            const long lp1 = 1;
            const long ln1 = -1;
            Cmpr( Fast.Abs( lp1 ), Def.Abs( lp1 ), "Abs(1L)" );
            Cmpr( Fast.Abs( ln1 ), Def.Abs( ln1 ), "Abs(-1L)" );
            Cmpr( Fast.Abs( long.MaxValue ), Def.Abs( long.MaxValue ), "Abs(long.max)" );
            Cmpr( Fast.Abs( long.MaxValue - 1L ), Def.Abs( long.MaxValue - 1L ), "Abs(long.max-1)" );
            Cmpr( Fast.Abs( long.MinValue+1L ), Def.Abs( long.MinValue+1L ), "Abs(long.min+1)" );
            
            //abs(short)
            const short sp1 = 1;
            const short sn1 = -1;
            Cmpr( Fast.Abs( sp1 ), Def.Abs( sp1 ), "Abs(1s)" );
            Cmpr( Fast.Abs( sn1 ), Def.Abs( sn1 ), "Abs(-1s)" );
            Cmpr( Fast.Abs( short.MaxValue ), Def.Abs( short.MaxValue ), "Abs(short.max)");
            Cmpr( Fast.Abs( (short)(short.MaxValue - 1) ), Def.Abs( (short)(short.MaxValue - 1 )), "Abs(short.max-1)" );
            Cmpr( Fast.Abs( (short)( short.MinValue + 1 ) ), Def.Abs( (short)( short.MinValue + 1 ) ), "Abs(short.min+1)" );

            //abs(sbyte)
            const sbyte bp1 = 1;
            const sbyte bn1 = -1;
            Cmpr( Fast.Abs( bp1 ), Def.Abs( bp1 ), "Abs(1b)" );
            Cmpr( Fast.Abs( bn1 ), Def.Abs( bn1 ), "Abs(-1b)" );
            Cmpr( Fast.Abs( sbyte.MaxValue ), Def.Abs( sbyte.MaxValue ), "Abs(sbyte.max)" );
            Cmpr( Fast.Abs( (sbyte)(sbyte.MaxValue - 1) ), Def.Abs( (sbyte)(sbyte.MaxValue - 1) ), "Abs(sbyte.max-1)" );
            Cmpr( Fast.Abs( (sbyte)( sbyte.MinValue + 1 ) ), Def.Abs( (sbyte)( sbyte.MinValue + 1 ) ), "Abs(sbyte.min+1)" );
        }
        [TestMethod]
        public void Sign() {
            //Sign(int)
            const int ip1 = 1;
            const int in1 = -1;
            Cmpr( Fast.Sign( ip1 ), Def.Sign( ip1 ), "Sign(1)" );
            Cmpr( Fast.Sign( in1 ), Def.Sign( in1 ), "Sign(-1)" );
            Cmpr( Fast.Sign( int.MaxValue ), Def.Sign( int.MaxValue ), "Sign(int.max)" );
            Cmpr( Fast.Sign( int.MaxValue - 1 ), Def.Sign( int.MaxValue - 1 ), "Sign(int.max-1)" );
            Cmpr( Fast.Sign( int.MinValue ), Def.Sign( int.MinValue ), "Sign(int.min)" );
            Cmpr( Fast.Sign( int.MinValue + 1 ), Def.Sign( int.MinValue + 1 ), "Sign(int.min+1)" );

            //Sign(long)
            const long lp1 = 1;
            const long ln1 = -1;
            Cmpr( Fast.Sign( lp1 ), Def.Sign( lp1 ), "Sign(1L)" );
            Cmpr( Fast.Sign( ln1 ), Def.Sign( ln1 ), "Sign(-1L)" );
            Cmpr( Fast.Sign( long.MaxValue ), Def.Sign( long.MaxValue ), "Sign(long.max)" );
            Cmpr( Fast.Sign( long.MaxValue - 1L ), Def.Sign( long.MaxValue - 1L ), "Sign(long.max-1)" );
            Cmpr( Fast.Sign( long.MinValue ), Def.Sign( long.MinValue ), "Sign(long.min)" );
            Cmpr( Fast.Sign( long.MinValue + 1L ), Def.Sign( long.MinValue + 1L ), "Sign(long.min+1)" );

            //Sign(short)
            const short sp1 = 1;
            const short sn1 = -1;
            Cmpr( Fast.Sign( sp1 ), Def.Sign( sp1 ), "Sign(1s)" );
            Cmpr( Fast.Sign( sn1 ), Def.Sign( sn1 ), "Sign(-1s)" );
            Cmpr( Fast.Sign( short.MaxValue ), Def.Sign( short.MaxValue ), "Sign(short.max)" );
            Cmpr( Fast.Sign( (short)( short.MaxValue - 1 ) ), Def.Sign( (short)( short.MaxValue - 1 ) ), "Sign(short.max-1)" );
            Cmpr( Fast.Sign( short.MinValue ), Def.Sign( short.MinValue ), "Sign(short.min)" );
            Cmpr( Fast.Sign( (short)( short.MinValue + 1 ) ), Def.Sign( (short)( short.MinValue + 1 ) ), "Sign(short.min+1)" );

            //Sign(sbyte)
            const sbyte bp1 = 1;
            const sbyte bn1 = -1;
            Cmpr( Fast.Sign( bp1 ), Def.Sign( bp1 ), "Sign(1b)" );
            Cmpr( Fast.Sign( bn1 ), Def.Sign( bn1 ), "Sign(-1b)" );
            Cmpr( Fast.Sign( sbyte.MaxValue ), Def.Sign( sbyte.MaxValue ), "Sign(sbyte.max)" );
            Cmpr( Fast.Sign( (sbyte)( sbyte.MaxValue - 1 ) ), Def.Sign( (sbyte)( sbyte.MaxValue - 1 ) ), "Sign(sbyte.max-1)" );
            Cmpr( Fast.Sign( sbyte.MinValue ), Def.Sign( sbyte.MinValue ), "Sign(sbyte.min)" );
            Cmpr( Fast.Sign( (sbyte)( sbyte.MinValue + 1 ) ), Def.Sign( (sbyte)( sbyte.MinValue + 1 ) ), "Sign(sbyte.min+1)" );
        }
        [TestMethod]
        public void IsPowerOfTwo() {
            //int
            Assert.IsFalse( Fast.IsPowerOfTwo( 0 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 1 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 2 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( 6 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 8 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( int.MaxValue - 1 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( int.MaxValue ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( int.MinValue + 1 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( int.MinValue ) );
            //long
            Assert.IsFalse( Fast.IsPowerOfTwo( 0L ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 1L ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 2L ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( 6L ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 8L ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( long.MaxValue - 1L ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( long.MaxValue ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( long.MinValue + 1L ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( long.MinValue ) );
            //byte
            Assert.IsFalse( Fast.IsPowerOfTwo( (byte)0 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (byte)1 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (byte)2 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (byte)6 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (byte)8 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (byte)( byte.MaxValue - 1 ) ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( byte.MaxValue ) );
            //sbyte
            Assert.IsFalse( Fast.IsPowerOfTwo( (sbyte)0 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (sbyte)1 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (sbyte)2 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (sbyte)6 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (sbyte)8 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (sbyte)( sbyte.MaxValue - 1 ) ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( sbyte.MaxValue ) );
            //uint
            Assert.IsFalse( Fast.IsPowerOfTwo( 0u ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 1u ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 2u ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( 6u ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 8u ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( uint.MaxValue - 1u ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( uint.MaxValue ) );
            //ulong
            Assert.IsFalse( Fast.IsPowerOfTwo( 0UL ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 1UL ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 2UL ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( 6UL ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( 8UL ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( ulong.MaxValue - 1UL ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( ulong.MaxValue ) );

            //ushort
            Assert.IsFalse( Fast.IsPowerOfTwo( (ushort)0 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (ushort)1 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (ushort)2 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (ushort)6 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (ushort)8 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (ushort) (ushort.MaxValue - 1) ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( ushort.MaxValue ) );

            //short
            Assert.IsFalse( Fast.IsPowerOfTwo( (short)0 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (short)1 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (short)2 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (short)6 ) );
            Assert.IsTrue( Fast.IsPowerOfTwo( (short)8 ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (short)( short.MaxValue - 1 ) ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( short.MaxValue ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( (short)(short.MinValue + 1) ) );
            Assert.IsFalse( Fast.IsPowerOfTwo( short.MinValue ) );
        }
        [TestMethod]
        public void Miniluv() {
            //int


            Assert.AreEqual( Fast.QMin( 0, -1 ), Def.Min( 0, -1 ) );
            Assert.AreEqual( Fast.QMin( 0, 1 ), Def.Min( 0, 1 ) );
            Assert.AreEqual( Fast.QMin( -1, 1 ), Def.Min( -1, 1 ) );

            Assert.AreEqual( Fast.QMin( 0, 0 ), Def.Min( 0, 0 ) );
            Assert.AreEqual( Fast.QMin( -1, -1 ), Def.Min( -1, -1 ) );
            Assert.AreEqual( Fast.QMin( 1, 1 ), Def.Min( 1, 1 ) );

            Assert.AreEqual( Fast.QMin( int.MinValue + 1, int.MinValue ), Def.Min( int.MinValue+1, int.MinValue ) );
            Assert.AreEqual( Fast.QMin( int.MaxValue, int.MaxValue - 1 ), Def.Min( int.MaxValue, int.MaxValue - 1 ) );

            //long

            Assert.AreEqual(Fast.QMin(0L, -1L), Def.Min(0L, -1L));
            Assert.AreEqual(Fast.QMin(0L, 1L), Def.Min(0L, 1L));
            Assert.AreEqual(Fast.QMin(-1L, 1L), Def.Min(-1L, 1L));

            Assert.AreEqual(Fast.QMin(0L, 0L), Def.Min(0L, 0L));
            Assert.AreEqual(Fast.QMin(-1L, -1L), Def.Min(-1L, -1L));
            Assert.AreEqual(Fast.QMin(1L, 1L), Def.Min(1L, 1L));

            Assert.AreEqual(Fast.QMin(long.MinValue + 1L, long.MinValue), Def.Min(long.MinValue + 1L, long.MinValue));
            Assert.AreEqual(Fast.QMin(long.MaxValue, long.MaxValue - 1L), Def.Min(long.MaxValue, long.MaxValue - 1L));

            //short
            Assert.AreEqual(Fast.QMin((short)0, (short)-1), Def.Min((short)0, (short)-1));
            Assert.AreEqual(Fast.QMin((short)0, (short)1), Def.Min((short)0, (short)1));
            Assert.AreEqual(Fast.QMin((short)-1, (short)1), Def.Min((short)-1, (short)1));

            Assert.AreEqual(Fast.QMin((short)0, (short)0), Def.Min((short)0, (short)0));
            Assert.AreEqual(Fast.QMin((short)-1, (short)-1), Def.Min((short)-1, (short)-1));
            Assert.AreEqual(Fast.QMin((short)1, (short)1), Def.Min((short)1, (short)1));

            Assert.AreEqual(Fast.QMin((short)(short.MinValue + 1), short.MinValue), Def.Min((short)(short.MinValue + 1), short.MinValue));
            Assert.AreEqual(Fast.QMin(short.MaxValue, (short)(short.MaxValue - 1)), Def.Min(short.MaxValue, (short)(short.MaxValue - 1)));
        }
        [TestMethod]
        public void Maxtest()
        {
            //int
            Assert.AreEqual(Fast.QMax(0, -1), Def.Max(0, -1));
            Assert.AreEqual(Fast.QMax(0, 1), Def.Max(0, 1));
            Assert.AreEqual(Fast.QMax(-1, 1), Def.Max(-1, 1));

            Assert.AreEqual(Fast.QMax(0, 0), Def.Max(0, 0));
            Assert.AreEqual(Fast.QMax(-1, -1), Def.Max(-1, -1));
            Assert.AreEqual(Fast.QMax(1, 1), Def.Max(1, 1));

            Assert.AreEqual(Fast.QMax(int.MinValue + 1, int.MinValue), Def.Max(int.MinValue + 1, int.MinValue));
            Assert.AreEqual(Fast.QMax(int.MaxValue, int.MaxValue - 1), Def.Max(int.MaxValue, int.MaxValue - 1));

            //long

            Assert.AreEqual(Fast.QMax(0L, -1L), Def.Max(0L, -1L));
            Assert.AreEqual(Fast.QMax(0L, 1L), Def.Max(0L, 1L));
            Assert.AreEqual(Fast.QMax(-1L, 1L), Def.Max(-1L, 1L));

            Assert.AreEqual(Fast.QMax(0L, 0L), Def.Max(0L, 0L));
            Assert.AreEqual(Fast.QMax(-1L, -1L), Def.Max(-1L, -1L));
            Assert.AreEqual(Fast.QMax(1L, 1L), Def.Max(1L, 1L));

            Assert.AreEqual(Fast.QMax(long.MinValue + 1L, long.MinValue), Def.Max(long.MinValue + 1L, long.MinValue));
            Assert.AreEqual(Fast.QMax(long.MaxValue, long.MaxValue - 1L), Def.Max(long.MaxValue, long.MaxValue - 1L));

            //short
            Assert.AreEqual(Fast.QMax((short)0, (short)-1), Def.Max((short)0, (short)-1));
            Assert.AreEqual(Fast.QMax((short)0, (short)1), Def.Max((short)0, (short)1));
            Assert.AreEqual(Fast.QMax((short)-1, (short)1), Def.Max((short)-1, (short)1));

            Assert.AreEqual(Fast.QMax((short)0, (short)0), Def.Max((short)0, (short)0));
            Assert.AreEqual(Fast.QMax((short)-1, (short)-1), Def.Max((short)-1, (short)-1));
            Assert.AreEqual(Fast.QMax((short)1, (short)1), Def.Max((short)1, (short)1));

            Assert.AreEqual(Fast.QMax((short)(short.MinValue + 1), short.MinValue), Def.Max((short)(short.MinValue + 1), short.MinValue));
            Assert.AreEqual(Fast.QMax(short.MaxValue, (short)(short.MaxValue - 1)), Def.Max(short.MaxValue, (short)(short.MaxValue - 1)));

        }

        private static void Cmpr( int actual, int expected, string funcName ) {
            Assert.AreEqual( expected, actual, "{0} returned {1} instead of {2}", funcName, actual, expected );
        }
        private static void Cmpr( short actual, short expected, string funcName ) {
            Assert.AreEqual( expected, actual, "{0} returned {1} instead of {2}", funcName, actual, expected );
        }
        private static void Cmpr( long actual, long expected, string funcName ) {
            Assert.AreEqual( expected, actual, "{0} returned {1} instead of {2}", funcName, actual, expected );
        }
        private static void Cmpr( sbyte actual, sbyte expected, string funcName ) {
            Assert.AreEqual( expected, actual, "{0} returned {1} instead of {2}", funcName, actual, expected );
        }
    }
}
