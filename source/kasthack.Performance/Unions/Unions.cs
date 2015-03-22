using System.Runtime.InteropServices;

namespace kasthack.Performance.Unions {
    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    public struct OneByte {
        [FieldOffset(0)]
        public bool Bool;
        [FieldOffset(0)]
        public byte Byte;
        [FieldOffset(0)]
        public sbyte SByte;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    public struct TwoBytes {
        [FieldOffset(0)]
        public char Char;
        [FieldOffset(0)]
        public short Short;
        [FieldOffset(0)]
        public ushort Ushort;

        [FieldOffset( 0 )]
        public byte ByteFirst;
        [FieldOffset(1)]
        public byte ByteSecond;

        [FieldOffset(0)]
        public bool BoolFirst;
        [FieldOffset(1)]
        public bool BoolSecond;

        [FieldOffset(0)]
        public sbyte SByteFirst;
        [FieldOffset(1)]
        public sbyte SByteSecond;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    public unsafe struct FourBytes {
        [FieldOffset(0)]
        public float Float;

        [FieldOffset( 0 )]
        public int Int;

        [FieldOffset( 0 )]
        public uint UInt;
        
        [FieldOffset(0)]
        public char CharFirst;
        [FieldOffset(2)]
        public char CharSecond;

        [FieldOffset(0)]
        public short ShortFirst;
        [FieldOffset(2)]
        public short ShortSecond;
        
        [FieldOffset(0)]
        public fixed bool Bools[ 4 ];
        [FieldOffset(0)]
        public fixed byte Bytes[ 4 ];
        [FieldOffset(0)]
        public fixed sbyte SBytes[ 4 ];
    }

    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    public unsafe struct EightBytes {
        [FieldOffset( 0 )]
        public long Long;
        [FieldOffset(0)]
        public ulong Ulong;

        [FieldOffset(0)]
        public double Double;
        
        [FieldOffset(0)]
        public float FloatFirst;
        [FieldOffset(4)]
        public float FloatSecond;

        [FieldOffset(0)]
        public int IntFirst;
        [FieldOffset(4)]
        public int IntSecond;


        [FieldOffset(0)]
        public uint UIntFirst;
        [FieldOffset(4)]
        public uint UIntSecond;


        [FieldOffset(0)]
        public fixed bool Bools[ 8 ];
        [FieldOffset( 0 )]
        public fixed byte Bytes[8];
        [FieldOffset(0)]
        public fixed sbyte SBytes[ 8 ];

        [FieldOffset(0)]
        public fixed char Chars[ 4 ];
        [FieldOffset(0)]
        public fixed short Shorts[ 4 ];
    }
}
