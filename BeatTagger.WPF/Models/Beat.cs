using Reactive.Bindings;

namespace BeatTagger.WPF.Models
{
    /// <summary>
    /// ビート
    /// </summary>
    public sealed class Beat
    {
        public ReactiveProperty<int> Index { get; } = new ReactiveProperty<int>();

        /// <summary>
        /// このビートの位置 (0 ~ 1)
        /// </summary>
        public ReactiveProperty<float> Position { get; } = new ReactiveProperty<float>();

        public Beat(float position, int index)
        {
            Index.Value = index;
            Position.Value = position;
        }
    }
}