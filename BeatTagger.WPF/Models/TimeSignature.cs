using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTagger.WPF.Models
{
    /// <summary>
    /// 拍子
    /// </summary>
    public sealed record TimeSignature
    {
        /// <summary>
        /// 一小節の拍数
        /// </summary>
        public int BeatsPerMeasure { get; }

        /// <summary>
        /// ビートの単位
        /// </summary>
        public BeatUnit BeatUnit { get; }

        public TimeSignature(int beatsPerMeasure, BeatUnit beatUnit)
        {
            BeatsPerMeasure = beatsPerMeasure;
            BeatUnit = beatUnit;
        }
    }
}