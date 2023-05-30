using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.NationalLanguageSupportWrapper.Native;
using static WindowsAPI.NationalLanguageSupportWrapper.Enumerations;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;
using static WindowsAPI.DiagnosticsWrapper.Native.Win32ErrorConstants;
using static WindowsAPI.NationalLanguageSupportWrapper.Callbacks;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSCallbacks;
using System.Configuration;
using WindowsAPI.NationalLanguageSupportWrapper.DataClasses;
using System.IO;
using static WindowsAPI.General.Native.GeneralStructures;
using WindowsAPI.General.Native;
using static WindowsAPI.DiagnosticsWrapper.Native.ErrorHandling.ErrorHandlingFunctions;

namespace WindowsAPI.NationalLanguageSupportWrapper
{
    /// <summary>
    /// Metodi per l'interazione con le funzionalità NLS.
    /// </summary>
    public static class NLS
    {
        /// <summary>
        /// Callback gestito per l'enumerazione delle informazioni sul calendario.
        /// </summary>
        private static CalendarInfoEnumCallback CalendarInfoEnumManagedCallback;

        /// <summary>
        /// Callback gestito per l'enumerazione dei formati data.
        /// </summary>
        private static DateFormatsEnumCallback DateFormatsEnumManagedCallback;

        /// <summary>
        /// Callback gestito per l'enumerazione delle code page.
        /// </summary>
        private static CodePagesEnumCallback CodePagesEnumManagedCallback;

        /// <summary>
        /// Callback gestito per l'enumerazione delle località.
        /// </summary>
        private static LocaleEnumCallback LocaleEnumManagedCallback;

        /// <summary>
        /// Callback gestito per l'enumerazione dei formati tempo.
        /// </summary>
        private static TimeFormatsEnumCallback TimeFormatsEnumManagedCallback;

        /// <summary>
        /// Callback gestito per l'enumerazione dei nomi geografici.
        /// </summary>
        private static GeoNamesEnumCallback GeoNamesEnumManagedCallback;

