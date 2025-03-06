using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<AnimationClip> _clips;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(_delay);

        var index = Random.Range(0, _clips.Count);
        _animator.Play(_clips[index].name);

        StartCoroutine(StartAnimation());
    }
}
