using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTagger.WPF.Models
{
    /// <summary>
    /// 小節
    /// </summary>
    public sealed class Measure
    {
        /// <summary>
        /// 拍子
        /// </summary>
        public ReactiveProperty<TimeSignature> TimeSignature { get; } = new ReactiveProperty<TimeSignature>();

        /// <summary>
        /// BPM
        /// </summary>
        public ReactiveProperty<float> BPM { get; } = new ReactiveProperty<float>();

        /// <summary>
        /// ビート
        /// </summary>
        public ReadOnlyReactiveCollection<Beat> Beats { get; }

        /// <summary>
        /// 一小節の長さ（秒）
        /// </summary>
        public ReadOnlyReactivePropertySlim<float> Duration { get; }

        /// <summary>
        /// 何小節目か
        /// </summary>
        public ReactiveProperty<int> Index { get; } = new ReactiveProperty<int>();

        public ReactiveProperty<bool> IsChanged { get; } = new ReactiveProperty<bool>();

        public ReadOnlyReactivePropertySlim<int> BaseWidth { get; }

        public ReadOnlyReactivePropertySlim<float> Width { get; }

        public Measure(int index, TimeSignature timeSignature, float bpm, ReactiveProperty<int> baseWidth)
        {
            Index.Value = index;
            TimeSignature.Value = timeSignature;
            BPM.Value = bpm;
            BaseWidth = baseWidth.ToReadOnlyReactivePropertySlim();
            Duration = Observable
                .Merge(BPM.PropertyChangedAsObservable(), TimeSignature.PropertyChangedAsObservable())
                .Select(_ =>
                {
                    float secondsPerBeat = 60.0f / BPM.Value;
                    float secondsPerBeatInBeatUnit = secondsPerBeat * (BeatUnit.QuaterNotes.Value / (float)TimeSignature.Value.BeatUnit.Value);
                    return TimeSignature.Value.BeatsPerMeasure * secondsPerBeatInBeatUnit;
                })
                .ToReadOnlyReactivePropertySlim();

            Width = Observable
                .Merge(BaseWidth.PropertyChangedAsObservable(), Duration.PropertyChangedAsObservable())
                .Select(_ => BaseWidth.Value * Duration.Value)
                .ToReadOnlyReactivePropertySlim();

            Beats = Observable
                .Merge(TimeSignature.PropertyChangedAsObservable(), BPM.PropertyChangedAsObservable())
                .SelectMany(_ =>
                {
                    var beats = new List<Beat>();
                    float secondsPerBeat = 60.0f / BPM.Value;
                    float secondsPerBeatInBeatUnit = secondsPerBeat * (BeatUnit.QuaterNotes.Value / (float)TimeSignature.Value.BeatUnit.Value);
                    for (int i = 0; i < TimeSignature.Value.BeatsPerMeasure; ++i)
                    {
                        beats.Add(new Beat(secondsPerBeatInBeatUnit * i));
                    }
                    return beats;
                })
                .ToReadOnlyReactiveCollection();
        }
    }
}