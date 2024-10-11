using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using TEngine.Helpers;

namespace TEngine.AudioEngine
{
    public class AudioEngine
    {
        private string _path;
        private bool _musicPlaying;
        SoundPlayer player;
        public AudioEngine(string path) 
        {
            _path = path;
            _musicPlaying = false;
            player = new SoundPlayer(path);
        }

        public void Play(string fileName)
        {
            try
            {
                player.Stop();
                player.SoundLocation = _path + fileName;
                player.Play();
                _musicPlaying = true;
            }
            catch (Exception ex)
            {
                _musicPlaying = false;
                MessageUtils.SetErrorMessage("AudioEngine", "Play", $"An error occurred: {ex.Message}");
            }
        }

        public void PlayLoop(string fileName)
        {
            try
            {
                player.Stop();
                player.SoundLocation = _path + fileName;
                Console.WriteLine(_path + fileName);
                Environment.Exit(0);
                player.PlayLooping();
                _musicPlaying = true;
            }
            catch(Exception ex)
            {
                _musicPlaying = false;
                MessageUtils.SetErrorMessage("AudioEngine", "PlayLoop", $"An error occurred: {ex.Message}");
            }
        }

        public void Stop()
        {
            player.Stop();
            _musicPlaying = false;
        }
    }
}
