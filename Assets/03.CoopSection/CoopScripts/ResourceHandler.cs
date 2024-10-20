using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public static ResourceHandler instance;
    [SerializeField]List<RuntimeAnimatorController> anims = new List<RuntimeAnimatorController>();
    [SerializeField]List<Sprite> sprites = new List<Sprite>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public List<RuntimeAnimatorController> GetAnims() { return anims; }
    public List<Sprite> GetSprites() { return sprites; }
}