using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to handle movie playing.
    /// </summary>
    public interface IMoviePlayer : IEngineService<MoviesConfiguration>
    {
        /// <summary>
        /// Event invoked when playback is started.
        /// </summary>
        event Action OnMoviePlay;
        /// <summary>
        /// Event invoked when playback is stopped.
        /// </summary>
        event Action OnMovieStop;

        /// <summary>
        /// Whether currently playing or preparing to play a movie.
        /// </summary>
        bool Playing { get; }

        /// <summary>
        /// Starts playing a movie with the specified name.
        /// Returns texture to which the movie is rendered.
        /// </summary>
        UniTask<Texture> Play (string movieName, AsyncToken token = default);
        /// <summary>
        /// Stops the playback.
        /// </summary>
        void Stop ();
        /// <summary>
        /// Preloads the resources required to play a movie with the specified path.
        /// </summary>
        UniTask HoldResources (string movieName, object holder);
        /// <summary>
        /// Unloads the resources required to play a movie with the specified path.
        /// </summary>
        void ReleaseResources (string movieName, object holder);
    }
}
