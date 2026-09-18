using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents a list of <see cref="Command"/> based on the contents of a <see cref="Script"/>.
    /// </summary>
    [Serializable]
    public class ScriptPlaylist : IReadOnlyList<Command>
    {
        /// <summary>
        /// Local resource path of the script from which the contained commands were extracted.
        /// </summary>
        public string ScriptPath => scriptPath;
        /// <summary>
        /// Number of commands in the playlist.
        /// </summary>
        public int Count => commands.Count;

        [SerializeField] private string scriptPath;
        [SerializeReference] private List<Command> commands;

        /// <summary>
        /// Creates new instance from the specified commands collection.
        /// </summary>
        public ScriptPlaylist (string scriptPath, List<Command> commands)
        {
            this.scriptPath = scriptPath;
            this.commands = commands;
        }

        public Command this [int index] => commands[index];
        public IEnumerator<Command> GetEnumerator () => commands.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();
        public Command Find (Predicate<Command> predicate) => commands.Find(predicate);
        public int FindIndex (Predicate<Command> predicate) => commands.FindIndex(predicate);
        public List<Command> GetRange (int index, int count) => commands.GetRange(index, count);
        public bool IsIndexValid (int index) => commands.IsIndexValid(index);

        /// <summary>
        /// Finds first playback index that satisfies the condition, starting after specified index.
        /// Returns -1 when not found.
        /// </summary>
        public int FindIndexAfter (int afterIndex, Predicate<Command> predicate)
        {
            for (int i = afterIndex + 1; i < commands.Count; i++)
                if (predicate(commands[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// Preloads and holds all the resources required to execute
        /// <see cref="Command.IPreloadable"/> commands contained in this list.
        /// </summary>
        public async UniTask LoadResources () => await LoadResources(0, Count - 1);

        /// <summary>
        /// Preloads and holds resources required to execute
        /// <see cref="Command.IPreloadable"/> commands in the specified range.
        /// </summary>
        public async UniTask LoadResources (int startCommandIndex, int endCommandIndex, Action<float> onProgress = default)
        {
            if (Count == 0)
            {
                onProgress?.Invoke(1);
                return;
            }

            if (!IsIndexValid(startCommandIndex) || !IsIndexValid(endCommandIndex) || endCommandIndex < startCommandIndex)
                throw new Error($"Failed to preload '{ScriptPath}' script resources: [{startCommandIndex}, {endCommandIndex}] is not a valid range.");

            var count = endCommandIndex + 1 - startCommandIndex;
            var commandsToHold = GetRange(startCommandIndex, count).OfType<Command.IPreloadable>().ToArray();

            if (commandsToHold.Length == 0)
            {
                onProgress?.Invoke(1);
                return;
            }

            onProgress?.Invoke(0);
            var heldCommands = 0;
            await UniTask.WhenAll(commandsToHold.Select(PreloadCommand));

            async UniTask PreloadCommand (Command.IPreloadable command)
            {
                await command.PreloadResources();
                onProgress?.Invoke(++heldCommands / (float)commandsToHold.Length);
            }
        }

        /// <summary>
        /// Releases all the held resources required to execute
        /// <see cref="Command.IPreloadable"/> commands contained in this list.
        /// </summary>
        public void ReleaseResources () => ReleaseResources(0, commands.Count - 1);

        /// <summary>
        /// Releases all the held resources required to execute
        /// <see cref="Command.IPreloadable"/> commands in the specified range.
        /// </summary>
        public void ReleaseResources (int startCommandIndex, int endCommandIndex)
        {
            if (Count == 0) return;

            if (!IsIndexValid(startCommandIndex) || !IsIndexValid(endCommandIndex) || endCommandIndex < startCommandIndex)
                throw new Error($"Failed to unload '{ScriptPath}' script resources: [{startCommandIndex}, {endCommandIndex}] is not a valid range.");

            var commandsToRelease = GetRange(startCommandIndex, (endCommandIndex + 1) - startCommandIndex).OfType<Command.IPreloadable>();
            foreach (var cmd in commandsToRelease)
                cmd.ReleaseResources();
        }

        /// <summary>
        /// Returns a <see cref="Command"/> at the specified playlist/playback index; null if not found.
        /// </summary>
        public Command GetCommandByIndex (int index) =>
            IsIndexValid(index) ? commands[index] : null;

        /// <summary>
        /// Finds a <see cref="Command"/> that was created from a <see cref="CommandScriptLine"/>
        /// with specified line and inline indexes; null if not found.
        /// </summary>
        public Command GetCommandByLine (int lineIndex, int inlineIndex) =>
            Find(a => a.PlaybackSpot.LineIndex == lineIndex && a.PlaybackSpot.InlineIndex == inlineIndex);

        /// <summary>
        /// Finds a <see cref="Command"/> that was created from a <see cref="CommandScriptLine"/>
        /// located at or after the specified line and inline indexes; null if not found.
        /// </summary>
        public Command GetCommandAfterLine (int lineIndex, int inlineIndex) =>
            commands.FirstOrDefault(a => a.PlaybackSpot.LineIndex >= lineIndex && a.PlaybackSpot.InlineIndex >= inlineIndex);

        /// <summary>
        /// Finds a <see cref="Command"/> that was created from a <see cref="CommandScriptLine"/>
        /// located at or before the specified line and inline indexes; null if not found.
        /// </summary>
        public Command GetCommandBeforeLine (int lineIndex, int inlineIndex) =>
            commands.LastOrDefault(a => a.PlaybackSpot.LineIndex <= lineIndex && a.PlaybackSpot.InlineIndex <= inlineIndex);

        /// <summary>
        /// Returns first command in the list or null when the list is empty.
        /// </summary>
        public Command GetFirstCommand () => commands.FirstOrDefault();

        /// <summary>
        /// Returns last command in the list or null when the list is empty.
        /// </summary>
        public Command GetLastCommand () => commands.LastOrDefault();

        /// <summary>
        /// Finds index of a contained command with the specified playback spot or -1 when not found.
        /// </summary>
        public int IndexOf (PlaybackSpot spot) => FindIndex(c => c.PlaybackSpot == spot);

        /// <summary>
        /// Finds playback (command) index at or after specified line and inline indexes or -1 when not found.
        /// </summary>
        public int GetIndexByLine (int lineIndex, int inlineIndex)
        {
            var startCommand = GetCommandAfterLine(lineIndex, inlineIndex);
            return startCommand != null ? this.IndexOf(startCommand) : -1;
        }

        /// <summary>
        /// Given command under specified playback index is nested (ie, has indent above 0), 
        /// finds index of the command with the specified indent level which hosts the nested command.
        /// When host indent is not specified, looks for nearest host. Throws when not found.
        /// </summary>
        public int GetNestedHostIndex (int nestedIndex, int? hostIndent = null)
        {
            var nested = GetCommandByIndex(nestedIndex);
            var indent = hostIndent ?? nested.Indent - 1;
            for (int i = nestedIndex - 1; i >= 0; i--)
                if (this[i].Indent == indent && this[i] is Command.INestedHost)
                    return i;
            throw new Error(Engine.FormatMessage(
                "Failed to find host of nested command. " +
                "Make sure scenario script is indented correctly.", nested.PlaybackSpot));
        }

        /// <summary>
        /// Given command under specified playback index is nested (ie, has indent above 0), 
        /// finds the command with the specified indent level which hosts the nested command.
        /// When host indent is not specified, looks for nearest host. Throws when not found.
        /// </summary>
        public Command.INestedHost GetNestedHost (int nestedIndex, int? hostIndent = null)
        {
            return (Command.INestedHost)this[GetNestedHostIndex(nestedIndex, hostIndent)];
        }

        /// <summary>
        /// Checks whether command after specified index has higher indentation level
        /// than command at specified index, ie playback would enter nested block.
        /// </summary>
        public bool IsEnteringNestedAt (int index)
        {
            var command = GetCommandByIndex(index);
            var nextCommand = GetCommandByIndex(index + 1);
            if (command == null || nextCommand == null) return false;
            return nextCommand.Indent > command.Indent;
        }

        /// <summary>
        /// Checks whether command after specified index is exiting nested block
        /// of specified indent level.
        /// </summary>
        public bool IsExitingNestedAt (int index, int hostIndent)
        {
            var command = GetCommandByIndex(index);
            var nextCommand = GetCommandByIndex(index + 1);
            if (command == null) return false;
            if (nextCommand == null) return command.Indent > 0;
            return nextCommand.Indent <= hostIndent;
        }

        /// <summary>
        /// Given command under specified index is nested under host with specified indent,
        /// finds index of the last command in the nested block.
        /// </summary>
        public int GetNestedExitIndexAt (int index, int hostIndent)
        {
            for (int i = index; i < Count; i++)
                if (IsExitingNestedAt(i, hostIndent))
                    return i;
            return Count - 1;
        }

        /// <summary>
        /// Given command under specified index is nested (ie, has indent above 0) 
        /// and is the last command in the nested block with specified indent, resolves index
        /// of the next command to execute based on specifics of the outer block(s), if any.
        /// In case no outer blocks found, returns next index.
        /// </summary>
        /// <remarks>
        /// This is the default exit handler for nest hosts, which don't have any special
        /// exit behaviour; basically, this checks if current host is nested under other
        /// host, which may have custom exit behaviour (eg, @choice) and invoke it.
        /// </remarks>
        public int ExitNestedAt (int nestedIndex, int hostIndent)
        {
            // Resolve index via the host of the played command's host (outer host),
            // as next command may be n levels of nesting below the current block, eg:
            // '''
            // @if (outer host 1)
            //     @if (outer host 2)
            //         @if (this host)
            //             (this command)
            // (next command)
            // '''
            // — if we'd resolve host of the next command, exit behaviour of outer host 2 would be skipped.

            if (hostIndent == 0) return nestedIndex + 1;
            var outerHost = GetNestedHost(nestedIndex, hostIndent - 1);
            return outerHost.GetNextPlaybackIndex(this, nestedIndex);
        }

        /// <summary>
        /// Given command under specified index is nested under host with specified indent level,
        /// returns first index after exiting the block, based on specifics of the outer block(s), if any.
        /// </summary>
        public int SkipNestedAt (int nestedIndex, int hostIndent)
        {
            var exitIndex = GetNestedExitIndexAt(nestedIndex, hostIndent);
            return ExitNestedAt(exitIndex, hostIndent);
        }

        /// <summary>
        /// Returns next playback index, while taking into account nesting behaviour
        /// in case moved index is under nested host or is the host itself.
        /// </summary>
        public int MoveAt (int index)
        {
            var command = GetCommandByIndex(index);
            if (command is Command.INestedHost host && IsEnteringNestedAt(index))
                return host.GetNextPlaybackIndex(this, index);
            if (command.Indent == 0) return index + 1;
            return GetNestedHost(index).GetNextPlaybackIndex(this, index);
        }
    }
}
