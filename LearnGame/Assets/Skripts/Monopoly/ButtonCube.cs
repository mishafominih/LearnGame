using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCube : MonoBehaviour
{
    [SerializeField]
    private List<State> states;

    private State currentState;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(StartCast());
        });
        image = GetComponent<Image>();
    }

    private IEnumerator StartCast()
    {
        for(int i = 0; i < 2000; i += 25)
        {
            var randIndex = UnityEngine.Random.Range(0, 6);
            currentState = states[randIndex];
            image.sprite = currentState.SpriteState;
            yield return new WaitForSeconds(i / 2000);
        }
        Game.Instance.MoveStep(currentState.countStep);
    }

    [Serializable]
    public class State
    {
        public Sprite SpriteState;
        public int countStep;
    }
}
