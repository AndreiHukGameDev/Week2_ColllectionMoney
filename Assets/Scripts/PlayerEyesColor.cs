using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEyesColor : MonoBehaviour
{
    private Animator _animator;
    private Transform _coinsTransform;
    private float _distance;

    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _inputKey;
    private float _vectorY = 0.0f;
    private float _yRotation;


    public GameObject coinsGenerator;
    private List<Transform> _coinsTransforms = new List<Transform>();

    public Text remaningText;
    public Text playerCountText;
    public Text winText;
    private string remaning = "Remaining coins: ";
    private string playerCount = "Your count: ";
    private int playerCountInt = 0;
    private string winTextString = "You have collected all the coins!";

    public Button buttonExit;
    public Button buttonRestart;


    private void Awake()
    {
        Time.timeScale = 1.0f;
        _animator = GetComponent<Animator>();
        winText.enabled = false;
        buttonExit.gameObject.SetActive(false);
        buttonRestart.gameObject.SetActive(false);
        winText.text = winTextString;

    }
    private void Start()
    {
        AddCoins();
    }
    private void FixedUpdate()
    {
        ChooseTargetCoin();
        MovementPlayer();
        CreateText();
        CheckFinishedLvl();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        _animator.Play("Joy");
        playerCountInt += 100;
        _coinsTransforms.Remove(other.transform);
    }
    private void MovementPlayer()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _yRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, _yRotation * rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            _inputKey = new Vector3(_horizontalInput, _vectorY, _verticalInput) * speed * 3 * Time.deltaTime;

        }
        else
        {
            _inputKey = new Vector3(_horizontalInput, _vectorY, _verticalInput) * speed * Time.deltaTime;

        }
        transform.Translate(_inputKey);
        AnimationMovedOn();
    }
    private void AnimationMovedOn()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("Moved", true);

        }
        else
        {
            _animator.SetBool("Moved", false);

        }
    }
    
    private void AddCoins()
    {
        for (int i = 0; i < coinsGenerator.transform.childCount; i++)
        {
            _coinsTransforms.Add(coinsGenerator.transform.GetChild(i).gameObject.transform);
        }
        _coinsTransforms = _coinsTransforms.OrderBy(coin => Vector3.Distance(coin.position, transform.position)).ToList();
        _coinsTransform = _coinsTransforms[0];
        _coinsTransforms[_coinsTransforms.Count-1].gameObject.active = false;
    }
    private void ChooseTargetCoin()
    {
        _coinsTransforms = _coinsTransforms.OrderBy(coin => Vector3.Distance(coin.position, transform.position)).ToList();
        _coinsTransform = _coinsTransforms[0];
        _distance = Vector3.Distance(_coinsTransform.position, transform.position);
        if (_distance < 3.0f)
        {
            _animator.Play("GettingItem");
        }
    }
    private void CreateText()
    {
        remaningText.text = remaning + (_coinsTransforms.Count - 1).ToString();
        playerCountText.text = playerCount + playerCountInt.ToString();
    }
    private void CheckFinishedLvl()
    {
        if (_coinsTransforms.Count == 1)
        {
            winText.enabled = true;
            Time.timeScale = 0f;
            buttonExit.gameObject.SetActive(true);
            buttonRestart.gameObject.SetActive(true);

        }
    }
}
