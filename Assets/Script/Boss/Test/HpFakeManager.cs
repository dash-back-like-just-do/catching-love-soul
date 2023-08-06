using ChessClub;
using UnityEngine;
using utils;

public class HpFakeManager : MonoBehaviour,IHpManager {
    public GameObject _player;
    public GameObject _boss;
    public GameData gameData;
    HpManager _hpManager;

    public HpManager GetHpManager()
    {
        return _hpManager;
    }

    private void Start() {
        _hpManager = new HpManager();
        var playerIhp = _player.GetComponent<Ihp>();
        playerIhp.SetHpManager(_hpManager);
        playerIhp.SetHp(gameData.playerInitHp);
        var bossIhp = _boss.GetComponent<Ihp>();
        bossIhp.SetHpManager(_hpManager);
        bossIhp.SetHp(gameData.bossInitHp);
    }
}