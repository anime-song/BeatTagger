using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using NAudio.Wave;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeatTagger.WPF.ViewModels
{
    public sealed class AudioPlayerViewModel : IDropTarget
    {
        private AudioFileReader audioFileReader;
        private WaveOutEvent audioOutputDevice;

        public ReactiveCommand PlayPauseAudioCommand { get; }

        public AudioPlayerViewModel()
        {
            PlayPauseAudioCommand = new ReactiveCommand()
                .WithSubscribe(PlayPauseAudio);
        }

        private void PlayPauseAudio()
        {
            if (audioOutputDevice is null) return;

            if (audioOutputDevice.PlaybackState == PlaybackState.Playing)
            {
                audioOutputDevice.Pause();
            }
            else if (audioOutputDevice.PlaybackState == PlaybackState.Paused ||
                audioOutputDevice.PlaybackState == PlaybackState.Stopped)
            {
                audioOutputDevice.Play();
            }
        }

        private static bool IsSupportedAudioFile(string filePath)
        {
            var supportedExtensions = new[] { ".mp3", ".wav" };
            var fileExtension = Path.GetExtension(filePath)?.ToLowerInvariant();

            return supportedExtensions.Contains(fileExtension);
        }

        private void LoadAudioFile(string fileName)
        {
            audioFileReader = new AudioFileReader(fileName);
            audioOutputDevice = new WaveOutEvent();
            audioOutputDevice.Init(audioFileReader);
        }

        public void DragOver(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(fileName =>
            {
                return IsSupportedAudioFile(fileName);
            }) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(fileName =>
            {
                return IsSupportedAudioFile(fileName);
            }) ? DragDropEffects.Copy : DragDropEffects.None;

            var dragFile = dragFileList.FirstOrDefault();
            if (dragFile is null) return;
            if (!IsSupportedAudioFile(dragFile)) return;

            LoadAudioFile(dragFile);
        }
    }
}