using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_sequence : MonoBehaviour
{

    public GameObject _letterBox;
    public GameObject _buttons;
    public Enemie _enemy;
    public GameObject _enemyBar;
    public GameObject _rock;
    public GameObject _particlesRock;
    public GameObject _player;
    public GameObject _particle;
    [Space]
    public GameObject _ambience;
    public GameObject _boss_ambience;

    bool _deadOnce;
    // Start is called before the first frame update
    void Start()
    {
        _enemy.gameObject.GetComponent<Enemie>().enabled = false;
        _enemy.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.gameObject.activeSelf && !_deadOnce)
        {
            if (_enemy._currenthealth <= 0f)
            {
                _deadOnce = true;
                StartCoroutine(Finised());
            }
        }
    }

    IEnumerator Finised()
    {
        _ambience.gameObject.SetActive(true);
        _boss_ambience.gameObject.SetActive(false);

        _player.GetComponent<playermovment>()._controllable = false;
        _player.GetComponent<Animator>().Play("Idle");
        _enemyBar.SetActive(false);
        _letterBox.SetActive(true);
        _letterBox.transform.GetChild(2).gameObject.SetActive(false);
        _buttons.SetActive(false);

        FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.EnemyFocus;
        yield return new WaitForSeconds(2f);


        FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.EscapeRoute;

        yield return new WaitForSeconds(1f);

        _player.GetComponent<SpriteRenderer>().flipX = false;
        _player.transform.position = new Vector3(7.87f, -12.42f, 0f);
        _particle.SetActive(true);

        yield return new WaitForSeconds(1f);
        _particlesRock.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _rock.SetActive(false);
        FindAnyObjectByType<followplayer>().TriggerShake(0.3f);


        yield return new WaitForSeconds(1f);
        FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.PlayerFollow;
        _buttons.SetActive(true);
        _letterBox.SetActive(false);
        _player.GetComponent<playermovment>()._controllable = true;

    }

    IEnumerator _enemyShow()
    {
        _ambience.gameObject.SetActive(false);
        _boss_ambience.gameObject.SetActive(true);

        GetComponent<BoxCollider2D>().enabled = false;
        _letterBox.SetActive(true);
        _buttons.SetActive(false);

        yield return new WaitForSeconds(0.7f);

        _enemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<followplayer>().TriggerShake(1f);

        yield return new WaitForSeconds(1.5f);

        FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.PlayerFollow;
        _letterBox.SetActive(false);
        _buttons.SetActive(true);
        _enemy.gameObject.GetComponent<Enemie>().enabled = true;
        _enemyBar.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Player"))
        {
            FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.EnemyFocus;
            StartCoroutine(_enemyShow());
        }
    }
}