        /// <summary>
        /// Subset Unicode.
        /// </summary>
        internal static Dictionary<int, Tuple<int[], string>[]> UnicodeSubsetsInfo = new Dictionary<int, Tuple<int[], string>[]>()
        {
            {0, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 0, 127 }, "Basic Latin") } },
            {1, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 128, 255 }, "Latin-1 Supplement") } },
            {2, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 256, 383 }, "Latin Extended-A") } },
            {3, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 384, 591 }, "Latin Extended-B") } },
            {4, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 592, 687 }, "IPA Extension"),
                new Tuple<int[], string>(new int[2] { 7424, 7551}, "Phonetic Extensions"),
                new Tuple<int[], string>(new int[2] { 7552, 7615}, "Phonetic Extensions Supplement")
            }},
            {5, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 688, 767 }, "Spacing Modifier Letters"),
                new Tuple<int[], string>(new int[2] { 42752, 42783 }, "Modifier Tone Letters")
            }},
            {6, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 768, 879 }, "Combining Diacritical Marks"),
                new Tuple<int[], string>(new int[2] { 7616, 7679 }, "Combining Diacritical Marks Supplement")
            }},
            {7, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 880, 1023 }, "Greek and Coptic") } },
            {8, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 11392, 11519 }, "Coptic") } },
            {9, new Tuple<int[], string>[4] {
                new Tuple<int[], string>(new int[2] { 1024, 1279 }, "Cyrillic"),
                new Tuple<int[], string>(new int[2] { 1280, 1327}, "Cyrillic Supplement"),
                new Tuple<int[], string>(new int[2] { 11744, 11775}, "Cyrillic Extended-A"),
                new Tuple<int[], string>(new int[2] { 42560, 42655}, "Cyrillic Extended-B")
            }},
            {10, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 1328, 1423 }, "Armenian") } },
            {11, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 1424, 1535 }, "Hebrew") } },
            {12, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 42240, 42559 }, "Vai") } },
            {13, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 1536, 1791 }, "Arabic"),
                new Tuple<int[], string>(new int[2] { 1872, 1919 }, "Arabic Supplement")
            }},
            {14, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 1984, 2047 }, "NKo") } },
            {15, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 2304, 2431 }, "Devanagari") } },
            {16, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 2432, 2559 }, "Bangla") } },
            {17, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 2560, 2687 }, "Gurmukhi") } },
            {18, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 2688, 2815 }, "Gujarati") } },
            {19, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 2816, 2943 }, "Odia") } },
            {20, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 2944, 3071 }, "Tamil") } },
            {21, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3072, 3199 }, "Telugu") } },
            {22, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3200, 3327 }, "Kannada") } },
            {23, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3328, 3455 }, "Malayalam") } },
            {24, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3584, 3711 }, "Thai") } },
            {25, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3712, 3839 }, "Lao") } },
            {26, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 4256, 4351 }, "Georgian"),
                new Tuple<int[], string>(new int[2] { 11520, 11567 }, "Georgian Supplement")
            }},
            {27, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 6912, 7039 }, "Balinese") } },
            {28, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 4352, 4607 }, "Hangul Jamo") } },
            {29, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 7680, 7935 }, "Latin Extended Additional"),
                new Tuple<int[], string>(new int[2] { 11360, 11391}, "Latin Extended-C"),
                new Tuple<int[], string>(new int[2] { 42784, 43007}, "Latin Extended-D")
            }},
            {30, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 7936, 8191 }, "Greek Extended") } },
            {31, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 8192, 8303 }, "General Punctuation"),
                new Tuple<int[], string>(new int[2] { 11776, 11903 }, "Supplemental Punctuation")
            }},
            {32, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 8304, 8351 }, "Superscripts and Subscripts") } },
            {33, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 8352, 8399 }, "Currency Symbols") } },
            {34, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 8400, 8447 }, "Combining Diacritical Marks For Symbols") } },
            {35, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 8448, 8527 }, "Letterlike Symbols") } },
            {36, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 8528, 8591 }, "Number Forms") } },
            {37, new Tuple<int[], string>[4] {
                new Tuple<int[], string>(new int[2] { 8592, 8703 }, "Arrows"),
                new Tuple<int[], string>(new int[2] { 10224, 10239}, "Supplemental Arrows-A"),
                new Tuple<int[], string>(new int[2] { 10496, 10623}, "Supplemental Arrows-B"),
                new Tuple<int[], string>(new int[2] { 11008, 11263}, "Miscellaneous Symbols and Arrows")
            }},
            {38, new Tuple<int[], string>[4] {
                new Tuple<int[], string>(new int[2] { 8704, 8959 }, "Mathematical Operators"),
                new Tuple<int[], string>(new int[2] { 10176, 10223}, "Miscellaneous Mathematical Symbols-A"),
                new Tuple<int[], string>(new int[2] { 10624, 10751}, "Miscellaneous Mathematical Symbols-B"),
                new Tuple<int[], string>(new int[2] { 10752, 11007}, "Supplemental Mathematical Operators")
            }},
            {39, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 8960, 9215 }, "Miscellaneous Technical") } },
            {40, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9216, 9279 }, "Control Pictures") } },
            {41, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9280, 9311 }, "Optical Character Recognition") } },
            {42, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9312, 9471 }, "Enclosed Alphanumerics") } },
            {43, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9472, 9599 }, "Box Drawing") } },
            {44, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9600, 9631 }, "Block Elements") } },
            {45, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9632, 9727 }, "Geometric Shapes") } },
            {46, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9728, 9983 }, "Miscellaneous Symbols") } },
            {47, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 9984, 10175 }, "Dingbats") } },
            {48, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 12288, 12351 }, "CJK Symbols And Punctuation") } },
            {49, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 12352, 12447 }, "Hiragana") } },
            {50, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 12448, 12543 }, "Katakana"),
                new Tuple<int[], string>(new int[2] { 12784, 12799 }, "Katakana Phonetic Extensions")
            }},
            {51, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 12544, 12591 }, "Bopomofo"),
                new Tuple<int[], string>(new int[2] { 12704, 12735 }, "Bopomofo Extended")
            }},
            {52, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 12592, 12687 }, "Hangul Compatibility Jamo") } },
            {53, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 43072, 43135 }, "Phags-pa") } },
            {54, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 12800, 13055 }, "Enclosed CJK Letters And Months") } },
            {55, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 13056, 13311 }, "CJK Compatibility") } },
            {56, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 44032, 55215 }, "Hangul Syllables") } },
            {57, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 55296, 57343 }, "Non-Plane 0") } },
            {58, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 67840, 67871 }, "Phoenician") } },
            {59, new Tuple<int[], string>[7] {
                new Tuple<int[], string>(new int[2] { 11904, 12031 }, "CJK Radicals Supplement"),
                new Tuple<int[], string>(new int[2] { 12032, 12255}, "Kangxi Radicals"),
                new Tuple<int[], string>(new int[2] { 12272, 12287}, "Ideographic Description Characters"),
                new Tuple<int[], string>(new int[2] { 12688, 12703}, "Kanbun"),
                new Tuple<int[], string>(new int[2] { 13312, 19903}, "CJK Unified Ideographs Extension A"),
                new Tuple<int[], string>(new int[2] { 19968, 40959}, "CJK Unified Ideographs"),
                new Tuple<int[], string>(new int[2] { 131072, 173791}, "CJK Unified Ideographs Extension B"),
            }},
            {60, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 57344, 63743 }, "Private Use Area") } },
            {61, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 12736, 12783 }, "CJK Strokes"),
                new Tuple<int[], string>(new int[2] { 63744, 64255}, "CJK Compatibility Ideographs"),
                new Tuple<int[], string>(new int[2] { 194560, 195103}, "CJK Compatibility Ideographs Supplement")
            }},
            {62, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 64256, 64335 }, "Alphabetic Presentation Forms") } },
            {63, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 64336, 65023 }, "Arabic Presentation Forms-A") } },
            {64, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65056, 65071 }, "Combining Half Marks") } },
            {65, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 65040, 65055 }, "Vertical Forms"),
                new Tuple<int[], string>(new int[2] { 65072, 65103 }, "CJK Compatibility Forms")
            }},
            {66, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65104, 65135 }, "Small Form Variants") } },
            {67, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65136, 65279 }, "Arabic Presentation Forms-B") } },
            {68, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65280, 65519 }, "Halfwidth And Fullwidth Forms") } },
            {69, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65520, 65535 }, "Specials") } },
            {70, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3840, 4095 }, "Tibetan") } },
            {71, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 1792, 1871 }, "Syriac") } },
            {72, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 1920, 1983 }, "Thaana") } },
            {73, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 3456, 3583 }, "Sinhala") } },
            {74, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 4096, 4255 }, "Myanmar") } },
            {75, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 4608, 4991 }, "Ethiopic"),
                new Tuple<int[], string>(new int[2] { 4992, 5023}, "Ethiopic Supplement"),
                new Tuple<int[], string>(new int[2] { 11648, 11743}, "Ethiopic Extended")
            }},
            {76, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 5024, 5119 }, "Cherokee") } },
            {77, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 5120, 5759 }, "Unified Canadian Aboriginal Syllabics") } },
            {78, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 5760, 5791 }, "Ogham") } },
            {79, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 5792, 5887 }, "Runic") } },
            {80, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 6016, 6143 }, "Khmer"),
                new Tuple<int[], string>(new int[2] { 6624, 6655 }, "Khmer Symbols")
            }},
            {81, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 6144, 6319 }, "Mongolian") } },
            {82, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 10240, 10495 }, "Braille Patterns") } },
            {83, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 40960, 42127 }, "Yi Syllables"),
                new Tuple<int[], string>(new int[2] { 42128, 42191 }, "Yi Radicals")
            }},
            {84, new Tuple<int[], string>[4] {
                new Tuple<int[], string>(new int[2] { 5888, 5919 }, "Tagalog"),
                new Tuple<int[], string>(new int[2] { 5920, 5951}, "Hanunoo"),
                new Tuple<int[], string>(new int[2] { 5952, 5983}, "Buhid"),
                new Tuple<int[], string>(new int[2] { 5984, 6015}, "Tagbanwa")
            }},
            {85, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66304, 66351 }, "Old Italic") } },
            {86, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66352, 66383 }, "Gothic") } },
            {87, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66560, 66639 }, "Deseret") } },
            {88, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 118784, 119039 }, "Byzantine Musical Symbols"),
                new Tuple<int[], string>(new int[2] { 119040, 119295}, "Musical Symbols"),
                new Tuple<int[], string>(new int[2] { 119296, 119375}, "Ancient Greek Musical Notation")
            }},
            {89, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 119808, 120831 }, "Mathematical Alphanumeric Symbols") } },
            {90, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 1044480, 1048573 }, "Private Use (plane 15)"),
                new Tuple<int[], string>(new int[2] { 1048576, 1114109 }, "Private Use (plane 16)")
            }},
            {91, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 65024, 65039 }, "Variation Selectors"),
                new Tuple<int[], string>(new int[2] { 917760, 917999 }, "Variation Selectors Supplement")
            }},
            {92, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 917504, 917631 }, "Tags") } },
            {93, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 6400, 6479 }, "Limbu") } },
            {94, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 6480, 6527 }, "Tai Le") } },
            {95, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 6528, 6623 }, "New Tai Lue") } },
            {96, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 6656, 6687 }, "Buginese") } },
            {97, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 11264, 11359 }, "Glagolitic") } },
            {98, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 11568, 11647 }, "Tifinagh") } },
            {99, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 19904, 19967 }, "Yijing Hexagram Symbols") } },
            {100, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 43008, 43055 }, "Syloti Nagri") } },
            {101, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 65536, 65663 }, "Linear B Syllabary"),
                new Tuple<int[], string>(new int[2] { 65664, 65791}, "Linear B Ideograms"),
                new Tuple<int[], string>(new int[2] { 65792, 65855}, "Aegean Numbers")
            }},
            {102, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65856, 65935 }, "Ancient Greek Numbers") } },
            {103, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66432, 66463 }, "Ugaritic") } },
            {104, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66464, 66527 }, "Old Persian") } },
            {105, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66640, 66687 }, "Shavian") } },
            {106, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66688, 66735 }, "Osmanya") } },
            {107, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 67584, 67647 }, "Cypriot Syllabary") } },
            {108, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 68096, 68191 }, "Kharoshthi") } },
            {109, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 119552, 119647 }, "Tai Xuan Jing Symbols") } },
            {110, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 73728, 74751 }, "Cuneiform"),
                new Tuple<int[], string>(new int[2] { 74752, 74879 }, "Cuneiform Numbers and Punctuation")
            }},
            {111, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 119648, 119679 }, "Counting Rod Numerals") } },
            {112, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 7040, 7103 }, "Sundanese") } },
            {113, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 7168, 7247 }, "Lepcha") } },
            {114, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 7248, 7295 }, "Ol Chiki") } },
            {115, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 43136, 43231 }, "Saurashtra") } },
            {116, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 43264, 43311 }, "Kayah Li") } },
            {117, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 43312, 43359 }, "Rejang") } },
            {118, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 43520, 43615 }, "Cham") } },
            {119, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 65936, 65999 }, "Ancient Symbols") } },
            {120, new Tuple<int[], string>[1] { new Tuple<int[], string>(new int[2] { 66000, 66047 }, "Phaistos Disc") } },
            {121, new Tuple<int[], string>[3] {
                new Tuple<int[], string>(new int[2] { 66176, 66207 }, "Lycian"),
                new Tuple<int[], string>(new int[2] { 66208, 66271}, "Carian"),
                new Tuple<int[], string>(new int[2] { 67872, 67903}, "Lydian")
            }},
            {122, new Tuple<int[], string>[2] {
                new Tuple<int[], string>(new int[2] { 126976, 127023 }, "Mahjong Tiles"),
                new Tuple<int[], string>(new int[2] { 127024, 127135 }, "Domino Tiles")
            }},
        };

        /// <summary>
        /// Bitfield code page.
        /// </summary>
        internal static Dictionary<int, Tuple<int, string>> CodePageBitFields = new Dictionary<int, Tuple<int, string>>()
        {
            {0, new Tuple<int, string>(1252, "Latin 1") },
            {1, new Tuple<int, string>(1250, "Latin 2: Central Europe") },
            {2, new Tuple<int, string>(1251, "Cyrillic") },
            {3, new Tuple<int, string>(1253, "Greek") },
            {4, new Tuple<int, string>(1254, "Turkish") },
            {5, new Tuple<int, string>(1255, "Hebrew") },
            {6, new Tuple<int, string>(1256, "Arabic") },
            {7, new Tuple<int, string>(1257, "Baltic") },
            {8, new Tuple<int, string>(1258, "Vietnamese") },
            {16, new Tuple<int, string>(874, "Thai") },
            {17, new Tuple<int, string>(932, "Japanese, Shift-JIS") },
            {18, new Tuple<int, string>(936, "Simplified Chinese (PRC, Singapore)") },
            {19, new Tuple<int, string>(949, "Korean Unified Hangul Code") },
            {20, new Tuple<int, string>(950, "Traditional Chinese (Taiwan; Hong Kong SAR, PRC)") },
            {21, new Tuple<int, string>(1361, "Korean (Johab)") },
            {47, new Tuple<int, string>(1258, "Vietnamese") },
            {48, new Tuple<int, string>(869, "Modern Greek") },
            {49, new Tuple<int, string>(866, "Russian") },
            {50, new Tuple<int, string>(865, "Nordic") },
            {51, new Tuple<int, string>(864, "Arabic") },
            {52, new Tuple<int, string>(863, "Canadia French") },
            {53, new Tuple<int, string>(862, string.Empty) },
            {54, new Tuple<int, string>(861, "Icelandic") },
            {55, new Tuple<int, string>(860, "Portuguese") },
            {56, new Tuple<int, string>(857, "Turkish") },
            {57, new Tuple<int, string>(855, "Cyrillic; primarily Russian") },
            {58, new Tuple<int, string>(852, "Latin 2") },
            {59, new Tuple<int, string>(775, "Baltic") },
            {60, new Tuple<int, string>(737, "Greek; formerly 437G") },
            {61, new Tuple<int, string>(708, "Arabic; ASMO 708") },
            {62, new Tuple<int, string>(850, "Multilingual Latin 1") },
            {63, new Tuple<int, string>(437, "US") },
        };

        /// <summary>
        /// Confronta due stringhe.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="ComparisonOptions">Opzioni di confronto.</param>
        /// <param name="SortingOptions">Opzioni di ordinamento.</param>
        /// <param name="FirstString">Prima stringa da confrontare.</param>
        /// <param name="SecondString">Seconda stringa da confrontare.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>-1 se <paramref name="FirstString"/> ha un valore lessicale minore di <paramref name="SecondString"/>.<br/>
        /// 0 se entrambe le stringhe hanno lo stesso valore<br/>
        /// 1 se <paramref name="FirstString"/> ha un valore lessicale minore di <paramref name="SecondString"/>.</returns>
        /// <remarks>Il primo valore true tra <paramref name="UseInvariantLocale"/>. <paramref name="UseSystemDefaultLocale"/> e <paramref name="UseUserDefaultLocale"/> sarà considerato se <paramref name="LocaleName"/> è nullo.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int CompareStrings(string LocaleName, Enumerations.ComparisonOptions ComparisonOptions, Enumerations.SortingOptions SortingOptions, string FirstString, string SecondString, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(FirstString) || string.IsNullOrWhiteSpace(SecondString))
            {
                throw new ArgumentNullException(string.Empty, "The strings are required.");
            }
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, FirstString, FirstString.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(FirstString));
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, SecondString, SecondString.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(FirstString));
            }
            NLSVERSIONINFO Version = new NLSVERSIONINFO()
            {
                Size = (uint)Marshal.SizeOf(typeof(NLSVERSIONINFO))
            };
            if (!NLSFunctions.GetNLSVersion(SYSNLS_FUNCTION.COMPARE_STRING, LocaleName, ref Version))
            {
                throw new InvalidOperationException("Could not get NLS version.");
            }
            uint Flags = (uint)ComparisonOptions | (uint)SortingOptions;
            ComparisonResult Result = NLSFunctions.CompareString(LocaleName, Flags, FirstString, FirstString.Length, SecondString, SecondString.Length, ref Version, IntPtr.Zero, IntPtr.Zero);
            if (Result != 0)
            {
                return (int)Result - 2;
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != ERROR_SUCCESS)
                {
                    if (ErrorCode is ERROR_INVALID_FLAGS)
                    {
                        throw new ArgumentException("ComparisonOptions or SortingOptions are invalid.");
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
                else
                {
                    return (int)Result - 2;
                }
            }
        }

        /// <summary>
        /// Confronta la rappresentazione binaria di due stringhe.
        /// </summary>
        /// <param name="FirstString">Prima stringa da confrontare.</param>
        /// <param name="SecondString">Seconda stringa da confrontare.</param>
        /// <param name="IgnoreCase">Indica se ignorare la differenza tra maiuscole e minuscole.</param>
        /// <returns>-1 se <paramref name="FirstString"/> ha un valore lessicale minore di <paramref name="SecondString"/>.<br/>
        /// 0 se entrambe le stringhe hanno lo stesso valore<br/>
        /// 1 se <paramref name="FirstString"/> ha un valore lessicale minore di <paramref name="SecondString"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int CompareStringsOrdinal(string FirstString, string SecondString, bool IgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(FirstString) || string.IsNullOrWhiteSpace(SecondString))
            {
                throw new ArgumentNullException(string.Empty, "The strings are required.");
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, FirstString, FirstString.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(FirstString));
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, SecondString, SecondString.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(FirstString));
            }
            ComparisonResult Result = NLSFunctions.CompareStringOrdinal(FirstString, FirstString.Length, SecondString, SecondString.Length, IgnoreCase);
            if (Result != 0)
            {
                return (int)Result - 2;
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != ERROR_SUCCESS)
                {
                    throw new Win32Exception(ErrorCode);
                }
                else
                {
                    return (int)Result - 2;
                }
            }
        }

        /// <summary>
        /// Enumera le informazioni di un calendario.
        /// </summary>
        /// <param name="Callback">Callback che riceve le informazioni.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="Data">Informazione da recuperare.</param>
        /// <param name="Object">Oggetto passato al callback.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void EnumCalendarInfo(CalendarInfoEnumCallback Callback, string LocaleName, Calendar Calendar, Enumerations.CalendarData Data, object Object, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (Calendar != Calendar.All)
            {
                if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
                {
                    throw new InvalidEnumArgumentException("Invalid calendar.");
                }
            }
            if (IsLanguageTag)
            {
                StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                {
                    LocaleName = LocaleNameBuilder.ToString();
                }
                else
                {
                    throw new Win32Exception("Could not convert language tag to locale name.");
                }
            }
            if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
            {
                if (!NLSFunctions.IsValidLocaleName(LocaleName))
                {
                    throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                }
            }
            CalendarInfoEnumManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            CalendarInfoEnumProcedure NativeCallback = new CalendarInfoEnumProcedure(CalendarInfoEnumerationCallback);
            if (Object != null)
            {
                GCHandle ObjectManagedHandle = GCHandle.Alloc(Object);
                IntPtr ObjectHandle = GCHandle.ToIntPtr(ObjectManagedHandle);
                bool Result = NLSFunctions.EnumCalendarInfo(NativeCallback, LocaleName, (CalendarID)Calendar, null, (uint)Data, ObjectHandle);
                ObjectManagedHandle.Free();
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            else
            {
                bool Result = NLSFunctions.EnumCalendarInfo(NativeCallback, LocaleName, (CalendarID)Calendar, null, (uint)Data, IntPtr.Zero);
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
        }

        /// <summary>
        /// Callback per l'enumerazione delle informazioni di un calendario.
        /// </summary>
        /// <param name="Info">Informazione.</param>
        /// <param name="CalID">Calendario.</param>
        /// <param name="Reserved">Riservato.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        private static bool CalendarInfoEnumerationCallback(string Info, CalendarID CalID, string Reserved, IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                GCHandle ObjectHandle = GCHandle.FromIntPtr(lParam);
                return CalendarInfoEnumManagedCallback.Invoke(Info, (Calendar)CalID, ObjectHandle.Target);
            }
            else
            {
                return CalendarInfoEnumManagedCallback.Invoke(Info, (Calendar)CalID, null);
            }
        }

        /// <summary>
        /// Enumera i formati data.
        /// </summary>
        /// <param name="Callback">Callback che riceve le informazioni.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Format">Tipo di formato.</param>
        /// <param name="Object">Oggetto passato al callback.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static void EnumDateFormats(DateFormatsEnumCallback Callback, string LocaleName, Enumerations.DateFormat Format, object Object, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (IsLanguageTag)
            {
                StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                {
                    LocaleName = LocaleNameBuilder.ToString();
                }
                else
                {
                    throw new Win32Exception("Could not convert language tag to locale name.");
                }
            }
            if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
            {
                if (!NLSFunctions.IsValidLocaleName(LocaleName))
                {
                    throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                }
            }
            DateFormatsEnumManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            DateFormatEnumProcedure NativeCallback = new DateFormatEnumProcedure(DateFormatEnumerationCallback);
            if (Object != null)
            {
                GCHandle ObjectManagedHandle = GCHandle.Alloc(Object);
                IntPtr ObjectHandle = GCHandle.ToIntPtr(ObjectManagedHandle);
                bool Result = NLSFunctions.EnumDateFormats(NativeCallback, LocaleName, (uint)Format, ObjectHandle);
                ObjectManagedHandle.Free();
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Unable to access system registry.");
                    }
                }
            }
            else
            {
                bool Result = NLSFunctions.EnumDateFormats(NativeCallback, LocaleName, (uint)Format, IntPtr.Zero);
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Unable to access system registry.");
                    }
                }
            }
        }

        /// <summary>
        /// Callback per l'enumerazione dei formati data.
        /// </summary>
        /// <param name="DateFormat">Formato data.</param>
        /// <param name="CalID">Calendario.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        private static bool DateFormatEnumerationCallback(string DateFormat, CalendarID CalID, IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                GCHandle ObjectHandle = GCHandle.FromIntPtr(lParam);
                return DateFormatsEnumManagedCallback.Invoke(DateFormat, (Calendar)CalID, ObjectHandle.Target);
            }
            else
            {
                return DateFormatsEnumManagedCallback.Invoke(DateFormat, (Calendar)CalID, null);
            }
        }

        /// <summary>
        /// Enumera le code page.
        /// </summary>
        /// <param name="Callback">Callback che riceve le informazioni.</param>
        /// <param name="OnlyInstalledCodePages">Indica se enumerare solo le code pages installate.</param>
        /// <param name="OnlySupportedCodePages">Indica se enumerare solo le code pages supportate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static void EnumerateCodePages(CodePagesEnumCallback Callback, bool OnlyInstalledCodePages, bool OnlySupportedCodePages)
        {
            CodePagesEnumManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            CodePagesEnumProcedure NativeCallback = new CodePagesEnumProcedure(CodePagesEnumerationCallback);
            CodePageType Type = 0;
            if (OnlyInstalledCodePages && !OnlySupportedCodePages)
            {
                Type = CodePageType.CP_INSTALLED;
            }
            if (OnlySupportedCodePages && !OnlyInstalledCodePages)
            {
                Type = CodePageType.CP_SUPPORTED;
            }
            if (OnlyInstalledCodePages && OnlySupportedCodePages)
            {
                throw new ArgumentException("Only installed or supported code pages can be enumerated.", string.Empty);
            }
            bool Result = NLSFunctions.EnumSystemCodePages(NativeCallback, Type);
            if (!Result)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                {
                    throw new Win32Exception(ErrorCode);
                }
                if (ErrorCode is ERROR_BADDB)
                {
                    throw new ConfigurationErrorsException("Unable to access system registry.");
                }
            }
        }

        /// <summary>
        /// Callback per l'enumerazione delle code page.
        /// </summary>
        /// <param name="CodePage">Code page.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        private static bool CodePagesEnumerationCallback(string CodePage)
        {
            return CodePagesEnumManagedCallback.Invoke(CodePage);
        }

        /// <summary>
        /// Enumera le località.
        /// </summary>
        /// <param name="Callback">Callback che riceve le informazioni.</param>
        /// <param name="LocaleInfo">Informazioni località.</param>
        /// <param name="Object">Oggetto passato al callback.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static void EnumLocales(LocaleEnumCallback Callback, LocaleInfo LocaleInfo, object Object)
        {
            LocaleEnumManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            LocaleEnumProcedure NativeCallback = new LocaleEnumProcedure(LocaleEnumerationCallback);
            uint Flags = LocaleInfo != null ? LocaleInfo.ToCompositedValue() : 0;
            if (Object != null)
            {
                GCHandle ObjectManagedHandle = GCHandle.Alloc(Object);
                IntPtr ObjectHandle = GCHandle.ToIntPtr(ObjectManagedHandle);
                bool Result = NLSFunctions.EnumSystemLocales(NativeCallback, Flags, ObjectHandle, IntPtr.Zero);
                ObjectManagedHandle.Free();
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Unable to access system registry.");
                    }
                }
            }
            else
            {
                bool Result = NLSFunctions.EnumSystemLocales(NativeCallback, Flags, IntPtr.Zero, IntPtr.Zero);
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Unable to access system registry.");
                    }
                }
            }
        }

        /// <summary>
        /// Callback per l'enumerazione delle località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Informazioni sulla località.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        private static bool LocaleEnumerationCallback(string LocaleName, uint Flags, IntPtr lParam)
        {
            LocaleInfo Info = new LocaleInfo(LocaleName, Flags);
            if (lParam != IntPtr.Zero)
            {
                GCHandle ObjectHandle = GCHandle.FromIntPtr(lParam);
                return LocaleEnumManagedCallback.Invoke(Info, ObjectHandle.Target);
            }
            else
            {
                return LocaleEnumManagedCallback.Invoke(Info, null);
            }
        }

        /// <summary>
        /// Enumera i formati data.
        /// </summary>
        /// <param name="Callback">Callback che riceve le informazioni.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="UseShortTimeFormat">Indica se usare il formato corso dell'ora.</param>
        /// <param name="Object">Oggetto passato al callback.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static void EnumTimeFormats(TimeFormatsEnumCallback Callback, string LocaleName, bool UseShortTimeFormat, object Object, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (IsLanguageTag)
            {
                StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                {
                    LocaleName = LocaleNameBuilder.ToString();
                }
                else
                {
                    throw new Win32Exception("Could not convert language tag to locale name.");
                }
            }
            if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
            {
                if (!NLSFunctions.IsValidLocaleName(LocaleName))
                {
                    throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                }
            }
            TimeFormatsEnumManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            TimeFormatEnumProcedure NativeCallback = new TimeFormatEnumProcedure(TimeFormatEnumerationCallback);
            NLSEnumerations.TimeFormat Flags = 0;
            if (UseShortTimeFormat)
            {
                Flags = NLSEnumerations.TimeFormat.TIME_NOSECONDS;
            }
            if (Object != null)
            {
                GCHandle ObjectManagedHandle = GCHandle.Alloc(Object);
                IntPtr ObjectHandle = GCHandle.ToIntPtr(ObjectManagedHandle);
                bool Result = NLSFunctions.EnumTimeFormats(NativeCallback, LocaleName, Flags, ObjectHandle);
                ObjectManagedHandle.Free();
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Unable to access system registry.");
                    }
                }
            }
            else
            {
                bool Result = NLSFunctions.EnumTimeFormats(NativeCallback, LocaleName, Flags, IntPtr.Zero);
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Unable to access system registry.");
                    }
                }
            }
        }

        /// <summary>
        /// Callback per l'enumerazione dei formati tempo.
        /// </summary>
        /// <param name="TimeFormatString">Stringa di formato.</param>
        /// <param name="lParam">Valore fornito dal'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        private static bool TimeFormatEnumerationCallback(string TimeFormatString, IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                GCHandle ObjectHandle = GCHandle.FromIntPtr(lParam);
                return TimeFormatsEnumManagedCallback.Invoke(TimeFormatString, ObjectHandle.Target);
            }
            else
            {
                return TimeFormatsEnumManagedCallback.Invoke(TimeFormatString, null);
            }
        }

        /// <summary>
        /// Enumerazione dei nomi geografici.
        /// </summary>
        /// <param name="Class">Classe geografica.</param>
        /// <param name="Callback">Callback che riceve le informazioni.</param>
        /// <param name="Object">Oggetto passato al callback.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void EnumGeoNames(GeoClass Class, GeoNamesEnumCallback Callback, object Object)
        {
            GeoNamesEnumManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            GeoNameEnumProcedure NativeCallback = new GeoNameEnumProcedure(GeoNameEnumerationCallback);
            if (Object != null)
            {
                GCHandle ObjectManagedHandle = GCHandle.Alloc(Object);
                IntPtr ObjectHandle = GCHandle.ToIntPtr(ObjectManagedHandle);
                bool Result = NLSFunctions.EnumSystemGeoNames((GEOCLASS)Class, NativeCallback, ObjectHandle);
                ObjectManagedHandle.Free();
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            else
            {
                bool Result = NLSFunctions.EnumSystemGeoNames((GEOCLASS)Class, NativeCallback, IntPtr.Zero);
                if (!Result)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_FLAGS || ErrorCode is ERROR_INVALID_PARAMETER)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
        }

        /// <summary>
        /// Callback per l'enumerazione di nomi geografici.
        /// </summary>
        /// <param name="GeoName">Nome geografico.</param>
        /// <param name="lParam">Valore passato dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        private static bool GeoNameEnumerationCallback(string GeoName, IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                GCHandle ObjectHandle = GCHandle.FromIntPtr(lParam);
                return GeoNamesEnumManagedCallback.Invoke(GeoName, ObjectHandle.Target);
            }
            else
            {
                return GeoNamesEnumManagedCallback.Invoke(GeoName, null);
            }
        }

        /// <summary>
        /// Trova una stringa in un'altra stringa.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="SearchOptions">Opzioni di ricerca.</param>
        /// <param name="ComparisonOptions">Opzioni di confronto.</param>
        /// <param name="SourceString">Stringa originale.</param>
        /// <param name="StringToFind">Stringa da trovare.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Indice dove inizia <paramref name="StringToFind"/> in <paramref name="SourceString"/>, -1 se non trovata.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int FindString(string LocaleName, Enumerations.SearchOptions? SearchOptions, Enumerations.ComparisonOptions ComparisonOptions, string SourceString, string StringToFind, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (SearchOptions.HasValue)
            {
                if (SearchOptions.Value != Enumerations.SearchOptions.StartsWith && SearchOptions.Value != Enumerations.SearchOptions.EndsWith && SearchOptions.Value != Enumerations.SearchOptions.FromStart && SearchOptions.Value != Enumerations.SearchOptions.FromEnd)
                {
                    throw new ArgumentException("Search option invalid, only one option can be selected.", nameof(SearchOptions));
                }
            }
            if (string.IsNullOrWhiteSpace(SourceString) || string.IsNullOrWhiteSpace(StringToFind))
            {
                throw new ArgumentNullException(string.Empty, "The strings cannot be null or empty.");
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, SourceString, SourceString.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(SourceString));
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, StringToFind, StringToFind.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(StringToFind));
            }
            uint Flags = (uint)ComparisonOptions;
            if (SearchOptions.HasValue)
            {
                Flags |= (uint)SearchOptions.Value;
            }
            int FoundStringIndex = NLSFunctions.FindNLSString(LocaleName, Flags, SourceString, SourceString.Length, StringToFind, StringToFind.Length, out int FoundStringLength, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (FoundStringIndex is -1)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_INVALID_FLAGS)
                {
                    throw new ArgumentException("Invalid comparison options.", nameof(ComparisonOptions));
                }
                if (ErrorCode is ERROR_SUCCESS)
                {
                    return FoundStringIndex;
                }
                throw new Win32Exception(ErrorCode);
            }
            else
            {
                return FoundStringIndex;
            }
        }

        /// <summary>
        /// Trova una stringa in un'altra stringa.
        /// </summary>
        /// <param name="Options">Opzioni di ricerca.</param>
        /// <param name="SourceString">Stringa di origine.</param>
        /// <param name="StringToFind">Stringa da trovare.</param>
        /// <param name="IgnoreCase">Indica se ignorare la differenza tra le lettere maiuscole e minuscole.</param>
        /// <returns>Indice dove inizia <paramref name="StringToFind"/> in <paramref name="SourceString"/>, -1 se non trovata.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int FindStringBinary(Enumerations.SearchOptions Options, string SourceString, string StringToFind, bool IgnoreCase)
        {
            if (Options != Enumerations.SearchOptions.StartsWith && Options != Enumerations.SearchOptions.EndsWith && Options != Enumerations.SearchOptions.FromStart && Options != Enumerations.SearchOptions.FromEnd)
            {
                throw new ArgumentException("Search option invalid, only one option can be selected.", nameof(Options));
            }
            int FoundStringIndex = NLSFunctions.FindStringOrdinal((NLSEnumerations.SearchOptions)Options, SourceString, SourceString.Length, StringToFind, StringToFind.Length, IgnoreCase);
            if (FoundStringIndex is -1)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_SUCCESS)
                {
                    return FoundStringIndex;
                }
                throw new Win32Exception(ErrorCode);
            }
            else
            {
                return FoundStringIndex;
            }
        }

        /// <summary>
        /// Trasforma una stringa.
        /// </summary>
        /// <param name="Options">Opzioni di trasformazione.</param>
        /// <param name="SourceString">Stringa da convertire.</param>
        /// <returns>Stringa convertita.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InsufficientMemoryException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string TransformString(Enumerations.TransformationOptions Options, string SourceString)
        {
            if (Options.HasFlag(Enumerations.TransformationOptions.AccentedCharsToDecomposedChars) && Options.HasFlag(Enumerations.TransformationOptions.AccentedCharsToPrecomposedChars))
            {
                throw new ArgumentException("Option combination invalid.", nameof(Options));
            }
            if (Options.HasFlag(Enumerations.TransformationOptions.LigatureCharsToTwoCharEquivalent) && (Options.HasFlag(Enumerations.TransformationOptions.AccentedCharsToDecomposedChars) || Options.HasFlag(Enumerations.TransformationOptions.AccentedCharsToPrecomposedChars)))
            {
                throw new ArgumentException("Option combination invalid.", nameof(Options));
            }
            if (string.IsNullOrWhiteSpace(SourceString))
            {
                throw new ArgumentNullException(nameof(SourceString), "The string cannot be null or empty.");
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, SourceString, SourceString.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(SourceString));
            }
            int RequiredSize = NLSFunctions.FoldString((NLSEnumerations.TransformationOptions)Options, SourceString, SourceString.Length, null, 0);
            StringBuilder DestinationString = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.FoldString((NLSEnumerations.TransformationOptions)Options, SourceString, SourceString.Length, DestinationString, RequiredSize);
            if (CharCount > 0)
            {
                return DestinationString.ToString();
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_MOD_NOT_FOUND)
                {
                    throw new FileNotFoundException("Module not found.");
                }
                if (ErrorCode is ERROR_OUTOFMEMORY)
                {
                    throw new InsufficientMemoryException("Insufficient memory to complete the operation.");
                }
                if (ErrorCode is ERROR_PROC_NOT_FOUND)
                {
                    throw new MissingMethodException("The required procedure was not found.");
                }
                throw new Win32Exception(ErrorCode);
            }
        }

        /// <summary>
        /// Recupera l'identificatore della code page Windows ANSI per il sistema operativo.
        /// </summary>
        /// <returns>Identificativo della code page.</returns>
        public static int GetAnsiCodePage()
        {
            return (int)NLSFunctions.GetACP();
        }

        /// <summary>
        /// Recupera il calendario alternativo per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Valore di <see cref="Calendar"/> che identifica il calendario alternativo.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Calendar GetAlternateCalendar(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            uint Flags = (uint)NLSEnumerations.CalendarData.CAL_ICALINTVALUE | (uint)CalendarOptions.CAL_RETURN_NUMBER;
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, Flags, null, 0, ValuePointer);
            if (CharCount is 2)
            {
                int Value = Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return (Calendar)Value;
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il valore massimo dell'anno a due cifre per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="IgnoreUserSettings">Indica se recuperare le impostazioni di sistema al posto di quelle dell'utente.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Valore massimo dell'anno a due cifre.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetMaxTwoDigitYearValue(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool IgnoreUserSettings, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = (uint)NLSEnumerations.CalendarData.CAL_ITWODIGITYEARMAX | (uint)CalendarOptions.CAL_RETURN_NUMBER;
            if (IgnoreUserSettings)
            {
                Flags |= (uint)CalendarOptions.CAL_NOUSEROVERRIDE;
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, Flags, null, 0, ValuePointer);
            if (CharCount is 2)
            {
                int Value = Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera gli offset per ogni era.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località di sistema predefinita.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array che contiene l'offset per ogni era.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static int[] GetEraOffsets(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_IYEAROFFSETRANGE, null, 0, IntPtr.Zero);
            StringBuilder EraOffsets = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_IYEAROFFSETRANGE, EraOffsets, RequiredSize, IntPtr.Zero);
            if (CharCount > 0)
            {
                string[] EraOffsetStrings = EraOffsets.ToString().Split('\0');
                int[] EraOffsetValues = new int[EraOffsetStrings.Length];
                for (int i = 0; i < EraOffsetStrings.Length; i++)
                {
                    if (EraOffsetStrings[i] != "\0")
                    {
                        EraOffsetValues[i] = int.Parse(EraOffsetStrings[i]);
                    }
                }
                return EraOffsetValues;
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi abbreviati dei giorni della settimana per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei giorni della settimana.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetAbbreviatedWeekdayNames(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            string[] DayNames = new string[7];
            int RequiredSize;
            int CharCount;
            uint EnumValue = (uint)NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME1;
            for (int i = 0; i < DayNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, null, 0, IntPtr.Zero);
                StringBuilder DayName = new StringBuilder(RequiredSize);
                CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, DayName, RequiredSize, IntPtr.Zero);
                if (CharCount > 0)
                {
                    DayNames[i] = DayName.ToString();
                    EnumValue += 1;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return DayNames;
        }

        /// <summary>
        /// Recupera il nome abbreviato dell'era per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Il nome abbreviato dell'era.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetAbbreviatedEraName(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SABBREVERASTRING, null, 0, IntPtr.Zero);
            StringBuilder EraName = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SABBREVERASTRING, null, 0, IntPtr.Zero);
            if (CharCount > 0)
            {
                return EraName.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi abbreviati dei mesi.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Un array di stringhe con i nomi nativi dei mesi.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetAbbreviatedMonthNames(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            string[] MonthNames = new string[13];
            int RequiredSize;
            int CharCount;
            uint EnumValue = (uint)NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME1;
            for (int i = 0; i < MonthNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, null, 0, IntPtr.Zero);
                StringBuilder MonthName = new StringBuilder(RequiredSize);
                CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, MonthName, RequiredSize, IntPtr.Zero);
                if (CharCount > 0)
                {
                    MonthNames[i] = MonthName.ToString();
                    EnumValue += 1;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return MonthNames;
        }

        /// <summary>
        /// Recupera il nome alternativo di un calendario.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome alternativo del calendario.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetAlternateCalendarName(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SCALNAME, null, 0, IntPtr.Zero);
            StringBuilder CalendarName = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SCALNAME, null, 0, IntPtr.Zero);
            if (CharCount > 0)
            {
                return CalendarName.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi dei giorni della settimana per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei giorni della settimana.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetWeekdayNames(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            string[] DayNames = new string[7];
            int RequiredSize;
            int CharCount;
            uint EnumValue = (uint)NLSEnumerations.CalendarData.CAL_SDAYNAME1;
            for (int i = 0; i < DayNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, null, 0, IntPtr.Zero);
                StringBuilder DayName = new StringBuilder(RequiredSize);
                CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, DayName, RequiredSize, IntPtr.Zero);
                if (CharCount > 0)
                {
                    DayNames[i] = DayName.ToString();
                    EnumValue += 1;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return DayNames;
        }

        /// <summary>
        /// Recupera i nomi delle ere per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Il nome delle ere.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetEraName(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SERASTRING, null, 0, IntPtr.Zero);
            StringBuilder Eras = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SERASTRING, Eras, RequiredSize, IntPtr.Zero);
            if (CharCount > 0)
            {
                return Eras.ToString().Split('\0');
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il formato data lunga.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Formato data lunga.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetLongDateFormat(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SLONGDATE, null, 0, IntPtr.Zero);
            StringBuilder LongDateFormat = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SLONGDATE, null, 0, IntPtr.Zero);
            if (CharCount > 0)
            {
                return LongDateFormat.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il formato mese/giorno.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Formato mese/giorno.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetMonthDayFormat(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SMONTHDAY, null, 0, IntPtr.Zero);
            StringBuilder MonthDayFormat = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SMONTHDAY, null, 0, IntPtr.Zero);
            if (CharCount > 0)
            {
                return MonthDayFormat.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi dei mesi.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Un array di stringhe con i nomi nativi dei mesi.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetMonthNames(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            string[] MonthNames = new string[13];
            int RequiredSize;
            int CharCount;
            uint EnumValue = (uint)NLSEnumerations.CalendarData.CAL_SMONTHNAME1;
            for (int i = 0; i < MonthNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, null, 0, IntPtr.Zero);
                StringBuilder MonthName = new StringBuilder(RequiredSize);
                CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, MonthName, RequiredSize, IntPtr.Zero);
                if (CharCount > 0)
                {
                    MonthNames[i] = MonthName.ToString();
                    EnumValue += 1;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return MonthNames;
        }

        /// <summary>
        /// Recupera il formato data corta.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Formato data lunga.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetShortDateFormat(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SSHORTDATE, null, 0, IntPtr.Zero);
            StringBuilder ShortDateFormat = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SSHORTDATE, null, 0, IntPtr.Zero);
            if (CharCount > 0)
            {
                return ShortDateFormat.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi più corti dei giorni della settimana per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei giorni della settimana.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetShortestWeekdayNames(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            }
            string[] DayNames = new string[7];
            int RequiredSize;
            int CharCount;
            uint EnumValue = (uint)NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME1;
            for (int i = 0; i < DayNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, null, 0, IntPtr.Zero);
                StringBuilder DayName = new StringBuilder(RequiredSize);
                CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, EnumValue, DayName, RequiredSize, IntPtr.Zero);
                if (CharCount > 0)
                {
                    DayNames[i] = DayName.ToString();
                    EnumValue += 1;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return DayNames;
        }

        /// <summary>
        /// Recupera il formato anno/mese.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Formato anno/mese.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetYearMonthFormat(string LocaleName, Calendar Calendar, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if ((int)Calendar < 1 || ((int)Calendar > 13 && (int)Calendar <= 22) || (int)Calendar > 23)
            {
                throw new InvalidEnumArgumentException("Invalid calendar.");
            };
            int RequiredSize = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SYEARMONTH, null, 0, IntPtr.Zero);
            StringBuilder YearMonthFormat = new StringBuilder(RequiredSize);
            int CharCount = NLSFunctions.GetCalendarInfo(LocaleName, (CalendarID)Calendar, null, (uint)NLSEnumerations.CalendarData.CAL_SYEARMONTH, null, 0, IntPtr.Zero);
            if (CharCount > 0)
            {
                return YearMonthFormat.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni su una code page.
        /// </summary>
        /// <param name="CodePage">Code page.</param>
        /// <returns>Istanza di <see cref="CodePageInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static CodePageInfo GetCodePageInfo(int CodePage)
        {
            if (CodePage is (int)Enumerations.CodePageDefaultValue.Symbol || CodePage is (int)Enumerations.CodePageDefaultValue.UTF7 || CodePage is (int)Enumerations.CodePageDefaultValue.UTF8)
            {
                throw new ArgumentException("Invalid code page.", nameof(CodePage));
            }
            else
            {
                if (CodePage != (int)Enumerations.CodePageDefaultValue.WindowsANSICodePage && CodePage != (int)Enumerations.CodePageDefaultValue.MacintoshCodePage && CodePage != (int)Enumerations.CodePageDefaultValue.OEMCodePage && CodePage != (int)Enumerations.CodePageDefaultValue.CurrentThreadANSICodePage)
                {
                    if (!NLSFunctions.IsValidCodePage((uint)CodePage))
                    {
                        throw new ArgumentException("Invalid code page.", nameof(CodePage));
                    }
                }
            }
            return !NLSFunctions.GetCPInfo((uint)CodePage, 0, out CPINFO Info)
                ? throw new Win32Exception(Marshal.GetLastWin32Error())
                : new CodePageInfo(Info);
        }

        /// <summary>
        /// Formatta una stringa come un valore monetario.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="StringToFormat">Stringa da formattare.</param>
        /// <param name="Format">Istanza di <see cref="CurrencyFormat"/> con le informazioni di formato.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa formattata.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string FormatStringAsCurrency(string LocaleName, string StringToFormat, CurrencyFormat Format, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(StringToFormat))
            {
                throw new ArgumentException("The string cannot be null or empty.", nameof(StringToFormat));
            }
            else
            {
                bool IsDotPresent = false;
                for (int i = 0; i < StringToFormat.Length; i++)
                {
                    if (i == 0)
                    {
                        if (!char.IsDigit(StringToFormat[i]) && StringToFormat[0] != '-')
                        {
                            throw new FormatException("The string is incorrectly formatted.");
                        }
                    }
                    else
                    {
                        if (!char.IsDigit(StringToFormat[i]))
                        {
                            if (StringToFormat[i] == '.')
                            {
                                if (!IsDotPresent)
                                {
                                    IsDotPresent = true;
                                }
                                else
                                {
                                    throw new FormatException("The string is incorrectly formatted.");
                                }
                            }
                            else
                            {
                                throw new FormatException("The string is incorrectly formatted.");
                            }
                        }
                    }
                }
            }
            IntPtr FormatStructurePointer = IntPtr.Zero;
            if (Format != null)
            {
                CURRENCYFMT FormatStructure = Format.ToStructure();
                FormatStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(FormatStructure));
                Marshal.StructureToPtr(FormatStructure, FormatStructurePointer, false);
            }
            int RequiredSize = NLSFunctions.GetCurrencyFormat(LocaleName, IgnoreUserPreferences ? 0 : LOCALE_NOUSEROVERRIDE, StringToFormat, FormatStructurePointer, null, 0);
            if (RequiredSize is 0)
            {
                if (FormatStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(FormatStructurePointer);
                }
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                StringBuilder FormattedString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetCurrencyFormat(LocaleName, IgnoreUserPreferences ? 0 : LOCALE_NOUSEROVERRIDE, StringToFormat, FormatStructurePointer, FormattedString, RequiredSize);
                if (FormatStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(FormatStructurePointer);
                }
                if (CharCount is 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    return FormattedString.ToString();
                }
            }
        }

        /// <summary>
        /// Formatta una stringa come una data.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="Date">Data da formattare</param>
        /// <param name="Format">Formato data da usare per generare la stringa.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>La stringa formattata.</returns>
        /// <remarks>Se <paramref name="FormatString"/> non è nullo, <paramref name="Format"/> deve essere nullo.<br/><br/>
        /// Per utilizzare la data locale di sistema, impostare <paramref name="Date"/> a un valore nullo.</remarks>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string FormatStringAsDate(string LocaleName, string FormatString, DateTime? Date, Enumerations.DateFormat? Format, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            IntPtr DateStructurePointer = IntPtr.Zero;
            if (Date.HasValue)
            {
                SYSTEMTIME DateStructure = new SYSTEMTIME()
                {
                    Day = (ushort)Date.Value.Day,
                    DayOfWeek = (GeneralEnumerations.DayOfWeek)Date.Value.DayOfWeek,
                    Month = (GeneralEnumerations.Month)Date.Value.Month,
                    Year = (ushort)Date.Value.Year
                };
                DateStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(DateStructure));
                Marshal.StructureToPtr(DateStructure, DateStructurePointer, false);
            }
            uint Flags = 0;
            if (string.IsNullOrWhiteSpace(FormatString))
            {
                if (Format.HasValue)
                {
                    Flags = (uint)Format.Value;
                    if (IgnoreUserPreferences)
                    {
                        Flags |= LOCALE_NOUSEROVERRIDE;
                    }
                }
                else
                {
                    throw new ArgumentNullException(string.Empty, "FormatString or Format must have a value.");
                }
            }
            else
            {
                if (Format.HasValue)
                {
                    throw new ArgumentException("If FormatString has a value, Format must be null.", nameof(Format));
                }
            }
            int RequiredSize = NLSFunctions.GetDateFormat(LocaleName, Flags, DateStructurePointer, FormatString, null, 0, null);
            if (RequiredSize is 0)
            {
                if (DateStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(DateStructurePointer);
                }
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                StringBuilder FormattedString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetDateFormat(LocaleName, Flags, DateStructurePointer, FormatString, FormattedString, RequiredSize, null);
                if (DateStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(DateStructurePointer);
                }
                if (CharCount is 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    return FormattedString.ToString();
                }
            }
        }

        /// <summary>
        /// Formatta una stringa come una durata.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="Duration">Durata da formattare</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>La stringa formattata.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string FormatStringAsDuration(string LocaleName, TimeSpan Duration, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale, string FormatString = null)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            SYSTEMTIME DurationStructure = new SYSTEMTIME()
            {
                Hour = (ushort)Duration.Hours,
                Minute = (ushort)Duration.Minutes,
                Second = (ushort)Duration.Seconds,
                Milliseconds = (ushort)Duration.Milliseconds
            };
            IntPtr DurationStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(DurationStructure));
            Marshal.StructureToPtr(DurationStructure, DurationStructurePointer, false);
            uint Flags = 0;
            if (FormatString is null)
            {
                if (IgnoreUserPreferences)
                {
                    Flags = LOCALE_NOUSEROVERRIDE;
                }
            }
            int RequiredSize = NLSFunctions.GetDurationFormat(LocaleName, Flags, DurationStructurePointer, 0, FormatString, null, 0);
            if (RequiredSize is 0)
            {
                Marshal.FreeHGlobal(DurationStructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                StringBuilder FormattedString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetDurationFormat(LocaleName, Flags, DurationStructurePointer, 0, FormatString, FormattedString, RequiredSize);
                Marshal.FreeHGlobal(DurationStructurePointer);
                if (CharCount is 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    return FormattedString.ToString();
                }
            }
        }

        /// <summary>
        /// Recupera le informazioni sulla relazione tra la copertura dei caratteri necessari a supportare una località è i contenuti di un font.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Istanza di <see cref="LocaleSignature"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static LocaleSignature GetLocaleSignature(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_FONTSIGNATURE;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            int StructureSize = Marshal.SizeOf(typeof(LOCALESIGNATURE));
            IntPtr LocaleSignatureStructurePointer = Marshal.AllocHGlobal(StructureSize);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, LocaleSignatureStructurePointer, StructureSize / UnicodeEncoding.CharSize);
            if (CharCount is 0)
            {
                Marshal.FreeHGlobal(LocaleSignatureStructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                LOCALESIGNATURE FontSignatureStructure = (LOCALESIGNATURE)Marshal.PtrToStructure(LocaleSignatureStructurePointer, typeof(LOCALESIGNATURE));
                Marshal.FreeHGlobal(LocaleSignatureStructurePointer);
                return new LocaleSignature(LocaleName, FontSignatureStructure);
            }
        }

        /// <summary>
        /// Recupera il prefisso telefonico internazionale per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Prefisso telefonico internazionale.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetCountryDialingCode(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IDIALINGCODE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                int DialingCode = Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return DialingCode;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera l'identificativo della code page ANSI predefinita per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Code page ANSI predefinita.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetDefaultAnsiCodePage(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IDEFAULTANSICODEPAGE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                int Codepage = Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return Codepage;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera l'identificativo della code page OEM predefinita per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Code page ANSI predefinita.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetDefaultOEMCodePage(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IDEFAULTCODEPAGE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                int Codepage = Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return Codepage;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera l'identificativo della code page EBCDIC predefinita per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Code page ANSI predefinita.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetDefaultEBCDICCodePage(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IDEFAULTEBCDICCODEPAGE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                int Codepage = Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return Codepage;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera l'identificativo della code page Macintosh predefinita per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Code page ANSI predefinita.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetDefaultMacintoshCodePage(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IDEFAULTMACCODEPAGE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                int Codepage = Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return Codepage;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il formato di un valore percentuale negativo per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Formato di un valore paercentuale negativo.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string GetNegativePercentageFormat(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_INEGATIVEPERCENT | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                int Format = Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                switch (Format)
                {
                    case 0:
                        return "-# %";
                    case 1:
                        return "-#%";
                    case 2:
                        return "-%#";
                    case 3:
                        return "%-#";
                    case 4:
                        return "%#-";
                    case 5:
                        return "#-%";
                    case 6:
                        return "#%-";
                    case 7:
                        return "-% #";
                    case 8:
                        return "# %-";
                    case 9:
                        return "% #-";
                    case 10:
                        return "% -#";
                    case 11:
                        return "#- %";
                    default:
                        return null;
                }
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la posizione del segno in un valore monetario negativo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Valore dell'enumerazione <see cref="NegativeCurrencySignPosition"/> che indica la posizione del segno.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static NegativeCurrencySignPosition GetNegativeCurrencySignPosition(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_INEGSIGNPOSN | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                NegativeSignPosition Position = (NegativeSignPosition)Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return (NegativeCurrencySignPosition)Position;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Indica se il segno è separato dal numero in un valore monetario negativo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>true se il segno è separato dal valore, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsSignSeparatedInNegativeCurrency(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_INEGSEPBYSPACE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                bool Separated = Convert.ToBoolean(Marshal.ReadInt32(DataPointer));
                Marshal.FreeHGlobal(DataPointer);
                return Separated;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Indica se il simbolo della valuta precede il numero in un valore monetario negativo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>true se il simbolo precede il numero, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool DoesSymbolPrecedeValueInNegativeCurrency(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_INEGSYMPRECEDES | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                bool PrecedesValue = Convert.ToBoolean(Marshal.ReadInt32(DataPointer));
                Marshal.FreeHGlobal(DataPointer);
                return PrecedesValue;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il calendario facoltativo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Valore dell'enumerazione <see cref="Calendar"/> che indica il calendario facoltativo per la località.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Calendar GetOptionalCalendar(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IOPTIONALCALENDAR | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                Calendar Calendar = (Calendar)Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return (Calendar)Calendar;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il formato di un valore percentuale positivo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Valore dell'enumerazione <see cref="PositivePercentageFormat"/> che indica il formato.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static PositivePercentageFormat GetPositivePercentageFormat(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IPOSITIVEPERCENT | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                PositivePercentageFormat Format = (PositivePercentageFormat)Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return (PositivePercentageFormat)Format;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la posizione del segno in un valore monetario positivo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Valore dell'enumerazione <see cref="NegativeCurrencySignPosition"/> che indica la posizione del segno.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static NegativeCurrencySignPosition GetPositiveCurrencySignPosition(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IPOSSIGNPOSN | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                NegativeSignPosition Position = (NegativeSignPosition)Marshal.ReadInt32(DataPointer);
                Marshal.FreeHGlobal(DataPointer);
                return (NegativeCurrencySignPosition)Position;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Indica se il segno è separato dal numero in un valore monetario positivo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>true se il segno è separato dal valore, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsSignSeparatedInPositiveCurrency(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IPOSSESPBYSPACE | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                bool Separated = Convert.ToBoolean(Marshal.ReadInt32(DataPointer));
                Marshal.FreeHGlobal(DataPointer);
                return Separated;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Indica se il simbolo della valuta precede il numero in un valore monetario positivo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>true se il simbolo precede il valore, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool DoesSymbolPrecedeValueInPositiveCurrency(string LocaleName, bool IsLanguageTag, bool IgnoreUserPreferences, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            uint Flags = LOCALE_IPOSSYMPRECEDES | LOCALE_RETURN_NUMBER;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            IntPtr DataPointer = Marshal.AllocHGlobal(4);
            int CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, DataPointer, 2);
            if (CharCount > 0)
            {
                bool PrecedesValue = Convert.ToBoolean(Marshal.ReadInt32(DataPointer));
                Marshal.FreeHGlobal(DataPointer);
                return PrecedesValue;
            }
            else
            {
                Marshal.FreeHGlobal(DataPointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi abbreviati dei giorni della settimana per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei giorni della settimana.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetLocaleAbbreviatedWeekdayNames(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            string[] DayNames = new string[7];
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint EnumValue = LOCALE_SABBREVDAYNAME1;
            if (IgnoreUserPreferences)
            {
                EnumValue |= LOCALE_NOUSEROVERRIDE;
            }
            for (int i = 0; i < DayNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, IntPtr.Zero, 0);
                Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
                CharCount = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, Buffer, RequiredSize);
                if (CharCount > 0)
                {
                    DayNames[i] = Marshal.PtrToStringUni(Buffer);
                    EnumValue += 1;
                    Marshal.FreeHGlobal(Buffer);
                }
                else
                {
                    Marshal.FreeHGlobal(Buffer);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return DayNames;
        }

        /// <summary>
        /// Recupera i nomi nativi abbreviati dei mesi per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei mesi.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetLocaleAbbreviatedMonthNames(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            string[] MonthNames = new string[13];
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint EnumValue = LOCALE_SABBREVMONTHNAME1;
            if (IgnoreUserPreferences)
            {
                EnumValue |= LOCALE_NOUSEROVERRIDE;
            }
            for (int i = 0; i < MonthNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, IntPtr.Zero, 0);
                Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
                CharCount = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, Buffer, RequiredSize);
                if (CharCount > 0)
                {
                    MonthNames[i] = Marshal.PtrToStringUni(Buffer);
                    EnumValue += 1;
                    Marshal.FreeHGlobal(Buffer);
                }
                else
                {
                    Marshal.FreeHGlobal(Buffer);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return MonthNames;
        }

        /// <summary>
        /// Recupera il nome nativo abbreviato del paese/della regione.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome nativo abbreviato del paese/della regione.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetAbbreviatedCountryName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SABBREVCTRYNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string CountryName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return CountryName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome nativo abbreviato della lingua.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome nativo abbreviato del paese/della regione.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetAbbreviatedLanguageName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SABBREVLANGNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string LangName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return LangName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi dei giorni della settimana per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei giorni della settimana.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetWeekdayNames(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            string[] DayNames = new string[7];
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint EnumValue = LOCALE_SDAYNAME1;
            if (IgnoreUserPreferences)
            {
                EnumValue |= LOCALE_NOUSEROVERRIDE;
            }
            for (int i = 0; i < DayNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, IntPtr.Zero, 0);
                Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
                CharCount = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, Buffer, RequiredSize);
                if (CharCount > 0)
                {
                    DayNames[i] = Marshal.PtrToStringUni(Buffer);
                    EnumValue += 1;
                    Marshal.FreeHGlobal(Buffer);
                }
                else
                {
                    Marshal.FreeHGlobal(Buffer);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return DayNames;
        }

        /// <summary>
        /// Recupera la stringa di formato per una durata.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa di formato.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetDurationFormatString(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SDURATION;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string FormatString = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return FormatString;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome inglese della valuta per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome inglese della valuta.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetEnglishCurrencyName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SENGCURRNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string LangName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return LangName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome inglese per un paese/regione.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome inglese del paese/della regione.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetEnglishCountryName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SENGLISHCOUNTRYNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string CountryName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return CountryName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome comune inglese per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome comune inglese per la località.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetEnglishDisplayName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SENGLISHDISPLAYNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string DisplayName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return DisplayName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome inglese per una lingua.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome inglese della lingua.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetEnglishLanguageName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SENGLISHLANGUAGENAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string LanguageName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return LanguageName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il simbolo internazionale della valuta.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Simbolo della valuta.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetInternationalCurrencySymbol(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SINTLSYMBOL;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Symbol = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Symbol;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome del paese/regione basato sullo standard ISO 3166.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome del paese/regione.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetISO3166CountryName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SISO3166CTRYNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome del paese/regione basato sullo standard ISO 3166 a tre lettere.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome del paese/regione.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetISO3166ThreeLetterCountryName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SISO3166CTRYNAME2;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome della lingua in base allo standard ISO 639.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome della lingua.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetISO639LanguageName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SISO639LANGNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome della lingua basato sullo standard ISO 639-2 a tre lettere.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome della lingua.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetISO639ThreeLetterLanguageName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SISO639LANGNAME2;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi dei mesi per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei mesi.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetLocaleMonthNames(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            string[] MonthNames = new string[13];
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint EnumValue = LOCALE_SMONTHNAME1;
            if (IgnoreUserPreferences)
            {
                EnumValue |= LOCALE_NOUSEROVERRIDE;
            }
            for (int i = 0; i < MonthNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, IntPtr.Zero, 0);
                Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
                CharCount = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, Buffer, RequiredSize);
                if (CharCount > 0)
                {
                    MonthNames[i] = Marshal.PtrToStringUni(Buffer);
                    EnumValue += 1;
                    Marshal.FreeHGlobal(Buffer);
                }
                else
                {
                    Marshal.FreeHGlobal(Buffer);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return MonthNames;
        }

        /// <summary>
        /// Recupera il nome della località in base alle convenzioni IETF BCP 47.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome località in base alle convenzioni IETF BCP 47.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetLocaleNameIETFBCP47(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la stringa per Not a number.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa per Not a number.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetNaNString(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNAN;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string NANString = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return NANString;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome nativo del paese/della regione.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome nativo del paese/della regione.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetNativeCountryName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNATIVECOUNTRYNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome nativo della valuta.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome nativo della valuta.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetNativeCurrencyName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNATIVECURRNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome comune della località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome comune della località.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetLocaleDisplayName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNATIVEDISPLAYNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome nativo della lingua.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome nativo della lingua.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetNativeLanguageName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNATIVELANGUAGENAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Name = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Name;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la stringa per "infinito negativo".
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa per "infinito negativo".</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetNegativeInfinityString(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SNEGINFINITY;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string NegativeInfinityString = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return NegativeInfinityString;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il simbolo per indicare la percentuale.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Simbolo per indicare la percentuale.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetPercentageSymbol(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SPERCENT;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string Symbol = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return Symbol;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la stringa per "infinito positivo".
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa per "infinito positivo".</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetPositiveInfinityString(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SPOSINFINITY;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string PositiveInfinityString = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return PositiveInfinityString;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la lista degli script in base allo standard ISO 15924.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di script che usa la notazione dello standard ISO 15924.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetScriptsList(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint EnumValue = LOCALE_SSCRIPTS;
            if (IgnoreUserPreferences)
            {
                EnumValue |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string ScriptsList = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return ScriptsList.Split(';');
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la stringa più corta possibile per l'indicatore AM.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa per l'indicatore AM.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetShortestAMString(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SSHORTESTAM;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string AMString = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return AMString;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera i nomi nativi più corti possibile dei giorni della settimana.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Array di stringhe contenente i nomi dei giorni della settimana.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetShortestDayNames(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            string[] DayNames = new string[7];
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint EnumValue = LOCALE_SSHORTESTDAYNAME1;
            if (IgnoreUserPreferences)
            {
                EnumValue |= LOCALE_NOUSEROVERRIDE;
            }
            for (int i = 0; i < DayNames.Length; i++)
            {
                RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, IntPtr.Zero, 0);
                Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
                CharCount = NLSFunctions.GetLocaleInfo(LocaleName, EnumValue, Buffer, RequiredSize);
                if (CharCount > 0)
                {
                    DayNames[i] = Marshal.PtrToStringUni(Buffer);
                    EnumValue += 1;
                    Marshal.FreeHGlobal(Buffer);
                }
                else
                {
                    Marshal.FreeHGlobal(Buffer);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return DayNames;
        }

        /// <summary>
        /// Recupera la stringa più corta possibile per l'indicatore PM.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa per l'indicatore PM.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetShortestPMString(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SSHORTESTPM;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string PMString = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return PMString;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome completo localizzato dell'ordinamento per la località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Nome completo localizzato dell'ordinamento.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string GetSortName(string LocaleName, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            int RequiredSize;
            int CharCount;
            IntPtr Buffer;
            uint Flags = LOCALE_SSORTNAME;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            RequiredSize = NLSFunctions.GetLocaleInfo(LocaleName, Flags, IntPtr.Zero, 0);
            Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
            CharCount = NLSFunctions.GetLocaleInfo(LocaleName, Flags, Buffer, RequiredSize);
            if (CharCount > 0)
            {
                string SortName = Marshal.PtrToStringUni(Buffer);
                Marshal.FreeHGlobal(Buffer);
                return SortName;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la versione delle funzionalità NLS.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Istanza di <see cref="NLSVersionInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static NLSVersionInfo GetNLSVersion(string LocaleName, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            NLSVERSIONINFO VersionInfo = new NLSVERSIONINFO()
            {
                Size = (uint)Marshal.SizeOf(typeof(NLSVERSIONINFO))
            };
            if (NLSFunctions.GetNLSVersion(SYSNLS_FUNCTION.COMPARE_STRING, LocaleName, ref VersionInfo))
            {
                return new NLSVersionInfo(LocaleName, VersionInfo);
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Formatta una stringa come un numero.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="StringToFormat">Stringa da formattare.</param>
        /// <param name="Format">Istanza di <see cref="NumberFormat"/> con le informazioni di formato.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>Stringa formattata.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="InsufficientMemoryException"></exception>
        public static string FormatStringAsNumber(string LocaleName, string StringToFormat, NumberFormat Format, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(StringToFormat))
            {
                throw new ArgumentException("The string cannot be null or empty.", nameof(StringToFormat));
            }
            else
            {
                bool IsDotPresent = false;
                for (int i = 0; i < StringToFormat.Length; i++)
                {
                    if (i == 0)
                    {
                        if (!char.IsDigit(StringToFormat[i]) && StringToFormat[0] != '-')
                        {
                            throw new FormatException("The string is incorrectly formatted.");
                        }
                    }
                    else
                    {
                        if (!char.IsDigit(StringToFormat[i]))
                        {
                            if (StringToFormat[i] == '.')
                            {
                                if (!IsDotPresent)
                                {
                                    IsDotPresent = true;
                                }
                                else
                                {
                                    throw new FormatException("The string is incorrectly formatted.");
                                }
                            }
                            else
                            {
                                throw new FormatException("The string is incorrectly formatted.");
                            }
                        }
                    }
                }
            }
            IntPtr FormatStructurePointer = IntPtr.Zero;
            if (Format != null)
            {
                NUMBERFMT FormatStructure = Format.ToStructure();
                FormatStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(FormatStructure));
                Marshal.StructureToPtr(FormatStructure, FormatStructurePointer, false);
            }
            int RequiredSize = NLSFunctions.GetNumberFormat(LocaleName, IgnoreUserPreferences ? 0 : LOCALE_NOUSEROVERRIDE, StringToFormat, FormatStructurePointer, null, 0);
            if (RequiredSize is 0)
            {
                if (FormatStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(FormatStructurePointer);
                }
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                StringBuilder FormattedString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetNumberFormat(LocaleName, IgnoreUserPreferences ? 0 : LOCALE_NOUSEROVERRIDE, StringToFormat, FormatStructurePointer, FormattedString, RequiredSize);
                if (FormatStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(FormatStructurePointer);
                }
                if (CharCount is 0)
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_OUTOFMEMORY)
                    {
                        throw new InsufficientMemoryException("Insufficient memory to complete the operation.");
                    }
                    throw new Win32Exception(ErrorCode);
                }
                else
                {
                    return FormattedString.ToString();
                }
            }
        }

        /// <summary>
        /// Recupera la code page OEM per il sistema operativo.
        /// </summary>
        /// <returns>Code page OEM.</returns>
        public static int GetOEMCodePage()
        {
            return (int)NLSFunctions.GetOEMCP();
        }

        /// <summary>
        /// Recupera gli script presenti in una stringa.
        /// </summary>
        /// <param name="StringToAnalyze">Stringa da analizzare.</param>
        /// <param name="IncludeInheritedAndCommonScripts">Indica se includere gli script ereditati (Qaii) e quelli comuni (Zyyy) nell'elenco.</param>
        /// <returns>Gli script rilevati nella stringa.</returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string[] GetStringScripts(string StringToAnalyze, bool IncludeInheritedAndCommonScripts)
        {
            uint Flags = IncludeInheritedAndCommonScripts ? (uint)GSS_ALLOW_INHERITED_COMMON : 0;
            int RequiredSize = NLSFunctions.GetStringScripts(Flags, StringToAnalyze, StringToAnalyze.Length, null, 0);
            if (RequiredSize == 0)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_BADDB)
                {
                    throw new ConfigurationErrorsException("The function could not access the data.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
            else
            {
                StringBuilder Scripts = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetStringScripts(Flags, StringToAnalyze, StringToAnalyze.Length, Scripts, RequiredSize);
                if (CharCount > 0)
                {
                    return Scripts.ToString().Split(';').Where((value) => value != string.Empty).ToArray();
                }
                else
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("The function could not access the data.");
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
        }

        /// <summary>
        /// Recupera informazioni di tipo sui caratteri di una stringa.
        /// </summary>
        /// <param name="StringToAnalyze">Stringa da analizzare.</param>
        /// <returns>Un array di istanze di <see cref="CharacterTypeInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static CharacterTypeInfo[] GetStringCharsTypeInfo(string StringToAnalyze)
        {
            if (string.IsNullOrWhiteSpace(StringToAnalyze))
            {
                throw new ArgumentNullException(nameof(StringToAnalyze), "The string cannot be null.");
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, StringToAnalyze, StringToAnalyze.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(StringToAnalyze));
            }
            CharacterTypeInfo[] TypeInfoArray = new CharacterTypeInfo[StringToAnalyze.Length];
            IntPtr Buffer = Marshal.AllocHGlobal(2 * StringToAnalyze.Length);
            if (NLSFunctions.GetStringType(0, CharTypeInfo.CT_CTYPE1, StringToAnalyze, StringToAnalyze.Length, Buffer))
            {
                ushort[] TypeInfo = UtilityMethods.ReadUnmanagedArray<ushort>(Buffer, StringToAnalyze.Length);
                Marshal.FreeHGlobal(Buffer);
                for (int i = 0; i < TypeInfo.Length; i++)
                {
                    TypeInfoArray[i] = new CharacterTypeInfo(StringToAnalyze[i], (CharacterTypes)TypeInfo[i]);
                }
                return TypeInfoArray;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sul layout dei caratteri di una stringa.
        /// </summary>
        /// <param name="StringToAnalyze">Stringa da analizzare.</param>
        /// <returns>Un array di valori dell'enumerazione <see cref="CharacterLayout"/> con indica le informazioni di layout per ogni carattere.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static CharacterLayout[] GetStringCharsLayoutInfo(string StringToAnalyze)
        {
            if (string.IsNullOrWhiteSpace(StringToAnalyze))
            {
                throw new ArgumentNullException(nameof(StringToAnalyze), "The string cannot be null.");
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, StringToAnalyze, StringToAnalyze.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(StringToAnalyze));
            }
            CharacterLayout[] LayoutInfoArray = new CharacterLayout[StringToAnalyze.Length];
            IntPtr Buffer = Marshal.AllocHGlobal(2 * StringToAnalyze.Length);
            if (NLSFunctions.GetStringType(0, CharTypeInfo.CT_CTYPE2, StringToAnalyze, StringToAnalyze.Length, Buffer))
            {
                ushort[] TypeInfo = UtilityMethods.ReadUnmanagedArray<ushort>(Buffer, StringToAnalyze.Length);
                Marshal.FreeHGlobal(Buffer);
                for (int i = 0; i < TypeInfo.Length; i++)
                {
                    LayoutInfoArray[i] = (CharacterLayout)TypeInfo[i];
                }
                return LayoutInfoArray;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sull'elaborazione del testo per i caratteri di una stringa.
        /// </summary>
        /// <param name="StringToAnalyze">Stringa da analizzare.</param>
        /// <returns>Un array di istanze di <see cref="CharacterTextProcessingInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static CharacterTextProcessingInfo[] GetStringCharsTextProcessingInfo(string StringToAnalyze)
        {
            if (string.IsNullOrWhiteSpace(StringToAnalyze))
            {
                throw new ArgumentNullException(nameof(StringToAnalyze), "The string cannot be null.");
            }
            if (!NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, IntPtr.Zero, StringToAnalyze, StringToAnalyze.Length))
            {
                throw new ArgumentException("The string is not defined in NLS.", nameof(StringToAnalyze));
            }
            CharacterTextProcessingInfo[] TextProcessingInfoArray = new CharacterTextProcessingInfo[StringToAnalyze.Length];
            IntPtr Buffer = Marshal.AllocHGlobal(2 * StringToAnalyze.Length);
            if (NLSFunctions.GetStringType(0, CharTypeInfo.CT_CTYPE1, StringToAnalyze, StringToAnalyze.Length, Buffer))
            {
                ushort[] TextProcessingInfo = UtilityMethods.ReadUnmanagedArray<ushort>(Buffer, StringToAnalyze.Length);
                Marshal.FreeHGlobal(Buffer);
                for (int i = 0; i < TextProcessingInfo.Length; i++)
                {
                    TextProcessingInfoArray[i] = new CharacterTextProcessingInfo(StringToAnalyze[i], (TextProcessing)TextProcessingInfo[i]);
                }
                return TextProcessingInfoArray;
            }
            else
            {
                Marshal.FreeHGlobal(Buffer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera l'identificativo della lingua predefinita di sistema.
        /// </summary>
        /// <returns>L'identificativo della lingua.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static short GetSystemDefaultLanguageID()
        {
            return (short)NLSFunctions.GetSystemDefaultLangID();
        }

        /// <summary>
        /// Recupera il nome della località predefinita di sistema.
        /// </summary>
        /// <returns>Il nome della località.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static string GetSystemDefaultLocaleName()
        {
            StringBuilder LocaleName = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
            if (NLSFunctions.GetSystemDefaultLocaleName(LocaleName, LOCALE_NAME_MAX_LENGTH) != 0)
            {
                return LocaleName.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il nome località del thread chiamante.
        /// </summary>
        /// <returns>Nome della località del thread.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static string GetThreadLocaleName()
        {
            uint ThreadLocale = NLSFunctions.GetThreadLocale();
            StringBuilder LocaleName = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
            if (NLSFunctions.LCIDToLocaleName(ThreadLocale, LocaleName, LOCALE_NAME_MAX_LENGTH, 0) != 0)
            {
                return LocaleName.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Formatta una stringa come un'ora.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="Time">Ora da formattare</param>
        /// <param name="Format">Formato ora da usare per generare la stringa.</param>
        /// <param name="IgnoreUserPreferences">Indica se ignorare le preferenze dell'utente.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <returns>La stringa formattata.</returns>
        /// <remarks>Se <paramref name="FormatString"/> non è nullo, <paramref name="Format"/> deve essere nullo.<br/><br/>
        /// Per utilizzare la data locale di sistema, impostare <paramref name="Time"/> a un valore nullo.</remarks>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string FormatStringAsTime(string LocaleName, string FormatString, DateTime? Time, Enumerations.TimeFormat Format, bool IgnoreUserPreferences, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale)
        {
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            IntPtr TimeStructurePointer = IntPtr.Zero;
            if (Time.HasValue)
            {
                SYSTEMTIME DateStructure = new SYSTEMTIME()
                {
                    Day = (ushort)Time.Value.Day,
                    DayOfWeek = (GeneralEnumerations.DayOfWeek)Time.Value.DayOfWeek,
                    Month = (GeneralEnumerations.Month)Time.Value.Month,
                    Year = (ushort)Time.Value.Year
                };
                TimeStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(DateStructure));
                Marshal.StructureToPtr(DateStructure, TimeStructurePointer, false);
            }
            uint Flags = (uint)Format;
            if (IgnoreUserPreferences)
            {
                Flags |= LOCALE_NOUSEROVERRIDE;
            }
            int RequiredSize = NLSFunctions.GetTimeFormat(LocaleName, Flags, TimeStructurePointer, FormatString, null, 0);
            if (RequiredSize is 0)
            {
                if (TimeStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(TimeStructurePointer);
                }
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                StringBuilder FormattedString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetTimeFormat(LocaleName, Flags, TimeStructurePointer, FormatString, FormattedString, RequiredSize);
                if (TimeStructurePointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(TimeStructurePointer);
                }
                if (CharCount is 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    return FormattedString.ToString();
                }
            }
        }

        /// <summary>
        /// Recupera il codice a due lettere ISO 3166-1 o il codice numerico UN M.49 per la località geografica dell'utente.
        /// </summary>
        /// <returns>Codice a due lettere ISO 3166-1 o il codice numerico UN M.49.</returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string GetUserDefaultGeoName()
        {
            int RequiredSize = NLSFunctions.GetUserDefaultGeoName(null, 0);
            if (RequiredSize !=  0)
            {
                StringBuilder GeoName = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.GetUserDefaultGeoName(GeoName, RequiredSize);
                if (CharCount != 0)
                {
                    return GeoName.ToString();
                }
                else
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_BADDB)
                    {
                        throw new ConfigurationErrorsException("Could not read data from the system registry.");
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_BADDB)
                {
                    throw new ConfigurationErrorsException("Could not read data from the system registry.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Recupera l'identificativo della lingua predefinita dell'utente.
        /// </summary>
        /// <returns>L'identificativo della lingua.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static short GetUserDefaultLanguageID()
        {
            return (short)NLSFunctions.GetUserDefaultLangID();
        }

        /// <summary>
        /// Recupera il nome della località predefinita dell'utente.
        /// </summary>
        /// <returns>Il nome della località.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static string GetUserDefaultLocaleName()
        {
            StringBuilder LocaleName = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
            if (NLSFunctions.GetUserDefaultLocaleName(LocaleName, LOCALE_NAME_MAX_LENGTH) != 0)
            {
                return LocaleName.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Converte un IDN o un'altra etichetta internazionalizzata in una rappresentazione Unicode della stringa ASCII che rappresenta il nome in base alla sintassi di codifica Punycode.
        /// </summary>
        /// <param name="Options">Opzioni di conversione.</param>
        /// <param name="UnicodeSourceString">Stringa originale.</param>
        /// <returns>Stringa risultato della conversione.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string IDNToAscii(IDNConversionOptions Options, string UnicodeSourceString)
        {
            if (string.IsNullOrWhiteSpace(UnicodeSourceString))
            {
                throw new ArgumentNullException(nameof(UnicodeSourceString), "The string cannot be null or empty.");
            }
            int RequiredSize = NLSFunctions.IdnToAscii((IdnConversionOptions)Options, UnicodeSourceString, UnicodeSourceString.Length, null, 0);
            if (RequiredSize != 0)
            {
                StringBuilder AsciiString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.IdnToAscii((IdnConversionOptions)Options, UnicodeSourceString, UnicodeSourceString.Length, AsciiString, RequiredSize);
                if (CharCount != 0)
                {
                    return AsciiString.ToString();
                }
                else
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_NAME)
                    {
                        throw new FormatException("The syntax of UnicodeSourceString is invalid.");
                    }
                    else if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                    {
                        throw new FormatException("Invalid Unicode found in UnicodeSourceString.");
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_INVALID_NAME)
                {
                    throw new FormatException("The syntax of UnicodeSourceString is invalid.");
                }
                else if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                {
                    throw new FormatException("Invalid Unicode found in UnicodeSourceString.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Converte un IDN o un'altra etichetta internazionalizzata nella forma Nameprep specificata da RFC 3491 di Network Working Group.
        /// </summary>
        /// <param name="Options">Opzioni di conversione.</param>
        /// <param name="UnicodeSourceString">Stringa originale.</param>
        /// <returns>Stringa risultato della conversione.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string IDNToNameprepUnicode(IDNConversionOptions Options, string UnicodeSourceString)
        {
            if (string.IsNullOrWhiteSpace(UnicodeSourceString))
            {
                throw new ArgumentNullException(nameof(UnicodeSourceString), "The string cannot be null or empty.");
            }
            int RequiredSize = NLSFunctions.IdnToNameprepUnicode((IdnConversionOptions)Options, UnicodeSourceString, UnicodeSourceString.Length, null, 0);
            if (RequiredSize != 0)
            {
                StringBuilder NameprepString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.IdnToNameprepUnicode((IdnConversionOptions)Options, UnicodeSourceString, UnicodeSourceString.Length, NameprepString, RequiredSize);
                if (CharCount != 0)
                {
                    return NameprepString.ToString();
                }
                else
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_NAME)
                    {
                        throw new FormatException("The syntax of UnicodeSourceString is invalid.");
                    }
                    else if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                    {
                        throw new FormatException("Invalid Unicode found in UnicodeSourceString.");
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_INVALID_NAME)
                {
                    throw new FormatException("The syntax of UnicodeSourceString is invalid.");
                }
                else if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                {
                    throw new FormatException("Invalid Unicode found in UnicodeSourceString.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Converte un IDN o un'altra etichetta internazionalizzata codificata con Punycode in una stringa Unicode.
        /// </summary>
        /// <param name="Options">Opzioni di conversione.</param>
        /// <param name="AsciiSourceString">Stringa originale.</param>
        /// <returns>Stringa risultato della conversione.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string IDNToUnicode(IDNConversionOptions Options, string AsciiSourceString)
        {
            if (string.IsNullOrWhiteSpace(AsciiSourceString))
            {
                throw new ArgumentNullException(nameof(AsciiSourceString), "The string cannot be null or empty.");
            }
            int RequiredSize = NLSFunctions.IdnToUnicode((IdnConversionOptions)Options, AsciiSourceString, AsciiSourceString.Length, null, 0);
            if (RequiredSize != 0)
            {
                StringBuilder UnicodeString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.IdnToUnicode((IdnConversionOptions)Options, AsciiSourceString, AsciiSourceString.Length, UnicodeString, RequiredSize);
                if (CharCount != 0)
                {
                    return UnicodeString.ToString();
                }
                else
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode is ERROR_INVALID_NAME)
                    {
                        throw new FormatException("The syntax of AsciiSourceString is invalid.");
                    }
                    else if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                    {
                        throw new FormatException("Invalid Unicode found in AsciiSourceString.");
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_INVALID_NAME)
                {
                    throw new FormatException("The syntax of UnicodeSourceString is invalid.");
                }
                else if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                {
                    throw new FormatException("Invalid Unicode found in UnicodeSourceString.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Determina se una stringa è definita in NLS.
        /// </summary>
        /// <param name="Version">Versione di NLS, può essere nullo.</param>
        /// <param name="StringToAnalyze">Stringa da verificare.</param>
        /// <returns>true se la stringa è definita in NLS, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsNLSDefinedString(NLSVersionInfo Version, string StringToAnalyze)
        {
            if (string.IsNullOrWhiteSpace(StringToAnalyze)) 
            {
                throw new ArgumentNullException(nameof(StringToAnalyze), "The string cannot be null or empty.");
            }
            IntPtr VersionStructurePointer = IntPtr.Zero;
            if (Version != null)
            {
                VersionStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NLSVERSIONINFO)));
                NLSVERSIONINFO VersionInfo = Version.ToStructure();
                Marshal.StructureToPtr(VersionInfo, VersionStructurePointer, false);
            }
            bool Result = NLSFunctions.IsNLSDefinedString(SYSNLS_FUNCTION.COMPARE_STRING, 0, VersionStructurePointer, StringToAnalyze, StringToAnalyze.Length);
            if (!Result)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != ERROR_SUCCESS)
                {
                    throw new Win32Exception(ErrorCode);
                }
                else
                {
                    return Result;
                }
            }
            else
            {
                return Result;
            }
        }

        /// <summary>
        /// Determina se una stringa è normalizzata secondo Unicode 4.0 TR#15.
        /// </summary>
        /// <param name="NormalizationForm">Forma di normalizzazione.</param>
        /// <param name="StringToTest">Stringa da verificare.</param>
        /// <returns>true se la stringa è normalizzata, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsNormalizedString(Enumerations.NormalizationForm NormalizationForm, string StringToTest)
        {
            if (string.IsNullOrWhiteSpace(StringToTest))
            {
                throw new ArgumentNullException(nameof(StringToTest), "The string cannot be null or empty.");
            }
            SetLastError(ERROR_SUCCESS);
            bool Result = NLSFunctions.IsNormalizedString((NORM_FORM)NormalizationForm, StringToTest, StringToTest.Length);
            if (!Result)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != ERROR_SUCCESS)
                {
                    if (ErrorCode is ERROR_NO_UNICODE_TRANSLATION)
                    {
                        throw new FormatException("Invalid Unicode in StringToTest");
                    }
                    throw new Win32Exception(ErrorCode);
                }
                else
                {
                    return Result;
                }
            }
            else
            {
                return Result;
            }
        }

        /// <summary>
        /// Converte un ID località in un nome località.
        /// </summary>
        /// <param name="LCID">ID località.</param>
        /// <param name="AllowNeutralNames">Indica se permettere la restituzione di nomi neutrali.</param>
        /// <returns>Nome località risultato della conversione.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string LCIDToLocaleName(int LCID, bool AllowNeutralNames)
        {
            if (LCID < 0)
            {
                throw new ArgumentException("Invalid LCID.", nameof(LCID));
            }
            uint Flags = AllowNeutralNames ? (uint)LOCALE_ALLOW_NEUTRAL_NAMES : 0;
            StringBuilder LocaleName = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
            if (NLSFunctions.LCIDToLocaleName((uint)LCID, LocaleName, LOCALE_NAME_MAX_LENGTH, Flags) != 0)
            {
                return LocaleName.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Converte un nome località in un ID località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="AllowNeutralNames">Indica se permettere la restituzione di nomi neutrali.</param>
        /// <returns>Nome località risultato della conversione.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int LCIDToLocaleName(string LocaleName, bool AllowNeutralNames)
        {
            if (string.IsNullOrWhiteSpace(LocaleName) || !NLSFunctions.IsValidLocaleName(LocaleName))
            {
                throw new ArgumentException("Invalid locale name.", nameof(LocaleName));
            }
            uint Flags = AllowNeutralNames ? (uint)LOCALE_ALLOW_NEUTRAL_NAMES : 0;
            int LCID = (int)NLSFunctions.LocaleNameToLCID(LocaleName, Flags);
            if (LCID is 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return LCID;
        }

        /// <summary>
        /// Trasforma una stringa seguendo le regole indicate.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="SourceString">Stringa originale.</param>
        /// <param name="MappingOptions">Opzioni di mappatura.</param>
        /// <param name="ComparisonOptions">Opzioni di confronto.</param>
        /// <param name="SortingOptions">Opzioni di ordinamento.</param>
        /// <param name="IsLanguageTag">Indica se <paramref name="LocaleName"/> è un tag lingua.</param>
        /// <param name="UseInvariantLocale">Indica se usare la località invariante.</param>
        /// <param name="UseSystemDefaultLocale">Indica se usare la località predefinita di sistema.</param>
        /// <param name="UseUserDefaultLocale">Indica se usare la località predefinita dell'utente.</param>
        /// <param name="Version">Versione di NLS.</param>
        /// <returns>La stringa trasformata, la chiave di ordinamento oppure l'hash.</returns>
        /// <remarks>Se <paramref name="MappingOptions"/> è uguale a <see cref="Enumerations.MappingOptions.GenerateSortKey"/>, il metodo restituisce un array di byte che rappresenta la chiave di ordinamento.<br/>
        /// Se <paramref name="MappingOptions"/> include <see cref="Enumerations.MappingOptions.GenerateHash"/>, il metodo restituisce un intero a 32 bit.<br/>
        /// In tutti gli altri casi, il metodo restituisce una stringa.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static object MapString(string LocaleName, string SourceString, Enumerations.MappingOptions MappingOptions, Enumerations.ComparisonOptions? ComparisonOptions, Enumerations.SortingOptions? SortingOptions, bool IsLanguageTag, bool UseInvariantLocale, bool UseSystemDefaultLocale, bool UseUserDefaultLocale, NLSVersionInfo Version = null)
        {
            if (string.IsNullOrWhiteSpace(SourceString))
            {
                throw new ArgumentNullException(nameof(SourceString), "The string cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(LocaleName))
            {
                if (UseInvariantLocale)
                {
                    LocaleName = LOCALE_NAME_INVARIANT;
                }
                else if (UseSystemDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_SYSTEM_DEFAULT;
                }
                else if (UseUserDefaultLocale)
                {
                    LocaleName = LOCALE_NAME_USER_DEFAULT;
                }
            }
            else
            {
                if (IsLanguageTag)
                {
                    StringBuilder LocaleNameBuilder = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
                    if (NLSFunctions.ResolveLocaleName(LocaleName, LocaleNameBuilder, LOCALE_NAME_MAX_LENGTH) != 0)
                    {
                        LocaleName = LocaleNameBuilder.ToString();
                    }
                    else
                    {
                        throw new Win32Exception("Could not convert language tag to locale name.");
                    }
                }
                if (LocaleName != LOCALE_NAME_INVARIANT && LocaleName != LOCALE_NAME_SYSTEM_DEFAULT && LocaleName != LOCALE_NAME_USER_DEFAULT)
                {
                    if (!NLSFunctions.IsValidLocaleName(LocaleName))
                    {
                        throw new ArgumentException("Locale name invalid.", nameof(LocaleName));
                    }
                }
            }
            if (ComparisonOptions.HasValue)
            {
                if (SortingOptions.HasValue && MappingOptions != Enumerations.MappingOptions.GenerateSortKey)
                {
                    throw new InvalidEnumArgumentException("Invalid mapping options, only " + Enumerations.MappingOptions.GenerateSortKey.ToString() + " can be used when SortOptions has a value");
                }
                else
                {
                    if (ComparisonOptions.Value.HasFlag(Enumerations.ComparisonOptions.IgnoreNonSpacing) || ComparisonOptions.Value.HasFlag(Enumerations.ComparisonOptions.IgnoreSymbols))
                    {
                        if (MappingOptions != Enumerations.MappingOptions.None)
                        {
                            Enumerations.MappingOptions CombinedValue = Enumerations.MappingOptions.GenerateSortKey | Enumerations.MappingOptions.UseByteReversal;
                            if (MappingOptions != Enumerations.MappingOptions.GenerateSortKey && MappingOptions != Enumerations.MappingOptions.UseByteReversal && MappingOptions != CombinedValue)
                            {
                                throw new InvalidEnumArgumentException("Invalid comparison options.");
                            }
                        }
                    }
                }
            }
            else
            {
                if (MappingOptions.HasFlag(Enumerations.MappingOptions.UseWideChars) && MappingOptions.HasFlag(Enumerations.MappingOptions.UseNarrowChars))
                {
                    throw new InvalidEnumArgumentException("Invalid mapping options.");
                }
                if (MappingOptions.HasFlag(Enumerations.MappingOptions.MapKatakanaToHiragana) && MappingOptions.HasFlag(Enumerations.MappingOptions.MapHiraganaToKatakana))
                {
                    throw new InvalidEnumArgumentException("Invalid mapping options.");
                }
                if (MappingOptions.HasFlag(Enumerations.MappingOptions.UseLinguisticCasing))
                {
                    if (!MappingOptions.HasFlag(Enumerations.MappingOptions.MapToLowercase) && !MappingOptions.HasFlag(Enumerations.MappingOptions.MapToUppercase))
                    {
                        throw new InvalidEnumArgumentException("Invalid mapping options.");
                    }
                }
                if (MappingOptions.HasFlag(Enumerations.MappingOptions.MapTraditionalChineseToSimplifiedChinese) && MappingOptions.HasFlag(Enumerations.MappingOptions.MapSimplifiedChineseToTraditionalChinese))
                {
                    throw new InvalidEnumArgumentException("Invalid mapping options.");
                }
            }
            NLSVERSIONINFO VersionInfo = Version is null ? new NLSVERSIONINFO() : Version.ToStructure();
            if (VersionInfo.Size == 0)
            {
                VersionInfo.Size = (uint)Marshal.SizeOf(typeof(NLSVERSIONINFO));
            }
            uint Options = (uint)MappingOptions;
            if (ComparisonOptions.HasValue)
            {
                Options |= (uint)ComparisonOptions.Value;
            }
            if (SortingOptions.HasValue)
            {
                Options |= (uint)SortingOptions.Value;
            }
            int RequiredSize = NLSFunctions.LCMapString(LocaleName, Options, SourceString, SourceString.Length, IntPtr.Zero, 0, ref VersionInfo, IntPtr.Zero, IntPtr.Zero);
            if (RequiredSize == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                IntPtr Buffer;
                if (MappingOptions is Enumerations.MappingOptions.GenerateSortKey)
                {
                    Buffer = Marshal.AllocHGlobal(RequiredSize);
                    int ByteCount = NLSFunctions.LCMapString(LocaleName, Options, SourceString, SourceString.Length, Buffer, RequiredSize, ref VersionInfo, IntPtr.Zero, IntPtr.Zero);
                    if (ByteCount == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    else
                    {
                        byte[] SortKey = new byte[ByteCount];
                        Marshal.Copy(Buffer, SortKey, 0, ByteCount);
                        Marshal.FreeHGlobal(Buffer);
                        return SortKey;
                    }
                }
                if (MappingOptions.HasFlag(Enumerations.MappingOptions.GenerateHash))
                {
                    Buffer = Marshal.AllocHGlobal(4);
                    int CharCount = NLSFunctions.LCMapString(LocaleName, Options, SourceString, SourceString.Length, Buffer, RequiredSize, ref VersionInfo, IntPtr.Zero, IntPtr.Zero);
                    if (CharCount == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    else
                    {
                        int Hash = Marshal.ReadInt32(Buffer);
                        Marshal.FreeHGlobal(Buffer);
                        return Hash;
                    }
                }
                else
                {
                    Buffer = Marshal.AllocHGlobal(RequiredSize * UnicodeEncoding.CharSize);
                    int CharCount = NLSFunctions.LCMapString(LocaleName, Options, SourceString, SourceString.Length, Buffer, RequiredSize, ref VersionInfo, IntPtr.Zero, IntPtr.Zero);
                    if (CharCount == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    else
                    {
                        string TransformedString = Marshal.PtrToStringUni(Buffer, RequiredSize);
                        Marshal.FreeHGlobal(Buffer);
                        return TransformedString;
                    }
                }
            }
        }
        
        /// <summary>
        /// Normalizza una stringa in base a Unicode 4.0 TR#15.
        /// </summary>
        /// <param name="NormalizationForm">Forma di normalizzazione.</param>
        /// <param name="SourceString">Stringa originale.</param>
        /// <returns>Stringa normalizzata.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string NormalizeString(Enumerations.NormalizationForm NormalizationForm, string SourceString)
        {
            if (string.IsNullOrWhiteSpace(SourceString))
            {
                throw new ArgumentNullException(nameof(SourceString), "The string cannot be null or empty.");
            }
            int RequiredSize = NLSFunctions.NormalizeString((NORM_FORM)NormalizationForm, SourceString, SourceString.Length, null, 0);
            if (RequiredSize > 0)
            {
                StringBuilder NormalizedString = new StringBuilder(RequiredSize);
                int CharCount = NLSFunctions.NormalizeString((NORM_FORM)NormalizationForm, SourceString, SourceString.Length, NormalizedString, RequiredSize);
                if (CharCount < 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    return NormalizedString.ToString();
                }
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Imposta il valore massimo per l'anno a due cifre per un calendario.
        /// </summary>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="MaxValue">Nuovo valore massimo.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetTwoDigitYearMax(Calendar Calendar, int MaxValue)
        {
            if (MaxValue < 0)
            {
                throw new ArgumentException("The value cannot be less than 0.", nameof(MaxValue));
            }
            string Value = MaxValue.ToString();
            if (!NLSFunctions.SetCalendarInfo(0, (CalendarID)Calendar, NLSEnumerations.CalendarData.CAL_ITWODIGITYEARMAX, Value))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Cambia il tipo di calendario corrente.
        /// </summary>
        /// <param name="Calendar">Nuovo tipo di calendario.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetCurrentCalendarType(Calendar Calendar)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_ICALENDARTYPE, ((int)Calendar).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta il numero di cifre decimali per il formato monetario locale.
        /// </summary>
        /// <param name="Count">Nuovo numero di cifre decimali.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetFractionalDigitsCountForMonetaryValues(int Count)
        {
            if (Count < 0 || Count > 9) 
            {
                throw new ArgumentException("Invalid value.", nameof(Count));
            }
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_ICURRDIGITS, Count.ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta la posizione del simbolo della valuta in un valore monetario positivo.
        /// </summary>
        /// <param name="Position">Nuova posizione del simbolo.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetPositiveCurrencySymbolPosition(PositiveCurrencySymbolPosition Position)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_ICURRENCY, ((int)Position).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta il numero di cifre decimali.
        /// </summary>
        /// <param name="Count">Nuovo numero di cifre decimali.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetFractionalDigitsCounts(int Count)
        {
            if (Count < 0 || Count > 9)
            {
                throw new ArgumentException("Invalid value.", nameof(Count));
            }
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_IDIGITS, Count.ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta il comportamento di sostituzione delle cifre.
        /// </summary>
        /// <param name="Behavior">Nuovo comportamento.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetDigitSubstitutionBehavior(DigitSubstitution Behavior)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_IDIGITSUBSTITUTION, ((int)Behavior).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta il primo giorno della settimana.
        /// </summary>
        /// <param name="FirstDay">Primo giorno della settimana.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetFirstDayOfWeek(Enumerations.FirstDayOfWeek FirstDay)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_IFIRSTDAYOFWEEK, ((int)FirstDay).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta la prima settimana dell'anno.
        /// </summary>
        /// <param name="FirstWeek">Prima settimana dell'anno.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetFirstWeekOfYear(Enumerations.FirstWeekOfYear FirstWeek)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_IFIRSTWEEKOFYEAR, ((int)FirstWeek).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta la presenza o assenza di zeri iniziali nei campi decimali.
        /// </summary>
        /// <param name="IncludeLeadingZeroes">Indica se includere o meno gli zeri iniziali.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetIfIncludeLeadingZeroesInDecimalFields(bool IncludeLeadingZeroes)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_ILZERO, Convert.ToInt32(IncludeLeadingZeroes).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Imposta la prima settimana dell'anno.
        /// </summary>
        /// <param name="System">Prima settimana dell'anno.</param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetMeasurementSystem(Enumerations.MeasurementSystem System)
        {
            if (!NLSFunctions.SetLocaleInfo(0, LOCALE_IFIRSTWEEKOFYEAR, ((int)System).ToString()))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode is ERROR_ACCESS_DISABLED_BY_POLICY)
                {
                    throw new UnauthorizedAccessException("Operation forbidden by group policy.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }
    }
}