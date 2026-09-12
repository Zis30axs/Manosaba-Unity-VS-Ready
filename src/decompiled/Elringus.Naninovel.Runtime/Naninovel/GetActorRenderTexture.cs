using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel;

[Token(Token = "0x2000056")]
public class GetActorRenderTexture : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x2000057")]
	private class RenderTextureRetrievedEvent : UnityEvent<RenderTexture>
	{
		[Token(Token = "0x6000147")]
		[Address(RVA = "0x456CF0", Offset = "0x4556F0", VA = "0x180456CF0")]
		public RenderTextureRetrievedEvent()
		{
		}
	}

	[Token(Token = "0x2000058")]
	private enum ActorType
	{
		[Token(Token = "0x400011D")]
		Character,
		[Token(Token = "0x400011E")]
		Background
	}

	[Token(Token = "0x4000119")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Whether the actor is a character or a background.")]
	[SerializeField]
	private ActorType actorType;

	[Token(Token = "0x400011A")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("ID of the actor from which to get the render texture.")]
	[SerializeField]
	private string actorId;

	[Token(Token = "0x400011B")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Invoked when render texture is retrieved from the specified actor.")]
	[SerializeField]
	private RenderTextureRetrievedEvent onRenderTextureRetrieved;

	[Token(Token = "0x6000143")]
	[Address(RVA = "0x4568B0", Offset = "0x4552B0", VA = "0x1804568B0")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x6000144")]
	[Address(RVA = "0x456980", Offset = "0x455380", VA = "0x180456980")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x6000145")]
	[Address(RVA = "0x456A20", Offset = "0x455420", VA = "0x180456A20")]
	private void RetrieveTexture()
	{
	}

	[Token(Token = "0x6000146")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public GetActorRenderTexture()
	{
	}
}
