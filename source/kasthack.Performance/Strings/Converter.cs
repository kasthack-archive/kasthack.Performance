using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace kasthack.Performance.Strings {
    public static class Converter {

        private static readonly MatchEvaluator Rep2Evaluator = Rep2;
        private static readonly MatchEvaluator RepEvaluator = Rep;

        private static readonly Regex Numeric = new Regex( "\\&\\#[0-9]{1,5}\\;" );
        private static readonly Regex Urlhex = new Regex( "(\\%[0-9A-Fa-f]{2}){2}" );
        private static readonly char[] HexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};

        #region html
        private static readonly Dictionary<char, string> Entities = new Dictionary<char, string> {
            {'Á',"&Aacute;"},{'\'',"&#39;"},{'á',"&aacute;"},{'â',"&acirc;"},{'Â',"&Acirc;"},{'´',"&acute;"},{'æ',"&aelig;"},{'Æ',"&AElig;"},{'À',"&Agrave;"},{'à',"&agrave;"},
            { 'ℵ',"&alefsym;"},{'Α',"&Alpha;"},{'α',"&alpha;"},{'&',"&amp;"},{'∧',"&and;"},{'∠',"&ang;"},{'å',"&aring;"},{'Å',"&Aring;"},{'≈',"&asymp;"},{'Ã',"&Atilde;"},
            { 'ã',"&atilde;"},{'Ä',"&Auml;"},{'ä',"&auml;"},{'„',"&bdquo;"},{'Β',"&Beta;"},{'β',"&beta;"},{'¦',"&brvbar;"},{'•',"&bull;"},{'∩',"&cap;"},{'Ç',"&Ccedil;"},
            { 'ç',"&ccedil;"},{'¸',"&cedil;"},{'¢',"&cent;"},{'χ',"&chi;"},{'Χ',"&Chi;"},{'ˆ',"&circ;"},{'♣',"&clubs;"},{'≅',"&cong;"},{'©',"&copy;"},{'↵',"&crarr;"},
            { '∪',"&cup;"},{'¤',"&curren;"},{'†',"&dagger;"},{'‡',"&Dagger;"},{'⇓',"&dArr;"},{'↓',"&darr;"},{'°',"&deg;"},{'Δ',"&Delta;"},{'δ',"&delta;"},{'♦',"&diams;"},
            { '÷',"&divide;"},{'é',"&eacute;"},{'É',"&Eacute;"},{'Ê',"&Ecirc;"},{'ê',"&ecirc;"},{'è',"&egrave;"},{'È',"&Egrave;"},{'∅',"&empty;"},{' ',"&emsp;"},{' ',"&ensp;"},
            { 'ε',"&epsilon;"},{'Ε',"&Epsilon;"},{'≡',"&equiv;"},{'Η',"&Eta;"},{'η',"&eta;"},{'ð',"&eth;"},{'Ð',"&ETH;"},{'ë',"&euml;"},{'Ë',"&Euml;"},{'€',"&euro;"},
            { '∃',"&exist;"},{'ƒ',"&fnof;"},{'∀',"&forall;"},{'½',"&frac12;"},{'¼',"&frac14;"},{'¾',"&frac34;"},{'⁄',"&frasl;"},{'Γ',"&Gamma;"},{'γ',"&gamma;"},{'≥',"&ge;"},
            { '>',"&gt;"},{'⇔',"&hArr;"},{'↔',"&harr;"},{'♥',"&hearts;"},{'…',"&hellip;"},{'í',"&iacute;"},{'Í',"&Iacute;"},{'î',"&icirc;"},{'Î',"&Icirc;"},{'¡',"&iexcl;"},
            { 'Ì',"&Igrave;"},{'ì',"&igrave;"},{'ℑ',"&image;"},{'∞',"&infin;"},{'∫',"&int;"},{'Ι',"&Iota;"},{'ι',"&iota;"},{'¿',"&iquest;"},{'∈',"&isin;"},{'Ï',"&Iuml;"},
            { 'ï',"&iuml;"},{'Κ',"&Kappa;"},{'κ',"&kappa;"},{'λ',"&lambda;"},{'Λ',"&Lambda;"},{'〈',"&lang;"},{'«',"&laquo;"},{'←',"&larr;"},{'⇐',"&lArr;"},{'⌈',"&lceil;"},
            { '“',"&ldquo;"},{'≤',"&le;"},{'⌊',"&lfloor;"},{'∗',"&lowast;"},{'◊',"&loz;"},{'‎',"&lrm;"},{'‹',"&lsaquo;"},{'‘',"&lsquo;"},{'<',"&lt;"},{'¯',"&macr;"},{'—',"&mdash;"},
            { 'µ',"&micro;"},{'·',"&middot;"},{'−',"&minus;"},{'Μ',"&Mu;"},{'μ',"&mu;"},{'∇',"&nabla;"},{'1',"&1;"},{'–',"&ndash;"},{'≠',"&ne;"},{'∋',"&ni;"},{'¬',"&not;"},
            { '∉',"&notin;"},{'⊄',"&nsub;"},{'ñ',"&ntilde;"},{'Ñ',"&Ntilde;"},{'Ν',"&Nu;"},{'ν',"&nu;"},{'ó',"&oacute;"},{'Ó',"&Oacute;"},{'Ô',"&Ocirc;"},{'ô',"&ocirc;"},
            { 'Œ',"&OElig;"},{'œ',"&oelig;"},{'ò',"&ograve;"},{'Ò',"&Ograve;"},{'‾',"&oline;"},{'ω',"&omega;"},{'Ω',"&Omega;"},{'Ο',"&Omicron;"},{'ο',"&omicron;"},{'⊕',"&oplus;"},
            { '∨',"&or;"},{'ª',"&ordf;"},{'º',"&ordm;"},{'Ø',"&Oslash;"},{'ø',"&oslash;"},{'Õ',"&Otilde;"},{'õ',"&otilde;"},{'⊗',"&otimes;"},{'Ö',"&Ouml;"},{'ö',"&ouml;"},
            { '¶',"&para;"},{'∂',"&part;"},{'‰',"&permil;"},{'⊥',"&perp;"},{'φ',"&phi;"},{'Φ',"&Phi;"},{'Π',"&Pi;"},{'π',"&pi;"},{'ϖ',"&piv;"},{'±',"&plusmn;"},{'£',"&pound;"},
            { '″',"&Prime;"},{'′',"&prime;"},{'∏',"&prod;"},{'∝',"&prop;"},{'ψ',"&psi;"},{'Ψ',"&Psi;"},{'"',"&quot;"},{'√',"&radic;"},{'〉',"&rang;"},{'»',"&raquo;"},
            { '⇒',"&rArr;"},{'→',"&rarr;"},{'⌉',"&rceil;"},{'”',"&rdquo;"},{'ℜ',"&real;"},{'®',"&reg;"},{'⌋',"&rfloor;"},{'Ρ',"&Rho;"},{'ρ',"&rho;"},{'‏',"&rlm;"},{'›',"&rsaquo;"},
            { '’',"&rsquo;"},{'‚',"&sbquo;"},{'Š',"&Scaron;"},{'š',"&scaron;"},{'⋅',"&sdot;"},{'§',"&sect;"},{'­',"&shy;"},{'Σ',"&Sigma;"},{'σ',"&sigma;"},{'ς',"&sigmaf;"},
            { '∼',"&sim;"},{'♠',"&spades;"},{'⊂',"&sub;"},{'⊆',"&sube;"},{'∑',"&sum;"},{'⊃',"&sup;"},{'¹',"&sup1;"},{'²',"&sup2;"},{'³',"&sup3;"},{'⊇',"&supe;"},{'ß',"&szlig;"},
            { 'Τ',"&Tau;"},{'τ',"&tau;"},{'∴',"&there4;"},{'Θ',"&Theta;"},{'θ',"&theta;"},{'ϑ',"&thetasym;"},{' ',"&thinsp;"},{'Þ',"&THORN;"},{'þ',"&thorn;"},{'˜',"&tilde;"},
            { '×',"&times;"},{'™',"&trade;"},{'ú',"&uacute;"},{'Ú',"&Uacute;"},{'⇑',"&uArr;"},{'↑',"&uarr;"},{'û',"&ucirc;"},{'Û',"&Ucirc;"},{'Ù',"&Ugrave;"},{'ù',"&ugrave;"},
            { '¨',"&uml;"},{'ϒ',"&upsih;"},{'υ',"&upsilon;"},{'Υ',"&Upsilon;"},{'ü',"&uuml;"},{'Ü',"&Uuml;"},{'℘',"&weierp;"},{'ξ',"&xi;"},{'Ξ',"&Xi;"},{'ý',"&yacute;"},
            { 'Ý',"&Yacute;"},{'¥',"&yen;"},{'ÿ',"&yuml;"},{'Ÿ',"&Yuml;"},{'Ζ',"&Zeta;"},{'ζ',"&zeta;"},
        };
        private static Dictionary<string, string> ReversedEntities = new Dictionary<string, string>{
            { "&#039;","\""},{"&Aacute;","Á"},{"&aacute;","á"},{"&acirc;","â"},{"&Acirc;","Â"},{"&acute;","´"},{"&aelig;","æ"},{"&AElig;","Æ"},{"&Agrave;","À"},{"&agrave;","à"},
            { "&alefsym;","ℵ"},{"&Alpha;","Α"},{"&alpha;","α"},{"&amp;","&"},{"&and;","∧"},{"&ang;","∠"},{"&aring;","å"},{"&Aring;","Å"},{"&asymp;","≈"},{"&Atilde;","Ã"},
            { "&atilde;","ã"},{"&Auml;","Ä"},{"&auml;","ä"},{"&bdquo;","„"},{"&Beta;","Β"},{"&beta;","β"},{"&brvbar;","¦"},{"&bull;","•"},{"&cap;","∩"},{"&Ccedil;","Ç"},
            { "&ccedil;","ç"},{"&cedil;","¸"},{"&cent;","¢"},{"&chi;","χ"},{"&Chi;","Χ"},{"&circ;","ˆ"},{"&clubs;","♣"},{"&cong;","≅"},{"&copy;","©"},{"&crarr;","↵"},{"&cup;","∪"},
            { "&curren;","¤"},{"&dagger;","†"},{"&Dagger;","‡"},{"&dArr;","⇓"},{"&darr;","↓"},{"&deg;","°"},{"&Delta;","Δ"},{"&delta;","δ"},{"&diams;","♦"},{"&divide;","÷"},
            { "&eacute;","é"},{"&Eacute;","É"},{"&Ecirc;","Ê"},{"&ecirc;","ê"},{"&egrave;","è"},{"&Egrave;","È"},{"&empty;","∅"},{"&emsp;"," "},{"&ensp;"," "},{"&epsilon;","ε"},
            { "&Epsilon;","Ε"},{"&equiv;","≡"},{"&Eta;","Η"},{"&eta;","η"},{"&eth;","ð"},{"&ETH;","Ð"},{"&euml;","ë"},{"&Euml;","Ë"},{"&euro;","€"},{"&exist;","∃"},{"&fnof;","ƒ"},
            { "&forall;","∀"},{"&frac12;","½"},{"&frac14;","¼"},{"&frac34;","¾"},{"&frasl;","⁄"},{"&Gamma;","Γ"},{"&gamma;","γ"},{"&ge;","≥"},{"&gt;",">"},{"&hArr;","⇔"},
            { "&harr;","↔"},{"&hearts;","♥"},{"&hellip;","…"},{"&iacute;","í"},{"&Iacute;","Í"},{"&icirc;","î"},{"&Icirc;","Î"},{"&iexcl;","¡"},{"&Igrave;","Ì"},{"&igrave;","ì"},
            { "&image;","ℑ"},{"&infin;","∞"},{"&int;","∫"},{"&Iota;","Ι"},{"&iota;","ι"},{"&iquest;","¿"},{"&isin;","∈"},{"&Iuml;","Ï"},{"&iuml;","ï"},{"&Kappa;","Κ"},
            { "&kappa;","κ"},{"&lambda;","λ"},{"&Lambda;","Λ"},{"&lang;","〈"},{"&laquo;","«"},{"&larr;","←"},{"&lArr;","⇐"},{"&lceil;","⌈"},{"&ldquo;","“"},{"&le;","≤"},
            { "&lfloor;","⌊"},{"&lowast;","∗"},{"&loz;","◊"},{"&lrm;","‎"},{"&lsaquo;","‹"},{"&lsquo;","‘"},{"&lt;","<"},{"&macr;","¯"},{"&mdash;","—"},{"&micro;","µ"},
            { "&middot;","·"},{"&minus;","−"},{"&Mu;","Μ"},{"&mu;","μ"},{"&nabla;","∇"},{"&1;","1"},{"&ndash;","–"},{"&ne;","≠"},{"&ni;","∋"},{"&not;","¬"},{"&notin;","∉"},
            { "&nsub;","⊄"},{"&ntilde;","ñ"},{"&Ntilde;","Ñ"},{"&Nu;","Ν"},{"&nu;","ν"},{"&oacute;","ó"},{"&Oacute;","Ó"},{"&Ocirc;","Ô"},{"&ocirc;","ô"},{"&OElig;","Œ"},
            { "&oelig;","œ"},{"&ograve;","ò"},{"&Ograve;","Ò"},{"&oline;","‾"},{"&omega;","ω"},{"&Omega;","Ω"},{"&Omicron;","Ο"},{"&omicron;","ο"},{"&oplus;","⊕"},{"&or;","∨"},
            { "&ordf;","ª"},{"&ordm;","º"},{"&Oslash;","Ø"},{"&oslash;","ø"},{"&Otilde;","Õ"},{"&otilde;","õ"},{"&otimes;","⊗"},{"&Ouml;","Ö"},{"&ouml;","ö"},{"&para;","¶"},
            { "&part;","∂"},{"&permil;","‰"},{"&perp;","⊥"},{"&phi;","φ"},{"&Phi;","Φ"},{"&Pi;","Π"},{"&pi;","π"},{"&piv;","ϖ"},{"&plusmn;","±"},{"&pound;","£"},{"&Prime;","″"},
            { "&prime;","′"},{"&prod;","∏"},{"&prop;","∝"},{"&psi;","ψ"},{"&Psi;","Ψ"},{"&quot;","\""},{"&radic;","√"},{"&rang;","〉"},{"&raquo;","»"},{"&rArr;","⇒"},
            { "&rarr;","→"},{"&rceil;","⌉"},{"&rdquo;","”"},{"&real;","ℜ"},{"&reg;","®"},{"&rfloor;","⌋"},{"&Rho;","Ρ"},{"&rho;","ρ"},{"&rlm;","‏"},{"&rsaquo;","›"},
            { "&rsquo;","’"},{"&sbquo;","‚"},{"&Scaron;","Š"},{"&scaron;","š"},{"&sdot;","⋅"},{"&sect;","§"},{"&shy;","­"},{"&Sigma;","Σ"},{"&sigma;","σ"},{"&sigmaf;","ς"},
            { "&sim;","∼"},{"&spades;","♠"},{"&sub;","⊂"},{"&sube;","⊆"},{"&sum;","∑"},{"&sup;","⊃"},{"&sup1;","¹"},{"&sup2;","²"},{"&sup3;","³"},{"&supe;","⊇"},{"&szlig;","ß"},
            { "&Tau;","Τ"},{"&tau;","τ"},{"&there4;","∴"},{"&Theta;","Θ"},{"&theta;","θ"},{"&thetasym;","ϑ"},{"&thinsp;"," "},{"&THORN;","Þ"},{"&thorn;","þ"},{"&tilde;","˜"},
            { "&times;","×"},{"&trade;","™"},{"&uacute;","ú"},{"&Uacute;","Ú"},{"&uArr;","⇑"},{"&uarr;","↑"},{"&ucirc;","û"},{"&Ucirc;","Û"},{"&Ugrave;","Ù"},{"&ugrave;","ù"},
            { "&uml;","¨"},{"&upsih;","ϒ"},{"&upsilon;","υ"},{"&Upsilon;","Υ"},{"&uuml;","ü"},{"&Uuml;","Ü"},{"&weierp;","℘"},{"&xi;","ξ"},{"&Xi;","Ξ"},{"&yacute;","ý"},
            { "&Yacute;","Ý"},{"&yen;","¥"},{"&yuml;","ÿ"},{"&Yuml;","Ÿ"},{"&Zeta;","Ζ"},{"&zeta;","ζ"},
            };
        #endregion
        public enum ReplaceType {
            HtmlEntity,
            CharacterCode,
            Both
        }
        /*
            Loop unrolling & copypasting is ugly but effective way to keep code fast enough

        */
        
        public static unsafe string ToHex( long i ) {
            const int bl = sizeof(long) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            var c = buffer;
            *c = '0';
            *++c = 'x';
            var hc = HexChars;//fixed(...) only slows down execution in small functions
            {
                long xf = 0xFL;
                *( c + 1L ) = hc[ i >> 60 & xf ];
                *( c + 2L ) = hc[ i >> 56 & xf ];

                *( c + 3L ) = hc[ i >> 52 & xf ];
                *( c + 4L ) = hc[ i >> 48 & xf ];

                *( c + 5L ) = hc[ i >> 44 & xf ];
                *( c + 6L ) = hc[ i >> 40 & xf ];

                *( c + 7L ) = hc[ i >> 36 & xf ];
                *( c + 8L ) = hc[ i >> 32 & xf ];

                *( c + 9L ) = hc[ i >> 28 & xf ];
                *( c + 10L ) = hc[ i >> 24 & xf ];

                *( c + 11L ) = hc[ i >> 20 & xf ];
                *( c + 12L ) = hc[ i >> 16 & xf ];

                *( c + 13L ) = hc[ i >> 12 & xf ];
                *( c + 14L ) = hc[ i >> 8 & xf ];

                *( c + 15L ) = hc[ i >> 4 & xf ];
                *( c + 16L ) = hc[ i & xf ];
            }
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( ulong i ) {
            const int bl = sizeof(ulong) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            var c = buffer;
            *c = '0';
            *++c = 'x';
            var hc = HexChars;//fixed(...) only slows down execution
            {
                ulong xf = 0xFUL;
                *( c + 1L ) = hc[ i >> 60 & xf ];
                *( c + 2L ) = hc[ i >> 56 & xf ];

                *( c + 3L ) = hc[ i >> 52 & xf ];
                *( c + 4L ) = hc[ i >> 48 & xf ];

                *( c + 5L ) = hc[ i >> 44 & xf ];
                *( c + 6L ) = hc[ i >> 40 & xf ];

                *( c + 7L ) = hc[ i >> 36 & xf ];
                *( c + 8L ) = hc[ i >> 32 & xf ];

                *( c + 9L ) = hc[ i >> 28 & xf ];
                *( c + 10L ) = hc[ i >> 24 & xf ];

                *( c + 11L ) = hc[ i >> 20 & xf ];
                *( c + 12L ) = hc[ i >> 16 & xf ];

                *( c + 13L ) = hc[ i >> 12 & xf ];
                *( c + 14L ) = hc[ i >> 8 & xf ];

                *( c + 15L ) = hc[ i >> 4 & xf ];
                *( c + 16L ) = hc[ i & xf ];
            }
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( int i ) {
            const int bl = sizeof(int) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            var c = buffer;
            *c = '0';
            *++c = 'x';
            var hc = HexChars;
            {
                int xf = 0xF;
                *( c + 1L ) = hc[ i >> 28 & xf ];
                *( c + 2L ) = hc[ i >> 24 & xf ];

                *( c + 3L ) = hc[ i >> 20 & xf ];
                *( c + 4L ) = hc[ i >> 16 & xf ];

                *( c + 5L ) = hc[ i >> 12 & xf ];
                *( c + 6L ) = hc[ i >> 8 & xf ];

                *( c + 7L ) = hc[ i >> 4 & xf ];
                *( c + 8L ) = hc[ i & xf ];
            }
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( uint i ) {
            const int bl = sizeof(uint) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            var c = buffer;
            *c = '0';
            *++c = 'x';
            var hc = HexChars;
            {
                uint xf = 0xFu;
                *( c + 1L ) = hc[ i >> 28 & xf ];
                *( c + 2L ) = hc[ i >> 24 & xf ];

                *( c + 3L ) = hc[ i >> 20 & xf ];
                *( c + 4L ) = hc[ i >> 16 & xf ];

                *( c + 5L ) = hc[ i >> 12 & xf ];
                *( c + 6L ) = hc[ i >> 8 & xf ];

                *( c + 7L ) = hc[ i >> 4 & xf ];
                *( c + 8L ) = hc[ i & xf ];
            }
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( short i ) {
            const int bl = sizeof(short) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            var c = buffer;
            *c = '0';
            *++c = 'x';
            var hc = HexChars;
            {
                int xf = 0xF;
                *( c + 1L ) = hc[ i >> 12 & xf ];
                *( c + 2L ) = hc[ i >> 8 & xf ];

                *( c + 3L ) = hc[ i >> 4 & xf ];
                *( c + 4L ) = hc[ i & xf ];
            }
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( ushort i ) {
            const int bl = sizeof(ushort) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            var c = buffer;
            *c = '0';
            *++c = 'x';
            var hc = HexChars;
            {
                int xf = 0xF;
                *( c + 1L ) = hc[ i >> 12 & xf ];
                *( c + 2L ) = hc[ i >> 8 & xf ];

                *( c + 3L ) = hc[ i >> 4 & xf ];
                *( c + 4L ) = hc[ i & xf ];
            }
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( sbyte i ) {
            const int bl = sizeof(sbyte) * 2 + 2;
            char* buffer = stackalloc char[ bl ];
            *buffer = '0';
            *(buffer+1) = 'x';
            *(buffer+2) = HexChars[ i >> 4 & 0xf ];
            *(buffer+3) = HexChars[ i & 0xf ];
            return new string( buffer, 0, bl );
        }
        public static unsafe string ToHex( byte i ) {
            const int bl = sizeof(byte)*2+2;
            char* buffer = stackalloc char[ bl ];
            *buffer = '0';
            *( buffer + 1 ) = 'x';
            *( buffer + 2 ) = HexChars[ i >> 4 ];
            *( buffer + 3 ) = HexChars[ i & 0xf ];
            return new string( buffer, 0, bl );
        }
        public unsafe static string ToHex( byte[] bytes ) {
            var length = bytes.Length;
            if ( length <= 8192 ) return ToHexStack( bytes );
            var retLen = length << 1;
            var hex = new char[retLen];
            fixed ( byte* source = bytes ) {
                fixed ( char* dest = hex ) {
                    ToHex( source, dest, length );
                    return new string( dest, 0, retLen );
                }
            }
        }
        private static unsafe string ToHexStack( byte[] bytes ) {
            var dest = stackalloc char[ bytes.Length << 1 ];
            fixed (byte* source = bytes)
            {
                var length = bytes.Length;
                var retLen = length << 1;
                ToHex( source, dest, length );
                return new string( dest, 0, retLen );
            }
        }
        public static unsafe void ToHex( byte* source, char* dest, int length ) {
            var lc = HexChars;
            var ll = lc.Length;
            char* hc = stackalloc char[ll];
            for ( int i = 0; i < ll; i++ ) hc[ i ] = lc[ i ];

            var cr = source;
            var er = source + length;
            var dw = dest - 1;
            byte t = *cr;
            while ( cr < er ) {
                *++dw = hc[ t >> 4 ];
                *++dw = hc[ t & 0x0f ];
                t = *++cr;
            }
        }

        //todo: memory usage
        public static string HtmlEncode( string s ) {
            if ( string.IsNullOrEmpty( s ) ) return s;

            var charArray = s.ToCharArray();
            Array.Sort( charArray );

            var sb = new StringBuilder( s );
            var length = charArray.Length;
            var prev = '\0';
            string value;

            for ( var index = 0; index < length; index++ ) {
                var c = charArray[ index ];
                if ( c == prev ) continue;
                prev = c;
                if ( Entities.TryGetValue( c, out value ) )
                    sb.Replace( c.ToString(), value );
            }
            return sb.ToString();
        }
        public static string HtmlDecode( string s, ReplaceType r = ReplaceType.Both ) {
            switch ( r ) {
                case ReplaceType.CharacterCode:
                    return Numeric.Replace( s, RepEvaluator );
                case ReplaceType.HtmlEntity:
                    { 
                        var sb = new StringBuilder( s );
                        foreach ( var x in ReversedEntities.Keys ) if ( s.Contains( x ) ) sb.Replace( x, ReversedEntities[ x ] );
                        return sb.ToString();
                    }
                case ReplaceType.Both:
                    {
                        //todo: 
                        s = Numeric.Replace( s, RepEvaluator );
                        var sb = new StringBuilder( s );
                        foreach ( var x in ReversedEntities.Keys ) if ( s.Contains( x ) ) sb.Replace( x, ReversedEntities[ x ] );
                        return sb.ToString();
                    }
            }
            return s;
        }
        private static string Rep2( Match m ) {
            var s = m.ToString();
            s = Convert.ToChar( int.Parse( s.Substring( 2, s.Length - 3 ), System.Globalization.NumberStyles.HexNumber ) ).ToString();
            return s;
        }
        private static string Rep( Match m ) {
            var s = m.ToString();
            s = Convert.ToChar( int.Parse( s.Substring( 2, s.Length - 3 ) ) ).ToString();
            return s;
        }
    }
}