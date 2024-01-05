using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace PlatformerDemo
{
    // Singleton Pattern
    // Centrale plaats voor het beheren van audio
    public class AudioManager
    {
        private static AudioManager instance;
        private Song backgroundMusic;
        private SoundEffect jumpSound;

        private AudioManager() { }

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            backgroundMusic = content.Load<Song>("Music/theme");
            jumpSound = content.Load<SoundEffect>("Music/jump");
        }

        public void PlayBackgroundMusic()
        {
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
        }

        public void PlayJumpSound()
        {
            jumpSound.Play();
        }
    }

}
