using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000045")]
    public interface ICharacterManager : IActorManager<ICharacterActor, CharacterState, CharacterMetadata, CharactersConfiguration>, IActorManager, IEngineService, IEngineService<CharactersConfiguration>, IStatefulService<GameStateMap>
    {
    	[Token(Token = "0x14000004")]
    	event Action<CharacterAvatarChangedArgs> OnCharacterAvatarChanged;

    	[Token(Token = "0x6000103")]
    	bool AvatarTextureExists(string avatarTexturePath);

    	[Token(Token = "0x6000104")]
    	void RemoveAvatarTextureFor(string characterId);

    	[Token(Token = "0x6000105")]
    	[CanBeNull]
    	Texture2D GetAvatarTextureFor(string characterId);

    	[Token(Token = "0x6000106")]
    	[CanBeNull]
    	string GetAvatarTexturePathFor(string characterId);

    	[Token(Token = "0x6000107")]
    	void SetAvatarTexturePathFor(string characterId, string avatarTexturePath);

    	[Token(Token = "0x6000108")]
    	[CanBeNull]
    	string GetAuthorName(string characterId);

    	[Token(Token = "0x6000109")]
    	CharacterLookDirection LookAtOriginDirection(float xPos);

    	[Token(Token = "0x600010A")]
    	UniTask ArrangeCharacters(bool lookAtOrigin, Tween tween, [Optional] AsyncToken token);
    }
}
