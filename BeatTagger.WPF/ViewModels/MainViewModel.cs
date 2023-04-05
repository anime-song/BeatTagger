using BeatTagger.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTagger.WPF.Views
{
    public sealed class MainViewModel
    {
        public TimeLine TimeLine { get; }

        public MainViewModel()
        {
            TimeLine = new TimeLine();

            TimeLine.AddMeasure(new TimeSignature(7, BeatUnit.QuaterNotes), 120);
            TimeLine.AddMeasure(new TimeSignature(7, BeatUnit.EighthNotes), 120);
            TimeLine.AddMeasure(new TimeSignature(4, BeatUnit.QuaterNotes), 120);
        }
    }
}