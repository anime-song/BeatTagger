using Reactive.Bindings;

namespace BeatTagger.WPF.Models
{
    /// <summary>
    /// ビート
    /// </summary>
    public sealed class Beat
    {
        /// <summary>
        /// このビートの時間（秒）
        /// </summary>
        public ReactiveProperty<float> Time { get; } = new ReactiveProperty<float>();

        public Beat(float time)
        {
            Time.Value = time;
        }
    }
}