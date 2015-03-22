using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kasthack.Performance.Strings.Tests {
    //todo: implement tests for String(16,24,32)
    [TestClass]
    public class StringTests {
        [TestMethod]
        public void HobbitThereAndBackAgain() {
            //full
            var s1 = "12345678";
            var ss = String8.FromString( s1 );
            Assert.AreEqual( s1, ss.ToString() );

            //partial
            var s = "1";
            var ones = String8.FromString( s );
            Assert.AreEqual( s, ones.ToString() );
        }

        [TestMethod]
        public void Comparisons() {
            var a = "12345678";
            var b = "12345679";
            var sa = String8.FromString( a );
            var sb = String8.FromString( b );

            Assert.IsFalse( sa.Equals( null ) );

            Assert.AreEqual( System.Math.Sign( sa.CompareTo( sb ) ), System.Math.Sign(String.Compare( a, b, StringComparison.Ordinal ) ));
            Assert.AreEqual( System.Math.Sign( String8.Compare( sa, sb ) ), System.Math.Sign( String.Compare( a, b, StringComparison.Ordinal ) ) );

            Assert.AreEqual( sa == sb, a == b );
            Assert.AreEqual( String8.Equals(sa, sb ), a == b );
            Assert.AreEqual( sa != sb, a != b );

            Assert.IsTrue( sa < sb );
            Assert.IsTrue( sa <= sb );

            Assert.IsFalse( sa > sb );
            Assert.IsFalse( sa >= sb );
        }
        //todo: test for arrays: one version of library was failing on 2+GB arrays
        [TestMethod]
        public void Conversions() {
            var a = "12345678";
            var sa = String8.FromString( a );

            //conversions to type and back
            Assert.IsTrue( (String8)(String32)sa == sa );
            Assert.IsTrue( (String8)(String24)sa == sa );
            Assert.IsTrue( (String8)(String16)sa == sa );

            // string8->type + cross-type comaprison
            Assert.IsTrue( (String32)sa == sa );
            Assert.IsTrue( (String24)sa == sa );
            Assert.IsTrue( (String16)sa == sa );

            //more weird comparisons
            Assert.IsTrue( (String32)sa == (String24)sa );
            Assert.IsTrue( (String24)sa == (String16)sa );
            Assert.IsTrue( (String32)sa == (String16)sa );

            //strings
            Assert.IsTrue( ((String32)sa).ToString() == a );
            Assert.IsTrue( ((String24)sa).ToString() == a );
            Assert.IsTrue( ((String16)sa).ToString() == a );
        }
    }
}
