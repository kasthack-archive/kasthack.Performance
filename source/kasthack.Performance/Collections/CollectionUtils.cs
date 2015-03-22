using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kasthack.Performance.Collections {
    public static class CollectionUtils {
        const int SequentialThreshold = 8192;
        private static readonly Lazy<kasthack.Performance.Math.Random> rng = new Lazy<Math.Random>();
        /// <summary>
        /// QuickSort array
        /// <param name="array">Array</param>
        /// </summary>
        public static void Sort<T>( this T[] array ) where T : IComparable<T> => Sort( array, ( a, b ) => a.CompareTo( b ), 0, array.Length - 1 );
        /// <summary>
        /// QuickSort array with custom comparison
        /// <param name="array">Array</param>
        /// <param name="comparison">Comparison delegate. Example: (a,b)=>String.Compare(a,b)</param>
        /// </summary>
        public static void Sort<T>( this T[] array, Comparison<T> comparison ) => Sort( array, comparison, 0, array.Length - 1 );
        /// <summary>
        /// Parallel QuickSort array
        /// <param name="array">Array</param>
        /// </summary>
        public static void ParallelSort<T>( this T[] array ) where T : IComparable<T> => ParallelSort( array, ( a, b ) => a.CompareTo( b ), 0, array.Length - 1 );
        /// <summary>
        /// Parallel QuickSort range with custom comparison
        /// <param name="array">Array</param>
        /// <param name="comparison">Comparison delegate. Example: (a,b)=>String.Compare(a,b)</param>
        /// </summary>
        public static void ParallelSort<T>( this T[] array, Comparison<T> comparison ) => ParallelSort( array, comparison, 0, array.Length - 1 );
        /// <summary>
        /// QuickSort range
        /// <param name="array">Array</param>
        /// <param name="comparison">Comparison delegate. Example: (a,b)=>String.Compare(a,b)</param>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// </summary>
        public static void Sort<T>( T[] array, Comparison<T> comparison, int left, int right ) {
            if ( right <= left ) return;
            var pivot = Partition( array, comparison, left, right );
            Sort( array, comparison, left, pivot - 1 );
            Sort( array, comparison, pivot + 1, right );
        }
        /// <summary>
        /// Parallel QuickSort range
        /// <param name="array">Array</param>
        /// <param name="comparison">Comparison delegate. Example: (a,b)=>String.Compare(a,b)</param>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// </summary>
        public static void ParallelSort<T>( T[] array, Comparison<T> comparison, int left, int right ) {
            if ( right <= left ) return;
            if ( right - left < SequentialThreshold )
                Sort( array, comparison, left, right );
            else {
                var pivot = Partition( array, comparison, left, right );
                Parallel.Invoke( new Action[] {
                    ()=>ParallelSort(array, comparison, left, pivot - 1),
                    ()=>ParallelSort(array, comparison, pivot + 1, right)
                } );
            }
        }

        private static int Partition<T>( T[] array, Comparison<T> comparison, int low, int high ) {
            var pivotPos = ( high + low ) / 2;
            var pivot = array[ pivotPos ];
            Swap( array, low, pivotPos );
            var left = low;
            for ( var i = low + 1; i <= high; i++ )
                if ( comparison( array[ i ], pivot ) < 0 )
                    Swap( array, i, ++left );
            Swap( array, low, left );
            return left;
        }
        /// <summary>
        /// Binary search with custom comparison
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="value">Value to search</param>
        /// <param name="comparison">Comparison delegate. Example: (a,b)=>String.Compare(a,b)</param>
        /// <returns>Index of value or ~index of nearest if not found</returns>
        public static int BinarySearch<T>( this T[] array, T value, Comparison<T> comparison ) => BinarySearch( array, value, comparison, 0, array.Length );
        /// <summary>
        /// Binary search with custom comparison
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="value">Value to search</param>
        /// <param name="comparison">Comparison delegate. Example: (a,b)=>String.Compare(a,b)</param>
        /// <param name="min">QMin index(inclusive)</param>
        /// <param name="max">QMax index(exclusive)</param>
        /// <param name="nochecks">Don't validate parameters</param>
        /// <returns>Index of value or ~index of nearest if not found</returns>
        public static int BinarySearch<T>( this T[] array, T value, Comparison<T> comparison, int min, int max, bool nochecks = false) {
            if (!nochecks) BinarySearchChecks( array, value, comparison );
            if ( array.Length == 0 )
                return -1;
            int iMin = min,
                    iMax = min + max - 1,
                    iCmp = 0;
            try {
                int iMid;
                while ( iMin <= iMax ) {
                    iMid = iMin + ( ( iMax - iMin ) / 2 );
                    iCmp = comparison( array[ iMid ], value );
                    if ( iCmp == 0 ) return iMid;
                    if ( iCmp > 0 ) iMax = iMid - 1;
                    else iMin = iMid + 1;
                }
            }
            catch ( Exception e ) {
                throw new InvalidOperationException( "Comparer threw an exception.", e );
            }
            return ~iMin;
        }
        private static void BinarySearchChecks<T>( T[] array, T value, Comparison<T> comparison ) {
            if ( array == null ) throw new ArgumentNullException( "array" );
            if ( array.Rank > 1 ) throw new RankException( "Only single dimension arrays are supported." );
            if ( comparison == null ) throw new ArgumentNullException( "comparison" );
            if ( ( value != null )
                 && !( value is IComparable ) ) throw new ArgumentException( "comparer is null and value does not support IComparable." );
        }
        /// <summary>
        /// Shuffle elements in array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="r">Your instanse of RNG</param>
        public static void Shuffle<T>( this IList<T> array ) => Shuffle( array, 0, array.Count - 1);

        /// <summary>
        /// Shuffle array between two indexes
        /// </summary>
        /// <param name="collection">Array</param>
        /// <param name="min">QMin index(inclusive)</param>
        /// <param name="max">QMin index(inclusive)</param>
        public static void Shuffle<T>( this IList<T> collection, int min, int max ) {
            var r = rng.Value;
            var cnt = max;
            var _cnt = cnt + 1;
            T o;
            var b = r.Next( min, _cnt );
            for ( var a = min; a < cnt; ) {
                //swap inlined
                o = collection[ a ];
                collection[ a ] = collection[ b ];
                collection[ b ] = o;
                b = r.Next( ++a, _cnt );
            }
        }
        /// <summary>
        /// Swap two elements in array
        /// </summary>
        /// <param name="collection">Array</param>
        /// <param name="a">Index of first element</param>
        /// <param name="b">Index of second element</param>
        public static void Swap<T>( this IList<T> collection, int a, int b ) {
            var o = collection[ a ];
            collection[ a ] = collection[ b ];
            collection[ b ] = o;
        }
    }
}