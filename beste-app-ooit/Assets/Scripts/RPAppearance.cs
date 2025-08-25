using System.Collections;
using System.Collections.Generic;
using Discogs;
using UnityEngine;
using UnityEngine.UI;

public class RPTextures {
	public string Color; //blue gray yellow
	
	public Sprite GetSprite(string type) {
		Texture2D t2d = Get.ImageFromPath($"Assets/Sprites/Lefonki Designs/Player/{Color} Player/{type}.png");
		return Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0, 0));
	}
}




public class RPAppearance : MonoBehaviour {
	[SerializeField] private Image curTexture;
	private RPTextures _texture = new();
	private string _curType = "Empty";
	
	public void SetTexture(string type) {
		_curType = type;
		Reset();
	}

	public void ChangeColor(string color) {
		_texture.Color = color;
		Reset();
	}

	private void Reset() {
		curTexture.sprite = _texture.GetSprite(_curType);
	}

	
}
