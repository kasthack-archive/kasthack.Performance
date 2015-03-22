using System;
using System.Diagnostics;
using static kasthack.Performance.Math.BitTwiddling;
namespace Benchmark {
    class Program {
        static void Main( string[] args ) {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            {
                var sw = new Stopwatch();
                sw.Start();
                var checkInt = CheckInt( 2000000000 );
                sw.Stop();
                Console.Write( "Check(int): {0} Time: {1}", checkInt.ToString(), sw.Elapsed );
            }
            {
                var sw = new Stopwatch();
                sw.Start();
                var checkInt = CheckLong( 2000000000 );
                sw.Stop();
                Console.Write( "Check(long): {0} Time: {1}", checkInt.ToString(), sw.Elapsed );
            }

            Console.ReadLine();
        }

        private static int CheckInt(int loopCount) {
            int cur = 0;
            int i1 = loopCount;
            for ( int i = 0; i < i1; i++ ) {
                cur ^= QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i )
                       ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i )
                       ^ QMax( cur, i ) ^ QMax( cur, i );
                cur++;
            }
            return cur;
        }
        private static long CheckLong( int loopCount ) {
            long cur = 0;
            long i1 = loopCount;
            for ( long i = 0; i < i1; i++ ) {
                cur ^= QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i )
                       ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i ) ^ QMax( cur, i )
                       ^ QMax( cur, i ) ^ QMax( cur, i );
                cur++;
            }
            return cur;
        }
    }
}
