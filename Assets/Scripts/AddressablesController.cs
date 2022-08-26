using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesController : MonoBehaviour
{
    [SerializeField]
    GameObject _player = null;
    SpriteRenderer _playerSkin = null;

    AsyncOperationHandle _handle;
    
    private void Start()
    {
        _player = this.gameObject;
        _playerSkin = _player.GetComponent<SpriteRenderer>();
    }

    public void OnLoadButtonClicked() => Load();

    public void OnUnloadButtonClicked() => Unload();

    public void Load()
    {
        Addressables.LoadAssetAsync<Texture2D>("DefaultSkin").Completed +=
            (AsyncOperationHandle<Texture2D> obj) =>
            {
                _handle = obj;
                Rect rect = new Rect(0, 0, obj.Result.width, obj.Result.height);
                _playerSkin.sprite = Sprite.Create(obj.Result, rect, new Vector2(0.5f, 0.5f));
            };
    }

    public void Unload()
    {
        Addressables.Release(_handle);
        _playerSkin.sprite = null;
    }
}
