using System;
using System.Diagnostics;
using kasthack.Performance.Math;
namespace Benchmark {
    class Program {
        static void Main( string[] args ) {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            {
                var sw = new Stopwatch();
                CheckInt(1);//jit warmup
                sw.Start();
                var checkInt = CheckInt( 2000000000 );
                sw.Stop();
                Console.Write( "Check(int): {0} Time: {1}", checkInt, sw.Elapsed );
            }
            {
                var sw = new Stopwatch();
                CheckLong(1);
                sw.Start();
                var checkInt = CheckLong( 2000000000 );
                sw.Stop();
                Console.Write( "Check(long): {0} Time: {1}", checkInt, sw.Elapsed );
            }

            Console.ReadLine();
        }

        private static int CheckInt(int loopCount) {
            int cur = 0;
            int i1 = loopCount;
            for ( int i = 0; i < i1; i++ ) {
                cur ^= BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i )
                       ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i )
                       ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i );
                cur++;
            }
            return cur;
        }
        private static long CheckLong( int loopCount ) {
            long cur = 0;
            long i1 = loopCount;
            for ( long i = 0; i < i1; i++ ) {
                cur ^= BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i )
                       ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i )
                       ^ BitTwiddling.QMax( cur, i ) ^ BitTwiddling.QMax( cur, i );
                cur++;
            }
            return cur;
        }
    }
}
