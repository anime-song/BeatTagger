using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTagger.WPF.Models
{
    /// <summary>
    /// タイムライン
    /// </summary>
    public sealed class TimeLine
    {
        public ReactiveCollection<Measure> Measures { get; } = new ReactiveCollection<Measure>();
        public ReactiveProperty<int> WidthPerSecond { get; } = new ReactiveProperty<int>(100);

        private int index = 1;

        public TimeLine()
        {
        }

        public void AddMeasure(TimeSignature timeSignature, float bpm)
        {
            var measure = new Measure(index, timeSignature, bpm, WidthPerSecond);
            Measures.Add(measure);

            index++;
        }
    }
}