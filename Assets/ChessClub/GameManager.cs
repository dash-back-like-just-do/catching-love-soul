using UnityEngine;
using utils;

namespace ChessClub
{
    public class GameManager : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject bossPrefab;
        public GameData gameData;
        private GameObject _boss;
        private HpManager _hpManager;
        private GameObject _player;


        // Start is called before the first frame update
        private void Start()
        {
            _boss = Instantiate(bossPrefab, gameData.bossInitPosition, transform.rotation);

            _player = Instantiate(playerPrefab, gameData.playerInitPosition, transform.rotation);
            _hpManager = new HpManager();
            var playerIhp = _player.GetComponent<Ihp>();
                playerIhp.SetHpManager(_hpManager);
                playerIhp.SetHp(gameData.playerInitHp);
            var bossIhp = _boss.GetComponent<Ihp>();
                bossIhp.SetHpManager(_hpManager);
                bossIhp.SetHp(gameData.bossInitHp);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
        }
    }
}