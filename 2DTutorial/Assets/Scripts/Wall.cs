using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Sprite dmgSprite;
    public int hp = 4;
    public AudioClip wallSound1;
    public AudioClip wallSound2;

    private SpriteRenderer spriteRender;

	// Use this for initialization
	void Awake () {
        spriteRender = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall( int loss)
    {
        spriteRender.sprite = dmgSprite;
        hp -= loss;
        SoundManager.instance.RandomizeSfx(wallSound1, wallSound2);
        if (hp <= 0)
            gameObject.SetActive(false);
    }

}
