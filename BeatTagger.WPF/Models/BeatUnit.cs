using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTagger.WPF.Models
{
    public sealed record BeatUnit
    {
        /// <summary>
        /// 2分音符
        /// </summary>
        public static readonly BeatUnit HalfNotes = new BeatUnit(2);
        /// <summary>
        /// 4分音符
        /// </summary>
        public static readonly BeatUnit QuaterNotes = new BeatUnit(4);
        /// <summary>
        /// 8分音符
        /// </summary>
        public static readonly BeatUnit EighthNotes = new BeatUnit(8);
        /// <summary>
        /// 16分音符
        /// </summary>
        public static readonly BeatUnit SixteenthNotes = new BeatUnit(16);
        /// <summary>
        /// 32分音符
        /// </summary>
        public static readonly BeatUnit ThirtySecondNotes = new BeatUnit(32);
        /// <summary>
        /// 64分音符
        /// </summary>
        public static readonly BeatUnit SixtyFourthNotes = new BeatUnit(64);

        public int Value { get; }

        /// <summary>
        /// ビート単位
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public BeatUnit(int value)
        {
            if (value != 2 &&
                value != 4 &&
                value != 8 &&
                value != 16 &&
                value != 32 &&
                value != 64)
            {
                throw new ArgumentException("対応していないビートの単位です");
            }
            Value = value;
        }
    }
}